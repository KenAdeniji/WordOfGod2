<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

<xsl:output method="html" indent="no" />
 <xsl:template match="/">
  <html>
   <body>
    <table align="center" border="1">
     <theader>
      <tr align="center">
       <th>Windows Data Type</th>
       <th>.NET Data Type</th>
      </tr>
     </theader>     
     <tbody>
      <xsl:apply-templates select="//WindowsNETDataType">
       <xsl:sort select="WindowsDataType" data-type="text" order="ascending" case-order="lower-first"/>
       <xsl:sort select="NETDataType" data-type="text" order="ascending" case-order="lower-first"/>
       <xsl:sort select="position()" data-type="number" order="ascending" case-order="lower-first"/> 
      </xsl:apply-templates>
     </tbody>
    </table>
  </body>
 </html>
 </xsl:template>

 <xsl:template match="//WindowsNETDataType">
  <tr align="center">
   <td>
    <xsl:value-of select="WindowsDataType"/>
   </td>
   <td>
    <xsl:value-of select="NETDataType"/>
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