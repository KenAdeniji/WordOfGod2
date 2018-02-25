@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityWin32Window.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation