using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles
{
    public class SortedList<T> : List<T>
    {
        public string SortOrder { get; private set; }

        public IQueryable<Service.Models.VehicleMake> sortMakeList(IQueryable<Service.Models.VehicleMake> list, string sortOrder)
        {
            SortOrder = sortOrder;

            switch (sortOrder)
            {
                case "nameDesc":
                    list = list.OrderByDescending(s => s.Name);
                    break;
                case "guid":
                    list = list.OrderBy(s => s.Guid);
                    break;
                case "guidDesc":
                    list = list.OrderByDescending(s => s.Guid);
                    break;
                case "abrv":
                    list = list.OrderBy(s => s.Abrv);
                    break;
                case "abrvDesc":
                    list = list.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    list = list.OrderBy(s => s.Guid);
                    break;
            }
            return list;
        }

        public IQueryable<Service.Models.VehicleModel> sortModelList(IQueryable<Service.Models.VehicleModel> list, string sortOrder)
        {
            SortOrder = sortOrder;

            switch (sortOrder)
            {
                case "nameDesc":
                    list = list.OrderByDescending(s => s.Name);
                    break;
                case "guid":
                    list = list.OrderBy(s => s.Guid);
                    break;
                case "guidDesc":
                    list = list.OrderByDescending(s => s.Guid);
                    break;
                case "makeId":
                    list = list.OrderBy(s => s.MakeId);
                    break;
                case "makeIdDesc":
                    list = list.OrderByDescending(s => s.MakeId);
                    break;
                case "abrv":
                    list = list.OrderBy(s => s.Abrv);
                    break;
                case "abrvDesc":
                    list = list.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    list = list.OrderBy(s => s.Guid);
                    break;
            }
            return list;
        }

    }
}
