all: AlphabetSequencePage.aspx.cs.dll AlphabetSequence.exe BibleBookClassification.exe BibleBookTitle.dll BibleBookTitle.exe BibleDictionary.exe BibleDictionaryWordQueryPage.aspx.cs.dll NoisePage.aspx.cs.dll

AlphabetSequencePage.aspx.cs.dll: AlphabetSequencePage.aspx.cs AlphabetSequence.cs AlphabetSequencePage.aspx.cs AlphabetSequence.cs BibleBookTitle.cs ScriptureReference.cs ScriptureReferenceAlphabetSequence.cs ScriptureReferenceSubset.cs ScriptureReferenceText.cs ScriptureReferenceTextSubset.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:..\WordOfGodDocumentation\AlphabetSequencePageDocumentation.xml /out:bin\AlphabetSequencePage.aspx.cs.dll /target:library AlphabetSequencePage.aspx.cs AlphabetSequence.cs BibleBookTitle.cs ScriptureReference.cs ScriptureReferenceAlphabetSequence.cs ScriptureReferenceSubset.cs ScriptureReferenceText.cs ScriptureReferenceTextSubset.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs

AlphabetSequence.exe: AlphabetSequence.cs AlphabetSequencePage.aspx.cs AlphabetSequence.cs BibleBookTitle.cs ScriptureReference.cs ScriptureReferenceAlphabetSequence.cs ScriptureReferenceSubset.cs ScriptureReferenceText.cs ScriptureReferenceTextSubset.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:..\WordOfGodDocumentation\AlphabetSequenceDocumentation.xml /main:WordEngineering.AlphabetSequence /out:AlphabetSequence.exe /target:exe AlphabetSequence.cs BibleBookTitle.cs ScriptureReferenceAlphabetSequence.cs ScriptureReference.cs ScriptureReferenceSubset.cs ScriptureReferenceText.cs ScriptureReferenceTextSubset.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs

BibleBookClassification.exe: BibleBookTitle.cs BibleBookClassification.cs
 csc /define:DEBUG /debug:full /doc:BibleBookClassificationXmlDocumentation.xml /main:WordEngineering.BibleBookClassification /target:exe BibleBookClassification.cs BibleBookTitle.cs UtilityCollection.cs

BibleBookTitle.dll BibleBookTitle.exe: BibleBookTitle.cs
 csc /define:DEBUG /debug:full /doc:..\WordOfGodDocumentation\BibleBookTitleDocumentation.xml /target:library /out:bin\BibleBookTitle.dll /target:exe /out:BibleBookTitle.exe BibleBookTitle.cs

BibleDictionary.exe: BibleDictionary.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:..\WordOfGodDocumentation\BibleDictionaryDocumentation.xml /main:WordEngineering.BibleDictionary /out:BibleDictionary.exe /target:exe BibleDictionary.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs

BibleDictionaryWordQueryPage.aspx.cs.dll: BibleDictionaryWordQueryPage.aspx.cs BibleDictionary.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs
 csc /define:DEBUG /debug:full /doc:..\WordOfGodDocumentation\BibleDictionaryWordQueryPageDocumentation.xml /out:bin\BibleDictionaryWordQueryPage.aspx.cs.dll /target:library BibleDictionaryWordQueryPage.aspx.cs BibleDictionary.cs UtilityEventLog.cs UtilityDatabase.cs UtilityXml.cs

NoisePage.aspx.cs.dll: NoisePage.aspx.cs UtilityDatabase.cs UtilityEventLog.cs UtilityXml.cs 
 csc /define:DEBUG /debug:full /doc:..\WordOfGodDocumentation\NoisePageDocumentation.xml /out:bin/NoisePage.aspx.cs.dll /target:library NoisePage.aspx.cs UtilityDatabase.cs UtilityEventLog.cs UtilityXml.cs