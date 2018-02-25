@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityXML.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation