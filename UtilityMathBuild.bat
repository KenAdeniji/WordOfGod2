@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityMath.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation