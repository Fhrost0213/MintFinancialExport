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
ON [AH].[ObjectID] = [A].[ObjectID]
INNER JOIN [dbo].[AccountMapping] AM
ON [A].[ObjectID] = [AM].[ObjectID]
INNER JOIN [dbo].[AccountType] AT
ON [AM].[ObjectID] = [AT].[ObjectID]