@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityWhoIsPort43.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation