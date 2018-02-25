all: bin\UserPage.aspx.cs.dll UtilityUser.exe
 
bin\UserPage.aspx.cs.dll: UserPage.aspx.cs UtilityCollection.cs UtilityClass.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityUser.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UserPageDocumentation.xml /out:bin\UserPage.aspx.cs.dll /target:library UserPage.aspx.cs UtilityCollection.cs UtilityClass.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityUser.cs UtilityXml.cs

UtilityUser.exe: UtilityCollection.cs UtilityClass.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityUser.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityUserDocumentation.xml /main:WordEngineering.UtilityUser /out:UtilityUser.exe /target:exe UtilityCollection.cs UtilityClass.cs UtilityCommandLineArgument.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityUser.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 