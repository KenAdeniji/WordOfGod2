all: bin\OWCTypeLib.dll bin\OfficeWebComponentOWCPiePage.aspx.cs.dll bin\OfficeWebComponentOWCColumnPage.aspx.cs.dll

bin\OWCTypeLib.dll: "C:\Program Files\Common Files\Microsoft Shared\Web Components\11\OWC11.DLL"
 TLBIMP "C:\Program Files\Common Files\Microsoft Shared\Web Components\11\OWC11.DLL" /Out:bin\OWCTypeLib.dll
 ILDASM bin\OWCTypeLib.dll

bin\OfficeWebComponentOWCPiePage.aspx.cs.dll: bin\OWCTypeLib.dll OfficeWebComponentOWCPiePage.aspx.cs 
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\OfficeWebComponentOWCPiePageDocumentation.xml /out:bin\OfficeWebComponentOWCPiePage.aspx.cs.dll /reference:bin\OWCTypeLib.dll /target:library /unsafe OfficeWebComponentOWCPiePage.aspx.cs

bin\OfficeWebComponentOWCColumnPage.aspx.cs.dll: bin\OWCTypeLib.dll OfficeWebComponentOWCColumnPage.aspx.cs 
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\OfficeWebComponentOWCColumnPageDocumentation.xml /out:bin\OfficeWebComponentOWCColumnPage.aspx.cs.dll /reference:bin\OWCTypeLib.dll /target:library /unsafe OfficeWebComponentOWCColumnPage.aspx.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation