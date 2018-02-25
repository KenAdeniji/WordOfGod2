all: AJAXMichaelSchwarzPage.aspx.cs.dll

AJAXMichaelSchwarzPage.aspx.cs.dll: AJAXMichaelSchwarzPage.aspx.cs 
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\AJAXMichaelSchwarzPageDocumentation.xml /out:bin\AJAXMichaelSchwarzPage.aspx.cs.dll /reference:bin\Ajax.dll /target:library AJAXMichaelSchwarzPage.aspx.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation