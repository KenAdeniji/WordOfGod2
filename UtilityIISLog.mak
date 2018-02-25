all: FormIISLog.exe IISLogPage.aspx.cs.dll UtilityIISLog.exe

FormIISLog.exe: CommandLineArguments.cs FormIISLog.cs UtilityIISLog.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\FormIISLogDocumentation.xml /main:WordEngineering.FormIISLog /out:FormIISLog.exe /target:winexe CommandLineArguments.cs FormIISLog.cs UtilityIISLog.cs

UtilityIISLog.exe: CommandLineArguments.cs UtilityIISLog.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityIISLogDocumentation.xml /main:WordEngineering.UtilityIISLog /out:UtilityIISLog.exe /target:exe CommandLineArguments.cs UtilityIISLog.cs

IISLogPage.aspx.cs.dll: bin\Ajax.dll CommandLineArguments.cs IISLogPage.aspx.cs UtilityIISLog.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\IISLogPageDocumentation.xml /out:bin\IISLogPage.aspx.cs.dll /reference:bin\Ajax.dll /target:library CommandLineArguments.cs IISLogPage.aspx.cs UtilityIISLog.cs


Clean:
 DEL XmlDocumentation /F /S /Q
 RD XmlDocumentation
