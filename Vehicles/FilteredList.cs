using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles
{
    public class FilteredList<T> : List<T>
    {
        public string SearchString { get; private set; }

        public IQueryable<Service.Models.VehicleMake> filterMakeList(IQueryable<Service.Models.VehicleMake>list, string currentFilter)
        {
            if (SearchString==null)
            {
                SearchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                list = (IQueryable<Service.Models.VehicleMake>)list.Where(s => s.Guid.ToString().Contains(SearchString));
            }
            return list;
        }

        public IQueryable<Service.Models.VehicleModel> filterModelList(IQueryable<Service.Models.VehicleModel> list, string currentFilter)
        {
            if (SearchString==null)
            {
                SearchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                list = (IQueryable<Service.Models.VehicleModel>)list.Where(s => s.MakeId.ToString().Contains(SearchString));
            }
            return list;
        }
    }
}
