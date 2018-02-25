@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityFax.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation