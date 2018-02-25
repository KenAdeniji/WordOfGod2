all: UtilitySimpleMailTransferProtocolSMTP.exe SimpleMailTransferProtocolSMTPPage.aspx.cs.dll

SimpleMailTransferProtocolSMTPPage.aspx.cs.dll: SimpleMailTransferProtocolSMTPPage.aspx.cs UtilitySimpleMailTransferProtocolSMTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\SimpleMailTransferProtocolSMTPPageDocumentation.xml /out:bin\SimpleMailTransferProtocolSMTPPage.aspx.cs.dll /target:library SimpleMailTransferProtocolSMTPPage.aspx.cs UtilitySimpleMailTransferProtocolSMTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

UtilitySimpleMailTransferProtocolSMTP.exe: UtilitySimpleMailTransferProtocolSMTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilitySimpleMailTransferProtocolSMTP.xml /main:WordEngineering.UtilitySimpleMailTransferProtocolSMTP /out:UtilitySimpleMailTransferProtocolSMTP.exe /target:exe UtilitySimpleMailTransferProtocolSMTP.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation