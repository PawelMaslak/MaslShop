namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilteredProductsSP : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FilteredProducts]') AND type in (N'P', N'PC'))
			BEGIN
			EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FilteredProducts] AS' 
			END
			GO
				SET ANSI_NULLS ON
			GO
				SET QUOTED_IDENTIFIER ON
			GO
            ALTER procedure [dbo].[FilteredProducts]
            (@query NVARCHAR(256))
            as
            select P.Id
            ,P.Name
            ,P.CategoryId
            ,P.Dimensions
            ,P.Id
            ,P.Manufacturer
            ,P.Year
            ,P.AddedDate
            ,P.Price
            ,P.StockAmount
            ,P.Description
            ,C.Name as CategoryName
            ,Min(F.FileName) as FileName
            From Products as P
            inner join Categories C on p.CategoryId = c.Id
            left join Files F on P.Id = F.ProductId
            Where (P.Id like '%'+@query+'%'
            or P.Name like '%'+@query+'%'
            or C.Name like '%'+@query+'%'
            or P.Manufacturer like '%'+@query+'%'
            or P.Year like '%'+@query+'%')
            Group by
            P.Id
            ,P.Name
            ,P.CategoryId
            ,P.Dimensions
            ,P.Id
            ,P.Manufacturer
            ,P.Year
            ,P.AddedDate
            ,P.Price
            ,P.StockAmount
            ,P.Description
            ,C.Name
            GO
            ");
        }
        
        public override void Down()
        {
            this.Sql(@"DROP PROCEDURE [dbo].[FilteredProducts]
            GO");
        }
    }
}
