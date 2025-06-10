using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public class SearchAndList : ISearchAndList
    {
        IRailwayService railway;
        IServiceService service;
        public SearchAndList(IRailwayService railway, IServiceService service)
        {
            this.railway = railway;
            this.service = service;
        }
        public string SearchRailwayLines(
           string? lineNumber,
           string? lineName ,
           string? trainType,
           string? from,
           string? to 
           )
        {
            var query = railway.GetAll().Join(service.GetAll(), railwayLine => railwayLine.LineNumber, service => service.LineNumber,
                (railwayLine, service) => new
                {
                    RailwayLine = railwayLine,
                    Service = service
                });
            if (!string.IsNullOrEmpty(lineNumber))
            {
                query = query.Where(line => line.RailwayLine.LineNumber == lineNumber);
            }
            if (!string.IsNullOrEmpty(lineName))
            {
                query = query.Where(line => line.RailwayLine.LineName == lineName);
            }

            if (!string.IsNullOrEmpty(trainType))
            {
                query = query.Where(line => line.Service.TrainType == trainType);
            }
            if (!string.IsNullOrEmpty(from))
            {
                query = query.Where(line => line.Service.From == from);
            }
            if (!string.IsNullOrEmpty(to))
            {
                query = query.Where(line => line.Service.To == to);

            }
            string check = "";
            string output = ("\nSearch Results:");
            foreach (var line in query)
            {
                if (line.RailwayLine.LineNumber != check)
                {
                    output += ($"\nLine Number: {line.RailwayLine.LineNumber}, Line Name: {line.RailwayLine.LineName}");
                }
                output += ($"\n    Train Type: {line.Service.TrainType}, From: {line.Service.From}, To: {line.Service.To} , Delay:  {line.Service.DelayAmount} min");
                check = line.RailwayLine.LineNumber;
                
            }
            return output;
        }

    }
}


