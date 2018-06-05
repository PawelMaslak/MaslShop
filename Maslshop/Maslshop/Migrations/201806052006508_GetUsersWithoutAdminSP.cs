namespace Maslshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetUsersWithoutAdminSP : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUsersWithoutAdmin]') AND type in (N'P', N'PC'))
			BEGIN
			EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetUsersWithoutAdmin] AS' 
			END
			GO
				SET ANSI_NULLS ON
			GO
				SET QUOTED_IDENTIFIER ON
			GO
                ALTER Procedure [dbo].[GetUsersWithoutAdmin]
                (@P NVARCHAR(256))
                As
                Select U.Name
                , U.Surname
                , U.Email
                , U.Id
                , U.Address
                , U.PostCode
                , U.City
                , UR.Name as Role_Name
                , R.RoleId
                , R.UserId
                , Ur.Id
                , U.RegistrationDate
                From AspNetUsers AS U
                Inner join AspNetUserRoles as R on U.Id = R.UserId
                Inner join AspNetRoles as UR on R.RoleId = UR.Id
                Where ur.Name != @P
            GO");
        }
        
        public override void Down()
        {
            this.Sql(@"DROP PROCEDURE [dbo].[GetUsersWithoutAdmin]
                GO");
        }
    }
}
