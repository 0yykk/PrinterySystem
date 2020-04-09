namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPowerControl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmpGroup",
                c => new
                    {
                        GroupId = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(),
                        Tip = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.PowerControlList",
                c => new
                    {
                        ControlId = c.String(nullable: false, maxLength: 128),
                        PowerId = c.String(maxLength: 128),
                        GroupId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ControlId)
                .ForeignKey("dbo.EmpGroup", t => t.GroupId)
                .ForeignKey("dbo.PowerControl", t => t.PowerId)
                .Index(t => t.PowerId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.PowerControl",
                c => new
                    {
                        PowerId = c.String(nullable: false, maxLength: 128),
                        PowerName = c.String(),
                        Tip = c.String(),
                    })
                .PrimaryKey(t => t.PowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PowerControlList", "PowerId", "dbo.PowerControl");
            DropForeignKey("dbo.PowerControlList", "GroupId", "dbo.EmpGroup");
            DropIndex("dbo.PowerControlList", new[] { "GroupId" });
            DropIndex("dbo.PowerControlList", new[] { "PowerId" });
            DropTable("dbo.PowerControl");
            DropTable("dbo.PowerControlList");
            DropTable("dbo.EmpGroup");
        }
    }
}
