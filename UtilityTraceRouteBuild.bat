@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityTraceRoute.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation