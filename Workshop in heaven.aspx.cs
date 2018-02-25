using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace WordEngineering
{
    public partial class Workshop_In_Heaven : System.Web.UI.Page
    {
        /// <summary>GridView_Insert</summary>
        public void GridView_Insert(Object sender, EventArgs e)
        {
            int contactID;           
            int sequenceOrderID;
            DateTime dated;
            string commentary;
            string connectionString = ConfigurationManager.ConnectionStrings["WordEngineering"].ConnectionString;
            string exceptionMessage = null;
            string scriptureReference;
            string uri;
            string value;
            string word;

            SqlCommand sqlCommand;
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlCommand = new SqlCommand("PopulateWorkshopInHeaven", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                value = ((System.Web.UI.WebControls.TextBox)GridViewWorkshopInHeaven.FooterRow.FindControl("TextBoxGridViewWorkshopInHeavenFooterTemplateSequenceOrderID")).Text;
                if (Int32.TryParse(value, out sequenceOrderID))
                {
                    sqlCommand.Parameters.AddWithValue("@sequenceOrderID", sequenceOrderID);
                }
                value = ((System.Web.UI.WebControls.TextBox)GridViewWorkshopInHeaven.FooterRow.FindControl("TextBoxGridViewWorkshopInHeavenFooterTemplateDated")).Text;
                if (DateTime.TryParse(value, out dated))
                {
                    sqlCommand.Parameters.AddWithValue("@dated", dated);
                }
                word = ((System.Web.UI.WebControls.TextBox)GridViewWorkshopInHeaven.FooterRow.FindControl("TextBoxGridViewWorkshopInHeavenFooterTemplateWord")).Text;
                sqlCommand.Parameters.AddWithValue("@word", word);
                commentary = ((System.Web.UI.WebControls.TextBox)GridViewWorkshopInHeaven.FooterRow.FindControl("TextBoxGridViewWorkshopInHeavenFooterTemplateCommentary")).Text;
                sqlCommand.Parameters.AddWithValue("@commentary", commentary);
                uri = ((System.Web.UI.WebControls.TextBox)GridViewWorkshopInHeaven.FooterRow.FindControl("TextBoxGridViewWorkshopInHeavenFooterTemplateURI")).Text;
                sqlCommand.Parameters.AddWithValue("@uri", uri);
                value = ((System.Web.UI.WebControls.TextBox)GridViewWorkshopInHeaven.FooterRow.FindControl("TextBoxGridViewWorkshopInHeavenFooterTemplateContactId")).Text;
                if (Int32.TryParse(value, out contactID))
                {
                    sqlCommand.Parameters.AddWithValue("@contactID", contactID);
                }
                scriptureReference = ((System.Web.UI.WebControls.TextBox)GridViewWorkshopInHeaven.FooterRow.FindControl("TextBoxGridViewWorkshopInHeavenFooterTemplateScriptureReference")).Text;
                sqlCommand.Parameters.AddWithValue("@scriptureReference", scriptureReference);
                sqlCommand.ExecuteNonQuery();
                DatabaseQuery();
            }
            catch (System.Exception exception)
            {
                exceptionMessage = "System.Exception: " + exception.Message;
            }
            if (exceptionMessage != null)
            {
                Feedback.Text = exceptionMessage;
                return;
            }
            else
            {
                Feedback.Text = null;
            }
        }

        public void GridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            UtilityGridView.GridView_PageIndexChanging(sender, e);
            DatabaseQuery();
        }

        public void GridView_RowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
        {
            //UtilityGridView.GridView_RowCancelingEdit(sender, e);
            GridView gridView = (GridView)sender;
            gridView.EditIndex = -1;
        }

        public void GridView_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            int sequenceOrderID = -1;
            string connectionString = ConfigurationManager.ConnectionStrings["WordEngineering"].ConnectionString;
            string logText = "";

            GridView gridView = (GridView)sender;
            GridViewRow gridViewRow = gridView.Rows[e.RowIndex];
            SqlCommand sqlCommand;
            SqlConnection sqlConnection;

            sequenceOrderID = Int32.Parse(((System.Web.UI.WebControls.Label)gridViewRow.FindControl("LabelGridViewWorkshopInHeavenItemTemplateSequenceOrderID")).Text);

            logText += String.Format
            (
                "e.RowIndex: {0} | SequenceOrderID: {1} | e.Keys.Count: {2} | e.Values.Count: {3}",
                e.RowIndex,
                sequenceOrderID,
                e.Keys.Count, 
                e.Values.Count
            );
            foreach (DictionaryEntry keyEntry in e.Keys)
            {
                logText += keyEntry.Key + "=" + keyEntry.Value + ";";
            }
            foreach (DictionaryEntry keyEntry in e.Values)
            {
                logText += keyEntry.Key + "=" + keyEntry.Value + ";";
            }
            //Feedback.Text = logText;

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlCommand = new SqlCommand
            (
                String.Format("DELETE FROM WorkshopInHeaven WHERE SequenceOrderID = {0}", sequenceOrderID),
                sqlConnection
            );
            sqlCommand.ExecuteNonQuery();
            DatabaseQuery();
        }

        public void GridView_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            //UtilityGridView.GridView_RowEditing(sender, e);
            GridView gridView = (GridView)sender;
            gridView.EditIndex = e.NewEditIndex;
        }

        public void GridView_RowUpdated(Object sender, GridViewUpdatedEventArgs e)
        {
            // Use the Exception property to determine whether an exception
            // occurred during the insert operation.
            if (e.Exception != null)
            {
                // Insert the code to handle the exception.
                Feedback.Text = e.Exception.Message;

                // Use the ExceptionHandled property to indicate that the 
                // exception is already handled.
                e.ExceptionHandled = true;

                // When an exception occurs, keep the GridView
                // control in edit mode.
                e.KeepInEditMode = true;
            }
        }

        public void GridView_Sorting(Object sender, GridViewSortEventArgs e)
        {
            DatabaseQuery();
            UtilityGridView.GridView_Sorting(sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DatabaseQuery();
            }
        }

        protected void DatabaseQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WordEngineering"].ConnectionString;
            DataSet dataSet;
            SqlCommand sqlCommand;
            SqlConnection sqlConnection;
            SqlDataAdapter sqlDataAdapter;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SELECT * FROM WorkshopInHeaven", sqlConnection);

            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            GridViewWorkshopInHeaven.DataSource = dataSet;
            GridViewWorkshopInHeaven.DataBind();
        }
    }
}