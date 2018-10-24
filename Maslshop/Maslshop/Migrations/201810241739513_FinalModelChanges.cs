namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalModelChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
