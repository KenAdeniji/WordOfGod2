@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityCommonwealthBankOfAustraliaTransaction.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation