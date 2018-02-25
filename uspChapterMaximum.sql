if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[uspChapterMaximum]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[uspChapterMaximum]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE uspChapterMaximum
AS 
BEGIN  -- Stored Procedure Begin

SET NOCOUNT ON

SELECT
  Max( chapterIdSequence )
FROM
  KJV ( NOLOCK )

END  -- Stored Procedure End.

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

