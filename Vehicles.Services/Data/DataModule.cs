using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Service.Data;
using Vehicles.Service.Services;

namespace Vehicles.Services.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();   
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
        }
    }
}
