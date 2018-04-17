using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.EntidadConfiguraciones
{
    public class UsuarioConfiguracion : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguracion()
        {
            ToTable("Usuarios");
            HasKey(u => u.idUsuario);

            Property(u => u.nombreUsuario)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.correoUsuario)
                .IsRequired()
                .HasMaxLength(150);

            Property(u => u.curpUsuario)
                .IsRequired()
                .HasMaxLength(30);

            Property(u => u.fechaNacimientoUsuario)
                .IsRequired();

            Property(u => u.generoUsuario)
                .IsRequired()
                .HasMaxLength(20);

            Property(u => u.institucionUsuario)
                .IsRequired()
                .HasMaxLength(150);

            Property(u => u.nivelUsuario)
                .IsRequired();

            Property(u => u.telefonoUsuario)
                .IsRequired()
                .HasMaxLength(20);

            Property(u => u.passwordUsuario)
                .IsRequired()
                .HasMaxLength(50);

            Property(u => u.Estatus)
                .IsRequired();

            HasMany(d => d.colaboraciones)
                .WithRequired(c => c.usuario)
                .HasForeignKey(d => d.idUsuario);

            HasMany(d => d.documentos)
                .WithRequired(c => c.usuario)
                .HasForeignKey(d => d.idUsuario);

            HasRequired(u => u.area)
                .WithMany(a => a.usuarios)
                .HasForeignKey(u => u.idArea);
        }
    }
}
