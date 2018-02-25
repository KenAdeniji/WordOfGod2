@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityAussieHomeLoansTransactionHistory.mak
DEL XmlDocumentation /F /S /Q
RD XmlDocumentation