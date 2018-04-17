using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Dominio
{
    public class Documento
    {
        public Documento()
        {
            colaboraciones = new HashSet<Colaboracion>();
        }
        public int idDocumento { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(4, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "El Area debe de comenzar con mayuscula y no se permiten solo numeros")]
        public string nombreDocumento { get; set; }

        public string rutaDocumento { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(4, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "La descripcion debe de comenzar con mayuscula y no se permiten solo numeros")]
        public string descripccionDocumento { get; set; }

        [DataType(DataType.Date)]
        public string fechaSubidaDocumento { get; set; }

        [DataType(DataType.Date)]
        public string fechaModificiacionDocumento { get; set; }
        
        public int estadoDocumento { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Seleccione la fecha de vencimiento del documento")]
        public string vigencia { get; set; }

        public int idUsuario { get; set; }

        public virtual Usuario usuario { get; set; }

        public int idTipo { get; set; }

        public virtual TipoDocumento tipo { get; set; }

        public virtual ICollection<Colaboracion> colaboraciones { get; set; }
    }
}