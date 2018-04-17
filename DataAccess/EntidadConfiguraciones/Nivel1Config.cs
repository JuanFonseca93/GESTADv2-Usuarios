using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntidadConfiguraciones
{
    class Nivel1Config : EntityTypeConfiguration<Nivel1>
    {
        public Nivel1Config()
        {
            ToTable("Nivel1");
            HasKey(a => a.idN);

            Property(a => a.nombreN)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.descripcionN)
                .IsRequired()
                .HasMaxLength(4000);

            HasMany(s => s.TipoDoc)
                .WithMany(c => c.N1)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdUsuario");
                    cs.MapRightKey("IdPermiso");
                    cs.ToTable("TIPON1");
                });
        }

    }
}
