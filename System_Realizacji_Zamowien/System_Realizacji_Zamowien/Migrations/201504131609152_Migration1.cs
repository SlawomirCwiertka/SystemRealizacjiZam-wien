namespace System_Realizacji_Zamowien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        NIP = c.String(),
                        Access_Id = c.Int(),
                        Employer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accesses", t => t.Access_Id)
                .ForeignKey("dbo.Firms", t => t.Employer_Id)
                .Index(t => t.Access_Id)
                .Index(t => t.Employer_Id);
            
            CreateTable(
                "dbo.Firms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                        PostCode = c.String(),
                        Nip = c.String(),
                        Regon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataOrder = c.DateTime(nullable: false),
                        Success = c.Boolean(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Term = c.DateTime(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.ItemCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        InProgress = c.Boolean(nullable: false),
                        Product_Id = c.Int(),
                        User_Id = c.Int(),
                        Invoice_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Invoice_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LinkPhoto = c.String(),
                        Vat = c.Double(nullable: false),
                        Availability = c.Int(nullable: false),
                        Category_Id = c.Int(),
                        UnitOfMeasure_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.UnitOfMeasures", t => t.UnitOfMeasure_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.UnitOfMeasure_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkPhoto = c.String(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.UnitOfMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ItemCards", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ItemCards", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.ItemCards", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "UnitOfMeasure_Id", "dbo.UnitOfMeasures");
            DropForeignKey("dbo.ItemCards", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            DropForeignKey("dbo.Invoices", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Users", "Employer_Id", "dbo.Firms");
            DropForeignKey("dbo.Users", "Access_Id", "dbo.Accesses");
            DropIndex("dbo.Categories", new[] { "Parent_Id" });
            DropIndex("dbo.Products", new[] { "UnitOfMeasure_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.ItemCards", new[] { "Order_Id" });
            DropIndex("dbo.ItemCards", new[] { "Invoice_Id" });
            DropIndex("dbo.ItemCards", new[] { "User_Id" });
            DropIndex("dbo.ItemCards", new[] { "Product_Id" });
            DropIndex("dbo.Invoices", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Employer_Id" });
            DropIndex("dbo.Users", new[] { "Access_Id" });
            DropTable("dbo.UnitOfMeasures");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.ItemCards");
            DropTable("dbo.Invoices");
            DropTable("dbo.Orders");
            DropTable("dbo.Firms");
            DropTable("dbo.Users");
            DropTable("dbo.Accesses");
        }
    }
}
