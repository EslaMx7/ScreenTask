using System;
using ScreenTask.Core.Audio;
using Xunit;

namespace ScreenTask.Tests.Core
{
    public class WasapiAudioCaptureTests
    {
        [Fact]
        public void GetRenderDevices_AlwaysIncludesDefaultPlaybackDevice()
        {
            var devices = WasapiAudioCaptureService.GetRenderDevices();

            Assert.NotNull(devices);
            Assert.NotEmpty(devices);
            Assert.Equal("Default Playback Device", devices[0].Name);
            Assert.Null(devices[0].Id);
            Assert.Null(devices[0].Device);
        }

        [Fact]
        public void Service_InitialState_IsNotRunning()
        {
            using var service = new WasapiAudioCaptureService();
            Assert.False(service.IsRunning);
            Assert.Null(service.CurrentDevice);
        }
    }
}
