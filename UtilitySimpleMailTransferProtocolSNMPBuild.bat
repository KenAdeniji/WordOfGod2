@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilitySimpleMailTransferProtocolSNMP.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation