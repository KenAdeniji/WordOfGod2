@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityQOTD.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation