@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityNetworkNewsTransferProtocolNNTP.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation