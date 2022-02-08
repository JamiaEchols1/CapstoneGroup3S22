CREATE TABLE [dbo].[Trip] (
	[Id] int not null Primary key,
	[Name] varchar(50) not null,
	[StartDate] date not null,
	[EndDate] date not null,
	[UserId] int not null,
	CONSTRAINT [FK_dbo.Trip_dbo.User_Id] FOREIGN KEY ([UserId]) 
        REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
	)