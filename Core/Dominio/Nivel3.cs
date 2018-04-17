using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dominio
{
    public class Nivel3
    {
        public int idN { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(4, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "El Area debe de comenzar con mayuscula y no se permiten solo numeros")]
        public string nombreN { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(4, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ .,A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "Debe de iniciar con mayuscula y no puede contener caracteres especiales")]
        public string descripcionN { get; set; }

        public string nivel2 { get; set; }

        public int idDoc { get; set; }
        public virtual ICollection<TipoDocumento> TipoDoc { get; set; }
    }
}
