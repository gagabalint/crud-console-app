using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public interface IMostDelayedDestinations
    {
        public Dictionary<string, string?> GetMostDelayedDestinations();
        public void SaveMostDelayedDestinations(string filePath);
        public void SaveMostDelayedDestinations();
    }
}
