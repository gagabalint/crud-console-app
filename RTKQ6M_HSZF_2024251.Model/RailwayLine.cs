using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTKQ6M_HSZF_2024251.Model
{
    public class RailwayLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LineNumber { get; set; }

        [Required]
        public string LineName { get; set; }

        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
        public override bool Equals(object obj)
        {
            if (obj is RailwayLine other)
            {
                return LineNumber == other.LineNumber && LineName == other.LineName;
            }
            return false;
        }
       
    }
}
