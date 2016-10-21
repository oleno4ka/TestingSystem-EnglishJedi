USE EnglishJedi;
GO

--Review - Oleg Shandra: Error during script execution because it does not consider foreign keys in tables.
--It is necessary to drop the tables in reverse order of their creation.
DROP TABLE Answer;
DROP TABLE Question;
DROP TABLE Mark;
DROP TABLE Test;
DROP TABLE AppUser;


