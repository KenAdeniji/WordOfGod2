@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityLogin.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation