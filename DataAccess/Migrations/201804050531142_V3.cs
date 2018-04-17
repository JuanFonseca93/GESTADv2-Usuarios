namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nivel2", "nivel1", c => c.String(nullable: false, maxLength: 4000));
            AddColumn("dbo.Nivel3", "nivel2", c => c.String(nullable: false, maxLength: 4000));
            AddColumn("dbo.Nivel4", "nivel3", c => c.String(nullable: false, maxLength: 4000));
            AddColumn("dbo.Nivel5", "nivel4", c => c.String(nullable: false, maxLength: 4000));
            DropColumn("dbo.Nivel1", "nivel2");
            DropColumn("dbo.Nivel2", "nivel3");
            DropColumn("dbo.Nivel3", "nivel4");
            DropColumn("dbo.Nivel4", "nivel5");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nivel4", "nivel5", c => c.String());
            AddColumn("dbo.Nivel3", "nivel4", c => c.String());
            AddColumn("dbo.Nivel2", "nivel3", c => c.String());
            AddColumn("dbo.Nivel1", "nivel2", c => c.String());
            DropColumn("dbo.Nivel5", "nivel4");
            DropColumn("dbo.Nivel4", "nivel3");
            DropColumn("dbo.Nivel3", "nivel2");
            DropColumn("dbo.Nivel2", "nivel1");
        }
    }
}
