namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifycolPaper : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Paper", "Ccount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Paper", "Ccount", c => c.String());
        }
    }
}
