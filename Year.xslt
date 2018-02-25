<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!-- 
 Created: 20031216. Sequence Order Id 5. 807 Years (Genesis 5:7). Heart beat, West.
-->

<xsl:output method="html" indent="no" />
 <xsl:template match="/">
  <html>
   <body>
    <table align="center" border="1">
     <theader>
      <tr align="center"><td colspan="2">
        Sign
        (
         <a target="_blank" href="http://bible.crosswalk.com/OnlineStudyBible/bible.cgi?new=1&amp;word=Genesis+1%3A14-19%2C+Revelation+8%3A12&amp;section=0&amp;version=nkj&amp;language=en">
          Genesis 1:14-19, Revelation 8:12
         </a>
        )
      </td></tr>
      <tr align="center">
       <td>Year</td>
       <td>ScriptureReference</td>
      </tr>
     </theader>     
     <tbody>
      <xsl:apply-templates select="/Year/Year">
       <xsl:sort select="Sign" data-type="number" order="ascending" case-order="lower-first"/>
       <xsl:sort select="position()" data-type="number" order="ascending" case-order="lower-first"/> 
      </xsl:apply-templates>
     </tbody>
    </table>
  </body>
 </html>
 </xsl:template>

 <xsl:template match="/Year/Year">
  <tr align="center">
   <td>
    <xsl:choose>
     <xsl:when test="ScriptureReferenceURI!=''">
      <a target="_blank" href="{ScriptureReferenceURI}">
       <xsl:value-of select="Sign"/>
      </a>
     </xsl:when>
     <xsl:otherwise>
      <xsl:value-of select="Sign"/>
     </xsl:otherwise>
    </xsl:choose>
   </td>
   <td>
    <xsl:choose>
     <xsl:when test="ScriptureReferenceURI!=''">
      <a target="_blank" href="{ScriptureReferenceURI}">
       <xsl:value-of select="ScriptureReference"/>
      </a>
     </xsl:when>
     <xsl:otherwise>
      <xsl:value-of select="ScriptureReference"/>
     </xsl:otherwise>
    </xsl:choose>
   </td>
   <td>
    <xsl:value-of select="Commentary"/>
   </td>
  </tr>
 </xsl:template>
 
</xsl:stylesheet>

<!--
Legal filter operators are:
 =    (equal) 
 !=   (not equal) 
 &lt; less than 
 &gt; greater than
-->

<!--
HTML Special Entities
Character   Meaning        Entity   Numeric
<           Less than      &lt;     &#60;
>           Greater than   &gt;     &#62;
&           Ampersand      &amp;    &#38;
'           Apostrophe     &apos;   &#39;
"           Quote          &quot;   &#34;
-->