using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Service.Data;
using Vehicles.Models;
using System.Linq;
using AutoMapper;
using Service.Models;
using System.ComponentModel;

namespace Vehicles.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly AppDBContext _db;

        public VehicleModelController(AppDBContext db)
        {
            _db = db;
        }

        FilteredList<Service.Models.VehicleModel> currentFL = new FilteredList<Service.Models.VehicleModel>(); // filtered list
        SortedList<Service.Models.VehicleModel> currentSL = new SortedList<Service.Models.VehicleModel>(); // sorted list
        public async Task<PaginatedList<Service.Models.VehicleModel>> getAll(string sortOrder, string searchString, int? pageNumber, int pageSize, int currentPageSize)
        {
            var VehicleModels = from s in _db.VehicleModel select s;
            
            return await PaginatedList<Service.Models.VehicleModel>.CreateAsync(
                                VehicleModels = currentSL.sortModelList(currentFL.filterModelList(VehicleModels, searchString), sortOrder).AsNoTracking(),
                                pageNumber > Math.Ceiling(Convert.ToDouble(VehicleModels.Count()) / Convert.ToDouble(pageSize)) ? 1 : pageNumber ?? 1,
                                pageSize);
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber, int pageSize, int currentPageSize)
        {
            // Sorting params
            ViewData["CurrentSort"] = sortOrder;
            ViewData["GuidSortParam"] =  String.IsNullOrEmpty(sortOrder) ? "guidDesc" : "";
            ViewData["NameSortParam"] = sortOrder == "name" ? "nameDesc" : "name";
            ViewData["MakeIdSortParam"] = sortOrder == "makeId" ? "makeIdDesc" : "makeId";
            ViewData["AbrvSortParam"] = sortOrder == "abrv" ? "abrvDesc" : "abrv";

            // Filtering 
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;//filtering params


            pageSize = currentPageSize == 0 ? pageSize = currentPageSize = 3 : currentPageSize;
            ViewData["CurrentPageSize"] = pageSize;//page size params

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
        public async Task<IActionResult> Create(Service.Models.VehicleModel obj)
        {
            if (ModelState.IsValid && _db.VehicleMake.Find(obj.MakeId) != null) // server side validation
            {
                _db.VehicleModel.Add(obj);
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
            var obj = _db.VehicleModel.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Service.Models.VehicleModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleModel.Remove(obj);
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
            var obj = _db.VehicleModel.Find(id);
            if (obj==null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Service.Models.VehicleModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleModel.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
