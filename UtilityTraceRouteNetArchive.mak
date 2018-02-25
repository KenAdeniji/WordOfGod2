all: UtilityTraceRouteNet.exe
 
UtilityTraceRouteNet.exe: UtilityTraceRouteNet.cs
 csc /define:DEBUG /debug:full /doc:XmlDocumentation\UtilityTraceRouteNetDocumentation.xml /main:WordEngineering.UtilityTraceRouteNet /out:UtilityTraceRouteNet.exe /target:exe UtilityTraceRouteNet.cs

Clean:
 DEL *.pdb XmlDocumentation /F /S /Q
 RD XmlDocumentation 