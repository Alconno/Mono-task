using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.Models.Models.Filters
{
    public interface IVehicleFilter
    {
        Task<IQueryable<VehicleMake>> filterMakeList(IQueryable<VehicleMake> list, string currentFilter);
        Task<IQueryable<VehicleModel>> filterModelList(IQueryable<VehicleModel> list, string currentFilter);
    }
}