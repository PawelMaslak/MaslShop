namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNewsSections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.News", "Image_Id", "dbo.NewsImages");
            DropIndex("dbo.News", new[] { "Image_Id" });
            DropTable("dbo.News");
            DropTable("dbo.NewsImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NewsImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.News", "Image_Id");
            AddForeignKey("dbo.News", "Image_Id", "dbo.NewsImages", "Id");
        }
    }
}
