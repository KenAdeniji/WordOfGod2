@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityBCP.mak
Rem Del *.pdb XmlDocumentation /F /S /Q
Rem Rd XmlDocumentation