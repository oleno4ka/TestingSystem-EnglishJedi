CREATE PROCEDURE [dbo].[spGetAllTests] AS
	SELECT Id,UserLogin,UserPassword FROM [dbo].AppUser
-------------------------------------------------------
CREATE PROCEDURE [dbo].[spGetAllQuestionToTest] 
    @testId int 
	AS
    SELECT Question.Id,Question  
FROM [dbo].Question
RIGHT JOIN   [dbo].Test ON Question.TestId = Test.Id
WHERE Question.TestId = @testId; 
-------------------------------------------------------
CREATE PROCEDURE [dbo].[spGetAllAnswersOnQuestion] 
    @questionId int 
	AS
    SELECT Answer.Answer  
FROM [dbo].Answer
RIGHT JOIN   [dbo].Question ON Question.Id = Answer.QuestionId
WHERE Answer.QuestionId = @questionId; 
-------------------------------------------------------
USE [EnglishGrammar]
GO
EXEC [dbo].[spGetAllAnswersOnQuestion] 3;