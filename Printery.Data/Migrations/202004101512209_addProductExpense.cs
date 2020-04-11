namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductExpense : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductionExpense",
                c => new
                    {
                        ProExId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(),
                        ProductName = c.String(),
                        InkId1 = c.String(),
                        InkId1Count = c.Int(),
                        InkId2 = c.String(),
                        InkId2Count = c.Int(),
                        PaperId1 = c.String(),
                        PaperId1Count = c.Int(),
                        PaperId2 = c.String(),
                        PaperId2Count = c.Int(),
                    })
                .PrimaryKey(t => t.ProExId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductionExpense");
        }
    }
}
