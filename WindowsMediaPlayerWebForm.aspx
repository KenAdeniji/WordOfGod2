<%@ Page 
 Debug=True
 Language='C#' 
 Trace=False 
%>

<!--
 http://steveorr.net/Articles/StreamingMedia.aspx
 http://www.msdn.microsoft.com/library/default.asp?url=/library/en-us/wmplay10/mmp_sdk/simpleexampleofscriptinginawebpage.asp
-->
 
<!--
 <bgsound 
  SRC="/Audio/Christian/1NC/1NC_-_Free.mp3" 
  loop="0" 
  balance="0" 
  volume="0"
  />
--> 

<html>
 <head><title>Windows Media Player</title></head>
 <body>

  <!--
  <bgsound 
   SRC="/Audio/Christian/1NC/1NC_-_Free.mp3" 
   loop="0" 
   balance="0" 
   volume="0"
   />
  --> 
 
  <OBJECT 
   ID="WindowsMediaPlayer" 
   height="0" 
   width="0"
   CLASSID="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6"
   VIEWASTEXT
  >
   <PARAM name="autoStart" value="True" /> 
   <PARAM name="URL" value="/Audio/Christian/1NC/1NC_-_Free.mp3" /> 
   <PARAM name="rate" value="1" /> 
   <PARAM name="balance" value="0" /> 
   <PARAM name="enabled" value="true" /> 
   <PARAM name="enabledContextMenu" value="true" /> 
   <PARAM name="fullScreen" value="false" /> 
   <PARAM name="playCount" value="1" /> 
   <PARAM name="volume" value="100" /> 
  </OBJECT>

  <input type="Button" name="ButtonPlay" value="Play" onClick="WindowsMediaPlayerPlay()" />
  <input type="Button" name="ButtonStop" value="Stop" onClick="WindowsMediaPlayerStop()" />

  <script>
  <!--

  function WindowsMediaPlayerPlay ()
  {
   WindowsMediaPlayer.URL = "/Audio/Christian/1NC/1NC_-_Free.mp3";
  }

  function WindowsMediaPlayerStop ()
  {
   WindowsMediaPlayer.controls.stop();
  }

  -->
  </script>
 </body>
</html>