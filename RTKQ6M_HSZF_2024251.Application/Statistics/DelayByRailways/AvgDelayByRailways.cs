using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public class AvgDelayByRailways : IAvgDelayByRailways
    {
        IRailwayService railway;
        IServiceService service;

        public AvgDelayByRailways(IRailwayService railway, IServiceService service)
        {
            this.railway = railway;
            this.service = service;
        }
        public Dictionary<string, double> AvgDelayByLines()
        {
            var statistics = new Dictionary<string, double>();
            var railwaylines = railway.GetAll();
            foreach (var line in railwaylines)
            {
                double averagedelay = line.Services.Average(e => e.DelayAmount);
                statistics[line.LineNumber] = averagedelay;
            }
            return statistics;
        }
        public string MostDelayedTrains(string id)
        {
            string ret = "";

            var train = service.GetAll(e => e.LineNumber == id).OrderBy(e => e.DelayAmount).First();
            ret = $"The most delayed train on this line: {train.TrainNumber} -- {train.From}-->{train.To} -- {train.TrainType} -- Delay amount: {train.DelayAmount}";
            return ret;


        }
        public string LeastDelayedTrains(string id)
        {
            string ret = "";

            var train = service.GetAll(e => e.LineNumber == id).OrderByDescending(e => e.DelayAmount).First();
            ret = $"The most delayed train on this line: {train.TrainNumber} -- {train.From}-->{train.To} -- {train.TrainType} -- Delay amount: {train.DelayAmount}";
            return ret;


        }
        public void SaveAvgDelayStatisticsToFile(string filePath)
        {
            var statistics = AvgDelayByLines();
            StreamWriter writer;
            if (filePath != "AverageDelayStatistics.txt")
            {
                writer = new StreamWriter(Path.Combine(filePath, "AverageDelayStatistics.txt"));
            }
            else
            {
                writer = new StreamWriter("AverageDelayStatistics.txt");
            }

                writer.WriteLine("Railway Line Statistics (Average Delay By Railway Lines with the most and least delay)");
            writer.WriteLine("-----------------------------------------------------");
            foreach (var line in statistics)
            {
                writer.WriteLine($"Line Number: {line.Key} ----------- Average delay: {line.Value} minutes");
                writer.WriteLine($"\t\t{MostDelayedTrains(line.Key)}");
                writer.WriteLine($"\t\t{LeastDelayedTrains(line.Key)}\n");
            }
            writer.Close();
        }
        public void SaveAvgDelayStatisticsToFile()
        {
            SaveAvgDelayStatisticsToFile("AverageDelayStatistics.txt");
        }
    }
}
