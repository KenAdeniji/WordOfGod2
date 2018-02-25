all: UtilityClipboard.exe

UtilityClipboard.exe: UtilityClipboard.cs
 csc /debug:full /main:WordEngineering.UtilityClipboard /out:UtilityClipboard.exe /target:exe UtilityClipboard.cs 
