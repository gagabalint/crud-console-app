using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public interface IAvgDelayByRailways
    {
        Dictionary<string, double> AvgDelayByLines();
        string MostDelayedTrains(string id);
        string LeastDelayedTrains(string id);
        void SaveAvgDelayStatisticsToFile(string filePath);
        void SaveAvgDelayStatisticsToFile();
    }
}
