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
                SelectedScreenIndex = 1
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
    }
}
