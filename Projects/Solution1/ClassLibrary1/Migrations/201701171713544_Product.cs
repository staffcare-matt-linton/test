namespace ClassLibrary1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        Account_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.LineItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Product_Id = c.String(maxLength: 128),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Product_Id)
                .Index(t => t.Order_OrderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        CostPrice = c.Double(nullable: false),
                        RetailPrice = c.Double(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineItems", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.LineItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Orders", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.LineItems", new[] { "Order_OrderId" });
            DropIndex("dbo.LineItems", new[] { "Product_Id" });
            DropIndex("dbo.Orders", new[] { "Account_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.LineItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Accounts");
        }
    }
}
