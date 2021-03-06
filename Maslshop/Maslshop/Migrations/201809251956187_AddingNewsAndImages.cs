namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNewsAndImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        AuthorName = c.String(),
                        AuthorSurname = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Content = c.String(),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsImages", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.NewsImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Image_Id", "dbo.NewsImages");
            DropIndex("dbo.News", new[] { "Image_Id" });
            DropTable("dbo.NewsImages");
            DropTable("dbo.News");
        }
    }
}
