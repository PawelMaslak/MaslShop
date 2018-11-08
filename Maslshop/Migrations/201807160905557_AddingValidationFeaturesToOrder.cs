namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingValidationFeaturesToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "PostCode", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "PostCode", c => c.String());
            AlterColumn("dbo.Orders", "Address", c => c.String());
            AlterColumn("dbo.Orders", "Surname", c => c.String());
            AlterColumn("dbo.Orders", "Name", c => c.String());
            DropColumn("dbo.Orders", "City");
        }
    }
}
