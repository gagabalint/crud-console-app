using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public interface IDelayStatistics
    {
        public Dictionary<string, int> GenerateDelayStatistics();
        public void SaveDelayStatisticsToFile(string filePath);
        public void SaveDelayStatisticsToFile();

    }
}
