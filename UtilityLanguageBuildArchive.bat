@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityLanguage.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation