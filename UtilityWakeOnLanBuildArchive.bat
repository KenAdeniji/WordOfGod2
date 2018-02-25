@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityWakeOnLan.mak
Del XmlDocumentation /F /S /Q
Rd XmlDocumentation