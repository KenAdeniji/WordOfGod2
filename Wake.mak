all: WakePage.aspx.cs.dll

WakePage.aspx.cs.dll: WakePage.aspx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\WakePageDocumentation.xml /out:bin\WakePage.aspx.cs.dll /target:library WakePage.aspx.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation