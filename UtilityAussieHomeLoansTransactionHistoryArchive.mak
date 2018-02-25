all: bin\AussieHomeLoansPage.aspx.cs.dll UtilityAussieHomeLoansTransactionHistory.exe
 
bin\AussieHomeLoansPage.aspx.cs.dll: AussieHomeLoansPage.aspx.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\AussieHomeLoansPageDocumentation.xml /out:bin\AussieHomeLoansPage.aspx.cs.dll /target:library AussieHomeLoansPage.aspx.cs

UtilityAussieHomeLoansTransactionHistory.exe: UtilityAussieHomeLoansTransactionHistory.cs UtilityCommandLineArgument.cs UtilityException.cs UtilityEventLog.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityAussieHomeLoansTransactionHistoryDocumentation.xml /main:WordEngineering.UtilityAussieHomeLoansTransactionHistory /out:UtilityAussieHomeLoansTransactionHistory.exe /target:exe UtilityAussieHomeLoansTransactionHistory.cs UtilityCommandLineArgument.cs UtilityException.cs UtilityEventLog.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 