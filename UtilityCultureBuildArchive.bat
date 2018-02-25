@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityCulture.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation