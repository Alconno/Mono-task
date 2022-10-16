using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Vehicles.REST;
using Vehicles.Service.Services;
using Vehicles.Models.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vehicles.Controllers
{
    public class VehicleModelController : Controller
    {
        private  IVehicleModelService _vehicleModelService { get;set; }
        private IVehicleMakeService _vehicleMakeService { get; set; }
        public VehicleModelController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
        }

        private async Task<List<VehicleMake>> GetVehicleMakesList()
        {
            return (await _vehicleMakeService.GetAllAsync()).ToList();
             
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int pageSize, int currentPageSize)
        {
            // Sorting params
            ViewData["CurrentSort"] = sortOrder;
            ViewData["GuidSortParam"] =  String.IsNullOrEmpty(sortOrder) ? "guidDesc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "nameDesc" : "name";
            ViewData["AbrvSortParam"] = sortOrder == "abrv" ? "abrvDesc" : "abrv";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;// Filtering params

            pageSize = currentPageSize == 0 ? pageSize = currentPageSize = 3 : currentPageSize;
            ViewData["CurrentPageSize"] = pageSize;// Page size params

            ViewData["VehicleMakes"] = await GetVehicleMakesList();

            return (View (await _vehicleModelService.GetAllFilters(sortOrder, searchString, pageNumber, pageSize, currentPageSize)));
        }

        // GET-Create
        public async Task<IActionResult> Create()
        {
            ViewData["VehicleMakes"] = (await GetVehicleMakesList()).Select(obj => new SelectListItem { Text=obj.Name, Value=obj.Id.ToString() }).ToList();
            return await Task.FromResult(View());
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModel obj)
        {
            var newObj = await _vehicleModelService.CreateAsync(obj);
            return RedirectToAction("Index");
        }

        // GET-Delete
        public async Task<IActionResult> Delete(Guid id)
        {
            var obj = await _vehicleModelService.GetAsync(id);
            if (obj != null) return View(obj);
            return NotFound();
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VehicleModel obj)
        {
            if (await _vehicleModelService.DeleteAsync(obj.Id)) return RedirectToAction("Index");
            return View(obj);
        }

        // GET-Update
        public async Task<IActionResult> Update(Guid id)
        {
            var obj = await _vehicleModelService.GetAsync(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VehicleModel obj)
        {
            await _vehicleModelService.UpdateAsync(obj.Id, obj);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DisplayMake(Guid id)
        {
            var obj = await _vehicleMakeService.GetAsync(id);
            if(obj == null) return NotFound();
            return View(obj);
        }
    }
}
