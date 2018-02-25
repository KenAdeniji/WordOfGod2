@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityRequest.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation