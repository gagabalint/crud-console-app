using RTKQ6M_HSZF_2024251.Application;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RTKQ6M_HSZF_2024251.Console
{
    public static class Commands
    {
        public static int GetInt(string p)
        {
            int n = AnsiConsole.Prompt(new TextPrompt<int>(p));
            System.Console.WriteLine();
            return n;
        }
        public static string GetString(string p)
        {
            string s = AnsiConsole.Prompt(new TextPrompt<string>(p));
            System.Console.WriteLine();
            return s;
        }
        public class MyConsoleLogger
        {
            public MyConsoleLogger()
            {

            }

            public void OnLeastDelayEvent(string message)
            {
                System.Console.WriteLine(message);
            }

        }
    }
}
