using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dominio
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Solo se aceptan un maximo de 100 letras"),
        MinLength(4, ErrorMessage = "Requiere un minimo de 4 letras")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "El Nombre debe de comenzar con mayuscula y no se permiten solo numeros")]
        public string nombreUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int nivelUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [EmailAddress]
        [Index("IX_FirstAndSecond", 1, IsUnique = true)]
        public string correoUsuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string passwordUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string generoUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression(@"/^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9][12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/}", ErrorMessage ="Formato de CURP incorrecto")]
        [Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public string curpUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [DataType(DataType.Date)]
        public string fechaNacimientoUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression(@"^[A-Z]{1}[ A-Za-záéíóú]+[ 0-9]*", ErrorMessage = "El Nombre debe comenzar con mayuscula")]
        public string institucionUsuario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        ErrorMessage = "Numero de telefono incorrecto")]
        public string telefonoUsuario { get; set; }

        public int Estatus { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int idArea { get; set; }

        public virtual Area area { get; set; }

        public virtual ICollection<Documento> documentos { get; set; }

        public virtual ICollection<Colaboracion> colaboraciones { get; set; }

    }
}