namespace System_Realizacji_Zamowien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration06 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Flag = c.Int(nullable: false),
                        Message_Id = c.Int(),
                        Order_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.Message_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Message_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.Messages", "IsRead");
            DropColumn("dbo.Orders", "IsRead");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsRead", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.States", "User_Id", "dbo.Users");
            DropForeignKey("dbo.States", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.States", "Message_Id", "dbo.Messages");
            DropIndex("dbo.States", new[] { "User_Id" });
            DropIndex("dbo.States", new[] { "Order_Id" });
            DropIndex("dbo.States", new[] { "Message_Id" });
            DropTable("dbo.States");
        }
    }
}
