namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddingProductAddedDateToProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "AddedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "AddedDate");
        }
    }
}
