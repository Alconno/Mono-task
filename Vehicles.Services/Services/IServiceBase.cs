using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Service.Services
{
    public interface IServiceBase<T>
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(Guid id, T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IQueryable<T>> GetAllAsync();
        Task<T>GetAsync(Guid id);

    }
}
