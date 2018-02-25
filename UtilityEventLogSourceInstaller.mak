all: bin\UtilityEventLogSourceInstaller.dll
 
bin\UtilityEventLogSourceInstaller.dll: UtilityCommandLineArgument.cs UtilityEventLogSourceInstaller.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityEventLogSourceInstallerDocumentation.xml /out:bin\UtilityEventLogSourceInstaller.dll /target:library /unsafe UtilityCommandLineArgument.cs UtilityEventLogSourceInstaller.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 