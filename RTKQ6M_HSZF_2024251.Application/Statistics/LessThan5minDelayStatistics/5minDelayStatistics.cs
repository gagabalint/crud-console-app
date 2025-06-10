using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public class DelayStatistics : IDelayStatistics
    {
        IRailwayService railway { get; set; }
        public DelayStatistics(IRailwayService railway)
        {
            this.railway = railway;
        }
        public Dictionary<string, int> GenerateDelayStatistics()
        {
            var statistics = new Dictionary<string, int>();

            var railwayLines = railway.GetAll();

            foreach (var line in railwayLines)
            {
                int count = line.Services.Count(service => service.DelayAmount < 5);
                statistics[line.LineNumber] = count;
            }

            return statistics;
        }
        public void SaveDelayStatisticsToFile(string filePath)
        {
            var statistics = GenerateDelayStatistics();
            StreamWriter writer;
            if (filePath != "RailwayLineStatistics.txt")
            {
                writer = new StreamWriter(Path.Combine(filePath, "RailwayLineStatistics.txt"));
            }
            else
            {
                writer = new StreamWriter("RailwayLineStatistics.txt");
            }
            writer.WriteLine("Railway Line Statistics (Trains with Delay less than 5 minutes)");
            writer.WriteLine("-----------------------------------------------------");

            foreach (var stat in statistics)
            {
                writer.WriteLine($"Line Number: {stat.Key}, Amount of trains with less than 5 min Delay: {stat.Value}");
            }


            writer.Close();
        }
        public void SaveDelayStatisticsToFile()
        {
            SaveDelayStatisticsToFile("RailwayLineStatistics.txt");
        }
    }
}