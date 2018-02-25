@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySQLServerArchive.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation