using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dominio
{
    public class TipoDocumento
    {
        public TipoDocumento()
        {
            N1 = new HashSet<Nivel1>();
            N2 = new HashSet<Nivel2>();
            N3 = new HashSet<Nivel3>();
            N4 = new HashSet<Nivel4>();
            N5 = new HashSet<Nivel5>();
        }
        public int idTipo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(4, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "El Area debe de comenzar con mayuscula y no se permiten solo numeros")]
        public string nombreTipo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(4, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "El detalle debe de comenzar con mayuscula y no se permiten solo numeros")]
        public string detallesTipo { get; set; }

        public string ubicacion { get; set; }
        public virtual ICollection<Documento> documentos { get; set; }

        public virtual ICollection<Nivel1> N1 { get; set; }
        public virtual ICollection<Nivel2> N2 { get; set; }
        public virtual ICollection<Nivel3> N3 { get; set; }
        public virtual ICollection<Nivel4> N4 { get; set; }
        public virtual ICollection<Nivel5> N5 { get; set; }
    }
}
