namespace Maslshop.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class GetLatestThreeProductsSP : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLatestThreeProducts]') AND type in (N'P', N'PC'))
			BEGIN
			EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetLatestThreeProducts] AS' 
			END
			GO
				SET ANSI_NULLS ON
			GO
				SET QUOTED_IDENTIFIER ON
			GO
            ALTER procedure [dbo].[GetLatestThreeProducts]
            as
            select top 3 P.Id
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
            order by P.Id desc
            GO");
        }
        
        public override void Down()
        {
            this.Sql(@"DROP PROCEDURE [dbo].[GetLatestThreeProducts]
            GO");
        }
    }
}
