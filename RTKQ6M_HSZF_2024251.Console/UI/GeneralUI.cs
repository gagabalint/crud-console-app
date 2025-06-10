using RTKQ6M_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Console.UI
{
    public class GeneralUI
    {
        public GeneralUI(DataLoaderUI dataloaderUI, ListingUI listingUI, RailwayUI railwayUI, ReportUI reportUI, ServiceUI serviceUI)
        {
            DataloaderUI = dataloaderUI;
            ListingUI = listingUI;
            RailwayUI = railwayUI;
            ReportUI = reportUI;
            ServiceUI = serviceUI;
        }

        public DataLoaderUI DataloaderUI { get; set; }
        public ListingUI ListingUI { get; set; }
        public RailwayUI RailwayUI { get; set; }
        public ReportUI ReportUI { get; set; }
        public ServiceUI ServiceUI { get; set; }
    }
}
