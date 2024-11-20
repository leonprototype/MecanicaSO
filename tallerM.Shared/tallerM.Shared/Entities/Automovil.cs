using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    internal class Automovil
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="El campo {0} debe tener max {1} caracteres")]
        [Display(Name = "Marca")]
        public string Brand { get; set; }
        public int? Year { get; set; }

    }
}
