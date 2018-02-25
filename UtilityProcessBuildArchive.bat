@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityProcess.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation