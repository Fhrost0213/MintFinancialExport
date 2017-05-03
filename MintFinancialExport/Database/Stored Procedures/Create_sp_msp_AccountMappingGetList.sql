IF OBJECT_ID ( 'msp_AccountMappingGetList', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountMappingGetList;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountMappingGetList]

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountID], [AccountTypeID]
	FROM [dbo].[AccountMapping]
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO