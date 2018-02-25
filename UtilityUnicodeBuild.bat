@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityUnicode.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation