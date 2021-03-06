CREATE TABLE [dbo].[Waypoints]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Location] varchar(250) not null,
    [StartDateTime] DateTime not null,
    [EndDateTime] DateTime not null,
    [TripId] int not null,
     [Description] VARCHAR(250) NULL, 
    CONSTRAINT [FK_dbo.Waypoint_dbo.Trip_Id] FOREIGN KEY ([TripId]) 
        REFERENCES [dbo].[Trips] ([Id]) ON DELETE CASCADE
)