namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherPropertiesToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "PostCode", c => c.String(nullable: false, maxLength: 6));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "PostCode");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
