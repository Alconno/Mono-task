using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicles.Data;
using Vehicles.Models;
using System.Linq;
using AutoMapper;

namespace Vehicles.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;

        public VehicleMakeController(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
            // Filtering params
            ViewData["CurrentFilter"] = searchString;



            if (currentPageSize == 0)
            {
                pageSize = currentPageSize = 3;
            }
            else
            {
                pageSize = currentPageSize;
            }
            // Page size params
            ViewData["CurrentPageSize"] = pageSize;



            var VehicleMakes = from s in _db.VehicleMake select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                VehicleMakes = VehicleMakes.Where(s =>
                                                        s.Guid.ToString().Contains(searchString)||
                                                        s.Name.Contains(searchString)||
                                                        s.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    VehicleMakes = VehicleMakes.OrderByDescending(s => s.Name);
                    break;
                case "guid":
                    VehicleMakes = VehicleMakes.OrderBy(s => s.Guid);
                    break;
                case "guidDesc":
                    VehicleMakes = VehicleMakes.OrderByDescending(s => s.Guid);
                    break;
                case "abrv":
                    VehicleMakes = VehicleMakes.OrderBy(s => s.Abrv);
                    break;
                case "abrvDesc":
                    VehicleMakes = VehicleMakes.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    VehicleMakes = VehicleMakes.OrderBy(s => s.Guid);
                    break;
            }

            return View(await PaginatedList<VehicleMake>.CreateAsync(
                                                        VehicleMakes.AsNoTracking(),
                                                        pageNumber > Math.Ceiling(Convert.ToDouble(VehicleMakes.Count()) / Convert.ToDouble(pageSize)) ? 1 : pageNumber ?? 1,
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
        public async Task<IActionResult> Create(VehicleMake obj)
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
        public async Task<IActionResult> Delete(VehicleMake obj)
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
        public async Task<IActionResult> Update(VehicleMake obj)
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
