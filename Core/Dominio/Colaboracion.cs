using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Dominio
{
    public class Colaboracion
    {
        public int idColaboracion { get; set; }

        public int propietarioColaboracion { get; set; }

        public bool tipoColaboracionInterno  { get; set; }

        public int idTipoColaboracion { get; set; }

        public TipoColaboracion TipoColaboracion { get; set; }

        public int idDocumento { get; set; }

        public Documento documento { get; set; }

        public int idUsuario { get; set; }

        public Usuario usuario { get; set; }

    }
}