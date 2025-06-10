using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public interface IRailwayService
    {
        ICollection<RailwayLine> GetAll(Func<RailwayLine, bool>? p = null);
        RailwayLine Get(string id);

        void Update(string id, RailwayLine mod);
        void Delete(string id);
        RailwayLine Add(RailwayLine line);
    }
}
