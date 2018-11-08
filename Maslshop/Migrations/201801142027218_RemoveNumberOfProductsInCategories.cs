namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNumberOfProductsInCategories : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "NumberOfProducts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "NumberOfProducts", c => c.Int(nullable: false));
        }
    }
}
