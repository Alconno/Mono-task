using System;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.Models.Models;

namespace Vehicles.Service.Services
{
    public interface IVehicleMakeService : IServiceBase<VehicleMake>
    {
        Task<VehicleMake> CreateAsync(VehicleMake entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IQueryable<VehicleMake>> GetAllAsync();
        Task<Pagination<VehicleMake>> GetAllFilters(string sortOrder, string searchString, int? pageNumber, int pageSize, int currentPageSize);
        Task<VehicleMake> GetAsync(Guid id);
        Task<VehicleMake> UpdateAsync(Guid id, VehicleMake entity);
    }
}