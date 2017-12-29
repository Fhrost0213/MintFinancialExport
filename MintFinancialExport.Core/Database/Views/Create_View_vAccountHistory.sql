CREATE VIEW [dbo].[vAccountHistory]
AS

SELECT 
	[A].[AccountName] ,
	[Amount],
	[IsManual],
	[AccountTypeDesc],
	[IsAsset],
	[AsOfDate],
	[RunID]
FROM [dbo].[AccountHistory] AH
INNER JOIN [dbo].[Account] A
ON [AH].[AccountID] = [A].[ObjectID]
INNER JOIN [dbo].[AccountMapping] AM
ON [A].[ObjectID] = [AM].[AccountID]
INNER JOIN [dbo].[AccountType] AT
ON [AM].[AccountTypeID] = [AT].[ObjectID]