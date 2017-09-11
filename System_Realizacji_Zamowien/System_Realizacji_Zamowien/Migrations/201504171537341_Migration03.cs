namespace System_Realizacji_Zamowien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            CreateTable(
                "dbo.MessageUsers",
                c => new
                    {
                        Message_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Message_Id, t.User_Id })
                .ForeignKey("dbo.Messages", t => t.Message_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Message_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.Messages", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "User_Id", c => c.Int());
            DropForeignKey("dbo.MessageUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.MessageUsers", "Message_Id", "dbo.Messages");
            DropIndex("dbo.MessageUsers", new[] { "User_Id" });
            DropIndex("dbo.MessageUsers", new[] { "Message_Id" });
            DropTable("dbo.MessageUsers");
            CreateIndex("dbo.Messages", "User_Id");
            AddForeignKey("dbo.Messages", "User_Id", "dbo.Users", "Id");
        }
    }
}
