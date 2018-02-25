@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityRSS.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation