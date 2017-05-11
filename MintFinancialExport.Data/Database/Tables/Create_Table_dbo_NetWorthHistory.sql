CREATE TABLE [dbo].[NetWorthHistory]
(
	ObjectID INT IDENTITY NOT NULL PRIMARY KEY,
	NetWorthAmount money,
	AssetAmount money,
	DebtAmount money,
	AsOfDate DATETIME,
	RunID INT
)