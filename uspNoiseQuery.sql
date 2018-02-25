-- uspNoiseQuery

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[uspNoiseQuery]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[uspNoiseQuery]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE uspNoiseQuery
 @datedFrom           AS varchar(50)       =  null,
 @datedTo             AS varchar(50)       =  null,
 @commentary          AS varchar(8000)     =  null,
 @noiseId             AS uniqueidentifier  =  null,
 @policeId            AS uniqueidentifier  =  null
AS
BEGIN

 --uspNoiseQuery @datedFrom = '20030101', @datedTo = '20031231'
 --uspNoiseQuery @commentary = '%'

 /*
 CREATE TABLE [dbo].[Noise] (
	[NoiseId] [uniqueidentifier] NOT NULL ,
	[Dated] [datetime] NOT NULL ,
	[Commentary] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[PoliceId] [uniqueidentifier] NULL 
 ) ON [PRIMARY]
 */

 DECLARE  @sql        AS nvarchar(4000)
 DECLARE  @and        AS varchar(5) 
 DECLARE  @from       AS varchar(4000)
 DECLARE  @select     AS varchar(4000)
 DECLARE  @where      AS varchar(8000)
 DECLARE  @orderBy    AS varchar(8000)
 
 SET      @select     =  'SELECT  ' +
                         ' Dated, ' +
                         ' ISNULL(Commentary, "") AS Commentary'
 SET      @and        =  '  AND  '
 SET      @from       =  ' FROM Noise (NOLOCK) '
 SET      @where      =  ' WHERE ' + SPACE(7)
 SET      @orderBy    =  ' ORDER BY Dated '  

 SET @commentary = LTRIM( RTRIM( @commentary ) )
 SET @datedFrom = LTRIM( RTRIM( @datedFrom ) )
 SET @datedTo = LTRIM( RTRIM( @datedTo ) )

 IF @commentary = ''
 BEGIN
  SET @commentary = null
 END

 IF @datedFrom = ''
 BEGIN
  SET @datedFrom = null
 END

 IF @datedTo = ''
 BEGIN
  SET @datedTo = null
 END

 IF @datedFrom <> null
 BEGIN
  SET @where = @where + ' Dated >= ''' + @datedFrom + '''' + @and
 END

 IF @datedTo <> null
 BEGIN
  SET @where = @where + ' Dated <= ''' + @datedTo + '''' + @and
 END

 IF @commentary <> null
 BEGIN
  SET @where = @where + ' commentary Like ''%' + @commentary + '%''' + @and
 END

 IF @noiseId <> null
 BEGIN
  SET @where = @where + ' noiseId = ' + CONVERT( VARCHAR, @noiseId ) + @and
 END

 IF @policeId <> null
 BEGIN
  SET @where = @where + ' policeId = ' + CONVERT( VARCHAR, @policeId ) + @and
 END

 --Print @where

 SET @where = LEFT( @where, LEN(@where) - LEN(@and) )

 SET @sql = @select + @from + @where + @orderBy

 EXECUTE sp_executesql @sql

 --PRINT @sql

END



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

