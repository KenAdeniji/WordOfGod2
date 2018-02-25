all: UtilityRSS.exe RSSFeedPage.aspx.cs.dll RSSFeedRepeaterPage.aspx.cs.dll RSSSyndicationFeedListPage.aspx.cs.dll RSSSyndicationFeedNewsItemPage.aspx.cs.dll RSSSyndicationFeedNewsItemSinglePage.aspx.cs.dll

UtilityRSS.exe: UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityRSSDocumentation.xml /main:WordEngineering.UtilityRSS /out:UtilityRSS.exe /target:exe UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

RSSFeedPage.aspx.cs.dll: RSSFeedPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\RSSFeedPageDocumentation.xml /out:bin\RSSFeedPage.aspx.cs.dll /target:library RSSFeedPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

RSSFeedRepeaterPage.aspx.cs.dll: RSSFeedRepeaterPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\RSSFeedRepeaterPageDocumentation.xml /out:bin\RSSFeedRepeaterPage.aspx.cs.dll /target:library RSSFeedRepeaterPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

RSSSyndicationFeedListPage.aspx.cs.dll: RSSSyndicationFeedListPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\RSSSyndicationFeedListPageDocumentation.xml /out:bin\RSSSyndicationFeedListPage.aspx.cs.dll /target:library RSSSyndicationFeedListPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

RSSSyndicationFeedNewsItemPage.aspx.cs.dll: RSSSyndicationFeedNewsItemPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\RSSSyndicationFeedNewsItemPageDocumentation.xml /out:bin\RSSSyndicationFeedNewsItemPage.aspx.cs.dll /target:library RSSSyndicationFeedNewsItemPage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

RSSSyndicationFeedNewsItemSinglePage.aspx.cs.dll: RSSSyndicationFeedNewsItemSinglePage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\RSSSyndicationFeedNewsItemSinglePageDocumentation.xml /out:bin\RSSSyndicationFeedNewsItemSinglePage.aspx.cs.dll /target:library RSSSyndicationFeedNewsItemSinglePage.aspx.cs UtilityRSS.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation