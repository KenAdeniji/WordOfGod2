@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySimpleMailTransferProtocolSMTP.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation