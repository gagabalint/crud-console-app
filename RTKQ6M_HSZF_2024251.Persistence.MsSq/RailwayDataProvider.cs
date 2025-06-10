using Microsoft.EntityFrameworkCore;
using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Persistence.MsSql
{
    public class RailwayDataProvider : IRailwayDataProvider
    {
        private readonly RailwayContext context;

        public RailwayDataProvider(RailwayContext context)
        {
            this.context = context;
        }

        public RailwayLine Add(RailwayLine line)
        {

            RailwayLine addition = line;
            ; context.Railways.Add(addition);
            context.SaveChanges();
            ;
            return addition;
        }

        public ICollection<RailwayLine> Batch(Func<RailwayLine, bool>? p = null)
        {
            if (p == null) p = (x) => true;
            return context.Railways.Include(e => e.Services).Where(p).ToHashSet();
        }

        public void Delete(string id)
        {
            RailwayLine line = context.Railways.First(e => e.LineNumber == id);
            context.Railways.Remove(line);
            context.SaveChanges();
        }

        public RailwayLine? Get(string id)
        {
            RailwayLine ret = context.Railways.FirstOrDefault(e => e.LineNumber == id);
            return ret;
        }

        public void Update(string id, RailwayLine modLine)
        {
            RailwayLine original = context.Railways.FirstOrDefault(e => e.LineNumber == id)!;
            context.Remove(original);
            context.Railways.Add(modLine);
            context.SaveChanges();
        }
    }
}
