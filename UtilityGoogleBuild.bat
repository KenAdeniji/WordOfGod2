@Echo Off
Md    XmlDocumentation
NMake UtilityGoogle.mak
Del   *.pdb XmlDocumentation /F /S /Q
Rd    XmlDocumentation