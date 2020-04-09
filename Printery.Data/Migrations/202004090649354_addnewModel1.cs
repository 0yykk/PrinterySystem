namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        CustomerName = c.String(),
                        Contact = c.String(),
                        Phone = c.String(),
                        MobilePhone = c.String(),
                        CAddress = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Password = c.String(),
                        Sex = c.String(),
                        Nation = c.String(),
                        Department = c.String(),
                        UserGroup = c.String(),
                        Phone = c.String(),
                        QQ = c.String(),
                        Ename = c.String(),
                        IDCardNum = c.String(),
                    })
                .PrimaryKey(t => t.EmpId);
            
            AddColumn("dbo.Order", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Contact");
            DropTable("dbo.Employee");
            DropTable("dbo.Customer");
        }
    }
}
