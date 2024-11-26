using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class Refaccion
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener un maximo de {1} caracteres")]
        [Display(Name = "Nombre de la Refacción")]
        public string Tipo { get; set; }
    }
}
