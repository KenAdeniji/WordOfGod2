all: GoogleSearchProxyClass.cs GoogleAdvancedSearchPage.aspx.cs.dll UtilityGoogle.exe

GoogleSearchProxyClass.cs: GoogleSearch.wsdl
 WSDL /language:cs /namespace:WordEngineering /out:GoogleSearchProxyClass.cs GoogleSearch.wsdl
 
GoogleAdvancedSearchPage.aspx.cs.dll: GoogleAdvancedSearchPage.aspx.cs GoogleSearchProxyClass.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\GoogleAdvancedSearchPageDocumentation.xml /out:bin\GoogleAdvancedSearchPage.aspx.cs.dll /target:library GoogleAdvancedSearchPage.aspx.cs GoogleSearchProxyClass.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs

UtilityGoogle.exe: GoogleSearchProxyClass.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityGoogleDocumentation.xml /main:WordEngineering.UtilityGoogle /out:UtilityGoogle.exe /target:exe GoogleSearchProxyClass.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityGoogle.cs UtilityURI.cs UtilityXml.cs
 
Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation 