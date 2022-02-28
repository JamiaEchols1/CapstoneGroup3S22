﻿CREATE TABLE [dbo].[Lodging]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TripId] INT NOT NULL, 
    [Location] VARCHAR(50) NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL,
    CONSTRAINT [FK_dbo.Lodging_dbo.Trip_Id] FOREIGN KEY ([TripId]) 
        REFERENCES [dbo].[Trip] ([Id]) ON DELETE CASCADE
)
