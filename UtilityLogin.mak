all: LoginPage.aspx.cs.dll

LoginPage.aspx.cs.dll: LoginPage.aspx.cs UtilityClass.cs UtilityCollection.cs Contact.cs UtilityContact.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs  UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\LoginPageDocumentation.xml /out:bin\LoginPage.aspx.cs.dll /target:library /unsafe LoginPage.aspx.cs UtilityClass.cs UtilityCollection.cs Contact.cs UtilityContact.cs UtilityDatabase.cs UtilityDebug.cs UtilityDirectory.cs UtilityEventLog.cs UtilityFile.cs UtilityJavaScript.cs  UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation