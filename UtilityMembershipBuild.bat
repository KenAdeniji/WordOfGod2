@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityMembership.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation