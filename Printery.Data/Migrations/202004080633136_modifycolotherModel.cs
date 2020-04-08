namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifycolotherModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InkPurchasing", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.InkPurchasing", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PaperPurchasing", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.PaperPurchasing", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ProductGoods", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductGoods", "Count", c => c.String());
            AlterColumn("dbo.PaperPurchasing", "Price", c => c.String());
            AlterColumn("dbo.PaperPurchasing", "Count", c => c.String());
            AlterColumn("dbo.InkPurchasing", "Price", c => c.String());
            AlterColumn("dbo.InkPurchasing", "Count", c => c.String());
        }
    }
}
