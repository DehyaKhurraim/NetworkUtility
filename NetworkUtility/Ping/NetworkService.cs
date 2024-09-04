using NetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Ping
{
    public class NetworkService
    {
        private readonly IDNSService _dNS;

        public NetworkService(IDNSService dNS)
        {
            _dNS = dNS;
        }

        public String SendPing()
        {
            var dnsService = _dNS.SendDNS();
            if (dnsService) { 
                return "Success:    Ping Sent !";
            }
            else
            {
                return "Failed:     Ping Not Sent !";
            }
        }

        public int PingTimeout(int a, int b)
        {
            return a + b;
        }

        public DateTime LastPingDate()
        {
            return DateTime.Now;
        }

        public IEnumerable<PingOptions> PingOptions()
        {
            IEnumerable<PingOptions> pingOptions = new[]
            {
                new PingOptions() {
                    DontFragment = true,
                    Ttl = 1,
                },
                new PingOptions() {
                    DontFragment = true,
                    Ttl = 1,
                },
                new PingOptions() {
                    DontFragment = true,
                    Ttl = 1,
                },
            };

            return pingOptions;
        }
    }
}
