using RTKQ6M_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Console.UI
{
    public class ReportUI
    {
        IServiceService service;
        IRailwayService railway;
        IAvgDelayByRailways avgDelayByRailways;
        IDelayStatistics delayStatistics;
        IMostDelayedDestinations destinations;

        public ReportUI(IAvgDelayByRailways avgDelayByRailways, IDelayStatistics delayStatistics, IMostDelayedDestinations destinations)
        {

            this.avgDelayByRailways = avgDelayByRailways;
            this.delayStatistics = delayStatistics;
            this.destinations = destinations;
        }
        public void LessThan5minDelay(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) delayStatistics.SaveDelayStatisticsToFile();
            else delayStatistics.SaveDelayStatisticsToFile(filePath);
        }
        public void AverageDelayByRailways(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) avgDelayByRailways.SaveAvgDelayStatisticsToFile();
            else avgDelayByRailways.SaveAvgDelayStatisticsToFile(filePath);

        }
        public void MostDelayedDestinations(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) destinations.SaveMostDelayedDestinations();
            else destinations.SaveMostDelayedDestinations(filePath);

        }
    }
}
