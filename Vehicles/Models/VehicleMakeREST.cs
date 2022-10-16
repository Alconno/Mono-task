


using System;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.REST
{
    public class VehicleMakeREST
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abrv { get; set; }
    }
}
