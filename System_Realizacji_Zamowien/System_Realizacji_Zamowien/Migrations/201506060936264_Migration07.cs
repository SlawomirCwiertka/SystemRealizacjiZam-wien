namespace System_Realizacji_Zamowien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration07 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Firms", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Firms", "City");
        }
    }
}
