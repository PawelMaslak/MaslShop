namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductPhotoNo1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductPhoto", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductPhoto");
        }
    }
}
