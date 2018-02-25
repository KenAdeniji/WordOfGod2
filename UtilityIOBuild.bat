@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityIO.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation