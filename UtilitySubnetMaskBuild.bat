@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilitySubnetMask.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation