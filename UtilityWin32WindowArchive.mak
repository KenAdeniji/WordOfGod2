all: UtilityWin32Window.exe RSSFeedPage.aspx.cs.dll RSSFeedRepeaterPage.aspx.cs.dll RSSSyndicationFeedListPage.aspx.cs.dll RSSSyndicationFeedNewsItemPage.aspx.cs.dll RSSSyndicationFeedNewsItemSinglePage.aspx.cs.dll

UtilityWin32Window.exe: UtilityWin32Window.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWin32Window.xml /main:WordEngineering.UtilityWin32Window /out:UtilityWin32Window.exe /target:exe /unsafe UtilityWin32Window.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation