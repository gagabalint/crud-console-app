using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Persistence.MsSql
{
    public interface IRailwayDataProvider
    {
        ICollection<RailwayLine> Batch(Func<RailwayLine,bool>? p=null);
        RailwayLine? Get(string id);
        RailwayLine Add(RailwayLine line);
        void Update(string id, RailwayLine modLine);
        void Delete(string id);
    }
}
