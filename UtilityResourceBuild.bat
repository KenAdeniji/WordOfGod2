@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityResource.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation