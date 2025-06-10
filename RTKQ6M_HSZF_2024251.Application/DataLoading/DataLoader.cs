using Newtonsoft.Json;
using RTKQ6M_HSZF_2024251.Model;
using RTKQ6M_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public class DataLoader:IDataLoader
    {
        IRailwayDataProvider railwayRepository;
        IServiceDataProvider serviceRepository;
        public DataLoader(IRailwayDataProvider repo, IServiceDataProvider serviceRepository)
        {
            railwayRepository = repo;
            this.serviceRepository = serviceRepository;
        }
        public void LoadDataFromFile(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            var railwayData = JsonConvert.DeserializeObject<Root>(jsonData);



            foreach (var line in railwayData.RailwayLines)
            {
                if (railwayRepository.Get(line.LineNumber) != null)
                {
                    foreach (var line2 in line.Services)
                    {
                        line2.LineNumber=line.LineNumber;
                        serviceRepository.Add(line2);
                    }
                }
               else railwayRepository.Add(line);
            }
        }
    }
}
