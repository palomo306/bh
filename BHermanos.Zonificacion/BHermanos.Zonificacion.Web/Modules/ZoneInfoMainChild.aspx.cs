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

namespace BHermanos.Zonificacion.Web.Modules
{
    public partial class ZoneInfoMainChild : System.Web.UI.Page
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


        private DataTable DataTableDetail
        {
            get
            {
                if (ViewState["TableDetail"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["TableDetail"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (DataTable)_formatter.Deserialize(Convert.ToBase64String(bytes));
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
                if (ViewState["TableDetail"] != null)
                    ViewState["TableDetail"] = ValueArrayCompressed;
                else
                    ViewState.Add("TableDetail", ValueArrayCompressed);
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

        private readonly int LayerIndex = 5;

        #endregion

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

        private void LoadZonas(string plazaId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Zona/GetZona/2/" + plazaId + "/0?type=json";
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
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + objResponse.Mensaje + "]');", true);                    
                }
            }
            catch (Exception ex)
            {
                hdnShowBackGroup.Value = "not";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al extraer las zonas [" + ex.Message + "]');", true);
            }
        }

        private void InitializeNewZone()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Zona/GetZona/0/0/0?type=json";
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
                    {
                        hdnShowBackGroup.Value = "not";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al generar una zona nueva [Se obtuvo más de un dato]');", true);
                    }
                }
                else
                {
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al generar una zona nueva [" + objResponse.Mensaje + "]');", true);
                }
            }
            catch (Exception ex)
            {
                hdnShowBackGroup.Value = "not";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al generar una zona nueva [" + ex.Message + "]');", true);
            }
        }

        private void PrintZonas(List<BE.Zona> lstZonas)
        {
            /*Carga y Formato del Grid Principal de Zonas*/
            ReportZonesConversor.Hdn1Name = hdnZonaId.ClientID;
            ReportZonesConversor.Hdn2Name = hdnSubzonaId.ClientID;
            ReportZonesConversor.BtnName = btnSelection.ClientID;
            DataTable dtReporteZonas = ReportZonesConversor.ToGeneralDataTable(CurrentZone, lstZonas, 2, Convert.ToInt32(this.CurrentLevel));
            dgvReportZonas.DataSource = dtReporteZonas;
            dgvReportZonas.DataBind();
            btnAll.Visible = true;
            btnNse.Visible = true;
            if (lstZonas.Count == 0)
            {
                btnAll.Visible = false;
                btnNse.Visible = false;
            }
        }

        private void PrintCurrentZona(BE.Zona zona)
        {
            DataTable dtReporteZonas = ReportZonesConversor.ToZoneDataTable(zona, true);
            dgZone.DataSource = dtReporteZonas;
            dgZone.DataBind();
        }
        #endregion

        #region Carga de la Página
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "InitializeScreen();ResizeMap();", true);
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnPdf);
            //ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExcel);
            hdnShowBackGroup.Value = "cloase";            
            if (!Page.IsPostBack)
            {
                cellZonaHead.Visible = false;
                cellSubzonaHead.Visible = false;
                cellZonaName.Visible = false;
                cellSubonaName.Visible = false;
                btnCancel.Visible = false;
                CurrentLevel = "0";
                LoadPlazas();
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                LoadZonas("0");
                PrintZonas(this.ListZonas);
                LoadMainMap();
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

        private void SetupZonaSubzonasCustomRenderSettings()
        {
            int shapeCount = sfmMainMap.LayerCount;
            if (shapeCount > 1)
            {
                int layerIndex = this.LayerIndex;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.GetLayer(layerIndex);
                    ZonaSubzonasCustomRenderSettings crsZ = new ZonaSubzonasCustomRenderSettings(sf.RenderSettings, this.CurrentZone, this.CurrentPlaza);
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
                string mainFilePath=Server.MapPath("~/Maps/Main.egp");
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

        private void ZoomToPlaza(List<BE.Estado> estados)
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
                        BE.Estado currEstado = estados.Where(est => est.Id.ToString() == rEdos[i]).FirstOrDefault();
                        if (currEstado != null)
                        {
                            BE.Municipio currMuni = currEstado.ListaMunicipios.Where(mun => mun.Id.ToString() == rMun[i]).FirstOrDefault();
                            if (currMuni != null)
                            {
                                BE.Colonia currCol = currMuni.ListaColonias.Where(col => col.Id.ToString() == colString).FirstOrDefault();
                                if (currCol != null && isFirstShape)
                                {
                                    ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(i);
                                    sfmMainMap.CenterPoint = puntos[0][0];
                                    sfmMainMap.Zoom = 3500;
                                    isFirstShape = false;
                                    break;
                                }
                            }
                        }
                    }
                    layerIndex += 5;
                }
                sfmMainMap.Zoom = sfmMainMap.Zoom;
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
                        e.Row.Cells[i].CssClass = "CellText";
                        string[] textParts = e.Row.Cells[i].Text.Split('|');
                        if (value.StartsWith("Editar"))
                        {
                            string button = HttpUtility.HtmlDecode(textParts[1]);
                            string color = textParts[2];
                            e.Row.Cells[i].Text = button;
                            e.Row.Cells[i].BackColor = ColorConverter.GetColor(color);
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                            e.Row.Cells[i].CssClass = "CellButton";
                        }
                        else
                        {
                            string valor = textParts[0];
                            string color = textParts[1];
                            e.Row.Cells[i].Text = valor;
                            //e.Row.Cells[i].BackColor = ColorConverter.GetColor(color);
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                        }
                    }

                }
            }
        }

        protected void dgZone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].CssClass = "CellText";
                    }
                }
            }
        }
        #endregion

        #region Seleccion de las Plazas
        protected void ddlPlazas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlazas.SelectedValue != "0")
            {
                //Se carga la nueva plaza
                this.CurrentPlaza = this.ListPlazas.Where(pl => pl.Id.ToString() == ddlPlazas.SelectedValue).FirstOrDefault();
                LoadPlazaShapes();
                ZoomToPlaza(this.CurrentPlaza.ListaEstados);
                //Se cargan las zonas relacionadas con la plaza
                LoadZonas(this.CurrentPlaza.Id.ToString());
                LoadCurrentPlazaRenderSetting();
                PrintZonas(this.ListZonas);
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
            }
            else
            {
                LoadMainMap();                
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                LoadZonas("0");
                PrintZonas(this.ListZonas);
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
            ZoomToPlaza(this.CurrentPlaza.ListaEstados);
        }

        protected void btnRefreshMap_Click(object sender, EventArgs e)
        {
            if ( ddlPlazas.SelectedValue != "0" )
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
                CurrentLevel = "0";
                hdnZonaId.Value = string.Empty;
                cellZonaHead.Visible = false;
                cellZonaName.Visible = false;
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                PrintZonas(this.ListZonas);
                LoadCurrentPlazaRenderSetting();
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

        #region Detalles
        protected void btnAll_Click(object sender, EventArgs e)
        {
            if ( ddlPlazas.SelectedValue != "0")
            {
                if (ListZonas.Count > 0)
                {
                    this.DataTableDetail = ReportZonesConversor.ToGeneralDataTableAll(CurrentZone, ListZonas);
                    spanTitleDetail.InnerText = "Información General por Zona";
                    gvDetail.DataSource = this.DataTableDetail;
                    gvDetail.DataBind();
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenDetailWindow();", true);
                }
                else
                {
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','No hay Zona para el estado y municipio seleccionado.');", true);
                }
            }
            else
            {
                hdnShowBackGroup.Value = "not";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','Por favor seleccione un estado y municipio.');", true);
            }
        }

        protected void btnNse_Click(object sender, EventArgs e)
        {
            if ( ddlPlazas.SelectedValue != "0")
            {
                if (ListZonas.Count > 0)
                {
                    this.DataTableDetail = ReportZonesConversor.ToGeneralDataTableNse(CurrentZone, ListZonas);
                    spanTitleDetail.InnerText = "Información Socioeconómica por Zona";
                    gvDetail.DataSource = this.DataTableDetail;
                    gvDetail.DataBind();
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenDetailWindow();", true);
                }
                else
                {
                    hdnShowBackGroup.Value = "not";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','No hay Zona para el estado y municipio seleccionado.');", true);
                }
            }
            else
            {
                hdnShowBackGroup.Value = "not";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Validación de datos','Por favor seleccione un estado y municipio.');", true);
            }
        }

        #region Export
        private void ExportToPdf(DataTable Dv, string FilePath, string Plaza)
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
            Phrase pEdoT = new Phrase("Plaza:", myfontHead);
            PdfPCell cellEdoT = new PdfPCell(pEdoT);
            cellEdoT.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
            cellEdoT.HorizontalAlignment = Element.ALIGN_LEFT;
            generalDat.AddCell(cellEdoT);
            Phrase pEdo = new Phrase(Plaza, myfont);
            PdfPCell cellEdo = new PdfPCell(pEdo);
            cellEdo.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            cellEdo.HorizontalAlignment = Element.ALIGN_CENTER;
            generalDat.AddCell(cellEdo);
            Phrase pMunT = new Phrase(" ", myfontHead);
            PdfPCell cellMunT = new PdfPCell(pMunT);
            cellMunT.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
            cellMunT.HorizontalAlignment = Element.ALIGN_LEFT;
            generalDat.AddCell(cellMunT);
            Phrase pMun = new Phrase(" ", myfont);
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

        private void ExportToExcel(DataTable Dv, string FilePath, string Plaza)
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
            range.Value = "Plaza:";
            range = xlWorkSheet.Cells[3, 2];
            range.Font.Size = 12;
            range.Font.Name = "Tahoma";
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            range.Value = Plaza;
            range = xlWorkSheet.Cells[4, 1];
            range.Font.Size = 12;
            range.Font.Name = "Tahoma";
            range.Font.Bold = true;
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            range.Value = " ";
            range = xlWorkSheet.Cells[4, 2];
            range.Font.Size = 12;
            range.Font.Name = "Tahoma";
            range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            range.Value = " ";
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
            string plaza = ddlPlazas.SelectedItem.Text;
            string fecha = DateTime.Now.ToString("ddMMyyyyHHmmsss");
            string urlPath = "/Files/Pdf/Report_" + plaza + "_" + fecha + ".pdf";
            string path = Server.MapPath("~/Files/Pdf/Report_" + plaza + "_" + fecha + ".pdf");
            DataTable dt = this.DataTableDetail;
            //Se genera el reporte
            ExportToPdf(dt, path, plaza);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenUrlNewWindow('" + urlPath + "')", true);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //Se extraen los parametros
            string plaza = ddlPlazas.SelectedItem.Text;
            string fecha = DateTime.Now.ToString("ddMMyyyyHHmmsss");
            string urlPath = "/Files/Excel/Report_" + plaza + "_" + fecha + ".xlsx";
            string path = Server.MapPath("~/Files/Excel/Report_" + plaza + "_" + fecha + ".xlsx");
            DataTable dt = this.DataTableDetail;
            //Se genera el reporte
            ExportToExcel(dt, path, plaza);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenUrlNewWindow('" + urlPath + "')", true);
        }
        #endregion



        #endregion
    }
}