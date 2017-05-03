IF OBJECT_ID ( 'msp_AccountTypeGet', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountTypeGet;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountTypeGet]
	AccountTypeID INT

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountTypeID], [AccountTypeName]
	FROM [dbo].[AccountType]
	WHERE [AccountTypeID] = @AccountTypeID
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO