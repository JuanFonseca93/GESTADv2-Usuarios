using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.EntidadConfiguraciones
{
    class TipoColaboracionConfiguracion : EntityTypeConfiguration<TipoColaboracion>
    {
        public TipoColaboracionConfiguracion()
        {
            ToTable("TipoColaboraciones");
            HasKey(u => u.idTipoColaboracion);

            Property(u => u.nombreTipoColaboracion)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.descripcionTipoColaboracion)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(d => d.Colaboraciones)
                .WithRequired(c => c.TipoColaboracion)
                .HasForeignKey(d => d.idTipoColaboracion);
        }
    }
}
