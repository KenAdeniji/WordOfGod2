all: SoundPlayerPage.aspx.cs.dll UtilitySoundPlayer.exe

UtilitySoundPlayer.exe: UtilitySoundPlayer.cs
 csc /define:DEBUG /debug:full /main:WordEngineering.UtilitySoundPlayer /out:UtilitySoundPlayer.exe /target:exe /unsafe UtilitySoundPlayer.cs

SoundPlayerPage.aspx.cs.dll: bin\Ajax.dll SoundPlayerPage.aspx.cs UtilitySoundPlayer.cs
 csc /define:DEBUG /debug:full /out:bin\SoundPlayerPage.aspx.cs.dll /reference:bin\Ajax.dll /target:library SoundPlayerPage.aspx.cs UtilitySoundPlayer.cs
  
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation