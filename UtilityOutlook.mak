all: MSOUTLTypeLib.dll UtilityOutlook.exe

MSOUTLTypeLib.dll: MSOUTL.OLB
 CALL TLBIMP MSOUTL.OLB /Out:MSOUTLTypeLib.dll
 CALL RegSVR32 MSOUTLTypeLib.dll
 CALL ILDASM MSOUTLTypeLib.dll
 CALL Explorer %SystemRoot%\Assembly
 CALL Xcopy MSOUTLTypeLib.dll bin /C /D /E /I /R /S /Y /Z

UtilityOutlook.exe: Outlook.dll UtilityOutlook.cs
 csc /define:DEBUG /debug:full /main:WordEngineering.UtilityOutlook /out:UtilityOutlook.exe /reference:Outlook.dll /target:exe UtilityOutlook.cs