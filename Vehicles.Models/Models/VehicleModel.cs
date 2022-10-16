using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models.Models
{
    public class VehicleModel : VehicleBase
    {
        public Guid MakeId { get; set; }
        public VehicleMake Make { get; set; }
    }
}
