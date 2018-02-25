@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityASCII.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation