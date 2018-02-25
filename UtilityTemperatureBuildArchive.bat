@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityTemperature.mak
REM WSDL http://localhost/WordOfGod/ServiceTemperature.asmx?WSDL
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation