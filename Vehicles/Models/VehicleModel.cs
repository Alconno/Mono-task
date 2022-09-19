using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Vehicles.Models
{
    public class VehicleModel { 
        [Key]
        public int Guid { get; set; }

        [Required]
        [Range(100, 99999999, ErrorMessage = "Id must contain 3-7 digits")]
        [ForeignKey("FK_VehicleMake")]
        public int MakeId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Abrv { get; set; }
 
        
    }
}
