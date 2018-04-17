using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntidadConfiguraciones
{
    public class TipoDocumentoConfiguracion : EntityTypeConfiguration<TipoDocumento>
    {
        public TipoDocumentoConfiguracion()
        {
            ToTable("TipoDocumentos");
            HasKey(u => u.idTipo);

            Property(u => u.nombreTipo)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.detallesTipo)
                .IsRequired()
                .HasMaxLength(300);

            Property(u => u.ubicacion)
                .IsRequired()
                .HasMaxLength(500);

            HasMany(s => s.N1)
                 .WithMany(c => c.TipoDoc)
                 .Map(cs =>
                 {
                     cs.MapLeftKey("IdUsuario");
                     cs.MapRightKey("IdPermiso");
                     cs.ToTable("TIPON1");
                 });

            HasMany(s => s.N2)
                .WithMany(c => c.TipoDoc)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdUsuario");
                    cs.MapRightKey("IdPermiso");
                    cs.ToTable("TIPON2");
                });

            HasMany(s => s.N3)
                .WithMany(c => c.TipoDoc)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdUsuario");
                    cs.MapRightKey("IdPermiso");
                    cs.ToTable("TIPON3");
                });
            HasMany(s => s.N4)
                .WithMany(c => c.TipoDoc)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdUsuario");
                    cs.MapRightKey("IdPermiso");
                    cs.ToTable("TIPON4");
                });
            HasMany(s => s.N5)
                .WithMany(c => c.TipoDoc)
                .Map(cs =>
                {
                    cs.MapLeftKey("IdUsuario");
                    cs.MapRightKey("IdPermiso");
                    cs.ToTable("TIPON5");
                });
        }
    }
}
