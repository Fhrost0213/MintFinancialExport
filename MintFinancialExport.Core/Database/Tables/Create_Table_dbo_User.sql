CREATE TABLE [dbo].[User]
(
	ObjectID INT IDENTITY NOT NULL PRIMARY KEY,
	UserName VARCHAR(255),
	LastUsedDate DATETIME
)