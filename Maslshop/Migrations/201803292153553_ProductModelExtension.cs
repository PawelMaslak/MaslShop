namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ProductModelExtension : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "PureGoldWeight", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Fineness", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Dimensions", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Manufacturer", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Manufacturer");
            DropColumn("dbo.Products", "Dimensions");
            DropColumn("dbo.Products", "Fineness");
            DropColumn("dbo.Products", "PureGoldWeight");
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "Year");
        }
    }
}
