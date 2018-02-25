all: UtilityMessageQueue.exe

UtilityMessageQueue.exe: UtilityMessageQueue.cs
 csc /define:DEBUG /debug:full /main:WordEngineering.UtilityMessageQueue /out:UtilityMessageQueue.exe /target:exe /unsafe UtilityMessageQueue.cs
  
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation