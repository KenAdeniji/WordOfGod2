Response.Write( WebConfiguration.AppSettings["ConnectionString"]);
Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
config.AppSettings.Settings["ConnectionString"].Value = "";
config.Save();