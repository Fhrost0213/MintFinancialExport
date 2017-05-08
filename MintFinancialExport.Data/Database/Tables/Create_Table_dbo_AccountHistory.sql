CREATE TABLE [dbo].[AccountHistory]
(
	ObjectID INT IDENTITY NOT NULL PRIMARY KEY,
	AccountID INT FOREIGN KEY REFERENCES Account(ObjectID),
	Amount money,
	AsOfDate DATETIME,
	RunID INT
)