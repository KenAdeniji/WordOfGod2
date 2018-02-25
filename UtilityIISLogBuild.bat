@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityIISLog.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation