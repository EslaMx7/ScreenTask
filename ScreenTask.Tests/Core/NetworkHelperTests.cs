using System.Linq;
using ScreenTask.Core.Network;
using Xunit;

namespace ScreenTask.Tests.Core
{
    public class NetworkHelperTests
    {
        [Fact]
        public void GetAvailableAdapters_ReturnsAtLeastOneValidAdapter()
        {
            var adapters = NetworkHelper.GetAvailableAdapters();

            Assert.NotNull(adapters);
            Assert.NotEmpty(adapters);

            // Each adapter must have a non-empty name and IPv4 address
            foreach (var a in adapters)
            {
                Assert.False(string.IsNullOrWhiteSpace(a.InterfaceName));
                Assert.False(string.IsNullOrWhiteSpace(a.IpAddress));
                Assert.Contains(".", a.IpAddress); // IPv4
            }
        }

        [Fact]
        public void GetAvailableAdapters_PrioritizesRecommendedLanAddresses()
        {
            var adapters = NetworkHelper.GetAvailableAdapters();

            var firstRecommended = adapters.FirstOrDefault(a => a.IsRecommended);
            if (firstRecommended != null)
            {
                // First recommended adapter should be near the top
                Assert.True(adapters[0].IsRecommended);
            }
        }
    }
}
