@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityProtocol.mak
DEL *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation