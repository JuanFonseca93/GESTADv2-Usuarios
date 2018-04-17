namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TipoDocumentos", "idAreaD", "dbo.AreasDoc");
            DropIndex("dbo.TipoDocumentos", new[] { "idAreaD" });
            CreateTable(
                "dbo.Nivel1",
                c => new
                    {
                        idN = c.Int(nullable: false, identity: true),
                        nombreN = c.String(nullable: false, maxLength: 100),
                        descripcionN = c.String(nullable: false, maxLength: 4000),
                        nivel2 = c.String(),
                        idDoc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idN);
            
            CreateTable(
                "dbo.Nivel2",
                c => new
                    {
                        idN = c.Int(nullable: false, identity: true),
                        nombreN = c.String(nullable: false, maxLength: 100),
                        descripcionN = c.String(nullable: false, maxLength: 4000),
                        nivel3 = c.String(),
                        idDoc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idN);
            
            CreateTable(
                "dbo.Nivel3",
                c => new
                    {
                        idN = c.Int(nullable: false, identity: true),
                        nombreN = c.String(nullable: false, maxLength: 100),
                        descripcionN = c.String(nullable: false, maxLength: 4000),
                        nivel4 = c.String(),
                        idDoc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idN);
            
            CreateTable(
                "dbo.Nivel4",
                c => new
                    {
                        idN = c.Int(nullable: false, identity: true),
                        nombreN = c.String(nullable: false, maxLength: 100),
                        descripcionN = c.String(nullable: false, maxLength: 4000),
                        nivel5 = c.String(),
                        idDoc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idN);
            
            CreateTable(
                "dbo.Nivel5",
                c => new
                    {
                        idN = c.Int(nullable: false, identity: true),
                        nombreN = c.String(nullable: false, maxLength: 100),
                        descripcionN = c.String(nullable: false, maxLength: 4000),
                        idDoc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idN);
            
            AddColumn("dbo.Usuarios", "Estatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Documentos", "estadoDocumento", c => c.Int(nullable: false));
            DropColumn("dbo.TipoDocumentos", "idAreaD");
            DropTable("dbo.AreasDoc");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AreasDoc",
                c => new
                    {
                        idAreaD = c.Int(nullable: false, identity: true),
                        nombreAreaD = c.String(nullable: false, maxLength: 100),
                        descripcionAreaD = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.idAreaD);
            
            AddColumn("dbo.TipoDocumentos", "idAreaD", c => c.Int(nullable: false));
            AlterColumn("dbo.Documentos", "estadoDocumento", c => c.Boolean(nullable: false));
            DropColumn("dbo.Usuarios", "Estatus");
            DropTable("dbo.Nivel5");
            DropTable("dbo.Nivel4");
            DropTable("dbo.Nivel3");
            DropTable("dbo.Nivel2");
            DropTable("dbo.Nivel1");
            CreateIndex("dbo.TipoDocumentos", "idAreaD");
            AddForeignKey("dbo.TipoDocumentos", "idAreaD", "dbo.AreasDoc", "idAreaD", cascadeDelete: true);
        }
    }
}
