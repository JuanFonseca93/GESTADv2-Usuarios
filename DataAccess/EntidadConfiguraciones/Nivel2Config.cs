using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntidadConfiguraciones
{
    class Nivel2Config : EntityTypeConfiguration<Nivel2>
    {
        public Nivel2Config()
        {
            ToTable("Nivel2");
            HasKey(a => a.idN);

            Property(a => a.nombreN)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.descripcionN)
                .IsRequired()
                .HasMaxLength(4000);

            Property(a => a.nivel1)
                .IsRequired()
                .HasMaxLength(4000);

            HasMany(s => s.TipoDoc)
                .WithMany(c => c.N2)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdUsuario");
                    cs.MapRightKey("IdPermiso");
                    cs.ToTable("TIPON2");
                });
        }

    }
}
