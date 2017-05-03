IF OBJECT_ID ( 'msp_AccountHistoryGetList', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountHistoryGetList;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountHistoryGetList]

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountID], [Amount], [AsOfDate]
	FROM [dbo].[AccountHistory]
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO