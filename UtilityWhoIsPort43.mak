all: bin\WhoIsPort43Page.aspx.cs.dll UtilityWhoIsPort43.exe

bin\WhoIsPort43Page.aspx.cs.dll: UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilityString.cs UtilityWebControl.cs UtilityWhoIsPort43.cs WhoIsPort43Page.aspx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\WhoIsPort43PageDocumentation.xml /out:bin\WhoIsPort43Page.aspx.cs.dll /target:library /unsafe UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilityString.cs UtilityWebControl.cs UtilityWhoIsPort43.cs WhoIsPort43Page.aspx.cs

UtilityWhoIsPort43.exe: UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilityString.cs UtilityWhoIsPort43.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWhoIsPort43Documentation.xml /main:WordEngineering.UtilityWhoIsPort43 /out:UtilityWhoIsPort43.exe /target:exe UtilityCommandLineArgument.cs UtilityEventLog.cs UtilityException.cs UtilityString.cs UtilityWhoIsPort43.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation