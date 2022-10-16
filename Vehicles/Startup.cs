using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vehicles.Controllers;
using Vehicles.Models;
using Vehicles.Models.Models;
using Vehicles.Service.Data;
using Vehicles.Service.Services;
using Vehicles.Services.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Vehicles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ContainerBuilder();

            
          

            // Register MVC Controllers
            builder.RegisterType<HomeController>().InstancePerRequest();
            builder.RegisterType<VehicleMakeController>().InstancePerRequest();
            // builder.RegisterControllers(typeof(MvcApplication).Assembly);


            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(VehicleModel).Assembly);
            builder.RegisterModelBinders(typeof(VehicleModel).Assembly);
            builder.RegisterModelBinders(typeof(ErrorViewModel).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();
            builder.Register(c => Configuration).As<IConfiguration>().SingleInstance();
        
            builder.RegisterModule(new DataModule());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Vehicles.Services"))
            );
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddMvc();
            services.AddScoped<IVehicleBase, VehicleMake>();
            services.AddScoped<IVehicleBase, VehicleModel>();
            services.AddScoped<IVehicleMakeService, VehicleMakeService>();
            services.AddScoped<IVehicleModelService, VehicleModelService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
