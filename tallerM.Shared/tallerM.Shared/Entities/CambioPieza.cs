using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tallerM.Shared.Entities
{
    public class CambioPieza
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Tipo Refaccion")]
        public string Pieza { get; private set; } = string.Empty; // String vacia por default

        [Required]
        [Display(Name ="Id Refaccion Utilizada")]
        public int IdRefaccion { get; set; }

        [ForeignKey("IdRefaccion")]
        public Refaccion Refaccion
        {
            get => _refaccion;
            set
            {
                _refaccion = value;
                if (_refaccion != null && string.IsNullOrEmpty(Pieza))
                {
                    Pieza = _refaccion.Tipo; // Asegurar que pieza solo sea asignada una vez
                }
            }
        }

        private Refaccion _refaccion;
    }
}
