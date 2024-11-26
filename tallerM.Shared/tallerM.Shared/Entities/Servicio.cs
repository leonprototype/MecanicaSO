using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class Servicio : DetalleServicio
    {
        [Required]
        [Display(Name = "Automóvil al que se realizó servicio")]
        public required Automovil Automovil { get; set; }
        public required Mecanico Mecanico { get; set; }
    }
}

