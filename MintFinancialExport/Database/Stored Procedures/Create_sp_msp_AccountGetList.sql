IF OBJECT_ID ( 'msp_AccountGetList', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountGetList;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountGetList]

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountID], [AccountName]
	FROM [dbo].[Account]
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO