using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vehicles.Models.Models;

namespace Vehicles.Controllers
{
    public interface IVehicleModelController
    {
        Task<IActionResult> Create();
        Task<IActionResult> Create(VehicleModel obj);
        Task<IActionResult> Delete(Guid id);
        Task<IActionResult> Delete(VehicleModel obj);
        Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int pageSize, int currentPageSize);
        Task<IActionResult> Update(Guid id);
        Task<IActionResult> Update(VehicleModel obj);
    }
}