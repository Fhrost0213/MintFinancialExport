IF OBJECT_ID ( 'msp_AccountMappingGet', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountMappingGet;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountMappingGet]
	AccountID INT

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountID], [AccountTypeID]
	FROM [dbo].[AccountMapping]
	WHERE [AccountID] = @AccountID
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO