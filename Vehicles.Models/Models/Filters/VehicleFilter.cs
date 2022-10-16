using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.Models.Models.Filters
{
    public class VehicleFilter<T> where T : IVehicleBase
    {
        public async Task<IQueryable<T>> filterMakeList(IQueryable<T> list, string currentFilter)
        { 
            if (currentFilter != null)
            {
                list = await Task.FromResult((IQueryable<T>)list.Where(s => s.Name.Contains(currentFilter)));
            }
            return list;
        }
    }

}
