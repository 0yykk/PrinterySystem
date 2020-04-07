namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InkPurchasing",
                c => new
                    {
                        PurchasingID = c.String(nullable: false, maxLength: 128),
                        InkId = c.String(maxLength: 128),
                        InkName = c.String(),
                        Count = c.String(),
                        Price = c.String(),
                        CreatePersonId = c.String(),
                        ProcessPersonId = c.String(),
                        Status = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ProcessDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PurchasingID)
                .ForeignKey("dbo.InkStock", t => t.InkId)
                .Index(t => t.InkId);
            
            CreateTable(
                "dbo.InkStock",
                c => new
                    {
                        InkId = c.String(nullable: false, maxLength: 128),
                        InkName = c.String(),
                        Ccount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InkId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        CartID = c.String(nullable: false, maxLength: 128),
                        OrderId = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductName = c.String(),
                        Count = c.Int(nullable: false),
                        eachPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.String(nullable: false, maxLength: 128),
                        ProductName = c.String(),
                        eachPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CCOunt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Paper",
                c => new
                    {
                        PaperId = c.String(nullable: false, maxLength: 128),
                        PaperName = c.String(),
                        Ccount = c.String(),
                    })
                .PrimaryKey(t => t.PaperId);
            
            CreateTable(
                "dbo.PaperPurchasing",
                c => new
                    {
                        PurchasingID = c.String(nullable: false, maxLength: 128),
                        PaperId = c.String(maxLength: 128),
                        PaperName = c.String(),
                        Count = c.String(),
                        Price = c.String(),
                        CreatePersonId = c.String(),
                        ProcessPersonId = c.String(),
                        Status = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ProcessDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PurchasingID)
                .ForeignKey("dbo.Paper", t => t.PaperId)
                .Index(t => t.PaperId);
            
            CreateTable(
                "dbo.ProductGoods",
                c => new
                    {
                        PurchasingID = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductName = c.String(),
                        Count = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        eachPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatePersonId = c.String(),
                        ProcessPersonId = c.String(),
                        Status = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ProcessDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PurchasingID)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductGoods", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PaperPurchasing", "PaperId", "dbo.Paper");
            DropForeignKey("dbo.OrderDetail", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.InkPurchasing", "InkId", "dbo.InkStock");
            DropIndex("dbo.ProductGoods", new[] { "ProductId" });
            DropIndex("dbo.PaperPurchasing", new[] { "PaperId" });
            DropIndex("dbo.OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.InkPurchasing", new[] { "InkId" });
            DropTable("dbo.ProductGoods");
            DropTable("dbo.PaperPurchasing");
            DropTable("dbo.Paper");
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.InkStock");
            DropTable("dbo.InkPurchasing");
        }
    }
}
