namespace System_Realizacji_Zamowien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsRead");
            DropColumn("dbo.Messages", "IsRead");
        }
    }
}
