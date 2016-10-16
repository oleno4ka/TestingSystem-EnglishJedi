USE EnglishJedi;

GO

INSERT INTO AppUser(UserLogin, UserPassword,FirstName, LastName)
VALUES
         ('Admin', '202cb962ac59075b964b07152d234b70','Olena','M'),
         ('Ura', '202cb962ac59075b964b07152d234b70', 'Ura','Maluga'),
		 ('Ivanko','96e79218965eb72c92a549dd5a330112','Ivan','A');
        
GO
INSERT INTO QuestionTheme(Theme)
VALUES
         ('Grammar'),
         ('Tenses'),
		 ('Vocabulary');        
GO
INSERT INTO TestLevel(TestLevel)
VALUES
         ('Elementary'),
         ('Intermediate'),
		 ('Advance');        
GO
INSERT INTO Test(Name,TestDescription,Duration,LevelId,PassValue)
VALUES
          ('Noun','Find noun in sentence',  '00:09' ,1,50),
		  ('Adjectives','Choode the right adjective', '00:07',1,60),
          ('Preposition','Fill the blank with the Appropriate preposition', '00:10',1,30),
		  ('Tenses','Fill in the blanks with Appropriate Tenses','00:05',2,50);

GO
INSERT INTO Question(TestId,Question,ThemeId)
VALUES    ( 1,'The car moved fast.',1), 
	      ( 1,'The works of many great poets have been placed on reserve.',1), 
	      ( 1,'The man was tall.' ,1), 
	      ( 1,'The girl was unhappy.',1), 
	      ( 1,'The doctor worked fast.',1), 
	      ( 2,'Mary likes ______ grandmother. She often visits her.',1), 
	      ( 2,'He did not pass the course as _______ as he thought he would.',1), 
	      ( 2,'I went to school this morning, but I didn`t learn anything. It was just _____.',1) , 
	      ( 2,'The weather this summer is even ______ than last summer.',1), 
	      ( 3,'There is a museum ___ the school.' ,1), 
	      ( 3,'Mary sometimes sits ___ John and Jill.',1),
		  (4,'Even if it rains I shall come - means',2),
		  (4,'She never visits any zoo because she is strong opponent of the idea of',2),
		  (4,'That is __ interesting book',2),
		  (4,'Despite _______ hard, he failed the exam',2),
		  (4,'What ___ he like? - He is very friendly.',2);
	      
 
-----------------------------------
GO
DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =5,

    @pAnswer  = 'doctor', 
	@pIsRight = 1,

    @pAnswer2 = 'The', 
	@pIsRight2 =0,
	
    @pAnswer3  = 'worked', 
	@pIsRight3 =0,

    @pAnswer4 = 'fast', 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO
	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =4,

    @pAnswer  = 'girl', 
	@pIsRight = 1,

    @pAnswer2 ='the' , 
	@pIsRight2 =0,
	
    @pAnswer3  = 'happy', 
	@pIsRight3 =0,

    @pAnswer4 ='was' , 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO
	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =3,

    @pAnswer  = 'Man', 
	@pIsRight = 1,

    @pAnswer2 = 'was', 
	@pIsRight2 =0,
	
    @pAnswer3  = 'The', 
	@pIsRight3 =0,

    @pAnswer4 = 'tall', 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO
	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =1,

    @pAnswer  = 'mooved', 
	@pIsRight = 0,

    @pAnswer2 = 'The', 
	@pIsRight2 =0,
	
    @pAnswer3  = 'fast', 
	@pIsRight3 =0,

    @pAnswer4 = 'car', 
	@pIsRight4 =1,
	@responseMessage=@responseMessage OUTPUT
	GO
	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =2,

    @pAnswer  = 'works', 
	@pIsRight =0,

    @pAnswer2 = 'great', 
	@pIsRight2 =0,
	
    @pAnswer3  = 'reserve', 
	@pIsRight3 =1,

    @pAnswer4 = 'many', 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO
	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =10,

    @pAnswer  = 'on', 
	@pIsRight = 0,

    @pAnswer2 = 'near', 
	@pIsRight2 =1,
	
    @pAnswer3  = 'under', 
	@pIsRight3 =0,

    @pAnswer4 = 'above', 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO
	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =11,

    @pAnswer  = 'between', 
	@pIsRight = 1,

    @pAnswer2 = 'on', 
	@pIsRight2 =0,
	
    @pAnswer3  = 'behind', 
	@pIsRight3 =0,

    @pAnswer4 = 'in', 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
GO
DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =12,

    @pAnswer  = 'if I come it will not rain', 
	@pIsRight = 0,

    @pAnswer2 ='if it rains I shall not come' , 
	@pIsRight2 =0,
	
    @pAnswer3  = 'I will come whether it rains or not', 
	@pIsRight3 =1,

    @pAnswer4 ='whenever it rains I shall come' , 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO

	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =13,

    @pAnswer  = 'feeding animals when everybody is looking', 
	@pIsRight = 0,

    @pAnswer2 ='watching animals in there natural conditions' , 
	@pIsRight2 =0,
	
    @pAnswer3  = 'keeping animals in cages', 
	@pIsRight3 =1,

    @pAnswer4 ='going outside the house' , 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO

DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =14,

    @pAnswer  = 'the', 
	@pIsRight = 0,

    @pAnswer2 ='a' , 
	@pIsRight2 =0,
	
    @pAnswer3  = 'an', 
	@pIsRight3 =1,

    @pAnswer4 ='none' , 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO

	DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =15,

    @pAnswer  = 'he studied', 
	@pIsRight = 0,

    @pAnswer2 ='he has studied' , 
	@pIsRight2 =0,
	
    @pAnswer3  = 'studying', 
	@pIsRight3 =1,

    @pAnswer4 ='study' , 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO

		DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.uspAdd4AnswerVariants 
    @pQuestionId =16,

    @pAnswer  = 'does', 
	@pIsRight = 0,

    @pAnswer2 ='did' , 
	@pIsRight2 =0,
	
    @pAnswer3  = 'is', 
	@pIsRight3 =1,

    @pAnswer4 ='has' , 
	@pIsRight4 =0,
	@responseMessage=@responseMessage OUTPUT
	GO

	INSERT [dbo].[MarkAnswer] ( [MarkId], [AnswerId]) VALUES ( 1, 23), ( 1, 26), ( 1, 19),( 1, 14),( 1, 8), ( 2, 8),
( 2, 12), ( 2, 16),( 2, 23),( 2, 26), ( 3, 23), ( 3, 16), ( 3, 13),( 3, 8), ( 3, 25),( 4, 29), ( 4, 32), ( 5, 30),( 5, 32),
( 6, 31), ( 6, 33),( 10, 21), ( 10, 26),( 10, 17), ( 10, 12),( 10, 11),( 11, 29),( 11, 32),( 18, 28), ( 18, 34),( 19, 29),
 ( 19, 32),( 20, 23),( 20, 24),( 20, 16),( 20, 12),( 20, 9), ( 21, 23),( 21, 26),( 21, 16), ( 21, 12), ( 21, 9),( 22, 29),
  ( 23, 29), ( 23, 32),( 24, 21),( 24, 26),( 24, 17),( 24, 14), ( 24, 9), ( 26, 29),( 26, 32),( 27, 23),( 27, 26),( 27, 16),
  ( 27, 12),( 27, 11),( 28, 38),( 28, 42), ( 28, 44),( 28, 50), ( 28, 54), ( 29, 37), (29, 41), ( 30, 38), ( 30, 42),( 30, 45), ( 30, 51), ( 30, 54)