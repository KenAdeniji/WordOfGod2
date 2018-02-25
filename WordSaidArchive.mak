all: WordSaidXmlLoad.exe WordSaidPage.aspx.cs.dll

WordSaidXmlLoad.exe: WordSaidXmlLoad.cs WordSaidXmlLoad.cs UtilityDateTimeParse.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:WordSaidXmlLoadDocumentation.xml /main:WordEngineering.WordSaidXmlLoad /out:WordSaidXmlLoad.exe /target:exe WordSaidXmlLoad.cs UtilityDateTimeParse.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs

WordSaidPage.aspx.cs.dll: JehovahRophe.cs UtilityDateTimeParse.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs WordSaid.cs WordSaidPage.aspx.cs
 csc /define:DEBUG /debug:full /doc:WordSaidWebFormDocumentation.xml /out:bin\WordSaidPage.aspx.cs.dll /target:library JehovahRophe.cs UtilityDateTimeParse.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs WordSaid.cs WordSaidPage.aspx.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation  