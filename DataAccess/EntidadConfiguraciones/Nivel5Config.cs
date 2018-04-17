using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntidadConfiguraciones
{
    class Nivel5Config : EntityTypeConfiguration<Nivel5>
    {
        public Nivel5Config()
        {
            ToTable("Nivel5");
            HasKey(a => a.idN);

            Property(a => a.nombreN)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.descripcionN)
                .IsRequired()
                .HasMaxLength(4000);

            Property(a => a.nivel4)
                .IsRequired()
                .HasMaxLength(4000);

            HasMany(s => s.TipoDoc)
                .WithMany(c => c.N5)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdUsuario");
                    cs.MapRightKey("IdPermiso");
                    cs.ToTable("TIPON5");
                });
        }

    }
}
