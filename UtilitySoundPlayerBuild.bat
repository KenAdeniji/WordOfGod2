@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySoundPlayer.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation