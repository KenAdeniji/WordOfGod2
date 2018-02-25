@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityURI.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation