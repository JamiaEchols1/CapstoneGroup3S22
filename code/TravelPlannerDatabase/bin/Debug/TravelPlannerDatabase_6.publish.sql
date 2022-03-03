﻿/*
Deployment script for TravelPlannerDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TravelPlannerDatabase"
:setvar DefaultFilePrefix "TravelPlannerDatabase"
:setvar DefaultDataPath "C:\Users\benev\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\benev\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key cdc004a7-0406-42e7-8295-e9bab9049ef7 is skipped, element [dbo].[Trip] (SqlTable) will not be renamed to [Trips]';


GO
PRINT N'Rename refactoring operation with key 5b316c40-3e6b-4f1b-b4ca-d7a703a76893 is skipped, element [dbo].[User] (SqlTable) will not be renamed to [Users]';


GO
PRINT N'The following operation was generated from a refactoring log file 867f997e-fc61-49dc-89f5-0956629e68f1';

PRINT N'Rename [dbo].[Lodging] to Lodgings';


GO
EXECUTE sp_rename @objname = N'[dbo].[Lodging]', @newname = N'Lodgings', @objtype = N'OBJECT';


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'cdc004a7-0406-42e7-8295-e9bab9049ef7')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('cdc004a7-0406-42e7-8295-e9bab9049ef7')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '5b316c40-3e6b-4f1b-b4ca-d7a703a76893')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('5b316c40-3e6b-4f1b-b4ca-d7a703a76893')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '867f997e-fc61-49dc-89f5-0956629e68f1')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('867f997e-fc61-49dc-89f5-0956629e68f1')

GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO

GO
PRINT N'Update complete.';


GO