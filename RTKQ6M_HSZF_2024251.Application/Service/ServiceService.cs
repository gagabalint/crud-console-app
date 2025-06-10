using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RTKQ6M_HSZF_2024251.Model;
using RTKQ6M_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{

    public class ServiceService : IServiceService
    {
        private readonly IServiceDataProvider dataProvider;
        public ServiceService(IServiceDataProvider repo)
        {
            this.dataProvider = repo;
        }
        public void Add(Service line)
        {
            if (dataProvider.Get(line.TrainNumber) is not null) { throw new InvalidOperationException(); }
            if (dataProvider.Batch(e => e.LineNumber == line.LineNumber).Count()!=0)
            {
                
                if (line.DelayAmount.CompareTo(dataProvider.Batch(e => e.LineNumber == line.LineNumber).FirstOrDefault().DelayAmount) < 1)
                {
                    Events.TriggerLeastDelayEvent(
                    $"New train No {line.TrainNumber} added with the smallest delay of {line.DelayAmount} on line {line.LineNumber}.");
                }
            }
            else Events.TriggerLeastDelayEvent(
                $"New train No {line.TrainNumber} added with the smallest delay of {line.DelayAmount} on line {line.LineNumber}.");

            dataProvider.Add(line);
        }



        public void Delete(int id)
        {
            Service? line = dataProvider.Get(id);
            if (line == null)
                throw new KeyNotFoundException();
            dataProvider.Delete(id);
        }

        public Service Get(int id)
        {
            Service? search = dataProvider.Get(id);

            if (search == null)
                throw new KeyNotFoundException();

            return search;
        }

        public ICollection<Service> GetAll(Func<Service, bool>? p = null)
        {
            return dataProvider.Batch(p);
        }

        public void Update(int id, Service mod)
        {
            Service? search = dataProvider.Get(id);
            if (search == null)
                throw new KeyNotFoundException();
            if (dataProvider.Batch().Any(e => e.LineNumber == mod.LineNumber))
            {
                ;
                if (mod.DelayAmount.CompareTo(dataProvider.Batch(e => e.LineNumber == mod.LineNumber).FirstOrDefault().DelayAmount) < 1)
                {
                    Events.TriggerLeastDelayEvent(
                    $"New train No {mod.TrainNumber} added with the smallest delay of {mod.DelayAmount} on line {mod.LineNumber}.");
                }
            }
            else Events.TriggerLeastDelayEvent(
                $"New train No {mod.TrainNumber} added with the smallest delay of {mod.DelayAmount} on line {mod.LineNumber}.");



            dataProvider.Update(id, mod);
        }

    }
}
