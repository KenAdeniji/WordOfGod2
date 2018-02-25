@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityWindowsMediaPlayerWMP.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation