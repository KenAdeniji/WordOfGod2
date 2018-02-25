@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityExcel.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation