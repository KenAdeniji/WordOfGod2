@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityKernel32.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation