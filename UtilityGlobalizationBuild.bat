@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityGlobalization.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation