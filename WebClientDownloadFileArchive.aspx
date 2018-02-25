<%@ Page 
 Language="C#" 
 debug="true"
%>

<html>
 <head>
  <title>Web Client Download File</title>
 </head>
 <body>
  <form runat="server">
  </form>
 </body>
</html>

<script runat=server language=C#>

 ///<summary>Page_Load()</summary>
 public void Page_Load() 
 {
  System.Net.WebClient  webClient = new System.Net.WebClient();
  try
  {
   webClient.DownloadFile
   ( 
    Server.MapPath( "WebClientDownloadFile.aspx" ), 
    Server.MapPath( "WebClientDownloadFileSupplement.aspx" ) 
   );
  }//try
  catch ( Exception exception ) { Response.Write( exception.Message ); }
 }//public void Page_Load()
</script>