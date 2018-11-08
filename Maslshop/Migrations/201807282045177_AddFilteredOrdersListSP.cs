namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilteredOrdersListSP : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FilteredOrders]') AND type in (N'P', N'PC'))
			BEGIN
			EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FilteredOrders] AS' 
			END

            GO
				SET ANSI_NULLS ON
			GO
				SET QUOTED_IDENTIFIER ON
			GO
            
            ALTER Procedure[dbo].[FilteredOrders]
				(@query NVARCHAR(256))
				as
                select O.OrderId
				, O.UserName
				, O.Name
				, O.Surname
				, O.OrderDate
				, O.OrderTotal
                , P.Name as PaymentTypeName
				, D.Name as DeliveryTypeName
				, OS.Status as OrderStatusName
				, sum(OD.Quantity) as Quantity

			    From Orders as O
				Inner join OrderDetails OD on O.OrderId = OD.OrderId
				Inner join OrderStatus OS on O.OrderStatusId = OS.Id
				Inner join Deliveries D on O.DeliveryId = D.Id
                Inner join Payments P on O.PaymentTypeId = P.Id
			
				Where (O.OrderId like '%'+@query+'%'
				or O.UserName like '%'+@query+'%'
				or O.Name like '%'+@query+'%'
				or O.Surname like '%'+@query+'%'
				or O.OrderDate like '%'+@query+'%'
				or O.OrderTotal like '%'+@query+'%'
				or P.Name like '%'+@query+'%'
				or D.Name like '%'+@query+'%'
				or OS.Status like '%'+@query+'%')
                
				Group by 
				O.OrderId
				, O.UserName
				, O.Name
				, O.Surname
				, O.OrderDate
				, O.OrderTotal
                , D.Name
                , P.Name
				, OS.Status
				
				Order by O.OrderDate desc

                GO");
        }
        
        public override void Down()
        {
            this.Sql(@"DROP PROCEDURE [dbo].[FilteredOrders]
                GO");
        }
    }
}
