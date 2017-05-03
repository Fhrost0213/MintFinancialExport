CREATE TABLE [dbo].[AccountHistory]
(
	AccountID INT FOREIGN KEY REFERENCES Account(AccountID),
	Amount money,
	AsOfDate DATE
)