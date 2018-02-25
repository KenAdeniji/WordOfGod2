@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityNetSession.mak
Del XmlDocumentation /F /S /Q
Rd XmlDocumentation