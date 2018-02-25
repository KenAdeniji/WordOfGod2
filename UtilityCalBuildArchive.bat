@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityCal.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation