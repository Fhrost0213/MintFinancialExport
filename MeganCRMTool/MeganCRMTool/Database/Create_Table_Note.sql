CREATE TABLE dbo.Note
(
	NoteId INT PRIMARY KEY IDENTITY,
	PersonId INT,
	AddDate DATETIME,
	Info VARCHAR(MAX)
	FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
)