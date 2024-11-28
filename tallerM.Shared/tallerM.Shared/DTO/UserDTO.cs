using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tallerM.Shared.Entities;

namespace tallerM.Shared.DTO
{
    public class UserDTO:User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(20,MinimumLength = 6, ErrorMessage = "El campo {0} debe" +
            "tener entre {2} y {1}")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe" +
            "tener entre {2} y {1}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Compare("Password", ErrorMessage ="no son iguales")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
