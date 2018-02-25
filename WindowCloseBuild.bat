@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE WindowClose.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation