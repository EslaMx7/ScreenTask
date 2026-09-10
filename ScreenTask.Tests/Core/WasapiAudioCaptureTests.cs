using System;
using System.Linq;
using NAudio.CoreAudioApi;
using ScreenTask.Core.Audio;
using Xunit;

namespace ScreenTask.Tests.Core
{
    public class WasapiAudioCaptureTests
    {
        [Fact]
        public void GetAudioDevices_IncludesMicrophoneAndSpeakerDefaults()
        {
            var devices = WasapiAudioCaptureService.GetAudioDevices();

            Assert.NotNull(devices);
            Assert.NotEmpty(devices);

            // Verify default microphone (input)
            var defaultMic = devices.FirstOrDefault(d => d.IsInput && d.Id == null);
            Assert.NotNull(defaultMic);
            Assert.Contains("Microphone", defaultMic.Name);

            // Verify default speaker / loopback (output)
            var defaultSpeaker = devices.FirstOrDefault(d => !d.IsInput && d.Id == null);
            Assert.NotNull(defaultSpeaker);
            Assert.Contains("Sound", defaultSpeaker.Name);
        }

        [Fact]
        public void Service_InitialState_IsNotRunningAndZeroLevel()
        {
            using var service = new WasapiAudioCaptureService();
            Assert.False(service.IsRunning);
            Assert.Null(service.CurrentDevice);
            Assert.Equal(0f, service.CurrentPeakLevel);
        }

        [Fact]
        public void AudioDeviceDescriptor_Properties_SetCorrectly()
        {
            var desc = new AudioDeviceDescriptor("test-id", "Test Mic", true, null);
            Assert.Equal("test-id", desc.Id);
            Assert.Equal("Test Mic", desc.Name);
            Assert.True(desc.IsInput);
            Assert.Null(desc.Device);
        }

        [Fact]
        public void Service_StartAndStop_ExecutesCleanlyWithoutHanging()
        {
            using var service = new WasapiAudioCaptureService();
            service.Start(null, isInput: true);
            System.Threading.Thread.Sleep(50);
            service.Stop();
            Assert.False(service.IsRunning);
        }
    }
}
