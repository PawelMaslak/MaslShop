namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddingFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 255),
                        Content = c.Binary(),
                        ProductId = c.Int(nullable: false),
                        FileType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "ProductId", "dbo.Products");
            DropIndex("dbo.Files", new[] { "ProductId" });
            DropTable("dbo.Files");
        }
    }
}
