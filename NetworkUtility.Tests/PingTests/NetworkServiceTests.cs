using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;
using NetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        private readonly IDNSService _dns;

        public NetworkServiceTests()
        {
            //Dependencies
            _dns = A.Fake<IDNSService>();

            //SUT
            _pingService = new NetworkService(_dns);
        }

        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            A.CallTo(() => _dns.SendDNS()).Returns(true);

            //Act
            var result = _pingService.SendPing();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success:    Ping Sent !");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,1,3)]
        [InlineData(1,2,3)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            //Act
            var result = _pingService.PingTimeout(a, b);

            //Assert
            result.Should().Be(expected);
            result.Should().NotBeInRange(-1000, -1);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            //Act
            var result = _pingService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }

        [Fact]
        public void NetworkService_PingOptions_ReturnObject()
        {
            //Arrange
            var expected = new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            };

            //Act
            var result = _pingService.PingOptions();

            //Assert
            //result.Should().BeOfType<IEnumerable<PingOptions>>();
            //result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}
