-- uspAlphabetSequence

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[uspAlphabetSequence]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[uspAlphabetSequence]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE PROCEDURE uspAlphabetSequence
 @word                               VARCHAR(600)  OUTPUT,
 @alphabetSequence                   INT           OUTPUT,
 @scriptureReferenceVerseForward     VARCHAR(600)  OUTPUT,
 @scriptureReferenceChapterForward   VARCHAR(600)  OUTPUT,
 @scriptureReferenceChapterBackward  VARCHAR(600)  OUTPUT,
 @scriptureReferenceVerseBackward    VARCHAR(600)  OUTPUT,
 @scriptureReference                 VARCHAR(600)  OUTPUT,
 @verseForward                       VARCHAR(600)  OUTPUT,
 @chapterForward                     VARCHAR(600)  OUTPUT,
 @chapterBackward                    VARCHAR(600)  OUTPUT,
 @verseBackward                      VARCHAR(600)  OUTPUT
AS 
BEGIN  -- Stored Procedure Begin

/*
 DECLARE
 @alphabetSequence                   INT,
 @scriptureReferenceVerseForward     VARCHAR(600),
 @scriptureReferenceChapterForward   VARCHAR(600),
 @scriptureReferenceChapterBackward  VARCHAR(600),
 @scriptureReferenceVerseBackward    VARCHAR(600),
 @scriptureReference                 VARCHAR(600),
 @word                               VARCHAR(600),
 @chapterBackward                    VARCHAR(600),
 @chapterForward                     VARCHAR(600),
 @verseBackward                      VARCHAR(600),
 @verseForward                       VARCHAR(600)

 SELECT
 @word                               = NULL,
 @alphabetSequence                   = 117,
 @scriptureReferenceVerseForward     = NULL,
 @scriptureReferenceChapterForward   = NULL,
 @scriptureReferenceChapterBackward  = NULL,
 @scriptureReferenceVerseBackward    = NULL,
 @scriptureReference                 = NULL,
 @verseForward                       = NULL,
 @chapterForward                     = NULL,
 @chapterBackward                    = NULL,
 @verseBackward                      = NULL

 EXEC uspAlphabetSequence
 @word                               OUTPUT,
 @alphabetSequence                   OUTPUT,
 @scriptureReferenceVerseForward     OUTPUT,
 @scriptureReferenceChapterForward   OUTPUT,
 @scriptureReferenceChapterBackward  OUTPUT,
 @scriptureReferenceVerseBackward    OUTPUT,
 @scriptureReference                 OUTPUT,
 @verseForward                       OUTPUT,
 @chapterForward                     OUTPUT,
 @chapterBackward                    OUTPUT,
 @verseBackward                      OUTPUT

 PRINT @scriptureReferenceVerseForward
 PRINT @scriptureReferenceChapterForward
 PRINT @scriptureReferenceChapterBackward
 PRINT @scriptureReferenceVerseBackward
 PRINT @scriptureReference
 PRINT @verseForward
 PRINT @chapterForward
 PRINT @chapterBackward
 PRINT @verseBackward

*/

/*
And the evening and the morning were the third day (Genesis 1:13).
Genesis 13
Revelation 10
Then saith he unto me, See thou do it not: for I am thy fellowservant, and of thy brethren the prophets, and of them which keep the sayings of this book: worship God (Revelation 22:9).
*/

SET NOCOUNT ON

DECLARE
  @chapterMaximum  INT,
  @verseMaximum    INT

SELECT
  @chapterMaximum = Max( chapterIdSequence ),
  @verseMaximum   = Max( verseIdSequence )
FROM
  KJV ( NOLOCK )

SELECT
  @verseForward = LTRIM( RTRIM( verseTextWithScriptureReference ) ),
  @scriptureReferenceVerseForward = scriptureReferenceWithoutBracket
FROM 
  KJV ( NOLOCK ) 
WHERE 
  verseIdSequence = @alphabetSequence

SELECT
  @chapterForward = LTRIM( RTRIM( bookTitle ) ) + ' ' + LTRIM( RTRIM( CONVERT( VARCHAR, chapterId ) ) ) + '.',
  @scriptureReferenceChapterForward = bookTitle + ' ' + CONVERT( VARCHAR, chapterId )
FROM 
  KJV ( NOLOCK ) 
WHERE 
  chapterIdSequence = @alphabetSequence

SELECT
  @chapterBackward = LTRIM( RTRIM( bookTitle ) ) + ' ' + LTRIM( RTRIM( CONVERT( VARCHAR, chapterId ) ) ) + '.',
  @scriptureReferenceChapterBackward = bookTitle + ' ' + CONVERT( VARCHAR, chapterId )
FROM 
  KJV ( NOLOCK ) 
WHERE 
  chapterIdSequence = @chapterMaximum + 1 - @alphabetSequence

SELECT
  @verseBackward = LTRIM( RTRIM(  verseTextWithScriptureReference ) ),
  @scriptureReferenceVerseBackward = scriptureReferenceWithoutBracket
FROM 
  KJV ( NOLOCK ) 
WHERE 
  verseIdSequence = @verseMaximum + 1 - @alphabetSequence

SELECT  @scriptureReference = 
                              @scriptureReferenceVerseForward    + ', ' +
                              @scriptureReferenceChapterForward  + ', ' +
                              @scriptureReferenceChapterBackward + ', ' +
                              @scriptureReferenceVerseBackward

/*
SELECT
 @word,
 @alphabetSequence,
 @scriptureReference,
 @verseForward,
 @chapterForward,
 @chapterBackward,
 @verseBackward
*/

/*
PRINT @scriptureReference
PRINT @verseForward
PRINT @chapterForward
PRINT @chapterBackward
PRINT @verseBackward
*/

END  -- Stored Procedure End.





GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

