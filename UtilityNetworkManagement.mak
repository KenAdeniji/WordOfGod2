all: UtilityNetworkManagement.exe

UtilityNetworkManagement.exe: UtilityNetworkManagement.cs
 csc /debug:full /main:WordEngineering.UtilityNetworkManagement /out:UtilityNetworkManagement.exe /target:exe UtilityNetworkManagement.cs