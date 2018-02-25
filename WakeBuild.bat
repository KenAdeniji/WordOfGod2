@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE Wake.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation