<%@ Page 
 Language="C#" 
 Inherits="WordEngineering.WordSearchPage" 
%>

<!--
 string                    word,
 FindVersesContaining      findVersesContaining,
 bool                      includePartialMatch,
 int                       bibleBookIdMinimum,
 int                       bibleBookIdMaximum,   
 ScriptureReference[]      scriptureReference,
 BibleBookClassification[] bibleBookClassification
-->

<html>

 <head>
  <title>WordSearch</title>
 </head>
 <body style="FONT-FAMILY: arial">
  <form runat="server">
   <table align="center" border="1">
    <theader>
     <tr align="center"><th align="center" colspan="2">Word Search</th></tr>
    </theader>
    <tbody>

     <tr align="center">
      <td align="center" colspan="2">
       <asp:Label id="LabelScriptureWord" runat="server">Word:</asp:Label>
       <asp:TextBox id="TextBoxScriptureWord" runat="server"></asp:TextBox>
      </td>     
     </tr>     

     <tr align="center">   
       <td align="center" colspan="2">  
        <asp:Panel 
         id="PanelFindVersesContaining" 
         runat="server" 
         HorizontalAlign="Center" 
         Wrap="False" 
        >
         Find Verses Containing
         
         <asp:RadioButton 
          id="RadioButtonFindVersesContainingAllWords"   
          Text="All Words" 
          Checked="True" 
          GroupName="RadioButtonGroupFindVersesContaining" 
          runat="server" 
         />

         <asp:RadioButton 
          id="RadioButtonFindVersesContainingAnyWords"   
          Text="Any Word" 
          Checked="False" 
          GroupName="RadioButtonGroupFindVersesContaining" 
          runat="server" 
         />

         <asp:RadioButton 
          id="RadioButtonFindVersesContainingPhrase"   
          Text="Phrase" 
          Checked="False" 
          GroupName="RadioButtonGroupFindVersesContaining" 
          runat="server" 
         />
        </asp:Panel>
      </td>     
     </tr>

     <tr align="center">   
       <td align="center" colspan="2">  
        <asp:Panel 
         id="PanelTestament" 
         HorizontalAlign="Center" 
         runat="server" 
         Wrap="False" 
        >
        
         <asp:RadioButton 
          id="RadioButtonTestamentEntireBible"
          AutoPostBack="True"
          Checked="True" 
          GroupName="RadioButtonGroupTestament" 
          OnCheckedChanged="RadioButtonTestament_CheckedChanged"
          Text="Entire Bible" 
          runat="server" 
         />

         <asp:RadioButton 
          id="RadioButtonTestamentOld"   
          AutoPostBack="True"          
          Checked="False" 
          GroupName="RadioButtonGroupTestament" 
          OnCheckedChanged="RadioButtonTestament_CheckedChanged"          
          Text="Old Testament" 
          runat="server" 
         />

         <asp:RadioButton 
          id="RadioButtonTestamentNew"   
          AutoPostBack="True"          
          Checked="False" 
          GroupName="RadioButtonGroupTestament" 
          OnCheckedChanged="RadioButtonTestament_CheckedChanged"          
          Text="New Testament" 
          runat="server" 
         />
        </asp:Panel>
      </td>     
     </tr>

     <tr align="center">
      <asp:Panel 
       id="PanelBibleBookSearchRange" 
       runat="server" 
       HorizontalAlign="Center" 
       Wrap="False" 
      >
       <td align="center" colspan="2">         
        <asp:DropDownList id="DropDownListBibleBookSearchRangeMinimum" runat="server"/>
        -
        <asp:DropDownList id="DropDownListBibleBookSearchRangeMaximum" runat="server"/>
       </td>             
      </asp:Panel>
     </tr>

     <tr align="center">
      <td align="center" colspan="2">
       Bible Book Classification:
       <asp:ListBox
        id="ListBoxBibleBookClassificationId" 
        Rows="2"
        SelectionMode="Multiple"
        runat="server"
       />
      </td>
     </tr>     

     <tr align="center">
      <td align="center" colspan="2">
        <asp:Button id="ButtonQuery" onclick="DatabaseQuery" runat="server" Text="Query"/>
      </td>
     </tr>     

     <tr>
      <td align="center" colspan="2">
       <asp:DataGrid id="DataGridBibleWord" runat="server"
        BorderColor="black"
        BorderWidth="1"
        CellPadding="3"
        ShowFooter="true"
        AutoGenerateColumns="false"
       > 
        <HeaderStyle BackColor="#00aaaa"/>
         <Columns>
          <asp:BoundColumn DataField="scriptureReferenceWithoutBracket" HeaderText="Scripture Reference"/>
          <asp:BoundColumn DataField="VerseText" HeaderText="Verse Text"/>
        </Columns>
       </asp:DataGrid>
      </td>
     </tr> 

     <tr align="center">
      <td align="center" colspan="2">
       <asp:Literal id="LiteralStaticTextMessage" Text="" runat="server"/>
      </td>     
     </tr>

    </tbody>    
   </table>    
  </form>
 </body>
</html>
