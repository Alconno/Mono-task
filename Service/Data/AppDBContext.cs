using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Vehicles;

namespace Service.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Service.Models.VehicleModel> VehicleModel { get; set; }
        public DbSet<Service.Models.VehicleMake> VehicleMake { get; set; }
    }
}
