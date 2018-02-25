<%@ Page 
 Language="C#" 
 Debug="true"
%>

<%@ Register 
 Tagprefix="UCSearch" 
 Tagname="SearchUserControl" 
 Src="SearchUserControl.ascx" 
%>

<%@ Register 
 TagPrefix="UCDisplayModeMenu" 
 TagName="DisplayModeMenuUserControl" 
 Src="DisplayModeMenuUserControl.ascx" 
%>

<html>

<head runat="server">
 <title runat="server">Web Parts Page</title>
</head>

<body>

 <h1>Web Parts Demonstration Page</h1>

 <form 
  runat="server" 
  id="formWebPart"
  enctype="multipart/form-data" 
  autocomplete="on"
 >
  <asp:webpartmanager 
   id="WebPartManagerWebPart" 
   runat="server" 
  /> 
  <UCDisplayModeMenu:DisplayModeMenuUserControl 
    ID="DisplayModeMenu1" 
    runat="server" 
  />
  <br />
  <table cellspacing="0" cellpadding="0" border="0">
   <tr>
  
    <td valign="top">

     <asp:webpartzone 
      id="WebPartZoneSide" 
      runat="server" 
      headertext="Sidebar"
     >
      <zonetemplate>
       <asp:label id="LabelContent" runat="server" title="Content">
        <h2>Welcome to My Home Page</h2>
        <p>Use links to visit my favorite sites!</p>
       </asp:label>
      </zonetemplate>
     </asp:webpartzone>

    </td>
  
    <td valign="top">
    
     <asp:webpartzone 
      id="WebPartZoneMain" 
      runat="server" 
      headertext="Main"
     >
      <zonetemplate>
       <asp:label runat="server" id="LabelLink" title="Link">
        <a href="http://www.asp.net">ASP.NET site</a> 
        <br />
        <a href="http://www.gotdotnet.com">GotDotNet</a> 
        <br />
        <a href="http://www.contoso.com">Contoso.com</a> 
        <br />
       </asp:label>
       <UCSearch:SearchUserControl id="searchPart" runat="server" title="Search" />
      </zonetemplate>
     </asp:WebPartZone>

    </td>
  
    <td valign="top">
     <asp:editorzone id="EditorZone1" runat="server">
      <zonetemplate>
       <asp:appearanceeditorpart 
       runat="server" 
       id="AppearanceEditorPart1" 
       />
       <asp:layouteditorpart 
        runat="server" 
        id="LayoutEditorPart1" 
       />
      </zonetemplate>
     </asp:editorzone>
     
     <asp:catalogzone 
      id="CatalogZone1" 
      runat="server" 
      headertext="Add Web Parts"
     >
      <zonetemplate>
       <asp:declarativecatalogpart 
        id="catalogpart1" 
        runat="server" 
        Title="My Catalog"
       >
        <webPartsTemplate>
         <asp:fileupload 
          runat="server" 
          id="upload1" 
          title="Upload Files" 
         />
         <asp:calendar 
          runat="server" 
          id="cal1" 
          Title="Team Calendar" 
         />
        </webPartsTemplate>
       </asp:declarativecatalogpart>
      </zonetemplate>
     </asp:catalogzone>

    </td>
    
   </tr>

  </table>
 </form>
</body>