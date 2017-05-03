IF OBJECT_ID ( 'msp_AccountHistoryGet', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountHistoryGet;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountHistoryGet]
	@AsOfDate DATE

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountID], [Amount], [AsOfDate]
	FROM [dbo].[AccountHistory]
	WHERE [AsOfDate] = @AsOfDate
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO