all: bin\ServiceTemperature.asmx.cs.dll

bin\ServiceTemperature.asmx.cs.dll: CommandLineArguments.cs ServiceTemperature.asmx.cs UtilityTemperature.cs
 csc /define:DEBUG /debug:full /out:bin\ServiceTemperature.aspx.cs.dll /reference:System.EnterpriseServices.dll /target:library CommandLineArguments.cs ServiceTemperature.asmx.cs UtilityTemperature.cs