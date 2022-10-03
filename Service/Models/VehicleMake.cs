using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Models
{
    public class VehicleMake
    {
        [Required]
        [Range(100, 99999999, ErrorMessage = "Id must contain 3-7 digits")]
        [Key]
        public int Guid { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }
    }
}
