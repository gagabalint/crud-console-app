using Microsoft.EntityFrameworkCore;
using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Persistence.MsSql
{
    public class ServiceDataProvider:IServiceDataProvider
    {
        private readonly RailwayContext context;
        public ServiceDataProvider(RailwayContext context)
        {
            this.context = context;
        }
        public Service Add(Service service)
        {
            ;
            Service addition = context.Services.Add(service).Entity;
            context.SaveChanges();
            return addition; 
        }
        public ICollection<Service> Batch(Func<Service, bool>? p = null)
        {
            if (p == null) p = (x) => true;

            return context.Services.Where(p).ToHashSet();
        }
        public void Delete(int id)
        {
            Service service = context.Services.First(e => e.TrainNumber == id);
            context.Services.Remove(service);
            context.SaveChanges();
        }
        public Service? Get(int id)
        {
            return context.Services.FirstOrDefault(e => e.TrainNumber == id);
        }
        public void Update(int id, Service mod)
        {
            Service original= context.Services.FirstOrDefault(e=>e.TrainNumber == id)!;
            context.Services.Remove(original);
            context.Services.Add(mod);
            context.SaveChanges();
        }

    }
}
