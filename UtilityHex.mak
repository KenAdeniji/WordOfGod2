all: UtilityHex.exe

UtilityHex.exe: UtilityHex.cs
 csc /debug:full /main:WordEngineering.UtilityHex /out:UtilityHex.exe /target:exe UtilityHex.cs
