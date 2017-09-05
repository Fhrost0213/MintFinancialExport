CREATE TABLE [dbo].[PreciousMetalsHistory]
(
	ObjectID INT IDENTITY NOT NULL PRIMARY KEY,
	AccountID INT FOREIGN KEY REFERENCES Account(ObjectID),
	GoldSpotPrice money,
	SilverSpotPrice money,
	PlatinumSpotPrice money,
	PalladiumSpotPrice money,
	GoldOunces int,
	SilverOunces int,
	PlatinumOunces int,
	PalladiumOunces int,
	AsOfDate DATETIME,
	RunID INT
)