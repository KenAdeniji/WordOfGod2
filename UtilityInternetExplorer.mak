# http://blog.monstuff.com/archives/000052.html Curiosity is bliss: Hosting a web browser component in a C# winform
# http://www.opussoftware.com/tutorial/TutMakefile.htm Tutorial - Makefile
all: SHDocVw.dll AxSHDocVw.dll MSHTML.dll UtilityInternetExplorer.exe

SHDocVw.dll: $(WINDIR)\system32\shdocvw.dll
 aximp $(WINDIR)\system32\shdocvw.dll

AxSHDocVw.dll: $(WINDIR)\system32\shdocvw.dll
 aximp $(WINDIR)\system32\shdocvw.dll

MSHTML.dll: $(WINDIR)\system32\mshtml.tlb
 tlbimp $(WINDIR)\system32\mshtml.tlb

UtilityInternetExplorer.exe: UtilityInternetExplorer.cs SHDocVw.dll AxSHDocVw.dll MSHTML.dll 
 csc /debug:full /main:WordEngineering.UtilityInternetExplorer /out:UtilityInternetExplorer.exe /target:exe UtilityInternetExplorer.cs /reference:SHDocVw.dll,AxSHDocVw.dll,MSHTML.dll 
