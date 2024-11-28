using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class User:IdentityUser
    {
        [Required(ErrorMessage = "El campo {0} es oblicatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es oblicatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; } = null!;
        public UserType UserType {  get; set; }


        
    }
}
