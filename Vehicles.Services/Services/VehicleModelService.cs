using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class VehicleModelService : IVehicleModelService
    {
        private AppDbContext _db { get; set; }
        public VehicleModelService(AppDbContext db)
        {
            _db=db;
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel entity)
        {
            if (entity.Name != null && entity.Abrv != null && _db.VehicleMake.Find(entity.MakeId) != null)
            {
                entity.Id = new Guid();
                _db.VehicleModel.Add(entity);
                _db.SaveChanges();
                return await Task.FromResult(entity);
            }
            return null;
        }

        public async Task<VehicleModel> UpdateAsync(Guid id, VehicleModel entity)
        {
            if (entity.Name != null && entity.Abrv != null)
            {
                entity.Id=id;
                _db.VehicleModel.Update(entity);
                _db.SaveChanges();
                return await Task.FromResult(entity);
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var obj = await Task.FromResult(_db.VehicleModel.Find(id));
            if (obj != null)
            {
                _db.VehicleModel.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<VehicleModel>> GetAllAsync()
        {
            return await Task.FromResult(_db.VehicleModel);
        }

        public async Task<VehicleModel> GetAsync(Guid id)
        {
            return await Task.FromResult(_db.VehicleModel.Find(id));
        }

        public async Task<Pagination<VehicleModel>> GetAllFilters(string sortOrder, string searchString, int? pageNumber, int pageSize, int currentPageSize)
        {
            Sort<VehicleModel> sort = new Sort<VehicleModel>();
            VehicleFilter<VehicleModel> filter = new VehicleFilter<VehicleModel>();
            var obj = sort.sortList(filter.filterMakeList(GetAllAsync().Result, searchString).Result, sortOrder);

            return await Pagination<VehicleModel>.CreateAsync(
                                obj.AsNoTracking(),
                                pageNumber > Math.Ceiling(Convert.ToDouble(obj.Count()) / Convert.ToDouble(pageSize)) ? 1 : pageNumber ?? 1,
                                pageSize);
        }
    }
}
