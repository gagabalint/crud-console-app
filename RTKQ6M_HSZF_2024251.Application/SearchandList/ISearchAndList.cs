using RTKQ6M_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    public interface ISearchAndList
    {
        public string SearchRailwayLines(
           string? lineNumber = null,
           string? lineName = null,
           string? trainType = null,
           string? from = null,
           string? to = null
           );
       


    }
}
