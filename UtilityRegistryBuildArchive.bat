@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityRegistry.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation