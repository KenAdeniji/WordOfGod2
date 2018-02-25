@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityPage.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation