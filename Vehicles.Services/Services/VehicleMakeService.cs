using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Models;
using Vehicles.Models.Models.Filters;
using Vehicles.Service.Data;

namespace Vehicles.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private AppDbContext _db { get; set; }
        public VehicleMakeService(AppDbContext db)
        {
            _db=db;
        }

        public async Task<VehicleMake> CreateAsync(VehicleMake entity)
        {
            if (entity.Name != null && entity.Abrv != null)
            {
                entity.Id = new Guid();
                _db.VehicleMake.Add(entity);
                _db.SaveChanges();
                return await Task.FromResult(entity);
            }
            return null;
        }

        public async Task<VehicleMake> UpdateAsync(Guid id, VehicleMake entity)
        {
            if (entity.Name != null && entity.Abrv != null)
            {
                entity.Id=id;
                _db.VehicleMake.Update(entity);
                _db.SaveChanges();
                return await Task.FromResult(entity);
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await Task.FromResult(_db.VehicleMake.Find(id));
            if (obj != null)
            {
                _db.VehicleMake.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<VehicleMake>> GetAllAsync()
        {
            return await Task.FromResult(_db.VehicleMake);
        }

        public async Task<VehicleMake> GetAsync(Guid id)
        {
            return await Task.FromResult(_db.VehicleMake.Find(id));
        }
        public async Task<Pagination<VehicleMake>> GetAllFilters(string sortOrder, string searchString, int? pageNumber, int pageSize, int currentPageSize)
        {
            Sort<VehicleMake> sort = new Sort<VehicleMake>();
            VehicleFilter filter = new VehicleFilter();
            var obj = sort.sortList(filter.filterMakeList(GetAllAsync().Result, searchString).Result, sortOrder);

            return await (Pagination<VehicleMake>.CreateAsync(
                                obj.AsNoTracking(),
                                pageNumber > Math.Ceiling(Convert.ToDouble(obj.Count()) / Convert.ToDouble(pageSize)) ? 1 : pageNumber ?? 1,
                                pageSize));
        }
    }
}
