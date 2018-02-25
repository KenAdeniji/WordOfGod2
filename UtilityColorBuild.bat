@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityColor.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation