@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilitySNMP.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation