@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityRegularExpression.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation