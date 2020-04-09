namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewModelPower : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuperUserList",
                c => new
                    {
                        EmpId = c.String(nullable: false, maxLength: 128),
                        PowerGUIDName = c.String(),
                    })
                .PrimaryKey(t => t.EmpId)
                .ForeignKey("dbo.Employee", t => t.EmpId)
                .Index(t => t.EmpId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuperUserList", "EmpId", "dbo.Employee");
            DropIndex("dbo.SuperUserList", new[] { "EmpId" });
            DropTable("dbo.SuperUserList");
        }
    }
}
