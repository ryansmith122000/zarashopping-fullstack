USE [ecommerce-app]
GO
/****** Object:  StoredProcedure [dbo].[Users_Delete]    Script Date: 7/2/2023 7:10:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		RYAN SMITH
-- Create date: 7/1/2023 7:10PM
-- Description:	"SOFT DELETE" FROM USERS TABLE.
-- =============================================
CREATE PROCEDURE [dbo].[Users_Delete]

@Id int

AS

/* 
declare
@Id int = 6,


execute [dbo].[Users_Delete]
@Id,

execute [dbo].[Users_SelectAll]
*/

BEGIN

UPDATE [dbo].[Users]
   SET [IsDeleted] = 1
 WHERE @Id = Id

END
GO
