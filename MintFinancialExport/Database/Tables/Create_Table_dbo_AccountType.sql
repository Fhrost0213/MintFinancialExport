CREATE TABLE [dbo].[AccountType]
(
	AccountTypeID INT NOT NULL PRIMARY KEY,
	AccountTypeName VARCHAR(255) NOT NULL,
	AccountTypeDesc VARCHAR(255)
)

INSERT INTO [dbo].[AccountType]
SELECT 1, 'Cash', 'Cash'
INSERT INTO [dbo].[AccountType]
SELECT 2, 'TradRetirement', 'Traditional Retirement Account'
INSERT INTO [dbo].[AccountType]
SELECT 3, 'RothRetirement', 'Roth Retirement Account'
INSERT INTO [dbo].[AccountType]
SELECT 4, 'HSA', 'Health Savings Account'
INSERT INTO [dbo].[AccountType]
SELECT 5, 'Taxable', 'Taxable Account'
INSERT INTO [dbo].[AccountType]
SELECT 6, 'RealEstate', 'Real Estate'
INSERT INTO [dbo].[AccountType]
SELECT 7, 'Physical', 'Physical Asset'
INSERT INTO [dbo].[AccountType]
SELECT 8, 'Automobiles', 'Automobiles'
INSERT INTO [dbo].[AccountType]
SELECT 9, 'CreditCards', 'Credit Cards'
INSERT INTO [dbo].[AccountType]
SELECT 10, 'StudentLoans', 'Student Loans'
INSERT INTO [dbo].[AccountType]
SELECT 11, 'Mortgages', 'Mortgages'	
INSERT INTO [dbo].[AccountType]
SELECT 99, 'None', 'None'	