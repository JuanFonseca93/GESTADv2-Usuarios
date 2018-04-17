using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Dominio
{
    public class Area
    {
        public Area()
        {
            usuarios = new HashSet<Usuario>();
        }
        public int idArea { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(3, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "El Area debe de comenzar con mayuscula y no se permiten solo numeros")]
        public string nombreArea { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(3, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ .,A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "Debe de iniciar con mayuscula y no puede contener caracteres especiales")]
        public string descripcionArea { get; set; }

        public ICollection<Usuario> usuarios { get; set; }
    }
}