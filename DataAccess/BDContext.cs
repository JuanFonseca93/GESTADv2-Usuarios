using Core.Dominio;
using DataAcces.EntidadConfiguraciones;
using DataAccess.EntidadConfiguraciones;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

public class BDContext : DbContext
    {
        public BDContext()
            : base("name=BDContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Colaboracion> Colaboracion { get; set; }
        public virtual DbSet<TipoColaboracion> TipoColaboracion { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<Nivel1> Nivel1 { get; set; }
        public virtual DbSet<Nivel2> Nivel2 { get; set; }
        public virtual DbSet<Nivel3> Nivel3 { get; set; }
        public virtual DbSet<Nivel4> Nivel4 { get; set; }
        public virtual DbSet<Nivel5> Nivel5 { get; set; }




    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AreaConfiguracion());
            modelBuilder.Configurations.Add(new ColaboracionConfiguracion());
            modelBuilder.Configurations.Add(new UsuarioConfiguracion());
            modelBuilder.Configurations.Add(new TipoColaboracionConfiguracion());
            modelBuilder.Configurations.Add(new DocumentosConfiguracion());
            modelBuilder.Configurations.Add(new Nivel1Config());
            modelBuilder.Configurations.Add(new Nivel2Config());
            modelBuilder.Configurations.Add(new Nivel3Config());
            modelBuilder.Configurations.Add(new Nivel4Config());
            modelBuilder.Configurations.Add(new Nivel5Config());
            modelBuilder.Configurations.Add(new TipoDocumentoConfiguracion());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }

