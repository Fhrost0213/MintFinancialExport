CREATE TABLE [dbo].[NetWorthHistory]
(
	ObjectID INT IDENTITY NOT NULL PRIMARY KEY,
	Amount money,
	AsOfDate DATETIME,
	RunID INT
)