@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityUser.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation