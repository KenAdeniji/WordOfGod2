@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityEventLog.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation