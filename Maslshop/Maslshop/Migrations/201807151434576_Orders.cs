namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "OrderTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
