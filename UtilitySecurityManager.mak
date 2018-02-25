all: UtilitySecurityManager.exe

UtilitySecurityManager.exe: UtilitySecurityManager.cs
 csc /debug:full /main:WordEngineering.UtilitySecurityManager /out:UtilitySecurityManager.exe /target:exe UtilitySecurityManager.cs