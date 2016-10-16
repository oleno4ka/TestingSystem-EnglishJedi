CREATE DATABASE [EnglishJedi]
GO

USE [EnglishJedi]
GO

CREATE TABLE [dbo].[Answer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Answer] [nvarchar](80) NOT NULL,
	[IsRight] [bit] NOT NULL,
 CONSTRAINT [pk_Answer_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO
CREATE TABLE [dbo].[AppUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserLogin] [nvarchar](30) NOT NULL,
	[UserPassword] [nvarchar](32) NOT NULL,
 CONSTRAINT [pk_AppUser_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
),
 CONSTRAINT [User_Login] UNIQUE NONCLUSTERED 
(
	[UserLogin] ASC
)) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Mark](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[Value] [int] NULL,
 CONSTRAINT [pk_Mark_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO

CREATE TABLE [dbo].[MarkAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MarkId] [int] NOT NULL,
	[AnswerId] [int] NOT NULL,
 CONSTRAINT [pk_MarkAnswer_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO
CREATE TABLE [dbo].[Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[Question] [nvarchar](120) NOT NULL,
	[ThemeId] [int] NOT NULL,
 CONSTRAINT [pk_Question_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO
CREATE TABLE [dbo].[QuestionTheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Theme] [nvarchar](50) NOT NULL,
 CONSTRAINT [pk_QuestionTheme_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[TestDescription] [nvarchar](100) NOT NULL,
	[Duration] [time](7) NOT NULL,
	[LevelId] [int] NOT NULL,
	[PassValue] [int] NULL,
 CONSTRAINT [pk_Test_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO

CREATE TABLE [dbo].[TestLevel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestLevel] [nvarchar](50) NOT NULL,
 CONSTRAINT [pk_TestLevel_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [fk_Answer_QuestionId_Question_Id] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [fk_Answer_QuestionId_Question_Id]
GO
ALTER TABLE [dbo].[Mark]  WITH CHECK ADD  CONSTRAINT [fk_Mark_TestId_Test_Id] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO
ALTER TABLE [dbo].[Mark] CHECK CONSTRAINT [fk_Mark_TestId_Test_Id]
GO
ALTER TABLE [dbo].[Mark]  WITH CHECK ADD  CONSTRAINT [fk_Mark_UserId_AppUser_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Mark] CHECK CONSTRAINT [fk_Mark_UserId_AppUser_Id]
GO
ALTER TABLE [dbo].[MarkAnswer]  WITH CHECK ADD  CONSTRAINT [fk_MarkAnswer_AnswerId_Answer_Id] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[Answer] ([Id])
GO
ALTER TABLE [dbo].[MarkAnswer] CHECK CONSTRAINT [fk_MarkAnswer_AnswerId_Answer_Id]
GO
ALTER TABLE [dbo].[MarkAnswer]  WITH CHECK ADD  CONSTRAINT [fk_MarkAnswer_MarkId_Mark_Id] FOREIGN KEY([MarkId])
REFERENCES [dbo].[Mark] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MarkAnswer] CHECK CONSTRAINT [fk_MarkAnswer_MarkId_Mark_Id]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [fk_Question_TestId_Test_Id] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [fk_Question_TestId_Test_Id]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [fk_Question_ThemeId_QuestionTheme_Id] FOREIGN KEY([ThemeId])
REFERENCES [dbo].[QuestionTheme] ([Id])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [fk_Question_ThemeId_QuestionTheme_Id]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [fk_Test_ThemeId_TestLevel_Id] FOREIGN KEY([LevelId])
REFERENCES [dbo].[TestLevel] ([Id])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [fk_Test_ThemeId_TestLevel_Id]
GO