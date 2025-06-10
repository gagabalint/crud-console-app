using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Application
{
    static public class Events
    {
        public static event Action<string> LeastDelayEvent;
        public static void TriggerLeastDelayEvent(string message)
        {
            LeastDelayEvent?.Invoke(message);
        }
    }
}
