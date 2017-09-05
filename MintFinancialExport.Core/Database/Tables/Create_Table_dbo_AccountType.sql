CREATE TABLE [dbo].[AccountType]
(
	ObjectID INT NOT NULL PRIMARY KEY,
	AccountTypeName VARCHAR(255) NOT NULL,
	AccountTypeDesc VARCHAR(255),
	IsAsset BIT
)

INSERT INTO [dbo].[AccountType]
SELECT 1, 'Cash', 'Cash', 1
INSERT INTO [dbo].[AccountType]
SELECT 2, 'TradRetirement', 'Traditional Retirement Account', 1
INSERT INTO [dbo].[AccountType]
SELECT 3, 'RothRetirement', 'Roth Retirement Account', 1
INSERT INTO [dbo].[AccountType]
SELECT 4, 'HSA', 'Health Savings Account', 1
INSERT INTO [dbo].[AccountType]
SELECT 5, 'Taxable', 'Taxable Account', 1
INSERT INTO [dbo].[AccountType]
SELECT 6, 'RealEstate', 'Real Estate', 1
INSERT INTO [dbo].[AccountType]
SELECT 7, 'Physical', 'Physical Asset', 1
INSERT INTO [dbo].[AccountType]
SELECT 8, 'Automobiles', 'Automobiles', 1
INSERT INTO [dbo].[AccountType]
SELECT 9, 'CreditCards', 'Credit Cards', 0
INSERT INTO [dbo].[AccountType]
SELECT 10, 'StudentLoans', 'Student Loans', 0
INSERT INTO [dbo].[AccountType]
SELECT 11, 'Mortgages', 'Mortgages', 0
INSERT INTO [dbo].[AccountType]
SELECT 99, 'None', 'None', 0