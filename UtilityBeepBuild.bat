@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityBeep.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation