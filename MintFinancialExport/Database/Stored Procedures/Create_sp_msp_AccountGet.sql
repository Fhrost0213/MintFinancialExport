IF OBJECT_ID ( 'msp_AccountGet', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountGet;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountGet]
	@AccountID INT

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountID], [AccountName]
	FROM [dbo].[Account]
	WHERE [AccountID] = @AccountID
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO