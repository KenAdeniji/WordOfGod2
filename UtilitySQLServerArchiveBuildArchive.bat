@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilitySQLServerArchive.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation