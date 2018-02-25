all: ServiceTemperature.asmx.cs.dll

ServiceTemperature.asmx.cs.dll: CommandLineArguments.cs ServiceTemperature.asmx.cs UtilityTemperature.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\ServiceTemperatureDocumentation.xml /out:bin\ServiceTemperature.aspx.cs.dll /reference:System.EnterpriseServices.dll /target:library CommandLineArguments.cs ServiceTemperature.asmx.cs UtilityTemperature.cs
 
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation
