using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsCore.Models
{
    public class Car
    {
        public string State { get; set; }

        public string LicensePlate { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public virtual ICollection<RecordOfSale> SaleHistory { get; set; }
    }
}
