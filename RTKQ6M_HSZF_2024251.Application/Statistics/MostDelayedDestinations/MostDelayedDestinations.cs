using RTKQ6M_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public class MostDelayedDestinations : IMostDelayedDestinations
    {
        IRailwayService railway;

        public MostDelayedDestinations(IRailwayService railway)
        {
            this.railway = railway;
        }

        public Dictionary<string, string?> GetMostDelayedDestinations()
        {
            var results = new Dictionary<string, string?>();

            var railwayLines = railway.GetAll();

            foreach (var line in railwayLines)
            {
                var delayedServices = line.Services
                    .Where(service => service.DelayAmount >= 5)
                    .ToList();

                var destinationCounts = delayedServices
                    .GroupBy(service => service.To)
                    .Select(group => new { Destination = group.Key, Count = group.Count() })
                    .OrderByDescending(g => g.Count)
                    .ToList();

                string? mostFrequentDestination = destinationCounts.FirstOrDefault()?.Destination;
                results[line.LineNumber] = mostFrequentDestination;
            }

            return results;
        }
        public void SaveMostDelayedDestinations(string filePath)
        {
            var statistics = GetMostDelayedDestinations();
            StreamWriter writer;
            if (filePath != "MostDelayedDestinations.txt")
            {
                 writer = new StreamWriter(Path.Combine(filePath, "MostDelayedDestinations.txt"));
            }
            else
            {
                 writer = new StreamWriter( "MostDelayedDestinations.txt");
            }

                writer.WriteLine("Most delayed destinations by railway lines");
            writer.WriteLine("-----------------------------------------------------");

            foreach (var stat in statistics)
            {
                writer.WriteLine($"\nLine Number: {stat.Key}, Destination: {stat.Value}");
            }

            writer.Close();

        }

        public void SaveMostDelayedDestinations()
        {
            SaveMostDelayedDestinations("MostDelayedDestinations.txt");
        }
    }
}
