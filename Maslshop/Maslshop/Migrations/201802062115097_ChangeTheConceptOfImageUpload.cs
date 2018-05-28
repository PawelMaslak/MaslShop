namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTheConceptOfImageUpload : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Files", "ContentType");
            DropColumn("dbo.Files", "Content");
            DropColumn("dbo.Files", "FileType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "FileType", c => c.Int(nullable: false));
            AddColumn("dbo.Files", "Content", c => c.Binary());
            AddColumn("dbo.Files", "ContentType", c => c.String(maxLength: 255));
        }
    }
}
