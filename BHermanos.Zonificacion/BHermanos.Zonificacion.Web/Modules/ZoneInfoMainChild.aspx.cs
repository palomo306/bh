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
    public partial class ZoneInfoMainChild : System.Web.UI.Page
    {
        #region Carga de Datos
        private void LoadPlazas()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Plaza/GetPlaza/1/0?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                PlazaModel objResponse = JsonSerializer.Parse<PlazaModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    List<BE.Plaza> lstPlazas= objResponse.ListaPlazas.ToList();
                    lstPlazas.Insert(0, new BE.Plaza() { Id = 0, Nombre = "--Seleccione una Plaza--" });
                    ddlPlazas.DataSource = lstPlazas;
                    ddlPlazas.DataValueField = "Id";
                    ddlPlazas.DataTextField= "Nombre";
                    ddlPlazas.DataBind();                    
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al extraer las plazas [" + objResponse.Mensaje + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer las plazas [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "InitializeScreen();", true);
        }
    }
}