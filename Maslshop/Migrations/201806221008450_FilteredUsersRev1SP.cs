namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilteredUsersRev1SP : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FilteredUsers]') AND type in (N'P', N'PC'))
			BEGIN
			EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FilteredUsers] AS' 
			END
			GO
				SET ANSI_NULLS ON
			GO
				SET QUOTED_IDENTIFIER ON
			GO
            ALTER Procedure [dbo].[FilteredUsers]
            (@query NVARCHAR(64))
            As
            Select U.Id
            , U.Email
            , U.Address
            , U.City
            , U.PostCode
            , U.Name
            , U.Surname
            , U.RegistrationDate
            , UR.Name as Role_Name

            From AspNetUsers AS U
            Inner join AspNetUserRoles as R on U.Id = R.UserId
            Inner join AspNetRoles as UR on R.RoleId = UR.Id

            Where (U.Id like '%'+@query+'%'
            or U.Email like '%'+@query+'%'
            or U.Name like '%'+@query+'%'
            or U.Surname like '%'+@query+'%'
            and UR.Name != 'Administrator')

            Order by U.RegistrationDate desc

            GO");
        }
        
        public override void Down()
        {
            this.Sql(@"DROP PROCEDURE [dbo].[FilteredUsers]
            GO");
        }
    }
}
