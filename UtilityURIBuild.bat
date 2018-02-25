@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityURI.mak
REM Del *.pdb XmlDocumentation /F /S /Q
REM Rd XmlDocumentation