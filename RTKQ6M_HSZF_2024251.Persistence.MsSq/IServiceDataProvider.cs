using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Persistence.MsSql
{
    public interface IServiceDataProvider
    {
        ICollection<Service> Batch(Func<Service, bool>? p=null);
        Service? Get(int id);
        Service Add(Service line);
        void Update(int id, Service modLine);
        void Delete(int id);

    }
}
