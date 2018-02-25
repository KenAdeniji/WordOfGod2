<%@ Page 
 Language="C#" 
 debug=true
 Inherits="WordEngineering.WindowClosePage"
%>

<!-- http://vb2themax.com/ShowContent.aspx?ID=ccc400c5-77d2-412b-bd5f-a89cf22ee854 -->

<html>
 <head>
  <title>Close</title>
 </head>
 <body>
  <form 
   ID="formClose" 
   runat="server"
   enctype="multipart/form-data"    
   autocomplete="on"
   defaultbutton="ButtonClose"
   defaultfocus="ButtonClose"
  >

   <table align="center" border="0">
    <tbody>
     <asp:Panel
      runat="server"
      id="PanelClose"
      BackColor="gainsboro"
     >
      <tr align="center">
       <td align="center">
        <asp:Button 
         runat="server"
         id="ButtonClose" 
         Text="Close"
         OnClick="ButtonClose_Click" 
         CauseValidation="False" 
        />
       </td>      
      </tr>        
     </asp:Panel>
    </tbody>    
   </table>
  </form>
 </body>
</html>