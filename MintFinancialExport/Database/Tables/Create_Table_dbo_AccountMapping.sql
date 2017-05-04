CREATE TABLE [dbo].[AccountMapping]
(
	AccountMappingID INT IDENTITY NOT NULL PRIMARY KEY,
	AccountID INT FOREIGN KEY REFERENCES Account(AccountID),
	AccountTypeID INT FOREIGN KEY REFERENCES AccountType(AccountTypeID)
)
