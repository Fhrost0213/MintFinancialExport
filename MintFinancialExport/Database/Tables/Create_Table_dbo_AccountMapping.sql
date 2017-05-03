CREATE TABLE [dbo].[AccountMapping]
(
	AccountID INT FOREIGN KEY REFERENCES Account(AccountID),
	AccountTypeID INT FOREIGN KEY REFERENCES AccountType(AccountTypeID)
)
