namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeleteRoleFromApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String(nullable: false));
        }
    }
}
