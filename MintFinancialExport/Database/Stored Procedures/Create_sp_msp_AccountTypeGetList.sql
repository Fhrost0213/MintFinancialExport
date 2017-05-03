IF OBJECT_ID ( 'msp_AccountTypeGetList', 'P' ) IS NOT NULL   
    DROP PROCEDURE msp_AccountTypeGetList;  
GO  

CREATE PROCEDURE [dbo].[msp_AccountTypeGetList]

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED	

BEGIN TRY
	
	SELECT [AccountTypeID], [AccountTypeName]
	FROM [dbo].[AccountType]
	
END TRY
BEGIN CATCH

	EXEC usp_GetErrorInfo;

END CATCH

GO