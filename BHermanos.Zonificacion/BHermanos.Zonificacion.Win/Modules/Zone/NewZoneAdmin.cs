using BE = BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.BusinessEntities.Cast;
using BHermanos.Zonificacion.WebService.Models;
using BHermanos.Zonificacion.Win.Clases.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EGIS.ShapeFileLib;
using BHermanos.Zonificacion.Win.Modules.Plaza.Modal;
using RegionDemo.Clases;
using System.Collections.ObjectModel;
using BHermanos.Zonificacion.Win.Modules.Zone.Modal;

namespace BHermanos.Zonificacion.Win.Modules.Zone
{
    public partial class NewZoneAdmin : Form
    {
        #region Propiedades
        private string LocalPath { get; set; }
        private List<BE.Plaza> ListPlazas { get; set; }
        private BE.Plaza CurrentPlaza { get; set; }
        private bool IsFirsTime;
        private List<BE.Zona> ListZonas { get; set; }
        List<BE.Zona> ListZonasReport { get; set; }
        private BE.Zona CurrentZone { get; set; }
        private BE.Zona CopyCurrentZone { get; set; }
        private BE.Zona CurrentSubzone { get; set; }
        private BE.Zona CopyCurrentSubzone { get; set; }
        private int LevelUpdate { get; set; }
        private bool updateZoomLevel = true;
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
                    ListPlazas = objResponse.ListaPlazas.ToList();
                    cmbPlazas.DataSource = ListPlazas;
                    cmbPlazas.DisplayMember = "Nombre";
                    cmbPlazas.SelectedItem = null;
                    cmbPlazas.Text = "--Seleccione una Plaza--";
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
                        this.CopyCurrentZone = (BE.Zona)this.CurrentZone.Clone();
                        btnSaveZone.Text = "Crear Zona";
                        lblCurrentZonaName.Text = "Zona Nueva";
                        sfmMainMap.CtrlDown = true;
                        pbbZoomSelect.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error al generar una zona nueva [Se obtuvo más de un dato]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al generar una zona nueva [" + objResponse.Mensaje + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al generar una zona nueva [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeNewSubzone()
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
                        this.CurrentSubzone = objResponse.ListaZonas.ToList()[0];
                        btnSaveZone.Text = "Crear Subzona";
                        lblCurrentZonaName.Text = "Subzona Nueva";
                        sfmMainMap.CtrlDown = false;
                        pbbZoomSelect.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                        MessageBox.Show("Ha ocurrido un error al generar una zona nueva [Se obtuvo más de un dato]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al generar una zona nueva [" + objResponse.Mensaje + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al generar una zona nueva [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadZonas()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Zona/GetZona/2/" + this.CurrentPlaza.Id.ToString() + "/0?type=json";
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
                    MessageBox.Show("Ha ocurrido un error al extraer las zonas [" + objResponse.Mensaje + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer las zonas [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintZonas(List<BE.Zona> lstZonas)
        {
            /*Carga y Formato del Grid Principal de Zonas*/
            DataTable dtReporteZonas = ReportZonesConversor.ToGeneralDataTable(CurrentZone, lstZonas, 1, 0);
            ListZonasReport = lstZonas;
            dgvReportZonas.DataSource = dtReporteZonas;
            //Se fija la columna del nombre del rubro
            dgvReportZonas.Columns[0].Frozen = true;
            dgvReportZonas.Columns[0].Width = 180;
            dgvReportZonas.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;
            //Se alinean las cifras
            for (int i = 1; i < dgvReportZonas.Columns.Count; i++)
            {
                dgvReportZonas.Columns[i].Width = 75;
                dgvReportZonas.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            /*-------------------------------------------*/

            /*Carga y Formato del Grid Lista de Zonas*/
            DataTable dtListaZonas = ReportZonesConversor.ToListZonesReport(this.ListZonas);
            dgListZones.DataSource = dtListaZonas;
            /*-------------------------------------------*/
        }

        private void PrintCurrentZona(BE.Zona zona)
        {
            DataTable dtReporteZonas = ReportZonesConversor.ToZoneDataTable(zona);
            dgZone.DataSource = dtReporteZonas;
            //Se fija la columna del nombre del rubro
            dgZone.Columns[0].Frozen = true;
            dgZone.Columns[0].Width = 180;
            dgZone.Columns[0].DefaultCellStyle.BackColor = SystemColors.Control;
            //Se alinean las cifras
            for (int i = 1; i < dgZone.Columns.Count; i++)
            {
                dgZone.Columns[i].Width = 75;
                dgZone.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private BE.Colonia GetNewCol(string colId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Colonia/GetColonia/2/" + this.CurrentPlaza.Id.ToString() + "/" + colId + "?type=json";  
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ColoniaModel objResponse = JsonSerializer.Parse<ColoniaModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    if (objResponse.ListaColonias.Count() == 1)
                    {
                        BE.Colonia col = objResponse.ListaColonias.ToList()[0];
                        return col;
                    }
                    else
                        throw new Exception("Ha ocurrido un error al extraer los datos de la colonia [Se obtuvo más de un dato]");
                }
                else
                {
                    throw new Exception("Ha ocurrido un error al extraer los datos de la colonia [" + objResponse.Mensaje + "]");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error al extraer los datos de la colonia [" + ex.Message + "]");
            }
        }
        #endregion

        #region Almacenamiento de la informacion
        private void SaveZona()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Zona/PostZona?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "POST";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentZone.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    BE.Plaza selPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                    LoadZonas();
                    PrintZonas(this.ListZonas);
                    ClearSelectedRecords();
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
                    LoadCurrentPlazaRenderSetting();
                    MessageBox.Show("Se ha almacenado correctamente la zona.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guardar la zona [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar la zona [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteZona()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Zona/DeleteZona/1/" + this.CurrentPlaza.Id.ToString() + "/" + this.CurrentZone.Id.ToString() + "?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "DELETE";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentZone.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    BE.Plaza selPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                    LoadZonas();
                    PrintZonas(this.ListZonas);
                    ClearSelectedRecords();
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
                    LoadCurrentPlazaRenderSetting();
                    MessageBox.Show("Se ha eliminado correctamente la zona.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al eliminar la zona [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar la zona [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateZona()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Zona/PutZona/1?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "PUT";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentZone.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    BE.Plaza selPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                    LoadZonas();
                    PrintZonas(this.ListZonas);
                    ClearSelectedRecords();
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
                    LoadCurrentPlazaRenderSetting();
                    MessageBox.Show("Se ha almacenado correctamente la zona.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guardar la zona [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar la zona [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveSubzona()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Zona/PostZona/" + this.CurrentZone.Id.ToString() + "?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "POST";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentSubzone.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    BE.Plaza selPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                    LoadZonas();
                    this.CurrentZone = this.ListZonas.Where(z => z.Id == this.CurrentZone.Id).FirstOrDefault();
                    this.CopyCurrentZone = (BE.Zona)this.CurrentZone;
                    ClearSelectedRecords();
                    SelectShapesZona(this.CurrentZone);
                    PrintZonas(this.CurrentZone.ListaSubzonas);
                    InitializeNewSubzone();
                    PrintCurrentZona(this.CurrentSubzone);
                    MessageBox.Show("Se ha almacenado correctamente la subzona.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guardar la zona [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar la zona [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSubzona()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Zona/PutZona/2?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "PUT";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentSubzone.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    BE.Plaza selPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                    LoadZonas();
                    this.CurrentZone = this.ListZonas.Where(z => z.Id == this.CurrentZone.Id).FirstOrDefault();
                    this.CopyCurrentZone = (BE.Zona)this.CurrentZone;
                    ClearSelectedRecords();
                    SelectShapesZona(this.CurrentZone);
                    PrintZonas(this.CurrentZone.ListaSubzonas);
                    InitializeNewSubzone();
                    PrintCurrentZona(this.CurrentSubzone);
                    MessageBox.Show("Se ha almacenado correctamente la subzona.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guardar la zona [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar la zona [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSubzona()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Zona/DeleteZona/2/" + this.CurrentPlaza.Id.ToString() + "/" + this.CurrentSubzone.Id.ToString() + "?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "DELETE";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentZone.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                ZonaModel objResponse = JsonSerializer.Parse<ZonaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    BE.Plaza selPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                    LoadZonas();
                    this.CurrentZone = this.ListZonas.Where(z => z.Id == this.CurrentZone.Id).FirstOrDefault();
                    this.CopyCurrentZone = (BE.Zona)this.CurrentZone;
                    ClearSelectedRecords();
                    SelectShapesZona(this.CurrentZone);
                    PrintZonas(this.CurrentZone.ListaSubzonas);
                    InitializeNewSubzone();
                    PrintCurrentZona(this.CurrentSubzone);
                    MessageBox.Show("Se ha eliminado correctamente la subzona.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al eliminar la subzona [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar la subzona [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Manejo de los Mapas
        private void LoadMainMap()
        {
            string mapPath = LocalPath + @"\Maps\Nacional\national_estatal.shp";
            ShapeFile f = this.sfmMainMap.AddShapeFile(mapPath, "México", "NOMBRE");
            this.sfmMainMap.ZoomLevel = 15;
        }

        private void LoadMap(string edoId, string mapFileName, string mapName, string fieldName, bool isSelectable, bool fillInterior, int transparency)
        {
            string mapPath = LocalPath + @"\Maps\Estados\" + edoId + @"\" + mapFileName + ".shp";
            EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.AddShapeFile(mapPath, mapName, fieldName);
            sf.RenderSettings.IsSelectable = isSelectable;
            sf.RenderSettings.FillInterior = fillInterior;
            sf.RenderSettings.FillColor = Color.FromArgb(transparency, sf.RenderSettings.FillColor);
            sf.RenderSettings.OutlineColor = sf.RenderSettings.OutlineColor;
            sf.RenderSettings.MinZoomLevel = 15;
            sf.RenderSettings.SelectFillColor = Color.FromArgb(0, 55, 33, 22);
            sf.RenderSettings.SelectOutlineColor = Color.DarkRed;
        }      

        private void LoadCurrentPlazaRenderSetting()
        {
            int shapeCount = sfmMainMap.ShapeFileCount;
            if (shapeCount > 1)
            {
                //Lista de control
                List<BE.Colonia> lstSelColonias = new List<BE.Colonia>();
                //Se recorren los datos
                int layerIndex = 2;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[layerIndex];
                    SinglePlazaCustomRenderSettings crsPl = new SinglePlazaCustomRenderSettings(sf.RenderSettings, this.CurrentPlaza, this.ListZonas);
                    sf.RenderSettings.CustomRenderSettings = crsPl;
                    layerIndex += 5;
                }
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }


        private void SetupZonaSubzonasCustomRenderSettings()
        {
            int shapeCount = sfmMainMap.ShapeFileCount;
            if (shapeCount > 1)
            {
                int layerIndex = 2;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[layerIndex];
                    ZonaSubzonasCustomRenderSettings crsZ = new ZonaSubzonasCustomRenderSettings(sf.RenderSettings, this.CurrentZone);
                    sf.RenderSettings.CustomRenderSettings = null;
                    sf.RenderSettings.CustomRenderSettings = crsZ;
                    sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
                    layerIndex += 5;
                }
            }
        }

        private void LoadMapsByEdo(BE.Estado selEdo)
        {
            LoadMap(selEdo.Id.ToString(), "Estado.shp", "edo" + selEdo.Id.ToString(), "NombreEsta", false, false, 0);
            LoadMap(selEdo.Id.ToString(), "Colonias.shp", "Colonias" + "edo" + selEdo.Id.ToString(), "Nombre", true, true, 0);
            LoadMap(selEdo.Id.ToString(), "Carreteras.shp", "Carreteras" + "edo" + selEdo.Id.ToString(), "Nombre", false, false, 60);
            LoadMap(selEdo.Id.ToString(), "Calles.shp", "Calles" + "edo" + selEdo.Id.ToString(), "Nombre", false, false, 60);
            LoadMap(selEdo.Id.ToString(), "Municipios.shp", "Municipios" + "edo" + selEdo.Id.ToString(), "NombreMuni", false, false, 0);
        }

        private void RemoveMapsByEdo(BE.Estado selEdo)
        {
            List<int> indexToRemove = new List<int>();
            for (int i = 1; i < this.sfmMainMap.ShapeFileCount; i++)
            {
                if (this.sfmMainMap[i].Name.EndsWith("edo" + selEdo.Id.ToString()))
                {
                    indexToRemove.Add(i);
                }
            }
            indexToRemove = indexToRemove.OrderByDescending(i => i).ToList();
            foreach (int index in indexToRemove)
            {
                this.sfmMainMap.RemoveShapeFile(this.sfmMainMap[index]);
            }
            this.sfmMainMap.ZoomLevel = 15;
        }

        private void LoadPlazaShapes()
        {
            if (this.CurrentPlaza != null)
            {
                foreach (BE.Estado edo in this.CurrentPlaza.ListaEstados)
                {
                    LoadMapsByEdo(edo);
                }                
            }
        }

        private void RemovePlazaShapes()
        {
            if (this.CurrentPlaza != null)
            {
                foreach (BE.Estado edo in this.CurrentPlaza.ListaEstados)
                {
                    RemoveMapsByEdo(edo);
                }
            }
        }

        private void ClearSelectedRecords()
        {
            int shapeCount = sfmMainMap.ShapeFileCount;
            if (shapeCount > 1)
            {
                //Se recorren los datos
                int layerIndex = 2;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[layerIndex];
                    sf.ClearSelectedRecords();
                    layerIndex += 5;
                }
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }

        private void SelectShapesZona(BE.Zona zona)
        {
            int shapeCount = sfmMainMap.ShapeFileCount;
            if (shapeCount > 1)
            {
                bool isFirstShape = true;
                int layerIndex = 2;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[layerIndex];
                    string[] rEdos = sf.GetRecords(1);
                    string[] rMun = sf.GetRecords(2);
                    string[] rCols = sf.GetRecords(7);
                    string[] rLocs = sf.GetRecords(3);
                    string[] rTipo = sf.GetRecords(4);
                    string[] rLocs2 = sf.GetRecords(8);
                    int i = 0, lastIndex = 0;
                    for (i = 0; i < sf.RecordCount; i++)
                    {
                        List<string> lstColsIds = zona.ListaColonias.Select(c => c.Id.ToString()).ToList();
                        string colString = rCols[i].Replace("|", "").Trim();
                        if (colString == "NA")
                        {
                            colString = rTipo[i].Trim() + rEdos[i].Trim().PadLeft(2, '0') + rMun[i].Trim().PadLeft(3, '0') + rLocs[i].Trim().PadLeft(4, '0') + rLocs2[i].Trim().PadLeft(5, '0');
                        }
                        else
                        {
                            colString = rTipo[i].Trim() + colString;
                        }
                        if (lstColsIds.Contains(colString))
                        {
                            lastIndex = i;
                            sf.SelectRecord(i, true);
                            if (isFirstShape)
                            {
                                ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(i);
                                sfmMainMap.SetZoomAndCentre(3500, puntos[0][0]);
                                isFirstShape = false;
                            }
                        }
                    }
                    layerIndex += 5;
                }
            }
        }

        private void ZoomToPlaza(List<BE.Estado> estados)
        {
            int shapeCount = sfmMainMap.ShapeFileCount;
            if (shapeCount > 1)
            {
                bool isFirstShape = true;
                //Se recorren los datos
                int layerIndex = 2;
                while (layerIndex < shapeCount)
                {
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[layerIndex];
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
                                if (currCol != null)
                                {

                                    ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(i);
                                    sfmMainMap.SetZoomAndCentre(3500, puntos[0][0]);
                                    isFirstShape = false;

                                    break;
                                }
                            }
                        }
                    }
                    layerIndex += 5;
                }
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }
        #endregion

        #region Constructor
        public NewZoneAdmin()
        {
            InitializeComponent();
            IsFirsTime = true;
            LocalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            InitializeNewZone();
            PrintCurrentZona(this.CurrentZone);
            LoadPlazas();
            LoadMainMap();
            IsFirsTime = false;
        }
        #endregion

        #region Selección de las colonias
        private void ChangeZonaSelections()
        {
            try
            {
                int shapeCount = sfmMainMap.ShapeFileCount;
                if (shapeCount > 1)
                {
                    string nameLevel = LevelUpdate == 0 || LevelUpdate == 1 ? "Zona" : "Subzona";
                    BE.Zona zoneLevel = LevelUpdate == 0 || LevelUpdate == 1 ? this.CurrentZone : this.CurrentSubzone;
                    //Lista de control
                    List<BE.Colonia> lstSelColonias = new List<BE.Colonia>();
                    //Se recorren los datos
                    int layerIndex = 2;
                    while (layerIndex < shapeCount)
                    {
                        //Se obtiene el layer
                        EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[layerIndex];
                        //Se sacan los elementos seleccionados y se recorren
                        List<int> lstSelected = sf.SelectedRecordIndices.ToList();
                        foreach (int selIndex in lstSelected)
                        {
                            //Se leen los valores
                            string[] values = sf.GetAttributeFieldValues(selIndex);
                            //Se obtienen los ID's
                            int estadoId = Convert.ToInt32(values[1]);
                            int municipioId = Convert.ToInt32(values[2]);
                            double coldId = 0;
                            string colString = values[7].Replace("|", "").Trim();
                            if (colString == "NA")
                            {
                                colString = values[4].Trim() + values[1].Trim().PadLeft(2, '0') + values[2].Trim().PadLeft(3, '0') + values[3].Trim().PadLeft(4, '0') + values[8].Trim().PadLeft(5, '0');
                            }
                            else
                            {
                                colString = values[4].Trim() + colString;
                            }
                            coldId = Convert.ToDouble(colString);
                            //Se revisa si se han seleccionado colonias fuera de la plaza actual
                            if (ExistInPlaza(estadoId, municipioId, coldId))
                            {
                                if (ExistInZona(estadoId, municipioId, coldId))
                                {
                                    //Se busca en la plaza si existen esa colonia seleccionada para otra plaza
                                    if (!ExistPrevios(estadoId, municipioId, coldId))
                                    {
                                        BE.Colonia oNewColonia = new BE.Colonia();
                                        oNewColonia.EstadoId = estadoId;
                                        oNewColonia.MunicipioId = municipioId;
                                        oNewColonia.Id = coldId;
                                        oNewColonia.Nombre = values[0];
                                        lstSelColonias.Add(oNewColonia);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Se han seleccionado colonias que pertenecen a otra " + nameLevel, "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        sf.SelectRecord(selIndex, false);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Se han seleccionado colonias que NO pertenecen a la zona seleccionada [" + this.CurrentPlaza.Nombre + "]", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    sf.SelectRecord(selIndex, false);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Se han seleccionado colonias que NO pertenecen a la plaza seleccionada [" + this.CurrentPlaza.Nombre + "]", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sf.SelectRecord(selIndex, false);
                            }
                        }
                        layerIndex += 5;
                    }
                    //Se actualiza la zona actual
                    List<double> lstCurrentCols = zoneLevel.ListaColonias.Select(zl => zl.Id).ToList();
                    //Se eliminan las colonias no necesarias
                    foreach (double colId in lstCurrentCols)
                    {
                        BE.Colonia colForDelete = lstSelColonias.Where(c => c.Id == colId).FirstOrDefault();
                        if (colForDelete == null)
                        {
                            BE.Colonia delCol = zoneLevel.ListaColonias.Where(c => c.Id == colId).FirstOrDefault();
                            if (delCol != null && delCol.Id != 0)
                                zoneLevel.ListaColonias.Remove(delCol);
                        }
                    }
                    //Se agregan las nuevas colonias seleccionadas
                    foreach (BE.Colonia newCol in lstSelColonias)
                    {
                        if (!lstCurrentCols.Contains(newCol.Id))
                        {
                            BE.Colonia newColWithData = GetNewCol(newCol.Id.ToString());
                            zoneLevel.ListaColonias.Add(newColWithData);
                        }
                    }
                    PrintCurrentZona(zoneLevel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ExistPrevios(int edoId, int munId, double colId)
        {
            bool result = false;
            List<BE.Zona> lstAuxZonas = null;
            if (LevelUpdate == 0 || LevelUpdate == 1)
                lstAuxZonas = this.ListZonas.Where(z => z.Id != this.CurrentZone.Id).ToList();
            else
                lstAuxZonas = this.CurrentZone.ListaSubzonas.Where(sz => sz.Id != this.CurrentSubzone.Id).ToList();

            foreach (BE.Zona zn in lstAuxZonas)
            {
                List<double> lstColIds = zn.ListaColonias.Select(c => c.Id).ToList();
                if (lstColIds.Contains(colId))
                    return true;
            }
            return result;
        }


        private bool ExistInPlaza(int edoId, int munId, double colId)
        {
            bool result = false;
            foreach (BE.Estado edo in this.CurrentPlaza.ListaEstados)
            {
                foreach (BE.Municipio mun in edo.ListaMunicipios)
                {
                    foreach (BE.Colonia col in mun.ListaColonias)
                    {
                        if (edo.Id == edoId && mun.Id == munId && col.Id == colId)
                            return true;
                    }
                }
            }
            return result;
        }

        private bool ExistInZona(int edoId, int munId, double colId)
        {
            bool result = true;
            if (LevelUpdate > 1)
                result = this.CurrentZone.ListaColonias.Where(c => c.Id == colId).Any();
            return result;
        }

        private void sfmMainMap_SelectedRecordsChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeZonaSelections();                
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer los datos de las colonias [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Selección de las Plazas
        private void cmbPlazas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsFirsTime)
            {
                if (cmbPlazas.SelectedItem != null)
                {
                    //Se limpian las selecciones actuales
                    ClearSelectedRecords();
                    BE.Plaza auxPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                    if (this.CurrentPlaza != null && auxPlaza != this.CurrentPlaza)
                    {
                        //Se remueven los shapes de la Plaza Seleccionada
                        RemovePlazaShapes();
                    }
                    //Se cargan los Shapes de la Plaza Actual
                    this.CurrentPlaza = auxPlaza;
                    LoadPlazaShapes();
                    ZoomToPlaza(this.CurrentPlaza.ListaEstados);
                    //Se cargan las zonas relacionadas con la plaza
                    LoadZonas();
                    LoadCurrentPlazaRenderSetting();
                    PrintZonas(this.ListZonas);
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
                    SetUpdateLevel(0);
                    sfmMainMap.CtrlDown = false;
                    sfmMainMap.Focus();
                }
                else
                {
                    ClearSelectedRecords();
                    RemovePlazaShapes();
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
                    LoadZonas();
                    PrintZonas(this.ListZonas);
                    SetUpdateLevel(-1);
                }
            }            
        }
        #endregion

        #region Menejo del Panel de Zoom
        private void pbbZoomSelect_Click(object sender, EventArgs e)
        {
            if (sfmMainMap.CtrlDown)
            {
                sfmMainMap.CtrlDown = false;
            }
            else
            {
                sfmMainMap.CtrlDown = true;
            }
        }

        private void pbbZoomMin_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox currentControl = (PictureBox)sender;
            currentControl.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pbbZoomMin_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox currentControl = (PictureBox)sender;
            currentControl.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pbbZoomMin_Click(object sender, EventArgs e)
        {
            if (trbZoom.Value >= 385)
            {
                trbZoom.Value = trbZoom.Value - 350;
            }
        }

        private void pbbZoomMax_Click(object sender, EventArgs e)
        {
            if (trbZoom.Value <= 499650)
            {
                trbZoom.Value = trbZoom.Value + 350;
            }
        }

        private void sfmMainMap_ZoomLevelChanged(object sender, EventArgs e)
        {
            if (updateZoomLevel)
            {
                updateZoomLevel = false;
                int zoomValue = Convert.ToInt32(sfmMainMap.ZoomLevel);
                if (zoomValue >= 15 && zoomValue < 500000)
                    trbZoom.Value = zoomValue;
            }
            updateZoomLevel = true;
        }

        private void trbZoom_Scroll(object sender, EventArgs e)
        {

        }

        private void trbZoom_ValueChanged(object sender, EventArgs e)
        {
            if (updateZoomLevel)
            {
                updateZoomLevel = false;
                sfmMainMap.ZoomLevel = trbZoom.Value;
            }
            updateZoomLevel = true;
        }

        private void sfmMainMap_OnControlKeyChange(object sender, EventArgs e)
        {
            if (sfmMainMap.CtrlDown)
            {
                pbbZoomSelect.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                pbbZoomSelect.BorderStyle = BorderStyle.FixedSingle;
            }
        }
        #endregion

        #region Cambio en el Nivel de Edición
        private void SetUpdateLevel(int level)
        {
            LevelUpdate = level;
            switch (LevelUpdate)
            {
                case -1:
                    txtCurrentZona.Text = string.Empty;
                    txtCurrentZona.BackColor = Color.White;
                    btnNewZubZona.Visible = false;
                    btnDelete.Visible = false;
                    btnCanelZone.Location = new Point(233, btnCanelZone.Location.Y);
                    btnSaveZone.Location = new Point(128, btnCanelZone.Location.Y);
                    pnlHeadFields.Width = 267;
                    break;
                case 0:
                    txtCurrentZona.Text = "Nueva Zona";
                    txtCurrentZona.BackColor = Color.White;
                    btnNewZubZona.Visible = false;
                    btnDelete.Visible = false;
                    btnCanelZone.Location = new Point(233, btnCanelZone.Location.Y);
                    btnSaveZone.Location = new Point(128, btnCanelZone.Location.Y);
                    pnlHeadFields.Width = 425;
                    break;
                case 1:
                    txtCurrentZona.Text = this.CurrentZone.Nombre;
                    txtCurrentZona.BackColor = this.CurrentZone.RealColor;
                    btnDelete.Text = "Eliminar Zona";
                    btnSaveZone.Text = "Actualizar Zona";
                    btnNewZubZona.Visible = true;
                    btnDelete.Visible = true;
                    btnCanelZone.Location = new Point(233, btnCanelZone.Location.Y);
                    btnSaveZone.Location = new Point(22, btnCanelZone.Location.Y);
                    btnNewZubZona.Location = new Point(128, btnCanelZone.Location.Y);
                    SetupZonaSubzonasCustomRenderSettings();
                    pnlHeadFields.Width = 425;
                    break;
                case 2:
                    txtCurrentSubzona.Text = "Nueva Subzona";
                    txtCurrentSubzona.BackColor = Color.White;
                    btnNewZubZona.Visible = false;
                    btnDelete.Visible = false;
                    btnCanelZone.Location = new Point(233, btnCanelZone.Location.Y);
                    btnSaveZone.Location = new Point(128, btnCanelZone.Location.Y);
                    pnlHeadFields.Width = 580;
                    SetupZonaSubzonasCustomRenderSettings();
                    break;
                case 3:
                    txtCurrentSubzona.Text = this.CurrentSubzone.Nombre;
                    txtCurrentSubzona.BackColor = this.CurrentSubzone.RealColor;
                    btnDelete.Text = "Elim. Subzona";
                    btnSaveZone.Text = "Act. Subzona";
                    btnNewZubZona.Visible = false;
                    btnDelete.Visible = true;
                    btnCanelZone.Location = new Point(233, btnCanelZone.Location.Y);
                    btnSaveZone.Location = new Point(128, btnCanelZone.Location.Y);
                    pnlHeadFields.Width = 580;
                    SetupZonaSubzonasCustomRenderSettings();
                    break;
            }
        }
        #endregion

        #region Formato de los DataGridView
        private void dgvReportZonas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow fila in dgvReportZonas.Rows)
            {
                if (string.IsNullOrEmpty(fila.Cells[0].Value.ToString()))
                {
                    for (int i = 1; i < dgvReportZonas.Columns.Count; i++)
                    {
                        DataGridViewButtonCell newDataGridViewButtonCell = new DataGridViewButtonCell();
                        newDataGridViewButtonCell.Style.BackColor = SystemColors.Control;
                        newDataGridViewButtonCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        fila.Cells[i] = newDataGridViewButtonCell;
                    }
                }
                else
                {
                    for (int i = 1; i < dgvReportZonas.Columns.Count; i++)
                    {
                        string zoneName = dgvReportZonas.Columns[i].Name.ToLower();
                        List<BE.Zona> lstCurrentZonas = ListZonasReport;
                        BE.Zona zona = lstCurrentZonas.Where(z => z.Nombre.ToLower() == zoneName).FirstOrDefault();
                        if (zona != null)
                        {
                            fila.Cells[i].Style.BackColor = zona.RealColor;
                        }
                    }

                }
            }
        }

        private void dgvReportZonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                if (e.RowIndex == dgvReportZonas.Rows.Count - 1)
                {
                    string zoneName = dgvReportZonas.Columns[e.ColumnIndex].Name.ToLower();
                    switch (LevelUpdate)
                    {
                        case 0:
                            this.CurrentZone = this.ListZonas.Where(z => z.Nombre.ToLower() == zoneName).FirstOrDefault();
                            this.CopyCurrentZone = (BE.Zona)this.CurrentZone.Clone();
                            if (this.CurrentZone == null)
                            {
                                MessageBox.Show("Ha ocurrido un error al extraer los datos de la zona [La zona no se ha encontrado para el estado y municipio seleccionado]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                ClearSelectedRecords();
                                PrintCurrentZona(this.CurrentZone);
                                SelectShapesZona(this.CurrentZone);
                                btnSaveZone.Text = "Actualizar Zona";
                                btnDelete.Text = "Eliminar Zona";
                                PrintZonas(this.CurrentZone.ListaSubzonas);
                                SetUpdateLevel(1);
                                SetupZonaSubzonasCustomRenderSettings();
                                sfmMainMap.Focus();
                            }
                            break;
                        case 1:
                            this.CurrentSubzone = this.CurrentZone.ListaSubzonas.Where(z => z.Nombre.ToLower() == zoneName).FirstOrDefault();
                            this.CopyCurrentSubzone = this.CurrentSubzone;
                            if (this.CurrentSubzone == null)
                            {
                                MessageBox.Show("Ha ocurrido un error al extraer los datos de la subzona [La subzona no se ha encontrado para la zona seleccionada]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                ClearSelectedRecords();
                                SelectShapesZona(this.CurrentSubzone);
                                txtCurrentSubzona.Text = this.CurrentSubzone.Nombre;
                                txtCurrentSubzona.BackColor = this.CurrentSubzone.RealColor;
                                btnSaveZone.Text = "Act. Subzona";
                                btnDelete.Text = "Elim. Subzona";
                                PrintCurrentZona(this.CurrentSubzone);
                                PrintZonas(new List<BE.Zona>());
                                SetUpdateLevel(3);
                                sfmMainMap.Focus();
                            }
                            break;
                    }
                }
            }
        }

        private void dgListZones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.ListZonas != null && this.ListZonas.Count > 0)
            {
                string zonaId = dgListZones.Rows[e.RowIndex].Cells[2].Value.ToString();
                BE.Zona selZona = this.ListZonas.Where(z => z.Id.ToString() == zonaId).FirstOrDefault();
                if (selZona != null)
                {
                    lblCurrentZonaName.Text = selZona.Nombre;
                    DataTable dtDetailZona = ReportZonesConversor.ToZoneDetailDataTable(selZona);
                    sgReportZoneSingle.DataSource = dtDetailZona;
                }
            }
        }
        #endregion

        #region Botones de Salvado  y Cancelado
        private void btnCanelZone_Click(object sender, EventArgs e)
        {
            switch (LevelUpdate)
            {
                case 1:
                case 0:
                    ClearSelectedRecords();
                    InitializeNewZone();
                    LoadZonas();
                    PrintZonas(this.ListZonas);
                    PrintCurrentZona(this.CurrentZone);
                    SetUpdateLevel(0);
                    break;
                case 2:
                case 3:
                    LoadZonas();
                    ClearSelectedRecords();
                    this.CurrentSubzone = null;
                    btnSaveZone.Text = "Actualizar Zona";
                    PrintZonas(this.CurrentZone.ListaSubzonas);
                    SetUpdateLevel(1);
                    SelectShapesZona(this.CurrentZone);
                    PrintCurrentZona(this.CurrentZone);
                    break;
            }
            if (LevelUpdate == 1 || LevelUpdate == 2 || LevelUpdate == 3)
                SetupZonaSubzonasCustomRenderSettings();
            else
                LoadCurrentPlazaRenderSetting();                
            sfmMainMap.Focus();
        }

        private void btnCreteZone_Click(object sender, EventArgs e)
        {
            if (cmbPlazas.SelectedItem != null)
            {
                BE.Plaza selPlaza = (BE.Plaza)cmbPlazas.SelectedItem;
                if (LevelUpdate == 0)
                {
                    if (this.CurrentZone.ListaColonias.Count > 1)
                    {
                        CreateZone oWindowZoneName = new CreateZone();
                        oWindowZoneName.ListZonas = this.ListZonas;
                        oWindowZoneName.ZoneEditId = -1;
                        oWindowZoneName.ShowDialog();
                        if (oWindowZoneName.DialogResult == DialogResult.OK)
                        {
                            this.CurrentZone.PlazaId = this.CurrentPlaza.Id;
                            this.CurrentZone.Nombre = oWindowZoneName.Nombre;
                            this.CurrentZone.Color = oWindowZoneName.Color;
                            SaveZona();                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione al menos una colonia para la nueva Zona.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (LevelUpdate == 1)
                {
                    if (this.CurrentZone.ListaColonias.Count > 1)
                    {
                        CreateZone oWindowZoneName = new CreateZone();
                        oWindowZoneName.ListZonas = this.ListZonas;
                        oWindowZoneName.ZoneEditId = CurrentZone.Id;
                        oWindowZoneName.Color = CurrentZone.Color;
                        oWindowZoneName.Nombre = CurrentZone.Nombre;
                        oWindowZoneName.ShowDialog();
                        if (oWindowZoneName.DialogResult == DialogResult.OK)
                        {
                            this.CurrentZone.Nombre = oWindowZoneName.Nombre;
                            this.CurrentZone.Color = oWindowZoneName.Color;
                            UpdateZona();
                            SetUpdateLevel(0);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione al menos una colonia para la Zona [" + this.CurrentZone.Nombre + "].", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (LevelUpdate == 2)
                {
                    if (this.CurrentSubzone.ListaColonias.Count > 1)
                    {
                        CreateZone oWindowZoneName = new CreateZone();
                        oWindowZoneName.ListZonas = this.CurrentZone.ListaSubzonas;
                        oWindowZoneName.ZoneEditId = -1;
                        oWindowZoneName.ShowDialog();
                        if (oWindowZoneName.DialogResult == DialogResult.OK)
                        {
                            this.CurrentSubzone.PlazaId = this.CurrentPlaza.Id;
                            this.CurrentSubzone.Nombre = oWindowZoneName.Nombre;
                            this.CurrentSubzone.Color = oWindowZoneName.Color;
                            SaveSubzona();
                            SetUpdateLevel(1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione al menos una colonia para la nueva Subzona.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (LevelUpdate == 3)
                {
                    if (this.CurrentSubzone.ListaColonias.Count > 1)
                    {
                        CreateZone oWindowZoneName = new CreateZone();
                        oWindowZoneName.ListZonas = this.ListZonas;
                        oWindowZoneName.ZoneEditId = CurrentSubzone.Id;
                        oWindowZoneName.Color = CurrentSubzone.Color;
                        oWindowZoneName.Nombre = CurrentSubzone.Nombre;
                        oWindowZoneName.ShowDialog();
                        if (oWindowZoneName.DialogResult == DialogResult.OK)
                        {
                            this.CurrentSubzone.Nombre = oWindowZoneName.Nombre;
                            this.CurrentSubzone.Color = oWindowZoneName.Color;
                            UpdateSubzona();
                            SetUpdateLevel(1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione al menos una colonia para la Subzona [" + this.CurrentSubzone.Nombre + "].", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un Estado - Municipio.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sfmMainMap.Focus();
        }
        #endregion

        #region Ver completo y Nse
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPlazas.SelectedItem != null)
                {
                    if (this.ListZonas.Count > 0)
                    {
                        DataTable dtInformationAll = ReportZonesConversor.ToGeneralDataTableAll(CurrentZone, ListZonas);
                        DetailView oWindowDetail = new DetailView();
                        oWindowDetail.Data = dtInformationAll;
                        oWindowDetail.Plaza = ((BE.Plaza)cmbPlazas.SelectedItem).Nombre;
                        oWindowDetail.LoadData();
                        oWindowDetail.Title = "Información General por Zona";
                        oWindowDetail.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No existen zonas para este Estado - Municipio", "Selección de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un estado y municipio", "Selección de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer los datos [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNSE_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPlazas.SelectedItem != null)
                {
                    if (this.ListZonas.Count > 0)
                    {
                        DataTable dtInformationAll = ReportZonesConversor.ToGeneralDataTableNse(CurrentZone, ListZonas);
                        DetailView oWindowDetail = new DetailView();
                        oWindowDetail.Data = dtInformationAll;
                        oWindowDetail.Plaza = ((BE.Plaza) cmbPlazas.SelectedItem).Nombre;
                        oWindowDetail.LoadData();
                        oWindowDetail.Title = "Información Socioeconómica por Zona";
                        oWindowDetail.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No existen zonas para este Estado - Municipio", "Selección de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un estado y municipio", "Selección de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer los datos [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Administración de subzonas
        private void btnNewZubZona_Click(object sender, EventArgs e)
        {
            try
            {
                //Se reasigna la zona en caso de que se haya modificado en memoria
                this.CurrentZone = this.CopyCurrentZone;
                InitializeNewSubzone();
                ClearSelectedRecords();
                PrintCurrentZona(this.CurrentSubzone);
                SetUpdateLevel(2);
                sfmMainMap.Focus();
            }
            catch
            {

            }
        }
        #endregion

        #region Eliminaion de Zonas
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbPlazas.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar la zona?", "Eliminación de zona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (LevelUpdate == 1)
                    {
                        if (this.CurrentZone != null)
                        {
                            DeleteZona();
                            SetUpdateLevel(0);
                        }
                        else
                        {
                            MessageBox.Show("Por favor seleccione una subzona.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (LevelUpdate == 3)
                    {
                        if (this.CurrentSubzone != null)
                        {
                            DeleteSubzona();
                            SetUpdateLevel(1);
                        }
                        else
                        {
                            MessageBox.Show("Por favor seleccione una subzona.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            sfmMainMap.Focus();
        }
        #endregion
    }
}