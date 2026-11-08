using System.IO;
using System.Xml.Serialization;
using ScreenTask;
using Xunit;

namespace ScreenTask.Tests.Core
{
    public class AppSettingsTests
    {
        [Fact]
        public void DefaultValues_AreProperlyConfigured()
        {
            var settings = new AppSettings();

            Assert.Equal("127.0.0.1", settings.IP);
            Assert.Equal(7070, settings.Port);
            Assert.Equal(30, settings.TargetFps);
            Assert.Equal(80, settings.ImageQuality);
            Assert.False(settings.IsPrivateSession);
            Assert.True(settings.IsShowMouseEnabled);
            Assert.False(settings.IsAudioEnabled);
            Assert.True(settings.IsCloseToTrayEnabled);
            Assert.True(settings.IsMinimizeToTrayEnabled);
            Assert.Equal("", settings.SelectedAudioDeviceId);
            Assert.False(settings.IsAutoStartServerEnabled);
            Assert.False(settings.IsStartMinimizedEnabled);
        }

        [Fact]
        public void XmlSerialization_RoundTripsAllPropertiesSuccessfully()
        {
            var original = new AppSettings
            {
                IP = "192.168.1.150",
                Port = 8088,
                TargetFps = 60,
                ImageQuality = 90,
                IsPrivateSession = true,
                PinCode = "secret123",
                IsShowMouseEnabled = false,
                IsAudioEnabled = true,
                SelectedScreenIndex = 1,
                IsCloseToTrayEnabled = false,
                SelectedAudioDeviceId = "audio-dev-loopback-01",
                IsAutoStartServerEnabled = true,
                IsStartMinimizedEnabled = true
            };

            var serializer = new XmlSerializer(typeof(AppSettings));
            using var ms = new MemoryStream();
            serializer.Serialize(ms, original);

            ms.Position = 0;
            var deserialized = (AppSettings)serializer.Deserialize(ms)!;

            Assert.NotNull(deserialized);
            Assert.Equal(original.IP, deserialized.IP);
            Assert.Equal(original.Port, deserialized.Port);
            Assert.Equal(original.TargetFps, deserialized.TargetFps);
            Assert.Equal(original.ImageQuality, deserialized.ImageQuality);
            Assert.Equal(original.IsPrivateSession, deserialized.IsPrivateSession);
            Assert.Equal(original.PinCode, deserialized.PinCode);
            Assert.Equal(original.IsShowMouseEnabled, deserialized.IsShowMouseEnabled);
            Assert.Equal(original.IsAudioEnabled, deserialized.IsAudioEnabled);
            Assert.Equal(original.SelectedScreenIndex, deserialized.SelectedScreenIndex);
            Assert.False(deserialized.IsCloseToTrayEnabled);
            Assert.False(deserialized.IsMinimizeToTrayEnabled);
            Assert.Equal(original.SelectedAudioDeviceId, deserialized.SelectedAudioDeviceId);
            Assert.True(deserialized.IsAutoStartServerEnabled);
            Assert.True(deserialized.IsStartMinimizedEnabled);
        }

        [Fact]
        public void LegacyCompatibility_ScreenshotsSpeedConvertsToTargetFps()
        {
            var settings = new AppSettings();
            settings.ScreenshotsSpeed = 500; // 500ms -> 2 FPS
            Assert.Equal(2, settings.TargetFps);

            settings.ScreenshotsSpeed = 33; // 33ms -> 30 FPS
            Assert.Equal(30, settings.TargetFps);
        }

        [Fact]
        public void XmlSerialization_BackwardCompatibility_PreservesDefaultsWhenElementsMissing()
        {
            string oldXml = """
                <?xml version="1.0" encoding="utf-8"?>
                <AppSettings>
                  <IP>192.168.1.100</IP>
                  <Port>7070</Port>
                  <TargetFps>30</TargetFps>
                  <ImageQuality>80</ImageQuality>
                </AppSettings>
                """;

            var serializer = new XmlSerializer(typeof(AppSettings));
            using var reader = new StringReader(oldXml);
            var deserialized = (AppSettings)serializer.Deserialize(reader)!;

            Assert.NotNull(deserialized);
            Assert.Equal("192.168.1.100", deserialized.IP);
            Assert.True(deserialized.IsCloseToTrayEnabled);
            Assert.True(deserialized.IsMinimizeToTrayEnabled);
            Assert.Equal("", deserialized.SelectedAudioDeviceId);
        }

        [Fact]
        public void MinimizeToTrayAlias_SynchronizesWithCloseToTray()
        {
            var settings = new AppSettings();
            Assert.True(settings.IsCloseToTrayEnabled);
            Assert.True(settings.IsMinimizeToTrayEnabled);

            settings.IsMinimizeToTrayEnabled = false;
            Assert.False(settings.IsCloseToTrayEnabled);
            Assert.False(settings.IsMinimizeToTrayEnabled);

            settings.IsCloseToTrayEnabled = true;
            Assert.True(settings.IsCloseToTrayEnabled);
            Assert.True(settings.IsMinimizeToTrayEnabled);
        }
    }
}
