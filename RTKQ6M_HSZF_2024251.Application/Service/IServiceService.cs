using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public interface IServiceService
    {
        ICollection<Service> GetAll(Func<Service, bool>? p = null);
        Service Get(int id);

        void Update(int id, Service mod);
        void Delete(int id);
        void Add(Service line);
    }
}
