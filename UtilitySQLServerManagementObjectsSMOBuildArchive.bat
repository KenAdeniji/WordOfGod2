@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySQLServerManagementObjectsSMO.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation