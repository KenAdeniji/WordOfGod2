-- uspBibleDynamicSQLQuery

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[uspBibleDynamicSQLQuery]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[uspBibleDynamicSQLQuery]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE uspBibleDynamicSQLQuery
 @dynamicSQLQuery  ntext  = null
AS
BEGIN

 /*
  Exec uspBibleDynamicSQLQuery @dynamicSQLQuery = "SELECT 0,  BookId ,  ChapterId ,  VerseId ,  VerseText  FROM  KJV   WHERE   BookTitle  = 'Genesis' AND  ChapterId  = 1 UNION  SELECT 1,  BookId ,  ChapterId ,  VerseId ,  VerseText  FROM  KJV   WHERE   BookTitle  = 'Genesis' AND  ChapterId  = 2 AND  VerseId  = 3  UNION  SELECT 2,  BookId ,  ChapterId ,  VerseId ,  VerseText  FROM  KJV   WHERE   BookTitle  = 'Genesis' AND  ChapterId  = 3 AND  VerseId  BETWEEN 4 AND 5  UNION  SELECT 3, BookId ,  ChapterId ,  VerseId ,  VerseText  FROM  KJV   WHERE  (  BookTitle  =  'Genesis' AND  ChapterId  = 6 AND  VerseId  >= 7 )  OR (  BookTitle  = 'Genesis'  AND  ChapterId  = 7 )  OR (  BookTitle  = 'Genesis' AND  ChapterId  = 8 AND  VerseId  <= 9 )"
 */

 /* 
  When SET NOCOUNT is ON, the count (indicating the number of rows 
  affected by a Transact-SQL statement) is not returned. When SET 
  NOCOUNT is OFF, the count is returned.
 */

 SET NOCOUNT ON

 EXEC sp_executesql @stmt = @dynamicSQLQuery

END  --End Procedure


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

