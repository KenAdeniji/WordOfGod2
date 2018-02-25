all: UtilityUnicode.exe UtilityUnicodePage.aspx.cs.dll

UtilityUnicode.exe: UtilityUnicode.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityUnicode.xml /main:WordEngineering.UtilityUnicode /out:UtilityUnicode.exe /target:exe UtilityUnicode.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs

UtilityUnicodePage.aspx.cs.dll: UtilityUnicodePage.aspx.cs UtilityUnicode.cs UtilityClass.cs UtilityCollection.cs UtilityDirectory.cs UtilityDatabase.cs UtilityDebug.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\URIPageDocumentation.xml /out:bin\UtilityUnicodePage.aspx.cs.dll /target:library UtilityUnicodePage.aspx.cs UtilityUnicode.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs  UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation