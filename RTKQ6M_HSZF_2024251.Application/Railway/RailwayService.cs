using RTKQ6M_HSZF_2024251.Model;
using RTKQ6M_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public class RailwayService : IRailwayService
    {
        private readonly IRailwayDataProvider _repo;

        public RailwayService(IRailwayDataProvider repo)
        {
            _repo = repo;
        }

        public RailwayLine Add(RailwayLine line)
        {
            RailwayLine? search = _repo.Get(line.LineNumber);
            if (search == null)
            {
                _repo.Add(line);
                return line;
            }
           else throw new InvalidOperationException();


        }

        public void Delete(string id)
        {
            RailwayLine? line = _repo.Get(id);
            if (line == null)
                throw new KeyNotFoundException();
            _repo.Delete(id);
        }

        public RailwayLine Get(string id)
        {
            RailwayLine? search = _repo.Get(id);

            if (search == null)
                throw new KeyNotFoundException();

            return search;
        }

        public ICollection<RailwayLine> GetAll(Func<RailwayLine,bool>?p=null)
        {
            return _repo.Batch(p);
        }

        public void Update(string id, RailwayLine mod)
        {
            RailwayLine? search = _repo.Get(id);
            if (search == null)
                throw new KeyNotFoundException();
            _repo.Update(id, mod);
        }
    }
}
