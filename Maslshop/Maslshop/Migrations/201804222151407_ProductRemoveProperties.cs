namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductRemoveProperties : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "PureGoldWeight");
            DropColumn("dbo.Products", "Fineness");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Fineness", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "PureGoldWeight", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Weight", c => c.Double(nullable: false));
        }
    }
}
