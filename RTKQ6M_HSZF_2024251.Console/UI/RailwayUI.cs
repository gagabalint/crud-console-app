using RTKQ6M_HSZF_2024251.Application;
using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Console.UI
{
    public class RailwayUI
    {
        IRailwayService railway;

        public RailwayUI(IRailwayService railway)
        {
            this.railway = railway;
        }
        public void Create()
        {
            RailwayLine r = new RailwayLine();
            r.LineNumber = Commands.GetString("Enter the line number");
            r.LineName = Commands.GetString("Enter the line name");
            railway.Add(r);
            System.Console.WriteLine($"The {r.LineNumber} has been added successfully");

        }
        public void Read()
        {
            string id = Commands.GetString("Enter the number of the line");
            RailwayLine r = railway.Get(id);
            System.Console.WriteLine($"The {id} line information:\n\tLine name:{r.LineName}");
        }
        public void Update()
        {
            string id = Commands.GetString("Enter the line number, you would like to modify");
            RailwayLine mod = new RailwayLine();
            mod.LineNumber = Commands.GetString("Enter the line number");

            mod.LineName = Commands.GetString("Enter the line name");
            railway.Update(id, mod);

            if (id == mod.LineNumber)
            {
                System.Console.WriteLine($"The line {id} has been updated with the given data.");
            }
            else
            {
                System.Console.WriteLine($"The line {id} has been updated to No {mod.LineNumber} together with the given data.");
            }
        }
        public void Delete()
        {
            string id = Commands.GetString("Enter the train number you would like to delete");
            railway.Delete(id);
            System.Console.WriteLine($"The line {id} has been deleted.");
        }
    }
}
