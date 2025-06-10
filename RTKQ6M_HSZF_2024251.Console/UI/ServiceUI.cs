using Microsoft.Extensions.Logging;
using RTKQ6M_HSZF_2024251.Application;
using RTKQ6M_HSZF_2024251.Console;
using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RTKQ6M_HSZF_2024251.Console.Commands;

namespace RTKQ6M_HSZF_2024251.Console.UI
{
    public class ServiceUI
    {
        IServiceService service;
        public ServiceUI(IServiceService service)
        {
            this.service = service;
        }
        public void Create()
        {
            MyConsoleLogger logger = new MyConsoleLogger();
            Events.LeastDelayEvent += logger.OnLeastDelayEvent;
            Service s = new Service();
            s.LineNumber = Commands.GetString("Enter the line number");
            
            s.From = Commands.GetString("Enter the departure station");
            s.To = Commands.GetString("Enter the final destination");
            s.TrainNumber = Commands.GetInt("Enter the train number");
            s.TrainType = Commands.GetString("Enter the train type");
            s.DelayAmount = Commands.GetInt("Enter the amount of delay");
            service.Add(s);
            System.Console.WriteLine($"The No {s.TrainNumber} train has been added successfully");
            Events.LeastDelayEvent -= logger.OnLeastDelayEvent;


        }
        public void Read()
        {
            int id = Commands.GetInt("Enter the number of the train");
            Service s=service.Get(id);
            System.Console.WriteLine(s.ToString());

        }
        public void Update()
        {
            MyConsoleLogger logger = new MyConsoleLogger();
            Events.LeastDelayEvent += logger.OnLeastDelayEvent;

            int id = Commands.GetInt("Enter the train number, you would like to modify");
            Service mod = new Service();
            mod.LineNumber = Commands.GetString("Enter the line number");

            mod.From = Commands.GetString("Enter the departure station");
            mod.To = Commands.GetString("Enter the final destination");
            mod.TrainNumber = Commands.GetInt("Enter the train number");
            mod.TrainType = Commands.GetString("Enter the train type");
            mod.DelayAmount = Commands.GetInt("Enter the amount of delay");
            service.Update(id, mod);
           

            if (id==mod.TrainNumber)
            {
                System.Console.WriteLine($"The train No {id} has been updated with the given data.");
            }
            else
            {
                System.Console.WriteLine($"The train No {id} has been updated to No {mod.TrainNumber} together with the given data.");
            }
            Events.LeastDelayEvent -= logger.OnLeastDelayEvent;

        }
        public void Delete()
        {
            int id = Commands.GetInt("Enter the train number you would like to delete");
            service.Delete(id);
            System.Console.WriteLine($"The train No {id} has been deleted.");
           
        }

    }
}
