all: WindowClosePage.aspx.cs.dll

WindowClosePage.aspx.cs.dll: WindowClosePage.aspx.cs
 csc /doc:XmlDocumentation\WindowClosePageFormDocumentation.xml /out:bin\WindowClosePage.aspx.cs.dll /target:library WindowClosePage.aspx.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation