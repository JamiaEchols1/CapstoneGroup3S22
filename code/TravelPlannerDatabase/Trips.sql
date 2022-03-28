CREATE TABLE [dbo].[Trips] (
	[Id] int not null Primary key,
	[Name] varchar(50) not null,
	[StartDate] date not null,
	[EndDate] date not null,
	[UserId] int not null,
	[Description] VARCHAR(250) NULL, 
    CONSTRAINT [FK_dbo.Trip_dbo.User_Id] FOREIGN KEY ([UserId]) 
        REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
	)