using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "El campo {0} debe tener max {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage ="Error en el campo")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

    }
}
