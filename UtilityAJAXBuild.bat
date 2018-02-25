@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityAJAX.mak
DEL *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation