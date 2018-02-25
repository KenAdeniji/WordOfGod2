all: GoogleSearchService.cs GoogleAdvancedSearchPage.aspx.cs.dll UtilityGoogle.exe

GoogleSearchService.cs: GoogleSearch.wsdl
 WSDL /language:cs /namespace:WordEngineering GoogleSearch.wsdl
 
GoogleAdvancedSearchPage.aspx.cs.dll: GoogleAdvancedSearchPage.aspx.cs GoogleSearchService.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\GoogleAdvancedSearchPageDocumentation.xml /out:bin\GoogleAdvancedSearchPage.aspx.cs.dll /target:library GoogleAdvancedSearchPage.aspx.cs GoogleSearchService.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs

UtilityGoogle.exe: GoogleSearchService.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityGoogleDocumentation.xml /main:WordEngineering.UtilityGoogle /out:UtilityGoogle.exe /target:exe GoogleSearchService.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs
 
Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation 