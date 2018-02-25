all: UtilityNumber.exe

UtilityNumber.exe: UtilityNumber.cs
 csc /debug:full /main:WordEngineering.UtilityNumber /out:UtilityNumber.exe /target:exe UtilityNumber.cs
