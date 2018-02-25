all: UtilityFtp.exe

UtilityFtp.exe: UtilityFtp.cs CommandLineArguments.cs
 csc /define:DEBUG /main:WordEngineering.UtilityFtp /out:UtilityFtp.exe /target:exe UtilityFtp.cs CommandLineArguments.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation