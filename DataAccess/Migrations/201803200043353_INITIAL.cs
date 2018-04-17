namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INITIAL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        idArea = c.Int(nullable: false, identity: true),
                        nombreArea = c.String(nullable: false, maxLength: 100),
                        descripcionArea = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.idArea);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        idUsuario = c.Int(nullable: false, identity: true),
                        nombreUsuario = c.String(nullable: false, maxLength: 100),
                        nivelUsuario = c.Int(nullable: false),
                        correoUsuario = c.String(nullable: false, maxLength: 150),
                        passwordUsuario = c.String(nullable: false, maxLength: 50),
                        generoUsuario = c.String(nullable: false, maxLength: 20),
                        curpUsuario = c.String(nullable: false, maxLength: 30),
                        fechaNacimientoUsuario = c.String(nullable: false),
                        institucionUsuario = c.String(nullable: false, maxLength: 150),
                        telefonoUsuario = c.String(nullable: false, maxLength: 20),
                        idArea = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idUsuario)
                .ForeignKey("dbo.Areas", t => t.idArea, cascadeDelete: true)
                .Index(t => t.idArea);
            
            CreateTable(
                "dbo.Colaboraciones",
                c => new
                    {
                        idColaboracion = c.Int(nullable: false, identity: true),
                        propietarioColaboracion = c.Boolean(nullable: false),
                        tipoColaboracionInterno = c.Boolean(nullable: false),
                        idTipoColaboracion = c.Int(nullable: false),
                        idDocumento = c.Int(nullable: false),
                        idUsuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idColaboracion)
                .ForeignKey("dbo.Documentos", t => t.idDocumento, cascadeDelete: true)
                .ForeignKey("dbo.TipoColaboraciones", t => t.idTipoColaboracion, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.idUsuario, cascadeDelete: true)
                .Index(t => t.idTipoColaboracion)
                .Index(t => t.idDocumento)
                .Index(t => t.idUsuario);
            
            CreateTable(
                "dbo.Documentos",
                c => new
                    {
                        idDocumento = c.Int(nullable: false, identity: true),
                        nombreDocumento = c.String(nullable: false, maxLength: 100),
                        rutaDocumento = c.String(nullable: false, maxLength: 50),
                        descripccionDocumento = c.String(nullable: false, maxLength: 300),
                        fechaSubidaDocumento = c.String(nullable: false),
                        fechaModificiacionDocumento = c.String(),
                        estadoDocumento = c.Boolean(nullable: false),
                        vigencia = c.String(nullable: false),
                        idUsuario = c.Int(nullable: false),
                        idTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idDocumento)
                .ForeignKey("dbo.TipoDocumentos", t => t.idTipo, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.idUsuario, cascadeDelete: false)
                .Index(t => t.idUsuario)
                .Index(t => t.idTipo);
            
            CreateTable(
                "dbo.TipoDocumentos",
                c => new
                    {
                        idTipo = c.Int(nullable: false, identity: true),
                        nombreTipo = c.String(nullable: false, maxLength: 100),
                        detallesTipo = c.String(nullable: false, maxLength: 300),
                        ubicacion = c.String(nullable: false, maxLength: 500),
                        idAreaD = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idTipo)
                .ForeignKey("dbo.AreasDoc", t => t.idAreaD, cascadeDelete: true)
                .Index(t => t.idAreaD);
            
            CreateTable(
                "dbo.AreasDoc",
                c => new
                    {
                        idAreaD = c.Int(nullable: false, identity: true),
                        nombreAreaD = c.String(nullable: false, maxLength: 100),
                        descripcionAreaD = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.idAreaD);
            
            CreateTable(
                "dbo.TipoColaboraciones",
                c => new
                    {
                        idTipoColaboracion = c.Int(nullable: false, identity: true),
                        nombreTipoColaboracion = c.String(nullable: false, maxLength: 100),
                        descripcionTipoColaboracion = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.idTipoColaboracion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "idArea", "dbo.Areas");
            DropForeignKey("dbo.Colaboraciones", "idUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Colaboraciones", "idTipoColaboracion", "dbo.TipoColaboraciones");
            DropForeignKey("dbo.Colaboraciones", "idDocumento", "dbo.Documentos");
            DropForeignKey("dbo.Documentos", "idUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.Documentos", "idTipo", "dbo.TipoDocumentos");
            DropForeignKey("dbo.TipoDocumentos", "idAreaD", "dbo.AreasDoc");
            DropIndex("dbo.TipoDocumentos", new[] { "idAreaD" });
            DropIndex("dbo.Documentos", new[] { "idTipo" });
            DropIndex("dbo.Documentos", new[] { "idUsuario" });
            DropIndex("dbo.Colaboraciones", new[] { "idUsuario" });
            DropIndex("dbo.Colaboraciones", new[] { "idDocumento" });
            DropIndex("dbo.Colaboraciones", new[] { "idTipoColaboracion" });
            DropIndex("dbo.Usuarios", new[] { "idArea" });
            DropTable("dbo.TipoColaboraciones");
            DropTable("dbo.AreasDoc");
            DropTable("dbo.TipoDocumentos");
            DropTable("dbo.Documentos");
            DropTable("dbo.Colaboraciones");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Areas");
        }
    }
}
