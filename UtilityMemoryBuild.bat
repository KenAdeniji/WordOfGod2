@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityMemory.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation