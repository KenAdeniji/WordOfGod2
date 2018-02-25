all: UtilityManagement.exe
 
UtilityManagement.exe: UtilityEventLog.cs UtilityException.cs UtilityManagement.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityManagementDocumentation.xml /main:WordEngineering.UtilityManagement /out:UtilityManagement.exe /target:exe UtilityEventLog.cs UtilityException.cs UtilityManagement.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 