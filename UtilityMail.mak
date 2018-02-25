all: UtilityMail.exe MailSendPage.aspx.cs.dll

UtilityMail.exe: UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityMail.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityMail.xml /main:WordEngineering.UtilityMail /out:UtilityMail.exe /target:exe /unsafe UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityMail.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

MailSendPage.aspx.cs.dll: MailSendPage.aspx.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityMail.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\MailSendPageDocumentation.xml /out:bin\MailSendPage.aspx.cs.dll /target:library /unsafe MailSendPage.aspx.cs UtilityCollection.cs UtilityCommandLineArgument.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityImpersonate.cs UtilityMail.cs UtilityProcess.cs UtilityTcpIp.cs UtilityXml.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation