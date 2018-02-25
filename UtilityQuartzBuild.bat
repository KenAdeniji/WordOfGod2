@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityQuartz.mak
REM ILDASM QuartzTypeLib.dll
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation