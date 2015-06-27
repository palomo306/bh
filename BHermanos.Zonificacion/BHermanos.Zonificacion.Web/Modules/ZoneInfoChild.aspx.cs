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
using BHermanos.Zonificacion.Web.Clases;
using System.Data;
using System.Collections.ObjectModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using BHermanos.Zonificacion.BusinessEntities;

namespace BHermanos.Zonificacion.Web.Modules
{
    public partial class ZoneInfoChild : System.Web.UI.Page
    {
        #region Propiedades
        public BE.Usuario CurrentUser
        {
            get
            {
                if (Session["User"] != null)
                    return (BE.Usuario)Session["User"];
                return null;
            }
            set
            {
                Session["User"] = value;
            }
        }

        private List<BE.Plaza> ListPlazas
        {
            get
            {
                if (ViewState["DatosPlazas"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["DatosPlazas"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (List<BE.Plaza>)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["DatosPlazas"] != null)
                    ViewState["DatosPlazas"] = ValueArrayCompressed;
                else
                    ViewState.Add("DatosPlazas", ValueArrayCompressed);
            }
        }

        private List<BE.Zona> ListZonas
        {
            get
            {
                if (ViewState["DatosZonas"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["DatosZonas"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (List<BE.Zona>)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["DatosZonas"] != null)
                    ViewState["DatosZonas"] = ValueArrayCompressed;
                else
                    ViewState.Add("DatosZonas", ValueArrayCompressed);
            }
        }

        private BE.Plaza CurrentPlaza
        {
            get
            {
                if (ViewState["DatosPlaza"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["DatosPlaza"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (BE.Plaza)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["DatosPlaza"] != null)
                    ViewState["DatosPlaza"] = ValueArrayCompressed;
                else
                    ViewState.Add("DatosPlaza", ValueArrayCompressed);
            }
        }



        private BE.Zona CurrentZone
        {
            get
            {
                if (ViewState["DatosZona"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["DatosZona"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (BE.Zona)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["DatosZona"] != null)
                    ViewState["DatosZona"] = ValueArrayCompressed;
                else
                    ViewState.Add("DatosZona", ValueArrayCompressed);
            }
        }

        private BE.Zona CurrentSubzone
        {
            get
            {
                if (ViewState["DatosSubzona"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["DatosSubzona"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (BE.Zona)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["DatosSubzona"] != null)
                    ViewState["DatosSubzona"] = ValueArrayCompressed;
                else
                    ViewState.Add("DatosSubzona", ValueArrayCompressed);
            }
        }


        private string LocalPath
        {
            get
            {
                if (ViewState["LocalPath"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["LocalPath"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (string)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["LocalPath"] != null)
                    ViewState["LocalPath"] = ValueArrayCompressed;
                else
                    ViewState.Add("LocalPath", ValueArrayCompressed);
            }
        }

        private string CurrentLevel
        {
            get
            {
                if (ViewState["CurrentLevel"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["CurrentLevel"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (string)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["CurrentLevel"] != null)
                    ViewState["CurrentLevel"] = ValueArrayCompressed;
                else
                    ViewState.Add("CurrentLevel", ValueArrayCompressed);
            }
        }

        private string PreviousLevel
        {
            get
            {
                if (ViewState["PreviousLevel"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["PreviousLevel"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (string)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["PreviousLevel"] != null)
                    ViewState["PreviousLevel"] = ValueArrayCompressed;
                else
                    ViewState.Add("PreviousLevel", ValueArrayCompressed);
            }
        }

        private string TabId
        {
            get
            {
                if (Request.QueryString["tabId"] != null)
                    return Request.QueryString["tabId"];
                return string.Empty;
            }
        }

        private readonly int LayerIndex = 5;

        #endregion

        #region Carga de Datos
        private void LoadPlazas()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                int timeOut = int.Parse(ConfigurationManager.AppSettings["ConnectTimeOut"].ToString());
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Plaza/GetPlaza/1/0?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = timeOut;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                PlazaModel objResponse = JsonSerializer.Parse<PlazaModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    List<BE.Plaza> lstPlazas = objResponse.ListaPlazas.ToList();
                    lstPlazas.Insert(0, new BE.Plaza() { Id = 0, Nombre = "--Seleccione una Plaza--" });
                    this.ListPlazas = lstPlazas;
                    ddlPlazas.DataSource = this.ListPlazas;
                    ddlPlazas.DataValueField = "Id";
                    ddlPlazas.DataTextField = "Nombre";
                    ddlPlazas.DataBind();
                }
                else
                {
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las plazas [" + objResponse.Mensaje + "]');", true);
                }
            }
            catch (Exception ex)
            {
                hdnShowBackGroup.Value = "not";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + ex.Message + "]');", true);
            }
        }

        private void LoadZonasTab(string plazaId, string fechaInicio, string fechaFin)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                int timeOut = int.Parse(ConfigurationManager.AppSettings["ConnectTimeOut"].ToString());
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                if (fechaInicio != "0")
                    url += "Tab/GetTab/" + TabId + "/" + plazaId + "/" + fechaInicio + "/" + fechaFin + "?type=json";
                else
                    url += "Tab/GetTab/" + TabId + "/" + plazaId + "?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = timeOut;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                TabModel objResponse = JsonSerializer.Parse<TabModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    List<BE.Tab> tabs = objResponse.ListaTabs.ToList();
                    if (tabs.Count == 1)
                    {
                        ListZonas = tabs[0].ListaZonas;
                    }
                    else
                    {
                        hdnShowBackGroup.Value = "not";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [Hay más de un Tab para el Id --> " + TabId + "]');", true);
                    }
                }
                else
                {
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + objResponse.Mensaje.Replace("'", "") + "]');", true);
                }
            }
            catch (Exception ex)
            {
                hdnShowBackGroup.Value = "not";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + ex.Message.Replace("'", "") + "]');", true);
            }
        }

        private void LoadFechas()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                int timeOut = int.Parse(ConfigurationManager.AppSettings["ConnectTimeOut"].ToString());
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Tab/GetTab/" + TabId + "/0?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = timeOut;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                TabModel objResponse = JsonSerializer.Parse<TabModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    List<BE.Tab> tabs = objResponse.ListaTabs.ToList();
                    if (tabs.Count == 1)
                    {
                        List<Fecha> lstFechas = Fecha.GetFechas(tabs[0].Fechas);
                        lstFechas.Insert(0, new Fecha() { Text = "--Fecha--" });
                        ddlStartDate.DataSource = lstFechas;
                        ddlStartDate.DataValueField = "FechaQueryString";
                        ddlStartDate.DataTextField = "FechaText";
                        ddlStartDate.DataBind();
                        ddlStartDate.SelectedValue = "0";
                        ddlEndDate.DataSource = lstFechas;
                        ddlEndDate.DataValueField = "FechaQueryString";
                        ddlEndDate.DataTextField = "FechaText";
                        ddlEndDate.DataBind();
                        ddlEndDate.SelectedValue = "0";
                        lblHead.Text = tabs[0].Nombre;
                    }
                    else
                    {
                        hdnShowBackGroup.Value = "not";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [Hay más de un Tab para el Id --> " + TabId + "]');", true);
                    }
                }
                else
                {
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + objResponse.Mensaje.Replace("'", "") + "]');", true);
                }
            }
            catch (Exception ex)
            {
                hdnShowBackGroup.Value = "not";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + ex.Message.Replace("'", "") + "]');", true);
            }
        }

        private void PrintCurrentInfo()
        {
            string umbralClass = string.Empty;
            lblNoDataMessage.Visible = false;
            dgReportTab.Visible = false;
            switch (this.CurrentLevel)
            {
                case "0":
                    //Se saca el reporte
                    DataTable dtReporteZonas = ReportZonesConversor.ToListPartidas(this.ListZonas, this.CurrentLevel);
                    if (dtReporteZonas.Rows.Count > 0)
                    {
                        dgReportTab.DataSource = dtReporteZonas;
                        dgReportTab.DataBind();
                        dgReportTab.Visible = true;
                    }
                    else
                    {
                        lblNoDataMessage.Text = "Sin datos disponibles";
                        lblNoDataMessage.Visible = true;
                    }
                    lblTotalDesc.Text = "Zonas";
                    lblTotalQty.InnerText = this.ListZonas.Count.ToString();
                    umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.ListZonas);
                    divUmbrales.InnerHtml = ReportZonesConversor.ToHtmlTotalesUmbrales(this.ListZonas);
                    lblColores.Text = umbralClass;
                    //Se colocan los nombres de la zona actual
                    cellZonaHead.Visible = false;
                    cellZonaName.Visible = false;
                    btnCancel.Visible = false;
                    //Se colocan los colores del mapa
                    SetupZonasCustomRenderSettings();
                    break;
                case "1":
                    DataTable dtReporteSubzonas = ReportZonesConversor.ToListPartidas(this.CurrentZone.ListaSubzonas, this.CurrentLevel);
                    if (dtReporteSubzonas.Rows.Count > 0)
                    {
                        dgReportTab.DataSource = dtReporteSubzonas;
                        dgReportTab.DataBind();
                        dgReportTab.Visible = true;
                    }
                    else
                    {
                        lblNoDataMessage.Text = "Sin datos disponibles";
                        lblNoDataMessage.Visible = true;
                    }
                    lblTotalDesc.Text = "Subzonas";
                    lblTotalQty.InnerText = this.CurrentZone.ListaSubzonas.Count.ToString();
                    umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.CurrentZone.ListaSubzonas);
                    divUmbrales.InnerHtml = ReportZonesConversor.ToHtmlTotalesUmbrales(this.CurrentZone.ListaSubzonas);
                    lblColores.Text = umbralClass;
                    //Se colocan los nombres de la zona actual
                    cellZonaHead.Visible = true;
                    cellZonaName.Visible = true;
                    cellSubzonaHead.Visible = false;
                    cellSubonaName.Visible = false;
                    btnCancel.Visible = true;
                    txtCurrentZona.Text = this.CurrentZone.Nombre;
                    txtCurrentZona.BackColor = this.CurrentZone.RealColor;
                    //Se colocan los colores del mapa
                    SetupZonaSubzonasCustomRenderSettings();
                    break;
                case "2":
                    if (PreviousLevel == "1")
                    {
                        DataTable dtReporteColonias = ReportZonesConversor.ToListPartidas(this.CurrentSubzone.ListaColonias);
                        if (dtReporteColonias.Rows.Count > 0)
                        {
                            dgReportTab.DataSource = dtReporteColonias;
                            dgReportTab.DataBind();
                            dgReportTab.Visible = true;
                        }
                        else
                        {
                            lblNoDataMessage.Text = "Sin datos disponibles";
                            lblNoDataMessage.Visible = true;
                        }
                        lblTotalDesc.Text = "Colonias";
                        lblTotalQty.InnerText = this.CurrentSubzone.ListaColonias.Count.ToString();
                        umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.CurrentSubzone.ListaColonias);
                        divUmbrales.InnerHtml = ReportZonesConversor.ToHtmlTotalesUmbrales(this.CurrentSubzone.ListaColonias);
                        lblColores.Text = umbralClass;
                        //Se colocan los nombres de la subzona actual
                        cellSubzonaHead.Visible = true;
                        cellSubonaName.Visible = true;
                        btnCancel.Visible = true;
                        txtCurrentSubzona.Text = this.CurrentSubzone.Nombre;
                        txtCurrentSubzona.BackColor = this.CurrentSubzone.RealColor;
                        //Se colocan los colores del mapa
                        SetupZonaSubzonasColoniasCustomRenderSettings();
                    }
                    else
                    {
                        DataTable dtReporteColonias = ReportZonesConversor.ToListPartidas(this.CurrentZone.ListaColonias);
                        if (dtReporteColonias.Rows.Count > 0)
                        {
                            dgReportTab.DataSource = dtReporteColonias;
                            dgReportTab.DataBind();
                            dgReportTab.Visible = true;
                        }
                        else
                        {
                            lblNoDataMessage.Text = "Sin datos disponibles";
                            lblNoDataMessage.Visible = true;
                        }
                        lblTotalDesc.Text = "Colonias";
                        lblTotalQty.InnerText = this.CurrentZone.ListaColonias.Count.ToString();
                        umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.CurrentZone.ListaColonias);
                        divUmbrales.InnerHtml = ReportZonesConversor.ToHtmlTotalesUmbrales(this.CurrentZone.ListaColonias);
                        lblColores.Text = umbralClass;
                        //Se colocan los nombres de la subzona actual
                        btnCancel.Visible = true;
                        //Se colocan los colores del mapa
                        SetupZonaSubzonasColoniasCustomRenderSettings();
                    }
                    break;
            }
            if (this.CurrentPlaza != null && this.CurrentPlaza.ListaEstados != null)
                //ZoomToPlaza(this.CurrentPlaza.ListaEstados);
                ZoomToPlaza(this.CurrentPlaza);
        }

        private void ClearData()
        {
            DataTable dtReporte = new DataTable();
            dgReportTab.DataSource = dtReporte;
            dgReportTab.DataBind();
            lblColores.Text = "Sin datos disponibles";
        }
        #endregion

        #region Carga de la Página
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "InitializeScreen();ResizeMap2();", true);
            string argument = Request["__EVENTARGUMENT"];

            hdnShowBackGroup.Value = "close";            
            if (!Page.IsPostBack)
            {
                cellZonaHead.Visible = false;
                cellSubzonaHead.Visible = false;
                cellZonaName.Visible = false;
                cellSubonaName.Visible = false;
                btnCancel.Visible = false;
                CurrentLevel = "0";
                LoadPlazas();
                LoadZonasTab("0", "0", "0");
                LoadFechas();
                LoadMainMap();
                PrintCurrentInfo();
            }

            if (argument == "notInfo")
            {
                if (ddlPlazas.SelectedValue != "0" && ddlStartDate.SelectedValue != "0" && ddlEndDate.SelectedValue != "0")
                {

                    //Se carga la nueva plaza
                    this.CurrentPlaza = this.ListPlazas.Where(pl => pl.Id.ToString() == ddlPlazas.SelectedValue).FirstOrDefault();
                    LoadPlazaShapes();
                    //ZoomToPlaza(this.CurrentPlaza.ListaEstados);
                    ZoomToPlaza(this.CurrentPlaza);
                    //Se cargan las zonas relacionadas con la plaza
                    //string stDate = ddlStartDate.SelectedValue;
                    //string edDate = ddlEndDate.SelectedValue;
                    //DateTime start = Fecha.GetDateTime(ddlStartDate.SelectedItem.Text);
                    //DateTime end = Fecha.GetDateTime(ddlEndDate.SelectedItem.Text);
                    //if (start > end)
                    //{
                    //    stDate = ddlEndDate.SelectedValue;
                    //    edDate = ddlStartDate.SelectedValue;
                    //}
                    //LoadZonasTab(this.CurrentPlaza.Id.ToString(), stDate, edDate);
                    LoadCurrentPlazaRenderSetting();
                    PrintCurrentInfo();
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);
                }
                else
                {
                    LoadMainMap();
                    LoadZonasTab("0", "0", "0");
                    PrintCurrentInfo();
                }
            }

        }
        #endregion

        #region Manejo de los Mapas
        private void LoadMainMap()
        {
            string mapPath = "~/Maps/Nacional/national_estatal.egp";
            sfmMainMap.ProjectName = mapPath;
            sfmMainMap.Zoom = 10;
            sfmMainMap.DataBind();
        }

        private void LoadCurrentPlazaRenderSetting()
        {
            int shapeCount = sfmMainMap.LayerCount;
            if (shapeCount > 1)
            {
                //Lista de control
                List<BE.Colonia> lstSelColonias = new List<BE.Colonia>();
                //Se recorren los datos
                int layerIndex = this.LayerIndex;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(layerIndex);
                    SinglePlazaCustomRenderSettings crsPl = new SinglePlazaCustomRenderSettings(sf.RenderSettings, this.CurrentPlaza, this.ListZonas);
                    sf.RenderSettings.CustomRenderSettings = crsPl;
                    layerIndex += 5;
                }
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void SetupZonasCustomRenderSettings()
        {
            int shapeCount = sfmMainMap.LayerCount;
            if (shapeCount > 1)
            {
                int layerIndex = this.LayerIndex;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(layerIndex);
                    ZonasPartidasCustomRenderSettings crsZ = new ZonasPartidasCustomRenderSettings(sf.RenderSettings, this.ListZonas);
                    sf.RenderSettings.CustomRenderSettings = null;
                    sf.RenderSettings.CustomRenderSettings = crsZ;
                    layerIndex += 5;
                }
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void SetupZonaSubzonasCustomRenderSettings()
        {
            int shapeCount = sfmMainMap.LayerCount;
            if (shapeCount > 1)
            {
                int layerIndex = this.LayerIndex;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(layerIndex);
                    ZonasPartidasCustomRenderSettings crsZ = new ZonasPartidasCustomRenderSettings(sf.RenderSettings, this.CurrentZone.ListaSubzonas);
                    sf.RenderSettings.CustomRenderSettings = null;
                    sf.RenderSettings.CustomRenderSettings = crsZ;
                    layerIndex += 5;
                }
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void LoadPlazaShapes()
        {
            if (this.CurrentPlaza != null)
            {
                string mainFilePath = Server.MapPath("~/Maps/Main.egp");
                string xmlMainContent = File.ReadAllText(mainFilePath);
                string xmlEdosContent = string.Empty;
                foreach (BE.Estado edo in this.CurrentPlaza.ListaEstados)
                {
                    string edoFilePath = Server.MapPath("~/Maps/Estados/" + edo.Id.ToString() + "/" + edo.Id.ToString() + ".egp");
                    xmlEdosContent += File.ReadAllText(edoFilePath) + Environment.NewLine;
                }
                xmlMainContent = xmlMainContent.Replace("|||ReplaceLayers|||", xmlEdosContent);
                try
                {
                    //Se trata de sobreescribir el archivo dEGP para la plaza
                    string urlMap = "~/Maps/" + this.CurrentPlaza.Id.ToString() + ".egp";
                    //string urlMap = "~/Maps/MapDinamic.egp";
                    string plazaFilePath = Server.MapPath(urlMap);
                    File.WriteAllText(plazaFilePath, xmlMainContent);
                    sfmMainMap.ProjectName = urlMap;
                    sfmMainMap.DataBind();
                }
                catch
                {

                }
            }
        }

        private void ZoomToPlaza(BE.Plaza plaza)
        {
            int shapeCount = sfmMainMap.LayerCount;
            if (shapeCount > 1)
            {
                bool isFirstShape = true;
                //Se recorren los datos
                int layerIndex = this.LayerIndex;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(layerIndex);
                    string[] rEdos = sf.GetRecords(1);
                    string[] rMun = sf.GetRecords(2);
                    string[] rCols = sf.GetRecords(7);
                    string[] rLocs = sf.GetRecords(3);
                    string[] rTipo = sf.GetRecords(4);
                    string[] rLocs2 = sf.GetRecords(8);
                    for (int i = 0; i < sf.RecordCount; i++)
                    {
                        //Se saca el Id de la colonia
                        string colString = rCols[i].Replace("|", "").Trim();
                        if (colString == "NA")
                        {
                            colString = rTipo[i].Trim() + rEdos[i].Trim().PadLeft(2, '0') + rMun[i].Trim().PadLeft(3, '0') + rLocs[i].Trim().PadLeft(4, '0') + rLocs2[i].Trim().PadLeft(5, '0');
                        }
                        else
                        {
                            colString = rTipo[i].Trim() + colString;
                        }
                        //Se revisa si la colonia existe en la plaza
                        //BE.Estado currEstado = estados.Where(est => est.Id.ToString() == rEdos[i]).FirstOrDefault();
                        //if (currEstado != null)
                        //{
                        //    BE.Municipio currMuni = currEstado.ListaMunicipios.Where(mun => mun.Id.ToString() == rMun[i]).FirstOrDefault();
                        //    if (currMuni != null)
                        //    {
                                BE.Colonia currCol = plaza.ListaColonias.Where(col => col.Id.ToString() == colString).FirstOrDefault();
                                if (currCol != null && isFirstShape)
                                {
                                    ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(i);
                                    sfmMainMap.CenterPoint = puntos[0][0];
                                    sfmMainMap.Zoom = 3500;
                                    isFirstShape = false;
                                    break;
                                }
                        //    }
                        //}
                    }
                    layerIndex += 5;
                }
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "__doPostBack(\'" + sfmMainMap.ClientID + "\',\'notInfo\');", true);
            }
        }

        private void SetupZonaSubzonasColoniasCustomRenderSettings()
        {
            int shapeCount = sfmMainMap.LayerCount;
            if (shapeCount > 1)
            {
                int layerIndex = this.LayerIndex;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(layerIndex);
                    if (PreviousLevel == "1")
                    {
                        ColoniasPartidasCustomRenderSettings crsZ = new ColoniasPartidasCustomRenderSettings(sf.RenderSettings, this.CurrentSubzone);
                        sf.RenderSettings.CustomRenderSettings = crsZ;
                    }
                    else
                    {
                        ColoniasPartidasCustomRenderSettings crsZ = new ColoniasPartidasCustomRenderSettings(sf.RenderSettings, this.CurrentZone);
                        sf.RenderSettings.CustomRenderSettings = crsZ;
                    }
                    layerIndex += 5;
                }
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }
        #endregion

        #region Formato del Grid (Adicion de botones)
        protected void dgReportTab_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        string value = e.Row.Cells[i].Text.ToString();
                        e.Row.Cells[i].CssClass = "CellText";
                        if (value.StartsWith("|||"))
                        {
                            e.Row.Cells[i].Text = value.Replace("|||", "");
                            e.Row.Cells[i].Font.Bold = true;
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                        }
                        else if (value.StartsWith("Editar"))
                        {
                            string[] textParts = value.Split('|');
                            string button = HttpUtility.HtmlDecode(textParts[1]);
                            e.Row.Cells[i].Text = button;
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                            e.Row.Cells[i].CssClass = "CellButton";
                            e.Row.Cells[i].BackColor = ColorConverter.GetColor(textParts[2]);
                        }
                    }
                }
            }
        }
        #endregion

        #region Seleccion de las Plazas
        protected void ddlPlazas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlazas.SelectedValue != "0" && ddlStartDate.SelectedValue != "0" && ddlEndDate.SelectedValue != "0")
            {                

                //Se carga la nueva plaza
                this.CurrentPlaza = this.ListPlazas.Where(pl => pl.Id.ToString() == ddlPlazas.SelectedValue).FirstOrDefault();
                LoadPlazaShapes();
                //ZoomToPlaza(this.CurrentPlaza.ListaEstados);
                ZoomToPlaza(this.CurrentPlaza);
                //Se cargan las zonas relacionadas con la plaza
                string stDate = ddlStartDate.SelectedValue;
                string edDate = ddlEndDate.SelectedValue;
                DateTime start = Fecha.GetDateTime(ddlStartDate.SelectedItem.Text);
                DateTime end = Fecha.GetDateTime(ddlEndDate.SelectedItem.Text);
                if (start > end)
                {
                    stDate = ddlEndDate.SelectedValue;
                    edDate = ddlStartDate.SelectedValue;
                }
                LoadZonasTab(this.CurrentPlaza.Id.ToString(), stDate, edDate);
                LoadCurrentPlazaRenderSetting();
                PrintCurrentInfo();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);
            }
            else
            {
                LoadMainMap();
                LoadZonasTab("0", "0", "0");
                PrintCurrentInfo();
            }
        }
        #endregion

        #region Seleccion de Zona o Subzona
        protected void btn_Click(object sender, EventArgs e)
        {
            if (CurrentLevel == "0") //De municipio (todas las zonas) a zona
            {
                PreviousLevel = "0";
                this.CurrentZone = ListZonas.Where(z => z.Id == Convert.ToInt32(hdnZonaId.Value)).FirstOrDefault();
                if (this.CurrentZone.ListaSubzonas.Count > 0)
                    CurrentLevel = "1";
                else
                    CurrentLevel = "2";
            }
            else if (CurrentLevel == "1") //De zona a subzona
            {
                PreviousLevel = "1";
                CurrentLevel = "2";
                this.CurrentSubzone = this.CurrentZone.ListaSubzonas.Where(z => z.Id == Convert.ToInt32(hdnSubzonaId.Value)).FirstOrDefault();
            }
            PrintCurrentInfo();
        }

        protected void btnRefreshMap_Click(object sender, EventArgs e)
        {
            if (ddlPlazas.SelectedValue != "0")
            {
                if (CurrentLevel == "0") //De municipio (todas las zonas) a zona
                {
                    SetupZonaSubzonasCustomRenderSettings();
                }
                else if (CurrentLevel == "1" || CurrentLevel == "2") //De zona a subzona
                {
                    SetupZonaSubzonasCustomRenderSettings();
                }
            }
            else
            {
                LoadMainMap();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (CurrentLevel == "1") //De Zona especifica a todas las zonas
            {
                this.CurrentLevel = "0";
                this.CurrentZone = null;
                hdnZonaId.Value = string.Empty;
            }
            else if (CurrentLevel == "2") //De Zubzona a zona
            {
                if (PreviousLevel == "1")
                {
                    this.CurrentLevel = "1";
                    this.CurrentSubzone = null;
                    hdnSubzonaId.Value = string.Empty;
                }
                else
                {
                    this.CurrentLevel = "0";
                    this.CurrentZone = null;
                    hdnZonaId.Value = string.Empty;
                }
            }
            PrintCurrentInfo();
        }
        #endregion
    }
}