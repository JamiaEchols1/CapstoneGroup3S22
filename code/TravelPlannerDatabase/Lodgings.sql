CREATE TABLE [dbo].[Lodgings]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TripId] INT NOT NULL, 
    [Location] VARCHAR(250) NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL,
    [Description] VARCHAR(250) NULL, 
    CONSTRAINT [FK_dbo.Lodging_dbo.Trip_Id] FOREIGN KEY ([TripId]) 
        REFERENCES [dbo].[Trips] ([Id]) ON DELETE CASCADE
)
