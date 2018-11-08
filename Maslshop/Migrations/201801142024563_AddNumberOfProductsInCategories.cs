namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberOfProductsInCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "NumberOfProducts", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "NumberOfProducts");
        }
    }
}
