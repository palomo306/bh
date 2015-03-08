using BE = BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.BusinessEntities.Cast;
using BHermanos.Zonificacion.WebService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BHermanos.Zonificacion.Web.Clases;
using System.Collections.ObjectModel;


namespace BHermanos.Zonificacion.Web.Modules
{
    public partial class ZoneInfo : System.Web.UI.Page
    {
        #region Campos
        private int LayerCount = 6;
        private int LayerEstados = 0;
        private int LayerMunicipios = 5;
        private int LayerColonias = 2;
        #endregion

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
        #endregion

        #region Carga de Datos
        private void LoadEstados()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Estado/GetEstado?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                EstadoModel objResponse = JsonSerializer.Parse<EstadoModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    List<BE.Estado> lstEstados = objResponse.ListaEstados.ToList();
                    lstEstados.Insert(0, new BE.Estado() { Id = 0, Nombre = "--Seleccione un estado--" });
                    this.cmbEstado.DataSource = lstEstados;
                    this.cmbEstado.DataTextField = "Nombre";
                    this.cmbEstado.DataValueField = "Id";
                    this.cmbEstado.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los estados [" + objResponse.Mensaje.Replace("'", "") + "]');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los estados [" + ex.Message.Replace("'", "") + "]');", true);
            }
        }

        private void LoadMunicipios(string edoId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Municipio/GetMunicipio/" + edoId + "?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                MunicipioModel objResponse = JsonSerializer.Parse<MunicipioModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    List<BE.Municipio> lstMunicipios = objResponse.ListaMunicipios.ToList();
                    lstMunicipios.Insert(0, new BE.Municipio() { Id = 0, Nombre = "--Seleccione un municipio--" });
                    this.cmbMunicipio.DataSource = lstMunicipios;
                    this.cmbMunicipio.DataTextField = "Nombre";
                    this.cmbMunicipio.DataValueField = "Id";
                    this.cmbMunicipio.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los municipios [" + objResponse.Mensaje.Replace("'", "") + "]');", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los municipios [" + ex.Message.Replace("'", "") + "]');", true);
            }
        }

        private void LoadZonasTab(string edoId, string munId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Tab/GetTab/" + TabId + "/" + edoId + "/" + munId + "?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [Hay más de un Tab para el Id --> " + TabId + "]');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + objResponse.Mensaje.Replace("'", "") + "]');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + ex.Message.Replace("'", "") + "]');", true);
            }
        }

        private void PrintCurrentInfo()
        {
            string umbralClass = string.Empty;            
            switch (this.CurrentLevel)
            {
                case "0":
                    //Se saca el reporte
                    DataTable dtReporteZonas = ReportZonesConversor.ToListPartidas(this.ListZonas, this.CurrentLevel);
                    dgReportTab.DataSource = dtReporteZonas;
                    dgReportTab.DataBind();
                    umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.ListZonas);
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
                    dgReportTab.DataSource = dtReporteSubzonas;
                    dgReportTab.DataBind();
                    umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.CurrentZone.ListaSubzonas);
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
                        dgReportTab.DataSource = dtReporteColonias;
                        dgReportTab.DataBind();
                        umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.CurrentSubzone.ListaColonias);
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
                        dgReportTab.DataSource = dtReporteColonias;
                        dgReportTab.DataBind();
                        umbralClass = ReportZonesConversor.ToHtmlUmbrales(this.CurrentZone.ListaColonias);
                        lblColores.Text = umbralClass;
                        //Se colocan los nombres de la subzona actual
                        btnCancel.Visible = true;                        
                        //Se colocan los colores del mapa
                        SetupZonaSubzonasColoniasCustomRenderSettings();
                    }
                    break;
            }
        }

        private void ClearData()
        {
            DataTable dtReporte = new DataTable();
            dgReportTab.DataSource = dtReporte;
            dgReportTab.DataBind();
            lblColores.Text = string.Empty;
            ClearLayerColonias();
        }
        #endregion

        #region Carga de Mapas
        private void LoadMainMap()
        {
            string mapPath = "~/Maps/Nacional/national_estatal.egp";
            sfmMainMap.ProjectName = mapPath;
            sfmMainMap.Zoom = 15;
            sfmMainMap.DataBind();
        }

        private void LoadMap(string edoId)
        {
            string mapPath = "~/Maps/Estados/" + edoId + "/" + edoId + ".egp";
            sfmMainMap.ProjectName = mapPath;
            sfmMainMap.DataBind();
            sfmMainMap.Zoom = sfmMainMap.Zoom;
        }

        private void ZoomToEstado(string estadoId)
        {
            if (this.sfmMainMap.LayerCount == 1)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerEstados);
                string[] r = sf.GetRecords(0);
                int i = 0;
                for (i = 0; i < sf.RecordCount; i++)
                {
                    if (Convert.ToInt32(r[i]) == Convert.ToInt32(estadoId))
                        break;
                }

                ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(i);
                sfmMainMap.CenterPoint = puntos[0][0];
                sfmMainMap.Zoom = 100;
            }
        }

        private void ZoomToMunicipio(string municipioId)
        {
            if (this.sfmMainMap.LayerCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerMunicipios);
                string[] r = sf.GetRecords(2);
                int i = 0;
                for (i = 0; i < sf.RecordCount; i++)
                {
                    if (Convert.ToInt32(r[i]) == Convert.ToInt32(municipioId))
                        break;
                }

                ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(i);
                sfmMainMap.CenterPoint = puntos[0][0];
                sfmMainMap.Zoom = 3500;
            }
        }

        private void SetupMunicipiosCustomRenderSettings()
        {
            if (this.sfmMainMap.LayerCount == LayerCount && cmbMunicipio.SelectedItem != null)
            {
                string municipioId = cmbMunicipio.SelectedValue;
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerMunicipios);
                MunicipioCustomRenderSettings crsZ = new MunicipioCustomRenderSettings(sf.RenderSettings, Convert.ToInt32(municipioId));
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void SetupZonasCustomRenderSettings()
        {
            if (this.sfmMainMap.LayerCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerColonias);
                ZonasPartidasCustomRenderSettings crsZ = new ZonasPartidasCustomRenderSettings(sf.RenderSettings, this.ListZonas);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void SetupZonaSubzonasCustomRenderSettings()
        {
            if (this.sfmMainMap.LayerCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerColonias);
                ZonasPartidasCustomRenderSettings crsZ = new ZonasPartidasCustomRenderSettings(sf.RenderSettings, this.CurrentZone.ListaSubzonas);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void SetupZonaSubzonasColoniasCustomRenderSettings()
        {
            if (this.sfmMainMap.LayerCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerColonias);
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
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void ClearLayerColonias()
        {
            if (this.sfmMainMap.LayerCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerColonias);
                sf.RenderSettings.CustomRenderSettings = null;
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void ClearLayerMunicipios()
        {
            if (this.sfmMainMap.LayerCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerMunicipios);
                sf.RenderSettings.CustomRenderSettings = null;
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }
        #endregion

        #region Carga de la Página
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "SetSplitter();", true);
            if (!Page.IsPostBack)
            {
                cellZonaHead.Visible = false;
                cellSubzonaHead.Visible = false;
                cellZonaName.Visible = false;
                cellSubonaName.Visible = false;
                btnCancel.Visible = false;
                CurrentLevel = "0";
                LoadEstados();
                LoadMunicipios("0");
                LoadZonasTab("0", "0");
                PrintCurrentInfo();                
                LoadMainMap();
            }
        }

        protected void btnRefreshMap_Click(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedValue != "0" && cmbMunicipio.SelectedValue != "0")
            {
                PrintCurrentInfo();
            }
            else
            {
                ClearLayerMunicipios();
                ClearLayerColonias();
            }
        }
        #endregion

        #region Selección de Estado y Municipio
        protected void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearData();
                //Se cargan los municipios
                if (cmbEstado.SelectedValue != "0")
                {
                    LoadMunicipios(cmbEstado.SelectedValue);
                    ZoomToEstado(cmbEstado.SelectedValue);
                    LoadMap(cmbEstado.SelectedValue);
                }
                else
                {
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al seleccionar un estado [" + ex.Message.Replace("'", "") + "]');", true);
            }
        }

        protected void cmbMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se cargan las zonas
            if (cmbEstado.SelectedValue != "0" && cmbMunicipio.SelectedValue != "0")
            {
                LoadZonasTab(cmbEstado.SelectedValue, cmbMunicipio.SelectedValue);
                PrintCurrentInfo();
                SetupMunicipiosCustomRenderSettings();                
                ZoomToMunicipio(cmbMunicipio.SelectedValue);
            }
            else
            {
                ClearData();
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
                        }
                    }
                }
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
        #endregion

        #region Cancelacion de la operacion
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