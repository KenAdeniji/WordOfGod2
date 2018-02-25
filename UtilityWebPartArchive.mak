all: DisplayModeMenuUserControl.ascx.cs.dll SearchUserControl.ascx.cs.dll

DisplayModeMenuUserControl.ascx.cs.dll: DisplayModeMenuUserControl.ascx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\DisplayModeMenuUserControlDocumentation.xml /out:bin\DisplayModeMenuUserControl.ascx.cs.dll /target:library /unsafe DisplayModeMenuUserControl.ascx.cs

SearchUserControl.ascx.cs.dll: SearchUserControl.ascx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SearchUserControlDocumentation.xml /out:bin\SearchUserControl.ascx.cs.dll /target:library /unsafe SearchUserControl.ascx.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation