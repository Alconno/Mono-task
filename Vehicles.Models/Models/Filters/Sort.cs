using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models.Models.Filters
{
    public class Sort<T> where T : IVehicleBase
    {
        public IQueryable<T>sortList(IQueryable<T>list, string sortOrder)
        {

            switch (sortOrder)
            {
                case "nameDesc":
                    list = list.OrderByDescending(s => s.Name);
                    break;
                case "guid":
                    list = list.OrderBy(s => s.Id);
                    break;
                case "guidDesc":
                    list = list.OrderByDescending(s => s.Id);
                    break;
                case "abrv":
                    list = list.OrderBy(s => s.Abrv);
                    break;
                case "abrvDesc":
                    list = list.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    list = list.OrderBy(s => s.Id);
                    break;
            }
            return list;
        }
    }
}
