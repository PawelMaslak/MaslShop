namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductPhoto : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ProductPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductPhoto", c => c.Byte(nullable: false));
        }
    }
}
