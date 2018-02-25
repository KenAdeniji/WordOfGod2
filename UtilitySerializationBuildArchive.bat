@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilitySerialization.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation