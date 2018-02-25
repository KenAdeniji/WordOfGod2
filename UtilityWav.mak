all: bin\UtilityWav.dll UtilityWav.exe
 
bin\UtilityWav.dll: UtilityWav.cs UtilityCommandLineArgument.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWavDocumentation.xml /out:bin\UtilityWav.dll /target:library UtilityWav.cs UtilityCommandLineArgument.cs

UtilityWav.exe: UtilityWav.cs UtilityCommandLineArgument.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityWavDocumentation.xml /main:WordEngineering.UtilityWav /out:UtilityWav.exe /target:exe UtilityWav.cs UtilityCommandLineArgument.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 