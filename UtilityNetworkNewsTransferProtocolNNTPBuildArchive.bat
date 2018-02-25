@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityNetworkNewsTransferProtocolNNTPworkNewsTransferProtocolNNTP.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation