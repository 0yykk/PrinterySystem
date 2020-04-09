namespace Printery.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewcol4Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ProductName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "ProductName");
        }
    }
}
