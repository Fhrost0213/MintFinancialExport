CREATE TABLE [dbo].[AccountHistory]
(
	AccountHistoryID INT IDENTITY NOT NULL PRIMARY KEY,
	AccountID INT FOREIGN KEY REFERENCES Account(AccountID),
	Amount money,
	AsOfDate DATETIME,
	RunID INT
)