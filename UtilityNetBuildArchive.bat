@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityNet.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation