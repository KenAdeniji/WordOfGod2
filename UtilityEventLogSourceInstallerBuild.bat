@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMAKE UtilityEventLogSourceInstaller.mak
DEL *.pdb XmlDocumentation /F /S /Q
RD XmlDocumentation