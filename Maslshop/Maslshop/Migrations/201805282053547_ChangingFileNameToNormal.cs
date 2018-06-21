namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangingFileNameToNormal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FileName", c => c.String(maxLength: 255));
            DropColumn("dbo.Files", "File_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "File_Name", c => c.String(maxLength: 255));
            DropColumn("dbo.Files", "FileName");
        }
    }
}
