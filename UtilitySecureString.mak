all: UtilitySecureString.exe

UtilitySecureString.exe: UtilitySecureString.cs
 csc /debug:full /main:WordEngineering.UtilitySecureString /out:UtilitySecureString.exe /target:exe UtilitySecureString.cs
