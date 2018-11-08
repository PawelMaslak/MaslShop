namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetProductsRev1SP : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"
			IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProducts]') AND type in (N'P', N'PC'))
			BEGIN
			EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetProducts] AS' 
			END
			GO
				SET ANSI_NULLS ON
			GO
				SET QUOTED_IDENTIFIER ON
			GO
				ALTER Procedure[dbo].[GetProducts]
				as
                select P.Id
				, P.Name
				, P.CategoryId
				, P.Dimensions
				, P.Id
				, P.Manufacturer
				, P.Year
				, P.AddedDate
				, P.Price
				, P.StockAmount
				, P.Description
				, C.Name as CategoryName
				, Max(F.FileName) as FileName

			    From Products as P
			    inner join Categories C on p.CategoryId = c.Id
			    left join Files F on P.Id = F.ProductId

			    Group by
			      P.Id
				, P.Name
				, P.CategoryId
				, P.Dimensions
				, P.Id
				, P.Manufacturer
				, P.Year
				, P.AddedDate
				, P.Price
				, P.StockAmount
				, P.Description
				, C.Name
				
                Order by P.AddedDate desc

                GO");
        }
        
        public override void Down()
        {
            this.Sql(@"DROP PROCEDURE [dbo].[GetProducts]
					GO
					");
        }
    }
}
