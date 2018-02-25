all: UtilityTraceRoute.exe TraceRoutePage.aspx.cs.dll

UtilityTraceRoute.exe: UtilityTraceRoute.cs UtilityICMP.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityTraceRouteDocumentation.xml /main:WordEngineering.UtilityTraceRoute /out:UtilityTraceRoute.exe /target:exe UtilityTraceRoute.cs UtilityICMP.cs UtilityXml.cs

TraceRoutePage.aspx.cs.dll: TraceRoutePage.aspx.cs UtilityTraceRoute.cs UtilityICMP.cs UtilityJavaScript.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\TraceRoutePageDocumentation.xml /out:bin\TraceRoutePage.aspx.cs.dll /target:library TraceRoutePage.aspx.cs UtilityTraceRoute.cs UtilityICMP.cs UtilityJavaScript.cs UtilityXml.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation