/*
<script language='JavaScript' src='Utility.js'>
</script>
*/

function LocationHash(locationHash)
{
 Location.Hash = locationHash;
}

/*
JavaScript & DHTML Cookbook 
Solutions and Example for Web Programmers 
By Danny Goodman
ISBN: 0-596-00467-2
http://www.oreilly.com/catalog/jvdhtmlckbk/chapter/ch10.pdf
*/
function LocationHref(locationHref)
{
 Location.Href = locationHref;
}

function LocationReplace(locationReplace)
{
 Location.Replace(locationReplace);
}

/*
http://vb2themax.com/ShowContent.aspx?ID=ccc400c5-77d2-412b-bd5f-a89cf22ee854
private void Page_Load(object sender, EventArgs e)
{
 if ( ! this.IsPostBack )
 {
  Button1.Attributes.Add("onClick", "javascript:window.close();");
 }
}
*/
function WindowClose()
{
 Window.Close();
}

/*
// prepare the javascript code
// NOTE: the Url must include an http:// prefix
string script = "javascript:window.open(' http://www.vb2themax.com', 'VB-2-The-Max', 'hei"
   + "ght=400, width=300, resizable=no, menubar=no, location=yes')";
// attach to the button's onClick client-side event
btnOpen.Attributes.Add("onClick", script);
*/
function WindowOpen()
{
 window.open("KnowledgeBase.aspx");
}


/*
Peter McMahon Peter@DotNet.za.net An Introduction to ASP.NET Using Visual Basic.NET 
http://www.dotnet.za.net/book/IntroToASP.NET.pdf
<html>
 <body onload="WindowStatus('Window.Status')" />
</html>	
*/
function WindowOpen( status )
{
 window.status = status
}

function WindowClose()
{
 window.Close()
}
