USE [ecommerce-app]
GO
/****** Object:  StoredProcedure [dbo].[Users_SelectById]    Script Date: 7/2/2023 7:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RYAN SMITH
-- Create date: 7/2/2023 1:23 AM
-- Description:	SELECTING ALL DATA FROM THE USERS TABLE BY ID
-- =============================================
CREATE PROCEDURE [dbo].[Users_SelectById]

	@Id int

AS

/* 
declare
	@Id int = 5

execute [dbo].[Users_SelectById]
	@Id
*/

BEGIN


SELECT U.[Id]
      ,U.[Name]
      ,U.[Email]
      ,U.[Password]
      ,U.[ProfilePicture]
      ,U.[Gender]
	  ,R.[RoleName]
	  ,U.[IsDeleted]
      ,A.[Street]
	  ,A.[LineTwo]
	  ,A.[City]
	  ,A.[State]
	  ,A.[ZipCode]
	  ,A.[Country]
	  ,U.[DateOfBirth]
      ,U.[DateCreated]
	  ,U.[DateModified]


  FROM [dbo].[Users] as U
  LEFT OUTER JOIN [dbo].[Address] A on U.[AddressId] = A.Id
  RIGHT OUTER JOIN [dbo].[Roles] R on U.[RoleId] = R.Id
  WHERE U.Id = @Id


END
GO
