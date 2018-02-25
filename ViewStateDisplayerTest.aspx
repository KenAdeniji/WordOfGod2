<%@ Register TagPrefix="vw" Namespace="ASPNETDebuggingControls" Assembly = "ASPNETDebuggingControls" %>
<%@ Import namespace="System.Data" %>
<%@ Page language="c#" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script runat="server" language="c#">
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add("Bands");
				ds.Tables["Bands"].Columns.Add("Name");
				ds.Tables["Bands"].Columns.Add("Genre");
				ds.Tables["Bands"].Columns.Add("Guitarists");


				ds.Tables["Bands"].Rows.Add(new object[] { "The Allman Brothers Band", "Southern Rock", "Duane Allman, Dickey Betts" });
				ds.Tables["Bands"].Rows.Add(new object[] { "Thin Lizzy", "Hard Rock", "Scott Gorham, Scotty Moore"});

				for (int i = 0; i < 10; i++)
				{
					ds.Tables["Bands"].Rows.Add(new object[] { "The " + i.ToString() + "ers", "Garage Rock", "Gerrard Lindsay"});	
				}

				DataGrid1.DataSource = ds.Tables["bands"];
				DataGrid1.DataBind();
			}

			ViewState["C#"] = "Db";

			Hashtable table = new Hashtable();
			table.Add("One", "1");
			table.Add("Two", "2");

			ViewState["Table"] = table;
		}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id="DataGrid1" runat="server" AllowPaging="True"></asp:datagrid>
			<P>&nbsp;</P>
			<P><vw:viewstatedisplayer id="ViewstateDisplayer1" runat="server"></vw:viewstatedisplayer></P>
		</form>
	</body>
</HTML>
