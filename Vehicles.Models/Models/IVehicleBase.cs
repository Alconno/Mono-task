using System;

namespace Vehicles.Models
{
    public interface IVehicleBase
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        string Name { get; set; }
    }
}