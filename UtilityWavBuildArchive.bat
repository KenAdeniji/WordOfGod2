@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityWav.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation