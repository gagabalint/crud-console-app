
using Abp.Domain.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTKQ6M_HSZF_2024251.Application;
using RTKQ6M_HSZF_2024251.Console;
using RTKQ6M_HSZF_2024251.Console.UI;
using RTKQ6M_HSZF_2024251.Persistence.MsSql;
using Spectre.Console;
using System;

namespace RTKQ6M_HSZF_2024251


{

    public class Program
    {
        static GeneralUI generalUI;
        public static void Main(string[] args)
        {


            var app = Host.CreateDefaultBuilder()
                .ConfigureServices(opt =>
                {
                    opt.AddDbContext<RailwayContext>();
                    opt.AddSingleton<IRailwayDataProvider, RailwayDataProvider>();
                    opt.AddSingleton<IServiceDataProvider, ServiceDataProvider>();
                    opt.AddSingleton<IRailwayService, RailwayService>();
                    opt.AddSingleton<IServiceService, ServiceService>();
                    opt.AddSingleton<IDataLoader, DataLoader>();
                    opt.AddSingleton<IDelayStatistics, DelayStatistics>();
                    opt.AddSingleton<IAvgDelayByRailways, AvgDelayByRailways>();
                    opt.AddSingleton<IMostDelayedDestinations, MostDelayedDestinations>();
                    opt.AddSingleton<ISearchAndList, SearchAndList>();

                })
                .Build();


            app.Start();
            IServiceProvider serviceProvider = app.Services.CreateScope().ServiceProvider;
            System.Console.Clear();

            ServiceUI serviceui = new ServiceUI(serviceProvider.GetService<IServiceService>());
            RailwayUI railwayui = new RailwayUI(serviceProvider.GetService<IRailwayService>());
            ListingUI listingui = new ListingUI(serviceProvider.GetService<ISearchAndList>());
            ReportUI reportui = new ReportUI(serviceProvider.GetService<IAvgDelayByRailways>(), serviceProvider.GetService<IDelayStatistics>(), serviceProvider.GetService<IMostDelayedDestinations>());
            DataLoaderUI dataLoaderUI = new DataLoaderUI(serviceProvider.GetService<IDataLoader>());
            generalUI = new GeneralUI(dataLoaderUI, listingui, railwayui, reportui, serviceui);
            string selected;
            do
            {
                selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[blue]NSZV Control Panel[/]")
                .PageSize(10)
                .AddChoices(new[] {
                "Read Data", "Add Data", "Upload Data","Update Data",
                "Delete Data", "Generate Reports","Seed Database","Exit Application",
                }));
                switch (selected)
                {
                    case "Read Data":
                        ReadDataMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Update Data":
                        UpdateDataMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Add Data":
                        AddDataMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Upload Data":
                        UploadDataMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Delete Data":
                        DeleteDataMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Generate Reports":
                        GenerateReportMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Seed Database":
                        Seed();
                        System.Console.WriteLine("Database is seeded!");
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                }
            } while (selected != "Exit Application");
            System.Console.WriteLine("");

        }
        private static void ReadDataMenu()
        {
            string selected = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                 .Title("[green]Select the table you would like to read data from:[/]")
                 .PageSize(10)
                 .AddChoices(new[] {
                "Railway Lines","Services","Search and List","Back to main menu"
                 }));
            switch (selected)
            {
                case "Services":
                    generalUI.ServiceUI.Read();
                    break;
                case "Railway Lines":
                    generalUI.RailwayUI.Read();
                    break;
                case "Search and List":
                    generalUI.ListingUI.SearchProgram();
                    break;
                case "Back to main menu":
                    break;
            }
        }
        private static void UpdateDataMenu()
        {
            string selected = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                 .Title("[red]Select the table you would like to update:[/]")
                 .PageSize(10)
                 .AddChoices(new[] {
                "Railway Lines","Services","Back to main menu"
                 }));
            switch (selected)
            {
                case "Services":
                    generalUI.ServiceUI.Update();
                    break;
                case "Railway Lines":
                    generalUI.RailwayUI.Update();
                    break;
                case "Back to main menu":
                    break;
            }
        }
        private static void AddDataMenu()
        {
            string selected = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                 .Title("[red]Select the table you would like to add:[/]")
                 .PageSize(10)
                 .AddChoices(new[] {
                "Railway Lines","Services","Back to main menu"
                 }));
            switch (selected)
            {
                case "Services":
                    generalUI.ServiceUI.Create();
                    break;
                case "Railway Lines":
                    generalUI.RailwayUI.Create();
                    break;
                case "Back to main menu":
                    break;
            }
        }
        private static void Seed()
        {
            generalUI.DataloaderUI.Load("tesztfajl.json");
        }
        private static void DeleteDataMenu()
        {
            string selected = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                 .Title("[blue]Select the table you would like to delete from:[/]")
                 .PageSize(10)
                 .AddChoices(new[] {
                "Railway Lines","Services", "Back to main menu"}));
            switch (selected)
            {
                case "Services":
                    generalUI.ServiceUI.Delete();
                    break;
                case "Railway Lines":
                    generalUI.RailwayUI.Delete();
                    break;
                case "Back to main menu":
                    break;

            }
        }
        private static void UploadDataMenu()
        {
            string filePath = Commands.GetString("Enter the path of the file you would like to upload:");
            generalUI.DataloaderUI.Load(filePath);
            System.Console.WriteLine("The file has been successfully uploaded in the database!");
        }
        private static void GenerateReportMenu()
        {
            string selected = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                 .Title("[blue]Select the report you would like to generate:[/]")
                 .PageSize(10)
                 .AddChoices(new[] {
                "Number of trains with less than 5 minute delay by railway lines","Average delay by railway lines","Destinations with the most number of delayed trains" ,"Back to main menu"}));
            switch (selected)
            {
                case "Number of trains with less than 5 minute delay by railway lines":
                    string filepath = "";
                    filepath = Tofile();
                    generalUI.ReportUI.LessThan5minDelay(filepath);
                    if (filepath == null) System.Console.WriteLine($"Report has been successfully generated to the default filepath as RailwayLineStatistics"); else System.Console.WriteLine($"Report has been successfully generated to {(filepath)} as RailwayLineStatistics");

                    break;
                case "Average delay by railway lines":
                    filepath = Tofile();
                    generalUI.ReportUI.AverageDelayByRailways(filepath);
                    if (filepath == null) System.Console.WriteLine($"Report has been successfully generated to the default filepath as AverageDelayStatistics"); else System.Console.WriteLine($"Report has been successfully generated to {(filepath)} as AverageDelayStatistics");


                    break;

                case "Destinations with the most number of delayed trains":
                    filepath = Tofile();
                    generalUI.ReportUI.MostDelayedDestinations(filepath);
                    if (filepath == null) System.Console.WriteLine($"Report has been successfully generated to the default filepath as MostDelayedDestinations"); else System.Console.WriteLine($"Report has been successfully generated to {(filepath)} as MostDelayedDestinations");

                    break;
                case "Back to main menu":
                    break;

            }

        }
        private static string Tofile()
        {
            System.Console.WriteLine("Enter the path where you would like to save the file (leave empty for default)");
            string ret = System.Console.ReadLine();
            if (string.IsNullOrEmpty(ret) || string.IsNullOrWhiteSpace(ret)) ret = null;
            return ret;
        }
    }

}

