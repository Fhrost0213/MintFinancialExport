CREATE TABLE [dbo].[Account]
(
	ObjectID INT IDENTITY NOT NULL PRIMARY KEY,
	AccountName VARCHAR(255) NOT NULL,
	IsManual BIT
)