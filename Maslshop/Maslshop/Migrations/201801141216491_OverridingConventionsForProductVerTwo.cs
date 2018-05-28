namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverridingConventionsForProductVerTwo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
        }
    }
}
