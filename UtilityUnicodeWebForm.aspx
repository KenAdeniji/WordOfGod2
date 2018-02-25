<%@ Page 
 Language="C#" 
 Inherits="WordEngineering.UtilityUnicodePage" 
%>
 
<html>
 <head>
  <title>URI Web Form</title>
 </head>
 <body>
  <form enctype="multipart/form-data" runat="server">
   <table align="center" border="0">
    <tbody>
     <tr><td align="center" colspan="1">
      <asp:hyperlink 
       id="HyperLinkWelcome" 
       runat="server"
       target="_self"
       text="Welcome"
       NavigateUrl="UtilityUnicodeWebForm.aspx?request=Welcome"
      /> 
     </td></tr>
     <tr><td align="center" colspan="1"><asp:Literal id="LiteralFeedback" runat="server"/></td></tr>    
    </tbody>    
   
   </table>    
  </form>
 </body>
</html>

<script language="javascript">
 document.forms[0].HyperLinkWelcome.focus();
</script> 