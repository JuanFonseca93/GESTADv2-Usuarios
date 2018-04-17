using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.EntidadConfiguraciones
{
    public class DocumentosConfiguracion : EntityTypeConfiguration<Documento>
    {
        public DocumentosConfiguracion()
        {
            ToTable("Documentos");
            HasKey(u => u.idDocumento);

            Property(u => u.nombreDocumento)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.descripccionDocumento)
                .IsRequired()
                .HasMaxLength(300);

            Property(u => u.estadoDocumento)
                .IsRequired();

            Property(u => u.fechaModificiacionDocumento);

            Property(u => u.fechaSubidaDocumento)
                .IsRequired();

            Property(u => u.rutaDocumento)
                .IsRequired()
                .HasMaxLength(500);

            Property(u => u.vigencia)
                .IsRequired();

            HasRequired(u => u.usuario)
                .WithMany(a => a.documentos)
                .HasForeignKey(u => u.idUsuario);

            HasMany(d => d.colaboraciones)
                .WithRequired(c => c.documento)
                .HasForeignKey(d => d.idDocumento);

            HasRequired(u => u.tipo)
                .WithMany(a => a.documentos)
                .HasForeignKey(u => u.idTipo);
        }
    }
}
