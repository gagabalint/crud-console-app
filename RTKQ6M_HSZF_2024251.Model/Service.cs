using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTKQ6M_HSZF_2024251.Model
{
    public class Service : IComparable<Service>
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int TrainNumber { get; set; }

        [Required]
        public int DelayAmount { get; set; }
        
        [Required]
        public string TrainType { get; set; }
        [Required]
        [ForeignKey("RailwayLine")]
        public string LineNumber { get; set; }
        public int CompareTo(Service? other)
        {
            if (other == null) return 1;

            return this.DelayAmount.CompareTo(other.DelayAmount);
        }
        public override string ToString()
        {
            return $"The No {TrainNumber} train information:\n\tLine number:{LineNumber}\n\tDeparture station:{From}" +
                $"\n\tFinal station:{To}\n\tTrain type:{TrainType}\n\tAmount of delay:{DelayAmount}";
        }
    }
}

