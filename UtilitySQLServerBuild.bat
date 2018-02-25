@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySQLServer.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation