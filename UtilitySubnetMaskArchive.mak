all: SubnetMaskPage.aspx.cs.dll UtilitySubnetMask.exe UtilityImpersonate.exe

SubnetMaskPage.aspx.cs.dll: SubnetMaskPage.aspx.cs UtilitySubnetMask.cs UtilityICMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SubnetMaskPageDocumentation.xml /out:bin\SubnetMaskPage.aspx.cs.dll /target:library SubnetMaskPage.aspx.cs UtilitySubnetMask.cs UtilityICMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityJavaScript.cs UtilityImpersonate.cs UtilityXml.cs

UtilityImpersonate.exe: UtilityImpersonate.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityImpersonateDocumentation.xml /main:WordEngineering.UtilityImpersonate /out:UtilityImpersonate.exe /target:exe UtilityImpersonate.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

UtilitySubnetMask.exe: UtilitySubnetMask.cs UtilityICMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySubnetMaskDocumentation.xml /main:WordEngineering.UtilitySubnetMask /out:UtilitySubnetMask.exe /target:exe UtilitySubnetMask.cs UtilityICMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation