@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySpeech.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation