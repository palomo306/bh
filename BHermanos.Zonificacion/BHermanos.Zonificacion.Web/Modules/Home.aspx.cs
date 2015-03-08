using AjaxControlToolkit;
using BE = BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.BusinessEntities.Cast;
using BHermanos.Zonificacion.WebService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BHermanos.Zonificacion.Web.Modules
{
    public partial class Home : System.Web.UI.Page
    {
        #region Carga de Datos
        private void LoadTabs()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Tab/GetTab/0/0?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                TabModel objResponse = JsonSerializer.Parse<TabModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    int i = 1;
                    foreach (BE.Tab tb in objResponse.ListaTabs)
                    {
                        TabPanel oNewTab = new TabPanel();
                        oNewTab.ID = "tab" + tb.Id.ToString();
                        oNewTab.HeaderText = tb.Nombre;
                        Label oContent = new Label();
                        oContent.ID = "lbl" + tb.Id.ToString();
                        oContent.Text = @"<iframe id=""tab" + i.ToString() + @""" seamless=""seamless"" src=""ZoneInfo.aspx?tabId=" + tb.Id.ToString() + @""" style=""width:100%; height:100%;""></iframe>";
                        oNewTab.Controls.Add(oContent);
                        tabMainContainer.Tabs.Add(oNewTab);
                        i++;
                    }
                }
                else
                {

                }
            }
            catch
            {
            
            }
        }
        #endregion

        #region Carga de la Página
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "SetHeightIframes();", true);            
            if (!Page.IsPostBack)
            {
                LoadTabs();
            }
        }
        #endregion
    }
}