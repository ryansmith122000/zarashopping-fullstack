USE [ecommerce-app]
GO
/****** Object:  StoredProcedure [dbo].[Users_Update]    Script Date: 7/2/2023 7:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:     RYAN SMITH
-- Create date: 7/1/2023
-- Description:    UPDATING A USER BASED ON ID
-- =============================================
CREATE PROCEDURE [dbo].[Users_Update]
    -- Add the parameters for the stored procedure here

    @Name NVARCHAR(50),
    @Email NVARCHAR(50),
    @Password NVARCHAR(250),
    @ProfilePicture NVARCHAR(250),
    @DateOfBirth NVARCHAR(50),
    @Gender NVARCHAR(50),
    @Street NVARCHAR(50),
    @LineTwo NVARCHAR(50),
    @City NVARCHAR(50),
    @State NVARCHAR(50),
    @ZipCode NVARCHAR(50),
    @Country NVARCHAR(50),
    @DateModified DATETIME2 = null,
	@Id INT
AS

/* 
declare
@Name NVARCHAR(50) = 'test',
@Email NVARCHAR(50) = 'test',
@Password NVARCHAR(50) = 'test',
@ProfilePicture NVARCHAR(250) = 'test',
@DateOfBirth NVARCHAR(50) = 'December 14 2000',
@Gender NVARCHAR(50) = 'test',
@Street NVARCHAR(50) = 'test',
@LineTwo NVARCHAR(50) = 'test',
@City NVARCHAR(50) = 'test',
@State NVARCHAR(50) = 'test',
@ZipCode int = 1,
@Country NVARCHAR(50) = 'test',
@DateModified DATETIME2 = GETUTCDATE(),
@Id INT = 11
execute [dbo].[Users_Update]
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
    @DateModified,
    @Id

execute [dbo].[Users_SelectAll]
*/


BEGIN
    UPDATE [dbo].[Users]
    SET [Name] = @Name,
        [Email] = @Email,
        [Password] = @Password,
        [ProfilePicture] = @ProfilePicture,
        [DateOfBirth] = @DateOfBirth,
        [Gender] = @Gender,
        [DateModified] = GETUTCDATE()
    WHERE Id = @Id

    UPDATE [dbo].[Address]
    SET [Street] = @Street,
        [LineTwo] = @LineTwo,
        [City] = @City,
        [State] = @State,
        [ZipCode] = @ZipCode,
        [Country] = @Country,
        [DateModified] = GETUTCDATE()
    WHERE Id = (SELECT [AddressId] FROM [dbo].[Users] WHERE Id = @Id)
END
GO
