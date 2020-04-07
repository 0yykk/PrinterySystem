namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreaateBg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128),
                        OrderCreate = c.DateTime(nullable: false),
                        OrderProcess = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deadline = c.DateTime(nullable: false),
                        Tips = c.String(),
                        CreatePersonId = c.String(),
                        ProcessPersonId = c.String(),
                        Status = c.String(),
                        CustomerId = c.String(),
                        CustomerName = c.String(),
                        Addressed = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Order");
        }
    }
}
