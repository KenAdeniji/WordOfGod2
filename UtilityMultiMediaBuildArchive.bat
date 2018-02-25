@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityMultiMedia.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation