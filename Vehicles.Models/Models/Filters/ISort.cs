using System.Linq;

namespace Vehicles.Models.Models.Filters
{
    public interface ISort<T> where T : IVehicleBase
    {
        IQueryable<T> sortList(IQueryable<T> list, string sortOrder);
    }
}