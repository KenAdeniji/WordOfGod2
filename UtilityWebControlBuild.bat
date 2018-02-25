@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityWebControl.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation