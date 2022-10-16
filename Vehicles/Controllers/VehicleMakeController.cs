using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Vehicles.REST;
using Vehicles.Service.Services;
using Vehicles.Models.Models;
using System.Collections.Generic;

namespace Vehicles.Controllers
{
    public class VehicleMakeController : Controller
    {
        private  IVehicleMakeService _vehicleMakeService { get;set; }
        public VehicleMakeController(IVehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService=vehicleMakeService;   
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


            return (View (await _vehicleMakeService.GetAllFilters(sortOrder, searchString, pageNumber, pageSize, currentPageSize)));
        }

        // GET-Create
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleMake obj)
        {
            var newObj = await _vehicleMakeService.CreateAsync(obj);
            return RedirectToAction("Index");
        }

        // GET-Delete
        public async Task<IActionResult> Delete(Guid id)
        {
            var obj = await _vehicleMakeService.GetAsync(id);
            if (obj != null) return View(obj);
            return NotFound();
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VehicleMake obj)
        {
            if (await _vehicleMakeService.DeleteAsync(obj.Id)) return RedirectToAction("Index");
            return View(obj);
        }

        // GET-Update
        public async Task<IActionResult> Update(Guid id)
        {
            var obj = await _vehicleMakeService.GetAsync(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VehicleMake obj)
        {
            await _vehicleMakeService.UpdateAsync(obj.Id, obj);
            return RedirectToAction("Index");
        }
    }
}
