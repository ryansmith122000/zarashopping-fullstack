USE [ecommerce-app]
GO
/****** Object:  StoredProcedure [dbo].[Users_Add]    Script Date: 7/2/2023 7:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: RYAN SMITH
-- Create date: 7/1/2023 5:52PM
-- Description: INSERTING A USER INTO THE USER TABLE.
-- =============================================
CREATE PROCEDURE [dbo].[Users_Add]
    -- Add the parameters for the stored procedure here
    @Name nvarchar(50),
    @Email nvarchar(50),
    @Password nvarchar(250),
    @ProfilePicture nvarchar(250),
    @DateOfBirth nvarchar(50),
    @Gender nvarchar(50),
    @Street nvarchar(50),
    @LineTwo nvarchar(50),
    @City nvarchar(50),
    @State nvarchar(50),
    @ZipCode nvarchar(50),
    @Country nvarchar(50),
    @RoleId int = NULL, -- Make RoleId parameter nullable
    @CreatedBy int,
    @Id INT OUTPUT

AS

/* 
--------- Test Code ------------
declare 
    @Name nvarchar(50) = 'Ryan',
    @Email nvarchar(50) = 'ryansmith12.2000@outlook.com',
    @Password nvarchar(50) = '12345',
    @ProfilePicture nvarchar(250) = '1',
    @DateOfBirth nvarchar(50) = 'December 14, 2000',
    @Gender nvarchar(50) = 'Male',
    @Street nvarchar(50) = '2330 Paseo De Laura',
    @LineTwo nvarchar(50) = '', -- Add the desired LineTwo value here
    @City nvarchar(50) = 'City Name',
    @State nvarchar(50) = 'State Name',
    @ZipCode nvarchar(50) = '12345',
    @Country nvarchar(50) = 'Country Name',
    @RoleId int = 1,
    @CreatedBy int = 1,
    @Id INT;

EXEC [dbo].[Users_Add]
    @Name,
    @Email,
    @Password,
    @ProfilePicture,
    @DateOfBirth,
    @Gender,
    @Street,
    @LineTwo,
    @City,
    @State,
    @ZipCode,
    @Country,
    @RoleId,
    @CreatedBy,
    @Id OUTPUT;

EXEC [dbo].[Users_SelectAll];
*/

DECLARE @AddressId INT;

BEGIN
    -- Insert address
    INSERT INTO [dbo].[Address]
        ([Street], 
		 [LineTwo], 
		 [City], 
		 [State], 
		 [ZipCode], 
		 [Country], 
		 [DateCreated])
    VALUES
        (@Street, 
		 @LineTwo,
		 @City, 
		 @State, 
		 @ZipCode, 
		 @Country, 
		 GETUTCDATE());

    SET @AddressId = SCOPE_IDENTITY();

    -- Insert user with assigned role and address
    INSERT INTO [dbo].[Users]
        ([Name], 
		 [Email], 
		 [Password], 
		 [ProfilePicture], 
	 	 [DateOfBirth], 
		 [Gender], 
		 [AddressId], 
		 [RoleId], 
		 [CreatedBy])
    VALUES
        (@Name, 
		 @Email, 
		 @Password, 
		 @ProfilePicture, 
		 @DateOfBirth, 
		 @Gender, 
		 @AddressId, 
		 @RoleId,
		 @CreatedBy);

    SET @Id = SCOPE_IDENTITY();
END
GO
