using SimpleAwsSpeedtest.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleAwsSpeedtest
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var programDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var builder = new ConfigurationBuilder()
                .SetBasePath(programDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            var pingType = configuration.GetValue<PingType>("pingType", PingType.ICMP);
            var resultCount = configuration.GetValue<int>("resultCount", 3);

            var regions = JsonConvert.DeserializeObject<IEnumerable<Region>>(File.ReadAllText(Path.Combine(programDirectory, "regions.json")));

            var results = new Dictionary<string, long>();

            foreach (var region in regions)
            {
                results.Add(region.Name, Pinger.Ping(pingType, region.Host));
            }

            // Limit result to resultCount
            var limitedResult = results.Where(o => o.Value > 0).OrderBy(o => o.Value).Take(resultCount).ToArray();


            if (results.Any(o => o.Value <= 0))
            {
                // Some regions failed
                Console.WriteLine("AWS regions failed to ping:");

                var failedRegions = results.Where(o => o.Value <= 0);
                foreach (var region in failedRegions)
                {
                    Console.WriteLine("\t{0}", region.Key);
                }
            }

            Console.WriteLine("Best AWS regions by ping times:");
            for (int i = 0; i < resultCount; i++)
            {
                Console.WriteLine("{0}.\t{1}\t(Elapsed Time: {2} ms)", i+1, limitedResult[i].Key, limitedResult[i].Value);
            }
        }
    }
}
