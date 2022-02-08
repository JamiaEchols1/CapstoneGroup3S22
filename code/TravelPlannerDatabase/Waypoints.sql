CREATE TABLE [dbo].[Waypoints]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Location] varchar(50) not null,
	[Time] time not null,
	[TripId] int not null,
	 CONSTRAINT [FK_dbo.Waypoint_dbo.Trip_Id] FOREIGN KEY ([TripId]) 
        REFERENCES [dbo].[Trip] ([Id]) ON DELETE CASCADE
)