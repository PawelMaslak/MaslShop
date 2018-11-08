namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDeliveryModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "DeliveryId");
            AddForeignKey("dbo.Orders", "DeliveryId", "dbo.Deliveries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DeliveryId", "dbo.Deliveries");
            DropIndex("dbo.Orders", new[] { "DeliveryId" });
        }
    }
}
