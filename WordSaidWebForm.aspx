<%@ Page 
 Language="C#" 
 Inherits="WordEngineering.WordSaidPage" 
%>
 
<html>
 <head>
  <title>WordSaid</title>
 </head>
 <body>
  <form runat="server">
   <table align="center" border="0">
    <tbody>
     <a name="top"/>

     <tr align="center"><td colspan="2">
      <input id="HtmlInputHiddenDocumentUniqueId" type="hidden" value="" runat="server" />
      Title: <asp:TextBox id="TextBoxDocumentTitle" runat="server" />
      Dated: <asp:TextBox id="TextBoxDocumentDated" runat="server" />
     </td></tr>

     <tr><td align="left"><a href="#JehovahRophe">Jehovah Rophe</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Exodus+15%3A26&section=0&version=nkj&language=en" target="_blank">Exodus 15:26</a>)</td></tr>
     <tr><td align="left"><a href="#Remember">Remember</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Genesis+1%3A14%2C+Ecclesiastes+3%3A1-8&section=0&version=nkj&language=en" target="_blank">Genesis 1:14, Ecclesiastes 3:1-8</a>)</td></tr>     
     <tr><td align="left"><a href="#Prophecy">Prophecy: Enoch</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Jude+1%3A14&section=0&version=nkj&language=en" target="_blank">Jude 1:14</a>)</td></tr>
     <tr><td align="left"><a href="#ScriptureReading">Scripture Reading: Berea</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Acts+17%3A11&section=0&version=nkj&language=en" target="_blank">Acts 17:11</a>)</td></tr>
     <tr><td align="left"><a href="#AlphabetSequence">Alphabet Sequence</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=John+1%3A1%2C+Hebrews+8%3A5%2C+2+Peter+1%3A21&section=0&version=nkj&language=en" target="_blank">John 1:1, Hebrews 8:5, 2 Peter 1:21</a>)</td></tr>
     <tr><td align="left"><a href="#CaseBasedReasoning">Case-Based Reasoning: Bildad</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Job+8%3A8-10&section=0&version=nkj&language=en" target="_blank">Job 8:8-10</a>)</td></tr>
     <tr><td align="left"><a href="#Software">Software: Solomon</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&amp;word=Proverbs+25%3A2&amp;section=0&amp;version=nkj&amp;language=en" target="_blank">Proverbs 25:2</a>)</td></tr>
     <tr><td align="left"><a href="#Export">Export: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Luke+19%3A16-19%2C+Luke+19%3A24-26&section=0&version=nkj&language=en" target="_blank">Luke 19:16-19, Luke 19:24-26</a>)</td></tr>     
     <tr><td align="left"><a href="#ClassAssociates">Class Associates: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Isaiah+53%3A7%2C+Matthew+12%3A34%2C+Matthew+15%3A11%2C+Matthew+15%3A18%2C+Luke+6%3A45&section=0&version=nkj&language=en" target="_blank">Isaiah 53:7, Matthew 12:34, Matthew 15:11, Matthew 15:18, Luke 6:45</a>)</td></tr>
     <tr><td align="left"><a href="#Error">Error: John</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=1+John+5%3A16&section=0&version=nkj&language=en" target="_blank">1 John 5:16</a>)</td></tr>
     <tr><td align="left"><a href="#Cry">Cry: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Isaiah+42%3A2%2C+Matthew+12%3A19&section=0&version=nkj&language=en" target="_blank">Isaiah 42:2, Matthew 12:19</a>)</td></tr>
     <tr><td align="left"><a href="#Advertisement">Advertisement: Absalom</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=2+Samuel+16%3A22&section=0&version=nkj&language=en" target="_blank">2 Samuel 16:22</a>)</td></tr>
     <tr><td align="left"><a href="#Keyboard">Keyboard</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Psalms+119&section=0&version=nkj&language=en" target="_blank">Psalms 119</a>)</td></tr>
     <tr><td align="left"><a href="#Mouse">Mouse: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Matthew+19%3A24%2C+Mark+10%3A25%2C+Luke+18%3A25&section=0&version=nkj&language=en" target="_blank">Matthew 19:24, Mark 10:25, Luke 18:25</a>)</td></tr>
     <tr><td align="left"><a href="#UserInterface">User Interface: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Matthew+23%3A24&section=0&version=nkj&language=en" target="_blank">Matthew 23:24</a>)</td></tr>     
    
     <tr><td align="center"><a name="JehovahRophe">Jehovah Rophe</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Exodus+15%3A26&section=0&version=nkj&language=en" target="_blank">Exodus 15:26</a>)</td></tr>
     <tr><td align="center">
      <asp:DataGrid
       id="DataGridJehovahRophe"
       runat="server"
       AutoGenerateColumns="false"
      > 
       <Columns>
        <asp:BoundColumn HeaderText="No." DataField="sequenceOrderId" />
        <asp:BoundColumn HeaderText="Dated" DataField="dated" />
        <asp:BoundColumn HeaderText="Scripture Reference" DataField="scriptureReference" />
        <asp:BoundColumn HeaderText="Commentary" DataField="commentary" />
       </Columns>
     </asp:DataGrid>      
     </td></tr>     

     <tr><td align="center"><a name="Remember">Remember</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&amp;word=Genesis+1%3A14%2C+Ecclesiastes+3%3A1-8&amp;section=0&amp;version=nkj&amp;language=en" target="_blank">Genesis 1:14, Ecclesiastes 3:1-8</a>)</td></tr>
     <tr><td align="center"><a name="Prophecy">Prophecy: Enoch</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Jude+1%3A14&section=0&version=nkj&language=en" target="_blank">Jude 1:14</a>)</td></tr>
     <tr><td align="center"><a name="ScriptureReading">Scripture Reading: Berea</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Acts+17%3A11&section=0&version=nkj&language=en" target="_blank">Acts 17:11</a>)</td></tr>
     <tr><td align="center"><a name="AlphabetSequence">Alphabet Sequence</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=John+1%3A1%2C+Hebrews+8%3A5%2C+2+Peter+1%3A21&section=0&version=nkj&language=en" target="_blank">John 1:1, Hebrews 8:5, 2 Peter 1:21</a>)</td></tr>
     <tr><td align="center"><a name="CaseBasedReasoning">Case-Based Reasoning: Bildad</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Job+8%3A8-10&section=0&version=nkj&language=en" target="_blank">Job 8:8-10</a>)</td></tr>
     <tr><td align="center"><a name="Software">Software: Solomon</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Proverbs+25%3A2&section=0&version=nkj&language=en" target="_blank">Proverbs 25:2</a>)</td></tr>
     <tr><td align="center"><a name="Export">Export: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Luke+19%3A16-19%2C+Luke+19%3A24-26&section=0&version=nkj&language=en" target="_blank">Luke 19:16-19, Luke 19:24-26</a>)</td></tr>  
     <tr><td align="center"><a name="ClassAssociates">Class Associates: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Isaiah+53%3A7%2C+Matthew+12%3A34%2C+Matthew+15%3A11%2C+Matthew+15%3A18%2C+Luke+6%3A45&section=0&version=nkj&language=en" target="_blank">Isaiah 53:7, Matthew 12:34, Matthew 15:11, Matthew 15:18, Luke 6:45</a>)</td></tr>
     <tr><td align="center"><a name="Error">Error: John</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Isaiah+53%3A7%2C+Matthew+12%3A34%2C+Matthew+15%3A11%2C+Matthew+15%3A18%2C+Luke+6%3A45&section=0&version=nkj&language=en" target="_blank">1 John 5:16</a>)</td></tr>
     <tr><td align="center"><a name="Cry">Cry: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Isaiah+42%3A2%2C+Matthew+12%3A19&section=0&version=nkj&language=en" target="_blank">Isaiah 42:2, Matthew 12:19</a>)</td></tr>
     <tr><td align="center"><a name="Advertisement">Advertisement: Absalom</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=2+Samuel+16%3A22&section=0&version=nkj&language=en" target="_blank">2 Samuel 16:22</a>)</td></tr>
     <tr><td align="center"><a name="Keyboard">Keyboard</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Psalms+119&section=0&version=nkj&language=en" target="_blank">Psalms 119</a>)</td></tr>     
     <tr><td align="center"><a name="Mouse">Mouse: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Matthew+19%3A24%2C+Mark+10%3A25%2C+Luke+18%3A25&section=0&version=nkj&language=en" target="_blank">Matthew 19:24, Mark 10:25, Luke 18:25</a>)</td></tr>
     <tr><td align="center"><a name="UserInterface">User Interface: Jesus Christ</a> (<a href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&word=Matthew+23%3A24&section=0&version=nkj&language=en" target="_blank">Matthew 23:24</a>)</td></tr>
     
     <tr align="center"><td colspan="2">
      <asp:Button id="ButtonReset" runat="server" text="Reset" onclick="ButtonReset_Click" />
      <asp:Button id="ButtonSubmit" runat="server" text="Submit" onclick="ButtonSubmit_Click" />
     </td></tr>
     
    </tbody>    
   </table>    
   <a name="bottom"/>
  </form>
 </body>
</html>

<script language="javascript">
  document.forms[0].TextBoxDocumentTitle.focus ();
</script> 