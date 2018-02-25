//a simple control to display the contents of the page's ViewState using some DHTML
//by gerrard@pickabar.com. Enjoy!

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using System.Collections;
using System.Reflection;

namespace ASPNETDebuggingControls
{
	/// <summary>
	/// Summary description for WebCustomControl1.
	/// </summary>
	[ToolboxData("<{0}:ViewstateDisplayer runat=server></{0}:ViewstateDisplayer>")]
	public class ViewstateDisplayer : System.Web.UI.WebControls.WebControl
	{

		const string scriptKey = "ViewstateDisplayerScript";

		const string scriptContents = 	"\r\n<script>" 
			+"\r\nfunction toggleViewStateDivDisplay(link, id)" 
			+"\r\n{" 
			+"\r\n	var target = document.getElementById(id);" 
			+"\r\n	if (!target)" 
			+"\r\n		window.status = 'Could not find ' + id;	" 
			+"\r\n	else" 
			+"\r\n	{" 
			+"\r\n		if (target.innerText == '')" 
			+"\r\n		{" 
			+"\r\n			link.innerText = '';" 
			+"\r\n		}" 
			+"\r\n		else" 
			+"\r\n		{" 
			+"\r\n			if (target.style.display == 'block')" 
			+"\r\n			{" 
			+"\r\n				target.style.display = 'none';" 
			+"\r\n				link.innerText = '+';" 
			+"\r\n			}" 
			+"\r\n			else" 
			+"\r\n			{" 
			+"\r\n				target.style.display = 'block';" 
			+"\r\n				link.innerText = '-';" 
			+"\r\n			}" 
			+"\r\n		}" 
			+"\r\n	}" 
			+"\r\n}" 
			+"\r\n</script>";

		/// <summary>
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			if (this.Enabled)
			{
				output.Write("<div id=\"ViewstateDisplayer" + this.ClientID + "\" style=\"BORDER-STYLE: double;\">"
					+ "<div id=\"ViewstateHeadline" + this.ClientID + "\" style=\"FONT-WEIGHT: bold; TEXT-ALIGN: center; FONT-VARIANT: small-caps; TEXT-DECORATION: underline\">"
					+ "Viewstate Display</div>");

				displayViewStateContents(output, this.Page);

				output.Write("</div>");
			}
		}

		public void displayViewStateContents(HtmlTextWriter writer, Control theControl)
		{
			string idToUse;

			if (theControl is Page)
				idToUse = "Page";
			else
				idToUse = theControl.ClientID != string.Empty ? theControl.ClientID : "No ID";

			string divFormatted = 
				string.Format("<DIV id='{0}_container'>\r\n<a onclick=\"toggleViewStateDivDisplay(this, '{0}_viewstate');\">+</a> {0} - {1} <div id=\"{0}_viewstate\" style=\"display: none\">",
				idToUse,theControl.GetType());

			writer.Write(divFormatted);

			Type t = typeof(Control);
			object rawResult = t.InvokeMember("ViewState", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty, null, theControl, new object[0]);
			StateBag bag = (StateBag)rawResult;
			object currentValue;

			foreach(string key in bag.Keys)
			{
				currentValue = bag[key];

				writer.Write(key.ToString() + "=");

				if (currentValue != null)
				{
					writer.Write("(" + currentValue.GetType().ToString() + ")");
 
					if (currentValue is IDictionary)
						displayDictionary(writer, (IDictionary)currentValue);
					else if (currentValue is IList)
						displayList(writer, (IList)currentValue);
					else if (currentValue is string)
						displayString(writer, (string)currentValue);
					else
						displayObject(writer, currentValue);	
				}
				else
					writer.Write("null");

				writer.Write("<br />");
			}

			writer.Write("<UL>");
			foreach(Control c in theControl.Controls)
			{
				writer.Write("<LI>");
				displayViewStateContents(writer, c);
				writer.Write("</LI>");
			}
			writer.Write("</UL>");

			writer.Write("</div></div>");
		}
	
		private void displayObject(HtmlTextWriter writer, object item)
		{
			writer.Write(item != null ? item.ToString() : "null"); 
		}

		private void displayString(HtmlTextWriter writer, string item)
		{
			writer.Write("\"");
			writer.Write(item != null ? item.ToString() : "null"); 
			writer.Write("\"");
		}

		private void displayList(HtmlTextWriter writer, IList list)
		{
			for(int i = 0; i < list.Count; i++)
			{
				writer.Write(list[i].ToString());
				
				if (i < list.Count - 1)
					 writer.Write(",");
			}
		}

		private void displayDictionary(HtmlTextWriter writer, IDictionary dict)
		{
			object currentValue;
			string result = string.Empty;


			foreach(DictionaryEntry entry in dict)
			{
				result += entry.Key.ToString() + ":";

				currentValue = entry.Value;

				result += currentValue != null ? currentValue.ToString() : "null";

				result += ",";
			}

			result = result.Remove(result.Length - 1, 1);
 
			writer.Write(result);
		}

		protected override void OnPreRender(EventArgs e)
		{
			if (this.Enabled && !Page.IsClientScriptBlockRegistered(scriptKey))
			{
				Page.RegisterClientScriptBlock(scriptKey, scriptContents); 
				base.OnPreRender (e);
			}
		}
	}
}
