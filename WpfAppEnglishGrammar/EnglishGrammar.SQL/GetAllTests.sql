CREATE PROCEDURE [dbo].[GetAllTests]
	@param1 varchar,
	@param2 varchar,
	@param3 varchar,
	@param4 varchar
AS
	SELECT @param1, @param2,@param3,@param4 FROM [dbo].AppUser
RETURN 0
