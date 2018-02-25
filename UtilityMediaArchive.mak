all: SteveOrrNetAudioControl.dll SteveOrrNetMediaControl.dll

SteveOrrNetAudioControl.ascx.cs.dll: SteveOrrNetAudioControl.ascx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SteveOrrNetAudioControlDocumentation.xml /out:bin\SteveOrrNetAudioControl.ascx.cs.dll /target:library /unsafe SteveOrrNetAudioControl.ascx.cs

SteveOrrNetAudioControl.dll: SteveOrrNetAudioControl.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SteveOrrNetAudioControlDocumentation.xml /out:bin\SteveOrrNetAudioControl.dll /target:library /unsafe SteveOrrNetAudioControl.cs

SteveOrrNetMediaControl.ascx.cs.dll: SteveOrrNetMediaControl.ascx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SteveOrrNetMediaControlDocumentation.xml /out:bin\SteveOrrNetMediaControl.ascx.cs.dll /target:library /unsafe SteveOrrNetMediaControl.ascx.cs

SteveOrrNetMediaControl.dll: SteveOrrNetMediaControl.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SteveOrrNetMediaControlDocumentation.xml /out:bin\SteveOrrNetMediaControl.dll /target:library /unsafe SteveOrrNetMediaControl.cs

MediaPlayerPage.aspx.cs.dll: MediaPlayerPage.aspx.cs bin\SteveOrrNetMediaControl.ascx.cs.dll
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\MediaPlayerPageDocumentation.xml /out:bin\MediaPlayerPage.aspx.cs.dll /reference:bin\SteveOrrNetMediaControl.ascx.cs.dll /target:library /unsafe MediaPlayerPage.aspx.cs 

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation