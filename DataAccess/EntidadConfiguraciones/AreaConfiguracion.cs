using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.EntidadConfiguraciones
{
    class AreaConfiguracion : EntityTypeConfiguration<Area>
    {
        public AreaConfiguracion()
        {
            ToTable("Areas");
            HasKey(a => a.idArea);

            Property(a => a.nombreArea)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.descripcionArea)
                .IsRequired()
                .HasMaxLength(4000);

            HasMany(d => d.usuarios)
                .WithRequired(c => c.area)
                .HasForeignKey(d => d.idArea);
        }

    }
}
