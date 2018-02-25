all: WebRequestResponse.exe

WebRequestResponse.exe: WebRequestResponse.cs
 csc /debug:full /main:WordEngineering.WebRequestResponse /out:WebRequestResponse.exe /target:exe WebRequestResponse.cs
