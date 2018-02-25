@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
NMake UtilityMasterContentPage.mak
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation