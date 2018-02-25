@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityServerManagementObjectSMO.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation