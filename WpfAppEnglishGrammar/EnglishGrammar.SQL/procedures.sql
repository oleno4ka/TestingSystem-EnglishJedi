USE EnglishJedi;
GO

--Review - Oleg Shandra: Best practice is to write all the keywords in capital letters

CREATE PROC [dbo].[spGetAllMarkAnswers]  
AS
BEGIN  
   select MarkId, AnswerId from MarkAnswer	
END;

GO

CREATE PROC [dbo].[spGetAllMarksForUser]  
  @Login VARCHAR(30)
AS
BEGIN  
SELECT  Mark.Id as 'MarkId', UserId, TestId, Value
	FROM Mark INNER JOIN AppUser 
	     ON Mark.UserId = AppUser.Id
		 WHERE AppUser.UserLogin = @Login	
END;

GO

CREATE PROC [dbo].[spGetAllTests]  
AS
BEGIN  
   select  Test.Id , Test.Name, Test.TestDescription, Test.Duration, Test.LevelId,PassValue, TestLevel  
		from Test INNER JOIN TestLevel 
		ON Test.LevelId = TestLevel.Id 			
END;
GO

CREATE PROC [dbo].[spGetAllUsersAnswers]  
AS
BEGIN  
   SELECT Id,QuestionId,Answer, IsRight FROM Answer
END;

GO

CREATE PROC [dbo].[spGetAllUsersLogins]  
AS
BEGIN  
   SELECT UserLogin FROM AppUser
END;

GO

CREATE PROC [dbo].[spGetAllUsersMarks]  
AS
BEGIN  
   SELECT UserLogin, Mark.Value ,Mark.Id,TestId
		FROM AppUser RIGHT JOIN Mark
		ON AppUser.Id = Mark.UserId		
END;
GO

CREATE PROC [dbo].[spGetAllUsersQuestions]  
AS
BEGIN  
   select Question.Id, Question,TestId,ThemeId, Theme 
   from Question INNER JOIN QuestionTheme
	     ON ThemeId = QuestionTheme.Id
END;
GO

CREATE PROCEDURE [dbo].[spGetAnswersToQuestion]

AS
BEGIN
SET NOCOUNT ON;	
		 SELECT Answer.Id as 'AnswerId',  QuestionId as 'AnswerQuestionId', Question.Question as 'AnswerQuestion',Answer, IsRight
	     FROM Answer INNER JOIN Question 
	     ON Answer.QuestionId  = Question.Id
END;
GO

CREATE PROCEDURE [dbo].[spGetMarksForCurrentUser]
	@Login VARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;	
		  select MarkId, AnswerId from Mark
		 INNER JOIN MarkAnswer ON
		 MarkId = Mark.Id
		INNER JOIN AppUser ON
          UserId = AppUser.Id
		  where AppUser.UserLogin = @Login
END;
GO

CREATE PROC [dbo].[spGetUserByLogin]
	@Login VARCHAR(30),
	@Password VARCHAR(32)
AS
BEGIN
   SELECT 
		Id, 
		FirstName, 
		LastName, 
		[UserLogin]
		--[Password], 
	FROM dbo.AppUser
	WHERE [UserLogin] = @Login and [UserPassword] =@Password;
END;
-----
GO

CREATE PROC [dbo].[spSetMarkAnswer]
  @MarkId INT,
  @AnswerId INT
AS
BEGIN  
		INSERT INTO MarkAnswer(MarkId,AnswerId)
		VALUES (@MarkId,@AnswerId)
END;

GO

CREATE PROC [dbo].[spSetMarks] 
@UserId INT, 
@TestId INT, 
@Value INT 

AS 
BEGIN 
declare @MyTableVar table(Id int); 

INSERT INTO Mark(UserId,TestId,Value) 
OUTPUT INSERTED.Id 
INTO @MyTableVar 
VALUES (@UserId,@TestId,@Value) 

SELECT Id FROM @MyTableVar 
END;

GO

GO
CREATE PROC [dbo].[spSetNewUser]
  @Login nvarchar(30),
  @Password nvarchar(32),
  @FirstName nvarchar(50),
  @LastName nvarchar(50)
AS
BEGIN  
		INSERT INTO AppUser(FirstName,LastName,UserLogin,UserPassword)
		VALUES (@FirstName,@LastName,@Login,@Password)				
END;

GO


CREATE PROCEDURE [dbo].[uspAdd4AnswerVariants]    
    @pQuestionId INT,

    @pAnswer  NVARCHAR(80), 
	@pIsRight BIT,

    @pAnswer2  NVARCHAR(80), 
	@pIsRight2 BIT,
	
    @pAnswer3  NVARCHAR(80), 
	@pIsRight3 BIT,

    @pAnswer4  NVARCHAR(80), 
	@pIsRight4 BIT,

    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        INSERT INTO dbo.[Answer] (  QuestionId , Answer , IsRight )
        VALUES
		( @pQuestionId,  @pAnswer, @pIsRight ),
		( @pQuestionId,  @pAnswer2, @pIsRight2 ),
		( @pQuestionId,  @pAnswer3, @pIsRight3 ),
		( @pQuestionId,  @pAnswer4, @pIsRight4 )

        SET @responseMessage='Success'
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END
GO
