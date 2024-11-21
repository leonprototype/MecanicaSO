using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class Automovil
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="El campo {0} debe tener max {1} caracteres")]
        [Display(Name = "Marca")]
        public string Brand { get; set; }
        public int? Year { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Error en el campo")]
        [Display(Name ="Placas del auto")]
        public string Plate { get; set; }
    }
}
