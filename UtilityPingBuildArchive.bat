@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityPing.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation