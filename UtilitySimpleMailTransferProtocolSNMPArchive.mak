all: UtilitySimpleMailTransferProtocolSNMP.exe SimpleMailTransferProtocolSNMPPage.aspx.cs.dll

SimpleMailTransferProtocolSNMPPage.aspx.cs.dll: SimpleMailTransferProtocolSNMPPage.aspx.cs UtilitySimpleMailTransferProtocolSNMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SimpleMailTransferProtocolSNMPPageDocumentation.xml /out:bin\SimpleMailTransferProtocolSNMPPage.aspx.cs.dll /target:library SimpleMailTransferProtocolSNMPPage.aspx.cs UtilitySimpleMailTransferProtocolSNMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

UtilitySimpleMailTransferProtocolSNMP.exe: UtilitySimpleMailTransferProtocolSNMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySimpleMailTransferProtocolSNMP.xml /main:WordEngineering.UtilitySimpleMailTransferProtocolSNMP /out:UtilitySimpleMailTransferProtocolSNMP.exe /target:exe UtilitySimpleMailTransferProtocolSNMP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation