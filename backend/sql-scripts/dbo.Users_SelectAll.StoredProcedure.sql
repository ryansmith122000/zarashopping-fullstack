USE [ecommerce-app]
GO
/****** Object:  StoredProcedure [dbo].[Users_SelectAll]    Script Date: 7/1/2023 10:01:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RYAN SMITH
-- Create date: 7/1/2023 5:40 PM
-- Description:	SELECT ALL FROM USER TABLE
-- =============================================
CREATE PROCEDURE [dbo].[Users_SelectAll]
	-- Add the parameters for the stored procedure here


AS

/* 

execute [dbo].[Users_SelectAll]
*/ 
BEGIN


SELECT U.[Id]
      ,U.[Name]
      ,U.[Email]
      ,U.[Password]
      ,U.[ProfilePicture]
      ,U.[DateOfBirth]
      ,U.[Gender]
      ,A.[Street]
	  ,A.[LineTwo]
	  ,A.[City]
	  ,A.[State]
	  ,A.[ZipCode]
	  ,A.[Country]
	  ,R.[RoleName]
      ,U.[CreatedBy]
      ,U.[ModifiedBy]
      ,U.[DateCreated]
      ,U.[DateModified]
	  ,U.[IsDeleted]
  FROM [dbo].[Users] as U
  INNER JOIN [dbo].[Address] A on U.[AddressId] = A.Id
  INNER JOIN [dbo].[Roles] R on U.[RoleId] = R.Id

END

GO
