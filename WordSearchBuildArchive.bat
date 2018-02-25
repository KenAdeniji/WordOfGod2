@Echo Off
csc /debug:full /doc:WordSearchXmlDocumentation.xml /main:WordEngineering.WordSearch /target:exe WordSearch.cs BibleBookClassification.cs BibleBookTitle.cs ScriptureReference.cs ScriptureReferenceSubset.cs ScriptureReferenceText.cs  
csc /debug:full /target:library /out:bin\WordSearch.dll WordSearch.cs BibleBookClassification.cs BibleBookTitle.cs ScriptureReference.cs ScriptureReferenceSubset.cs ScriptureReferenceText.cs  
