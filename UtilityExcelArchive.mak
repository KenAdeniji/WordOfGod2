all: UtilityExcel.exe Excel.aspx.cs.dll

UtilityExcel.exe: UtilityExcel.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityExcelDocumentation.xml /main:WordEngineering.UtilityExcel /out:UtilityExcel.exe /target:exe UtilityExcel.cs

ExcelPage.aspx.cs.dll: ExcelPage.aspx.cs UtilityExcel.cs
 csc /doc:XmlDocumentation\ExcelPageFormDocumentation.xml /out:bin\ExcelPage.aspx.cs.dll /target:library ExcelPage.aspx.cs UtilityExcel.cs

Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation
