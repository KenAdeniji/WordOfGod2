@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityMail.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation