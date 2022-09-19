using Microsoft.AspNetCore.Razor.Language;
using Vehicles.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext>options) : base(options)
        {

        }

        public DbSet<VehicleModel>VehicleModel { get; set; }
        public DbSet<VehicleMake>VehicleMake { get; set; }
    }
}
