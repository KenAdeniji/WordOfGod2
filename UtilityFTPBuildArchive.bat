@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityFTP.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation