@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityFileUpload.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation