namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TIPON1",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdPermiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUsuario, t.IdPermiso })
                .ForeignKey("dbo.Nivel1", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.TipoDocumentos", t => t.IdPermiso, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdPermiso);
            
            CreateTable(
                "dbo.TIPON2",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdPermiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUsuario, t.IdPermiso })
                .ForeignKey("dbo.Nivel2", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.TipoDocumentos", t => t.IdPermiso, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdPermiso);
            
            CreateTable(
                "dbo.TIPON3",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdPermiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUsuario, t.IdPermiso })
                .ForeignKey("dbo.Nivel3", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.TipoDocumentos", t => t.IdPermiso, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdPermiso);
            
            CreateTable(
                "dbo.TIPON4",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdPermiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUsuario, t.IdPermiso })
                .ForeignKey("dbo.Nivel4", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.TipoDocumentos", t => t.IdPermiso, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdPermiso);
            
            CreateTable(
                "dbo.TIPON5",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdPermiso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUsuario, t.IdPermiso })
                .ForeignKey("dbo.Nivel5", t => t.IdUsuario, cascadeDelete: true)
                .ForeignKey("dbo.TipoDocumentos", t => t.IdPermiso, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdPermiso);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TIPON5", "IdPermiso", "dbo.TipoDocumentos");
            DropForeignKey("dbo.TIPON5", "IdUsuario", "dbo.Nivel5");
            DropForeignKey("dbo.TIPON4", "IdPermiso", "dbo.TipoDocumentos");
            DropForeignKey("dbo.TIPON4", "IdUsuario", "dbo.Nivel4");
            DropForeignKey("dbo.TIPON3", "IdPermiso", "dbo.TipoDocumentos");
            DropForeignKey("dbo.TIPON3", "IdUsuario", "dbo.Nivel3");
            DropForeignKey("dbo.TIPON2", "IdPermiso", "dbo.TipoDocumentos");
            DropForeignKey("dbo.TIPON2", "IdUsuario", "dbo.Nivel2");
            DropForeignKey("dbo.TIPON1", "IdPermiso", "dbo.TipoDocumentos");
            DropForeignKey("dbo.TIPON1", "IdUsuario", "dbo.Nivel1");
            DropIndex("dbo.TIPON5", new[] { "IdPermiso" });
            DropIndex("dbo.TIPON5", new[] { "IdUsuario" });
            DropIndex("dbo.TIPON4", new[] { "IdPermiso" });
            DropIndex("dbo.TIPON4", new[] { "IdUsuario" });
            DropIndex("dbo.TIPON3", new[] { "IdPermiso" });
            DropIndex("dbo.TIPON3", new[] { "IdUsuario" });
            DropIndex("dbo.TIPON2", new[] { "IdPermiso" });
            DropIndex("dbo.TIPON2", new[] { "IdUsuario" });
            DropIndex("dbo.TIPON1", new[] { "IdPermiso" });
            DropIndex("dbo.TIPON1", new[] { "IdUsuario" });
            DropTable("dbo.TIPON5");
            DropTable("dbo.TIPON4");
            DropTable("dbo.TIPON3");
            DropTable("dbo.TIPON2");
            DropTable("dbo.TIPON1");
        }
    }
}
