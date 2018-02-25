@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityPostback.mak
DEL *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation