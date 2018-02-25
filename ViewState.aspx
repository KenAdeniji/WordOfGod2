<%@ Page Language="C#" AutoEventWireup="true" %>
<html>
 <head runat="server">
  <title runat="server">ViewState</title>
  <script runat="server" language="C#">
   protected void Increment_Click(Object sender, EventArgs e)
   {
    int counter = -1;
    if ( ViewState["Counter"] == null )
    {
     counter = 1;
    }
    else
    {
     counter = (int) ViewState["Counter"] + 1;
    }
    ViewState["Counter"] = counter;
    Counter.Text = counter.ToString();
   }
  </script>
 </head>
 <body>
  <form runat="server">
   <asp:Label runat="server" id="Counter" />
   <asp:Button runat="server" id="Increment" Text="Increment" OnClick="Increment_Click" />
  </form>
 </body>
</html>