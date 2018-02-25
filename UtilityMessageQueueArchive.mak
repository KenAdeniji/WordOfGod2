all: UtilityMessageQueue.exe

UtilityMessageQueue.exe: UtilityMessageQueue.cs UtilityClass.cs UtilityCollection.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityMessageQueue.xml /main:WordEngineering.UtilityMessageQueue /out:UtilityMessageQueue.exe /target:exe /unsafe UtilityMessageQueue.cs UtilityClass.cs UtilityCollection.cs Contact.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs UtilityXml.cs
  
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation