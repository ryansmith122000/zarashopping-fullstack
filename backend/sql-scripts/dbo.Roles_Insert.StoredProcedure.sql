USE [ecommerce-app]
GO
/****** Object:  StoredProcedure [dbo].[Roles_Insert]    Script Date: 7/2/2023 7:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RYAN SMITH
-- Create date: 7/1/2023 9:28 PM
-- Description:	INSERTING ROLES INTO THE ROLE TABLE
-- =============================================
CREATE PROCEDURE [dbo].[Roles_Insert]
	-- Add the parameters for the stored procedure here
	@RoleName nvarchar(50),
	@RoleDescription nvarchar(250),
	@Id int

AS

/* 
declare
	@RoleName nvarchar(50) = 'Guest',
	@RoleDescription nvarchar(250) = 'This role is designed for people who are interacting with our website without being logged into an account.',
	@Id int

	execute [dbo].[Roles_Insert]
	@RoleName,
	@RoleDescription,
	@Id


	select * from [dbo].[Roles]

*/
BEGIN


INSERT INTO [dbo].[Roles]
           ([RoleName]
           ,[RoleDescription])
     VALUES
           (@RoleName,
		    @RoleDescription)

			SET @Id = SCOPE_IDENTITY();




END
GO
