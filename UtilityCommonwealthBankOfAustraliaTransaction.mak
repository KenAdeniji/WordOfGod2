all: bin\CommonwealthBankOfAustraliaPage.aspx.cs.dll UtilityCommonwealthBankOfAustraliaTransaction.exe

bin\CommonwealthBankOfAustraliaPage.aspx.cs.dll: CommonwealthBankOfAustraliaPage.aspx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\CommonwealthBankOfAustraliaPageDocumentation.xml /out:bin\CommonwealthBankOfAustraliaPage.aspx.cs.dll /reference:System.Web.dll /target:library CommonwealthBankOfAustraliaPage.aspx.cs
 
UtilityCommonwealthBankOfAustraliaTransaction.exe: UtilityCommonwealthBankOfAustraliaTransaction.cs UtilityCommandLineArgument.cs UtilityException.cs UtilityEventLog.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityCommonwealthBankOfAustraliaTransactionDocumentation.xml /main:WordEngineering.UtilityCommonwealthBankOfAustraliaTransaction /out:UtilityCommonwealthBankOfAustraliaTransaction.exe /reference:System.Web.dll /target:exe UtilityCommonwealthBankOfAustraliaTransaction.cs UtilityCommandLineArgument.cs UtilityException.cs UtilityEventLog.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 