using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.EntidadConfiguraciones
{
    class ColaboracionConfiguracion : EntityTypeConfiguration<Colaboracion>
    {
        public ColaboracionConfiguracion()
        {
            ToTable("Colaboraciones");
            HasKey(a => a.idColaboracion);

            Property(a => a.propietarioColaboracion)
                .IsRequired();

            Property(a => a.tipoColaboracionInterno)
                .IsRequired();

            HasRequired(u => u.TipoColaboracion)
               .WithMany(a => a.Colaboraciones)
               .HasForeignKey(u => u.idTipoColaboracion);

            HasRequired(u => u.documento)
               .WithMany(a => a.colaboraciones)
               .HasForeignKey(u => u.idDocumento);

            HasRequired(u => u.usuario)
               .WithMany(a => a.colaboraciones)
               .HasForeignKey(u => u.idUsuario);
        }

    }
}
