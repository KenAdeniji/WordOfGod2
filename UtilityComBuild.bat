@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityCom.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation