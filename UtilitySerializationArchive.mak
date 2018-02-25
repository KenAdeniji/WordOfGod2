all: UtilitySerialization.exe

UtilitySerialization.exe: UtilitySerialization.cs BibleBookTitle.cs ScriptureReference.cs ScriptureReferenceBookChapterVersePrePost.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs TheWord.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySerialization.xml /main:WordEngineering.UtilitySerialization /out:UtilitySerialization.exe /target:exe /unsafe UtilitySerialization.cs BibleBookTitle.cs ScriptureReference.cs ScriptureReferenceBookChapterVersePrePost.cs UtilityClass.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityXml.cs TheWord.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation