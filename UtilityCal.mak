all: UtilityCal.exe

UtilityCal.exe: CommandLineArguments.cs UtilityCal.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityCalDocumentation.xml /main:WordEngineering.UtilityCal /out:UtilityCal.exe /target:exe CommandLineArguments.cs UtilityCal.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation
