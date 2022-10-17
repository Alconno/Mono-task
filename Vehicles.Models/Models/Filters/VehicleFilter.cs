using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.Models.Models.Filters
{
    public class VehicleFilter : IVehicleFilter
    {
        public async Task<IQueryable<VehicleMake>> filterMakeList(IQueryable<VehicleMake> list, string currentFilter)
        {
            if (currentFilter != null)
            {
                list = await Task.FromResult((IQueryable<VehicleMake>)list.Where(s => s.Name.Contains(currentFilter)));
            }
            return list;
        }

        public async Task<IQueryable<VehicleModel>> filterModelList(IQueryable<VehicleModel> list, string currentFilter)
        {
            if (currentFilter != null)
            {
                list = await Task.FromResult((IQueryable<VehicleModel>)list.Where(s => s.Make.Name.Contains(currentFilter)));
            }
            return list;
        }
    }

}
