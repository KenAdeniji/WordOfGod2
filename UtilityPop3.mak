all: bin\Pop3Page.aspx.cs.dll UtilityPop3.exe

bin\Pop3Page.aspx.cs.dll: CommandLineArguments.cs UtilityPop3.cs
 csc /debug:full /out:bin\Pop3Page.aspx.cs.dll /target:library CommandLineArguments.cs UtilityPop3.cs

UtilityPop3.exe: CommandLineArguments.cs UtilityPop3.cs
 csc /debug:full /main:WordEngineering.UtilityPop3 /out:UtilityPop3.exe /target:exe CommandLineArguments.cs UtilityPop3.cs

