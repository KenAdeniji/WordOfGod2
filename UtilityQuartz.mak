all: QuartzTypeLib.dll UtilityQuartz.exe QuartzPage.aspx.cs.dll

QuartzTypeLib.dll: C:\WINNT\System32\Quartz.dll
 TLBIMP C:\WINNT\System32\Quartz.dll /Out:QuartzTypeLib.dll
 Xcopy QuartzTypeLib.dll bin\Comforter /C /D /E /I /R /S /Y /Z

UtilityQuartz.exe: QuartzTypeLib.dll UtilityQuartz.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityQuartz.xml /main:WordEngineering.UtilityQuartz /out:UtilityQuartz.exe /target:exe UtilityQuartz.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs /reference:QuartzTypeLib.dll

QuartzPage.aspx.cs.dll: QuartzPage.aspx.cs UtilityQuartz.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\QuartzPageDocumentation.xml /out:bin\QuartzPage.aspx.cs.dll /target:library QuartzPage.aspx.cs UtilityQuartz.cs UtilityCollection.cs UtilityDirectory.cs UtilityFile.cs UtilityXml.cs /reference:QuartzTypeLib.dll
 
Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation