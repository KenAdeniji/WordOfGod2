@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityWindowsManagementInstrumentationWMI.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation