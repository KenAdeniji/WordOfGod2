@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityOfficeWebComponentOWC.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation