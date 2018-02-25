@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityResponse.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation