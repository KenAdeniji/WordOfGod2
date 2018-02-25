@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityMSCorEE.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation