all: ResponseOutputStreamWritePage.aspx.cs.dll

ResponseOutputStreamWritePage.aspx.cs.dll: ResponseOutputStreamWritePage.aspx.cs UtilityCollection.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityResponse.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\ResponseOutputStreamWritePageDocumentation.xml /out:bin\ResponseOutputStreamWritePage.aspx.cs.dll /target:library ResponseOutputStreamWritePage.aspx.cs UtilityCollection.cs UtilityEventLog.cs UtilityException.cs UtilityFile.cs UtilityResponse.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation