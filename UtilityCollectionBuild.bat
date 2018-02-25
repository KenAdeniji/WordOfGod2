@Echo Off
csc /debug:full /doc:UtilityCollectionXmlDocumentation.xml /main:WordEngineering.UtilityCollection /target:exe UtilityCollection.cs
csc /debug:full /target:library /out:bin\UtilityCollection.dll UtilityCollection.cs