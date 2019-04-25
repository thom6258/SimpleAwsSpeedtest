using SimpleAwsSpeedtest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace SimpleAwsSpeedtest
{
    public class Pinger
    {
        public static long Ping(PingType pingType, string host)
        {
            if (pingType == PingType.ICMP)
                return PingOverICMP(host);
            else if (pingType == PingType.HTTPS)
                return PingOverWeb("https://" + host + "/ping");

            return -1; // Unkown PingType
        }

        public static long PingOverICMP(string host)
        {
            using (var pinger = new Ping())
            {
                var reply = pinger.Send(host);
                if (reply.Status == IPStatus.Success)
                    return reply.RoundtripTime;

                return -1; // Error pinging host
            }
        }

        private static long PingOverWeb(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);

            var timer = Stopwatch.StartNew();
            using (WebResponse httpResponse = (WebResponse)webRequest.GetResponse())
            {
                timer.Stop();

                return timer.ElapsedMilliseconds;
            }
        }
    }
}
