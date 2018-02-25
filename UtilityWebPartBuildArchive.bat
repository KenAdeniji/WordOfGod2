@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityWebPart.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation