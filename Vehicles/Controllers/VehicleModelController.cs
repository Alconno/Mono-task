using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Vehicles.Data;
using Vehicles.Models;
using System.Linq;
using AutoMapper;

namespace Vehicles.Controllers
{
    abstract class AbstractVehicleModelController
    {
        public abstract Task<IActionResult> Index();
    }
    public class VehicleModelController : Controller
    {
        private readonly AppDBContext _db;

        public VehicleModelController(AppDBContext db)
        {
            _db = db;
        }

        
        public async Task<IActionResult>Index(string sortOrder, string searchString, string currentFilter, int?pageNumber, int pageSize, int currentPageSize)
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
            ViewData["CurrentFilter"] = searchString;


            // Page size
            if (currentPageSize == 0)
            {
                pageSize = currentPageSize = 3;
            }
            else
            {
                pageSize = currentPageSize;
            }
            ViewData["CurrentPageSize"] = pageSize;



            var VehicleModels = from s in _db.VehicleModel select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                VehicleModels = VehicleModels.Where(s => s.MakeId.ToString().Contains(searchString)); 
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    VehicleModels = VehicleModels.OrderByDescending(s => s.Name);
                    break;
                case "guid":
                    VehicleModels = VehicleModels.OrderBy(s => s.Guid);
                    break;
                case "guidDesc":
                    VehicleModels = VehicleModels.OrderByDescending(s => s.Guid);
                    break;
                case "makeId":
                    VehicleModels = VehicleModels.OrderBy(s => s.MakeId);
                    break;
                case "makeIdDesc":
                    VehicleModels = VehicleModels.OrderByDescending(s => s.MakeId);
                    break;
                case "abrv":
                    VehicleModels = VehicleModels.OrderBy(s => s.Abrv);
                    break;
                case "abrvDesc":
                    VehicleModels = VehicleModels.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    VehicleModels = VehicleModels.OrderBy(s => s.Guid);
                    break;
            }
            
            return View(await PaginatedList<VehicleModel>.CreateAsync(
                                                        VehicleModels.AsNoTracking(), 
                                                        pageNumber > Math.Ceiling(Convert.ToDouble(VehicleModels.Count()) / Convert.ToDouble(pageSize)) ? 1 : pageNumber ?? 1,
                                                        pageSize));
        }

        // GET-Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModel obj)
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
            var obj =  _db.VehicleModel.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VehicleModel obj)
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
        public async Task<IActionResult> Update(VehicleModel obj)
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
