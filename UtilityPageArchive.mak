all: bin\PageStubPage.aspx.cs.dll

bin\PageStubPage.aspx.cs.dll: PageStubPage.aspx.cs 
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\PageStubPageDocumentation.xml /out:bin\PageStubPage.aspx.cs.dll /target:library /unsafe PageStubPage.aspx.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation