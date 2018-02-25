@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityImpersonate.mak
Del XmlDocumentation /F /S /Q
Rd XmlDocumentation