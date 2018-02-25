@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityManagement.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation