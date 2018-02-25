<%@ Page 
 Language="C#" 
 debug=true
 Inherits="WordEngineering.WhoIsPort43Page"
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" 
"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
 <head runat="server" id="PageHead">
  <title runat="server" id="PageTitle">WhoIs</title>
 </head>
 <body>
  <form 
   runat="server"
   ID="FormWhoIs" 
   enctype="multipart/form-data"    
   autocomplete="on"
   defaultbutton="ButtonSubmit"
   defaultfocus="TextBoxDomainName"
  >

   <table align="center" border="0">
    <tbody>
     <tr align="center">
      <td align="left">
       <asp:Label
        runat="server"       
        id="LabelDomainName"
        Text="Domain Name:"
        AccessKey="D"
        AssociatedControlId="TextBoxDomainName"
       />
      </td>
      <td align="left">       
       <asp:TextBox
        ID="TextBoxDomainName"
        runat="Server"
        TabIndex="1"
       />        
      </td>
     </tr>
     <tr align="center">
      <td align="left">
       <asp:Label
        runat="server"
        id="LabelRegistry"
        Text="Registry:"
        AccessKey="R"
        AssociatedControlId="ListBoxRegistry"
       />
      </td>
      <td align="left"> 
       <asp:ListBox
        runat="Server"
        ID="ListBoxRegistry"
        OnPreRender="ListBoxRegistry_PreRender"
        Rows=1
        SelectionMode="Multiple"
        TabIndex="2"        
       />        
      </td>
     </tr>
     <tr align="center">
      <td align="left">
       <asp:Label
        runat="server"       
        id="LabelRegistryDomainSuffixOnly"
        Text="Registry Domain Suffix Only:"
        AccessKey="S"
        AssociatedControlId="CheckBoxRegistryDomainSuffixOnly"
       />
      </td>
      <td align="left">       
       <asp:CheckBox
        ID="CheckBoxRegistryDomainSuffixOnly"
        runat="Server"
        Checked="true"
        TabIndex="3"        
       />        
      </td>
     </tr>
     <tr align="center">
      <td align="left">
       <asp:Label
        runat="server"       
        id="LabelPort"
        Text="Port:"
        AccessKey="D"
        AssociatedControlId="TextBoxPortWhoIs"
       />
      </td>
      <td align="left">       
       <asp:TextBox
        ID="TextBoxPortWhoIs"
        runat="Server"
        Text="43"
        TabIndex="4"        
       />        
      </td>
     </tr>
    </tbody>
    <tfooter>
     <tr align="center">
      <td align="center" colspan="2">
       <asp:Button id="ButtonSubmit"  onclick="ButtonSubmit_Click"     runat="server"   Text="Submit"/>
       <asp:Button id="ButtonReset"   onclick="ButtonReset_Click"      runat="server"   Text="Reset"/>
      </td>      
     </tr>        
    </tfooter>
   </table>

   <table align="center" border="0">
    <tbody>
     <tr>
      <td align="center" colspan="2">
       <asp:Literal 
        id="LiteralFeedback" 
        runat="server" 
        EnableViewState=False
       />
      </td>
     </tr>    
    </tbody>
   </table>

  </form>
 </body>
</html>
