namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentTypeToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PaymentTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PaymentTypeId");
        }
    }
}
