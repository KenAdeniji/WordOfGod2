@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityProtocol.mak
DEL XmlDocumentation /F /S /Q
Rd XmlDocumentation