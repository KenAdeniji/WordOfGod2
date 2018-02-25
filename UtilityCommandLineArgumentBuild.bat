@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityCommandLineArgument.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation