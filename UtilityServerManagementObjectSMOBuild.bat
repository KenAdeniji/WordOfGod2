@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityServerManagementObjectSMO.mak
Del XmlDocumentation /F /S /Q
Rd XmlDocumentation