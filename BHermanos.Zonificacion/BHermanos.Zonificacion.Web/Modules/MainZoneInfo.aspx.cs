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
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace BHermanos.Zonificacion.Web.Modules
{
    public partial class MainZoneInfo : System.Web.UI.Page
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los estados [" + objResponse.Mensaje + "]');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los estados [" + ex.Message + "]');", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los municipios [" + objResponse.Mensaje + "]');", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer los datos de los municipios [" + ex.Message + "]');", true);
            }
        }

        private void LoadZonas(string edoId, string munId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Zona/GetZona/2/" + edoId + "/" + munId + "/0?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    ListZonas = objResponse.ListaZonas.ToList();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + objResponse.Mensaje + "]');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + ex.Message + "]');", true);
            }
        }

        private void InitializeNewZone()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Zona/GetZona/0/0/0/0?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    if (objResponse.ListaZonas.Count() == 1)
                    {
                        this.CurrentZone = objResponse.ListaZonas.ToList()[0];
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al generar una zona nueva [Se obtuvo más de un dato]');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al generar una zona nueva [" + objResponse.Mensaje + "]');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al generar una zona nueva [" + ex.Message + "]');", true);
            }
        }

        private void PrintZonas(List<BE.Zona> lstZonas)
        {
            /*Carga y Formato del Grid Principal de Zonas*/
            DataTable dtReporteZonas = ReportZonesConversor.ToGeneralDataTable(CurrentZone, lstZonas, 2, Convert.ToInt32(this.CurrentLevel));
            dgvReportZonas.DataSource = dtReporteZonas;
            dgvReportZonas.DataBind();
            ////Se fija la columna del nombre del rubro
            //dgvReportZonas.Columns[0].HeaderStyle.CssClass = "pinned col1";
            //dgvReportZonas.Columns[0].ItemStyle.CssClass = "pinned col1";
            //dgvReportZonas.Columns[0].HeaderStyle.Width = 180;
            //dgvReportZonas.Columns[0].ItemStyle.Width = 180;            
            ////Se alinean las cifras
            //for (int i = 1; i < dgvReportZonas.Columns.Count; i++)
            //{
            //    dgvReportZonas.Columns[i].HeaderStyle.Width = 75;
            //    dgvReportZonas.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //    dgvReportZonas.Columns[i].ItemStyle.Width = 75;
            //    dgvReportZonas.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            //}
            ///*-------------------------------------------*/
            /*Carga y Formato del Grid Lista de Zonas*/
            DataTable dtListaZonas = ReportZonesConversor.ToListZonesReport(this.ListZonas);
            dgListZones.DataSource = dtListaZonas;
            dgListZones.DataBind();
            /*-------------------------------------------*/
        }

        private void PrintCurrentZona(BE.Zona zona)
        {
            DataTable dtReporteZonas = ReportZonesConversor.ToZoneDataTable(zona);
            dgZone.DataSource = dtReporteZonas;
            dgZone.DataBind();
            ////Se fija la columna del nombre del rubro
            //dgZone.Columns[0].HeaderStyle.CssClass = "pinned col1";
            //dgZone.Columns[0].ItemStyle.CssClass = "pinned col1";
            //dgZone.Columns[0].HeaderStyle.Width = 180;
            //dgZone.Columns[0].ItemStyle.Width = 180;            
            ////Se alinean las cifras
            //for (int i = 1; i < dgZone.Columns.Count; i++)
            //{
            //    dgZone.Columns[i].HeaderStyle.Width = 75;
            //    dgZone.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //    dgZone.Columns[i].ItemStyle.Width = 75;
            //    dgZone.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            //}
        }
        #endregion

        #region Carga de Mapas
        private void LoadMainMap()
        {
            string mapPath = "~/Maps/Nacional/national_estatal.egp";
            sfmMainMap.ProjectName = mapPath;
            sfmMainMap.Zoom = 10;
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
                ZonaCustomRenderSettings crsZ = new ZonaCustomRenderSettings(sf.RenderSettings, this.ListZonas);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        private void SetupZonaSubzonasCustomRenderSettings()
        {
            if (this.sfmMainMap.LayerCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(LayerColonias);
                ZonaSubzonasCustomRenderSettings crsZ = new ZonaSubzonasCustomRenderSettings(sf.RenderSettings, this.CurrentZone);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.Zoom = sfmMainMap.Zoom;
            }
        }

        protected void btnRefreshMap_Click(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedValue != "0" && cmbMunicipio.SelectedValue != "0")
            {
                if (CurrentLevel == "0") //De municipio (todas las zonas) a zona
                {
                    SetupZonasCustomRenderSettings();
                }
                else if (CurrentLevel == "1" || CurrentLevel == "2") //De zona a subzona
                {
                    SetupZonaSubzonasCustomRenderSettings();
                }
            }
            else
            {
                ClearLayerMunicipios();
                ClearLayerColonias();
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
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                LoadZonas("0", "0");
                PrintZonas(this.ListZonas);
                LoadMainMap();
                lblCurrentZonaName.Text = "";
            }
        }
        #endregion

        #region Selección de Estado y Municipio
        protected void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Se limpian los valores
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                LoadZonas("0", "0");
                PrintZonas(this.ListZonas);
                //Se cargan los municipios
                if (cmbEstado.SelectedValue != "0")
                {
                    LoadMunicipios(cmbEstado.SelectedValue);
                    ZoomToEstado(cmbEstado.SelectedValue);
                    LoadMap(cmbEstado.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al seleccionar un estado [" + ex.Message + "]');", true);
            }
        }

        protected void cmbMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se limpian los valores
            InitializeNewZone();
            PrintCurrentZona(this.CurrentZone);
            //Se cargan las zonas
            if (cmbEstado.SelectedValue != "0" && cmbMunicipio.SelectedValue != "0")
            {
                LoadZonas(cmbEstado.SelectedValue, cmbMunicipio.SelectedValue);
                PrintZonas(this.ListZonas);
                SetupMunicipiosCustomRenderSettings();
                SetupZonasCustomRenderSettings();
                ZoomToMunicipio(cmbMunicipio.SelectedValue);
            }
            else
            {
                LoadZonas("0", "0");
                PrintZonas(this.ListZonas);
            }
        }
        #endregion

        #region Formato del Grid (Adicion de botones)
        protected void dgvReportZonas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    string value = e.Row.Cells[1].Text.ToString();

                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        string[] textParts = e.Row.Cells[i].Text.Split('|');
                        if (value.StartsWith("Editar"))
                        {
                            string button = HttpUtility.HtmlDecode(textParts[1]);
                            string color = textParts[2];
                            e.Row.Cells[i].Text = button;
                            e.Row.Cells[i].BackColor = ColorConverter.GetColor(color);
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                        }
                        else
                        {
                            string valor = textParts[0];
                            string color = textParts[1];
                            e.Row.Cells[i].Text = valor;
                            e.Row.Cells[i].BackColor = ColorConverter.GetColor(color);
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
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
                CurrentLevel = "1";
                cellZonaHead.Visible = true;
                cellZonaName.Visible = true;
                this.CurrentZone = ListZonas.Where(z => z.Id == Convert.ToInt32(hdnZonaId.Value)).FirstOrDefault();
                txtCurrentZona.Text = this.CurrentZone.Nombre;
                txtCurrentZona.BackColor = this.CurrentZone.RealColor;
                PrintCurrentZona(this.CurrentZone);
                PrintZonas(this.CurrentZone.ListaSubzonas);
            }
            else if (CurrentLevel == "1") //De zona a subzona
            {
                CurrentLevel = "2";
                cellSubzonaHead.Visible = true;
                cellSubonaName.Visible = true;
                this.CurrentSubzone = this.CurrentZone.ListaSubzonas.Where(z => z.Id == Convert.ToInt32(hdnSubzonaId.Value)).FirstOrDefault();
                txtCurrentSubzona.Text = this.CurrentSubzone.Nombre;
                txtCurrentSubzona.BackColor = this.CurrentSubzone.RealColor;
                PrintCurrentZona(this.CurrentSubzone);
                PrintZonas(new List<BE.Zona>());
            }
            btnCancel.Visible = true;
            SetupZonaSubzonasCustomRenderSettings();
        }
        #endregion

        #region Cancelacion de la operacion
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (CurrentLevel == "1") //De Zona especifica a todas las zonas
            {
                CurrentLevel = "0";
                hdnZonaId.Value = string.Empty;
                cellZonaHead.Visible = false;
                cellZonaName.Visible = false;
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                PrintZonas(this.ListZonas);
                SetupZonasCustomRenderSettings();
                btnCancel.Visible = false;
            }
            else if (CurrentLevel == "2") //De Zubzona a zona
            {
                CurrentLevel = "1";
                cellSubzonaHead.Visible = false;
                cellSubonaName.Visible = false;
                this.CurrentSubzone = null;
                hdnSubzonaId.Value = string.Empty;
                PrintCurrentZona(this.CurrentZone);
                PrintZonas(this.CurrentZone.ListaSubzonas);
                SetupZonaSubzonasCustomRenderSettings();
                btnCancel.Visible = true;
            }
        }
        #endregion

        #region Funcionalidad del segundo tab
        protected void dgListZones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string zonaId = e.CommandArgument.ToString();
            BE.Zona selZona = this.ListZonas.Where(z => z.Id.ToString() == zonaId).FirstOrDefault();
            if (selZona != null)
            {
                lblCurrentZonaName.Text = selZona.Nombre;
                DataTable dtDetailZona = ReportZonesConversor.ToZoneDetailDataTable(selZona);
                sgReportZoneSingle.DataSource = dtDetailZona;
                sgReportZoneSingle.DataBind();
            }
        }
        #endregion

        #region Detalles
        protected void btnAll_Click(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedValue != "0" && cmbMunicipio.SelectedValue != "0")
            {
                if (ListZonas.Count > 0)
                {
                    DataTable dtInformationAll = ReportZonesConversor.ToGeneralDataTableAll(CurrentZone, ListZonas);
                    spanTitleDetail.InnerText = "Información General por Zona";
                    gvDetail.DataSource = dtInformationAll;
                    gvDetail.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenDetailWindow();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','No hay Zona para el estado y municipio seleccionado.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','Por favor seleccione un estado y municipio.');", true);
            }
        }

        protected void btnNse_Click(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedValue != "0" && cmbMunicipio.SelectedValue != "0")
            {
                if (ListZonas.Count > 0)
                {
                    DataTable dtInformationAll = ReportZonesConversor.ToGeneralDataTableNse(CurrentZone, ListZonas);
                    spanTitleDetail.InnerText = "Información Socioeconómica por Zona";
                    gvDetail.DataSource = dtInformationAll;
                    gvDetail.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenDetailWindow();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','No hay Zona para el estado y municipio seleccionado.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','Por favor seleccione un estado y municipio.');", true);
            }
        }

        #region Export
        private void ExportToPdf(DataTable Dv, string FilePath, string Estado, string Municipio)
        {
            FontFactory.RegisterDirectories();
            iTextSharp.text.Font myfont = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font myfontHead = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE);
            iTextSharp.text.Font myfontTitle = FontFactory.GetFont("Tahoma", BaseFont.IDENTITY_H, 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 2f, 2f, 2f, 2f);

            pdfDoc.Open();
            PdfWriter wri = PdfWriter.GetInstance(pdfDoc, new FileStream(FilePath, FileMode.Create));
            pdfDoc.Open();

            PdfPTable generalDat = new PdfPTable(4);
            Phrase pTitle = new Phrase(this.Title, myfontTitle);
            PdfPCell cellTitle = new PdfPCell(pTitle);
            cellTitle.Colspan = 4;
            cellTitle.Padding = 2;
            cellTitle.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTitle.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            generalDat.AddCell(cellTitle);

            Phrase pBlank0 = new Phrase("    ", myfont);
            PdfPCell cellBlank0 = new PdfPCell(pBlank0);
            cellBlank0.Colspan = 4;
            cellBlank0.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellBlank0);
            Phrase pEdoT = new Phrase("Estado:", myfontHead);
            PdfPCell cellEdoT = new PdfPCell(pEdoT);
            cellEdoT.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
            cellEdoT.HorizontalAlignment = Element.ALIGN_LEFT;
            generalDat.AddCell(cellEdoT);
            Phrase pEdo = new Phrase(Estado, myfont);
            PdfPCell cellEdo = new PdfPCell(pEdo);
            cellEdo.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            cellEdo.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellEdo);
            Phrase pMunT = new Phrase("Municipio:", myfontHead);
            PdfPCell cellMunT = new PdfPCell(pMunT);
            cellMunT.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
            cellMunT.HorizontalAlignment = Element.ALIGN_LEFT;
            generalDat.AddCell(cellMunT);
            Phrase pMun = new Phrase(Municipio, myfont);
            PdfPCell cellMun = new PdfPCell(pMun);
            cellMun.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            cellMun.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellMun);
            Phrase pBlank = new Phrase("    ", myfont);
            PdfPCell cellBlank = new PdfPCell(pBlank);
            cellBlank.Colspan = 4;
            cellBlank.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellBlank);


            PdfPTable _mytable = new PdfPTable(Dv.Columns.Count);
            for (int j = 0; j < Dv.Columns.Count; ++j)
            {
                Phrase p = new Phrase(Dv.Columns[j].ColumnName, myfontHead);
                PdfPCell cell = new PdfPCell(p);
                cell.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                _mytable.AddCell(cell);
            }
            //-------------------------
            for (int i = 0; i < Dv.Rows.Count - 1; ++i)
            {
                for (int j = 0; j < Dv.Columns.Count; ++j)
                {
                    Phrase p = new Phrase(Dv.Rows[i][j].ToString(), myfont);
                    PdfPCell cell = new PdfPCell(p);
                    if (j != 0)
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    else
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    if (i % 2 == 1)
                        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                    _mytable.AddCell(cell);
                }
            }
            //------------------------  
            pdfDoc.Add(generalDat);
            pdfDoc.Add(_mytable);
            pdfDoc.Close();
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;

            }
            finally
            {
                GC.Collect();
            }
        }

        private void ExportToExcel(DataTable Dv, string FilePath, string Estado, string Municipio)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int i = 0;
            int j = 0;

            Microsoft.Office.Interop.Excel.Range range = xlWorkSheet.Cells[1, 1];
            range.Font.Size = 16;
            range.Font.Name = "Tahoma";
            range.Font.Bold = true;
            range.Value = this.Title;

            range = xlWorkSheet.Cells[3, 1];
            range.Font.Size = 12;
            range.Font.Name = "Tahoma";
            range.Font.Bold = true;
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            range.Value = "Estado:";
            range = xlWorkSheet.Cells[3, 2];
            range.Font.Size = 12;
            range.Font.Name = "Tahoma";
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            range.Value = Estado;
            range = xlWorkSheet.Cells[4, 1];
            range.Font.Size = 12;
            range.Font.Name = "Tahoma";
            range.Font.Bold = true;
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            range.Value = "Municipio:";
            range = xlWorkSheet.Cells[4, 2];
            range.Font.Size = 12;
            range.Font.Name = "Tahoma";
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            range.Value = Municipio;
            for (j = 0; j < Dv.Columns.Count; ++j)
            {
                range = xlWorkSheet.Cells[i + 6, j + 1];
                range.Value = Dv.Columns[j].ColumnName;
                range.Font.Size = 12;
                range.Font.Name = "Tahoma";
                range.Font.Bold = true;
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            }
            for (i = 0; i <= Dv.Rows.Count - 1; i++)
            {
                for (j = 0; j <= Dv.Columns.Count - 1; j++)
                {
                    range = xlWorkSheet.Cells[i + 7, j + 1];
                    range.Font.Size = 12;
                    range.Font.Name = "Tahoma";
                    if (i % 2 == 1)
                        range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    range.Value = Dv.Rows[i][j].ToString();
                    range.EntireColumn.AutoFit();
                }
            }
            xlWorkBook.SaveAs(FilePath);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            System.Diagnostics.Process.Start(FilePath);
        }


        protected void btnPdf_Click(object sender, EventArgs e)
        {
            //Se extraen los parametros
            string estado = cmbEstado.SelectedItem.Text;
            string municipio = cmbMunicipio.SelectedItem.Text;
            string fecha = DateTime.Now.ToString("ddMMyyyyHHmmsss");
            string path = Server.MapPath("~/Files/Pdf/Report_" + estado + "_" + municipio + "_" + fecha);
            DataTable dt = (DataTable)gvDetail.DataSource;
            //Se genera el reporte
            ExportToPdf(dt, path, estado, municipio);
            //Se descarga mediante el response
            Response.TransmitFile(path);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //Se extraen los parametros
            string estado = cmbEstado.SelectedItem.Text;
            string municipio = cmbMunicipio.SelectedItem.Text;
            string fecha = DateTime.Now.ToString("ddMMyyyyHHmmsss");
            string path = Server.MapPath("~/Files/Pdf/Report_" + estado + "_" + municipio + "_" + fecha);
            DataTable dt = (DataTable)gvDetail.DataSource;
            //Se genera el reporte
            ExportToExcel(dt, path, estado, municipio);
            //Se descarga mediante el response
            Response.TransmitFile(path);
        }
        #endregion
  
        #endregion
    }
}