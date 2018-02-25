@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityMedia.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation