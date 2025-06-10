using RTKQ6M_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Console.UI
{
    public class DataLoaderUI
    {
        IDataLoader loader;

        public DataLoaderUI(IDataLoader loader)
        {
            this.loader = loader;
        }
        public void Load(string filePath) {loader.LoadDataFromFile(filePath);}
    }
}
