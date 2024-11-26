using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class DetalleServicio
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Detalles del servicio")]
        public string Descripcion { get; set; }

        public int? IdCambioPieza { get; private set; } // Nullable, since it depends on usoRefaccion

        [ForeignKey("IdCambioPieza")]
        public CambioPieza? CambioPieza { get; set; }

        [Required]
        [Display(Name ="Verificacion de uso refaccion")]
        public bool usoRefaccion { get; private set; }

        /// Asigna si usoRefaccion es True. Si lo es, se asigna IdCambioPieza; en lo contrario, Lo mantiene como un nulo.
        public void SetUsoRefaccion(bool uso, CambioPieza cambioPieza = null)
        {
            usoRefaccion = uso;

            if (uso && cambioPieza != null)
            {
                IdCambioPieza = cambioPieza.Id;
                CambioPieza = cambioPieza;
            }
            else
            {
                IdCambioPieza = null;
                CambioPieza = null;
            }
        }
    }
}
