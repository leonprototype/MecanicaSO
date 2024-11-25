using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class Servicio
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Automovil al que se le realizó servicio")]
        public required Automovil Automovil { get; set; }

    }
}
