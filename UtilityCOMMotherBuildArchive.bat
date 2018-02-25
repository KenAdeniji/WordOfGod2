@ECHO OFF
IF NOT EXIST XmlDocumentation MD XmlDocumentation
SN -k UtilityCOMMother.snk
NMAKE UtilityCOMMother.mak
GACUtil /i UtilityCOMMother.dll
REGASM UtilityCOMMother.dll /tlb:UtilityCOMMother.tlb 
Del *.pdb XmlDocumentation /F /S /Q
Rd XmlDocumentation