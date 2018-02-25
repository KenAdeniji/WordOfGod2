@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySQLCLR.mak
Del XmlDocumentation /F /S /Q
Rd XmlDocumentation