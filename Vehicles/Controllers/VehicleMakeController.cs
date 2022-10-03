using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicles.Models;
using System.Linq;
using AutoMapper;
using Service.Data;
using Service.Models;
using Vehicles.Controllers;



namespace Vehicles.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly AppDBContext _db;

        public VehicleMakeController(AppDBContext db)
        {
            _db = db;
        }

        FilteredList<Service.Models.VehicleMake> currentFL = new FilteredList<Service.Models.VehicleMake>(); // filtered list
        SortedList<Service.Models.VehicleMake> currentSL = new SortedList<Service.Models.VehicleMake>(); // sorted list
        public async Task<PaginatedList<Service.Models.VehicleMake>> getAll(string sortOrder, string searchString, int? pageNumber, int pageSize, int currentPageSize)
        {
            var VehicleMakes = from s in _db.VehicleMake select s;

            return await PaginatedList<Service.Models.VehicleMake>.CreateAsync(
                                VehicleMakes = currentSL.sortMakeList(currentFL.filterMakeList(VehicleMakes, searchString),sortOrder).AsNoTracking(),
                                pageNumber > Math.Ceiling(Convert.ToDouble(VehicleMakes.Count()) / Convert.ToDouble(pageSize)) ? 1 : pageNumber ?? 1,
                                pageSize);
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

            return View(await getAll(sortOrder, searchString, pageNumber, pageSize, currentPageSize));
        }

        // GET-Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service.Models.VehicleMake obj)
        {
            if (ModelState.IsValid && _db.VehicleMake.Find(obj.Guid) == null) // server side validation
            {
                _db.VehicleMake.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index"); // for updated display
            }

            return View(obj);
        }

        // GET-Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var obj = _db.VehicleMake.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Service.Models.VehicleMake obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleMake.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET-Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null||id==0)
            {
                return NotFound();
            }
            var obj = _db.VehicleMake.Find(id);
            if (obj==null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Service.Models.VehicleMake obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleMake.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
