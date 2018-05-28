namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductPhotoVer2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductPhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductPhoto");
        }
    }
}
