@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityProcess.mak
Del XmlDocumentation /F /S /Q
Rd XmlDocumentation