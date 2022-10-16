using System;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.Models.Models;

namespace Vehicles.Service.Services
{
    public interface IVehicleModelService : IServiceBase<VehicleModel>
    {
        Task<VehicleModel> CreateAsync(VehicleModel entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IQueryable<VehicleModel>> GetAllAsync();
        Task<Pagination<VehicleModel>> GetAllFilters(string sortOrder, string searchString, int? pageNumber, int pageSize, int currentPageSize);
        Task<VehicleModel> GetAsync(Guid id);
        Task<VehicleModel> UpdateAsync(Guid id, VehicleModel entity);
    }
}