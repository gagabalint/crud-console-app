using RTKQ6M_HSZF_2024251.Application;
using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Console.UI
{
    public class ListingUI
    {
        ISearchAndList searchAndList;

        public ListingUI(ISearchAndList searchAndList)
        {
            this.searchAndList = searchAndList;
        }
        public void SearchProgram()
        {

            bool continueSearching = true;

            while (continueSearching)
            {
                string?[] data = new string[5];
                data[0]=Commands.GetString("Enter Line Number to search (enter null to skip): ");


                data[1] = Commands.GetString("Enter Line Name to search (enter null to skip): ");


                data[2]=Commands.GetString("Enter Train Type to search (enter null to skip): ");


                data[3] = Commands.GetString("Enter 'From' station to search (enter null to skip): ");


                data[4]  =Commands.GetString("Enter 'To' station to search (enter null to skip): ");
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i]=="null")
                    {
                        data[i] = null;
                    }
                }

                System.Console.WriteLine(searchAndList.SearchRailwayLines(data[0], data[1], data[2], data[3], data[4]));
                var response=Commands.GetString("\nWould you like to search again? (y/n): ");
               System.Console.Clear();
                continueSearching = response?.Trim().ToLower() == "y";

                if (!continueSearching)
                {
                    System.Console.WriteLine("Exiting search...");
                    Thread.Sleep(1000);
                }
            }
        }

    }
}
