using BHermanos.Zonificacion.BusinessEntities.Cast;
using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;
using BHermanos.Zonificacion.WebService.Models;
using System.Reflection;
using BHermanos.Zonificacion.Win.Modules.Zone.Modal;
using System.Collections.ObjectModel;
using RegionDemo.Clases;
using EGIS.Controls;
using EGIS.ShapeFileLib;

namespace BHermanos.Zonificacion.Win.Modules.Zone
{
    public partial class ZoneAdmin : Form
    {
        #region Campos y Propiedades
        private List<BE.Zona> ListZonas { get; set; }
        List<BE.Zona> ListZonasReport { get; set; }
        private BE.Zona CurrentZone { get; set; }
        private BE.Zona CopyCurrentZone { get; set; }
        private BE.Zona CurrentSubzone { get; set; }
        private BE.Zona CopyCurrentSubzone { get; set; }
        private int LevelUpdate { get; set; }
        private bool IsFirstTime { get; set; }
        private string LocalPath { get; set; }
        private int LayerColonias = 2;
        private int LayerBorderColonias = 5;
        private int LayerMunicipios = 5;
        private int LayerCount = 6;
        private bool updateZoomLevel = true;
        #endregion

        #region Carga de Mapas
        private void LoadMainMap()
        {
            string mapPath = LocalPath + @"\Maps\Nacional\national_estatal.shp";
            ShapeFile f = this.sfmMainMap.AddShapeFile(mapPath, "México", "NOMBRE");
        }

        private void LoadMap(string edoId, string mapFileName, string mapName, string fieldName, bool isSelectable, bool fillInterior, int transparency)
        {
            string mapPath = LocalPath + @"\Maps\Estados\" + edoId + @"\" + mapFileName + ".shp";
            EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap.AddShapeFile(mapPath, mapName, fieldName);
            sf.RenderSettings.IsSelectable = isSelectable;
            sf.RenderSettings.FillInterior = fillInterior;
            sf.RenderSettings.FillColor = Color.FromArgb(transparency, sf.RenderSettings.FillColor);
            sf.RenderSettings.OutlineColor = Color.FromArgb(transparency, sf.RenderSettings.OutlineColor);
            sf.RenderSettings.MinZoomLevel = 15;
            sf.RenderSettings.SelectFillColor = Color.FromArgb(0, 55, 33, 22);
        }

        private void ZoomToMunicipio(string mapLayer, string municipioId)
        {
            if (this.sfmMainMap.ShapeFileCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerMunicipios];
                string[] r = sf.GetRecords(2);
                int i = 0;
                for (i = 0; i < sf.RecordCount; i++)
                {
                    if (Convert.ToInt32(r[i]) == Convert.ToInt32(municipioId))
                        break;
                }
                ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(i);
                sfmMainMap.SetZoomAndCentre(3500, puntos[0][0]);
            }
        }

        private void GetNewColId(List<double> newCols, List<double> delCols, BE.Zona zone, BE.Zona subzona)
        {
            try
            {
                if (this.sfmMainMap.ShapeFileCount == LayerCount)
                {
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerColonias];
                    List<int> lstSelected = sf.SelectedRecordIndices.ToList();
                    List<double> lstSelCols = new List<double>();
                    foreach (int selIndex in lstSelected)
                    {
                        string[] values = sf.GetAttributeFieldValues(selIndex);
                        if (LevelUpdate == 0 || LevelUpdate == 1)
                        {
                            if (Convert.ToInt32(values[1]) == selEdo.Id && Convert.ToInt32(values[2]) == selMunicipio.Id)
                            {
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

                                if (!ExistPreviosZona(this.ListZonas, zone, coldId))
                                    lstSelCols.Add(coldId);
                                else
                                {
                                    MessageBox.Show("Se han seleccionado colonias que pertenecen a otra Zona." + Environment.NewLine + "", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    sf.SelectRecord(selIndex, false);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Se han seleccionado colonias fuera del Estado - Municipio seleccionados", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sf.SelectRecord(selIndex, false);
                            }
                        }
                        else if (LevelUpdate == 2 || LevelUpdate == 3)
                        {
                            if (Convert.ToInt32(values[1]) == selEdo.Id && Convert.ToInt32(values[2]) == selMunicipio.Id)
                            {
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

                                List<double> listColsIds = zone.ListaColonias.Select(c => c.Id).ToList();
                                if (listColsIds.Contains(coldId))
                                {
                                    if (!ExistPreviosZona(zone.ListaSubzonas, subzona, coldId))
                                    {
                                        lstSelCols.Add(coldId);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Se han seleccionado colonias que pertenecen a otra Subzona de esta Zona", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        sf.SelectRecord(selIndex, false);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Se han seleccionado colonias fuera de la Zona actual", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    sf.SelectRecord(selIndex, false);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Se han seleccionado colonias fuera del Estado - Municipio seleccionados", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sf.SelectRecord(selIndex, false);
                            }
                        }
                    }
                    List<double> lstCurrentCols = new List<double>();
                    if (LevelUpdate == 0 || LevelUpdate == 1)
                        lstCurrentCols = zone.ListaColonias.Where(c => c.Id != 0).Select(c => c.Id).ToList();
                    else if (LevelUpdate == 2 || LevelUpdate == 3)
                        lstCurrentCols = subzona.ListaColonias.Where(c => c.Id != 0).Select(c => c.Id).ToList();

                    foreach (double colId in lstCurrentCols)
                    {
                        if (!lstSelCols.Contains(colId))
                            delCols.Add(colId);
                    }
                    foreach (double colId in lstSelCols)
                    {
                        if (!lstCurrentCols.Contains(colId))
                            newCols.Add(colId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetupZonasCustomRenderSettings()
        {
            if (this.sfmMainMap.ShapeFileCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerColonias];
                ZonaCustomRenderSettings crsZ = new ZonaCustomRenderSettings(sf.RenderSettings, this.ListZonas);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }

        private void SetupZonaSubzonasCustomRenderSettings()
        {
            if (this.sfmMainMap.ShapeFileCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerColonias];
                ZonaSubzonasCustomRenderSettings crsZ = new ZonaSubzonasCustomRenderSettings(sf.RenderSettings, this.CurrentZone);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }

        private void SetupMunicipiosCustomRenderSettings()
        {
            if (this.sfmMainMap.ShapeFileCount == LayerCount && cmbMunicipio.SelectedItem != null)
            {
                BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerMunicipios];
                MunicipioCustomRenderSettings crsZ = new MunicipioCustomRenderSettings(sf.RenderSettings, selMunicipio.Id);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }

        private void SetupColoniasCustomRenderSettings()
        {
            if (this.sfmMainMap.ShapeFileCount == LayerCount)
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerBorderColonias];
                ColoniaCustomRenderSettings crsZ = new ColoniaCustomRenderSettings(sf.RenderSettings);
                sf.RenderSettings.CustomRenderSettings = crsZ;
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }

        private void ClearSelectedRecords(int layer)
        {
            try
            {
                EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerColonias];
                if (sf != null)
                    sf.ClearSelectedRecords();
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
            catch { }
        }

        private void SelectShapesZona(BE.Zona zona)
        {
            //if (this.sfmMainMap.ShapeFileCount == LayerCount)
            //{
            //    EGIS.ShapeFileLib.ShapeFile sf = this.sfmMainMap[LayerColonias];
            //    string[] rEdos = sf.GetRecords(1);
            //    string[] rMun = sf.GetRecords(2);
            //    string[] rCols = sf.GetRecords(7);
            //    string[] rLocs = sf.GetRecords(3);
            //    string[] rTipo = sf.GetRecords(4);
            //    string[] rLocs2 = sf.GetRecords(8);


            //    int i = 0, lastIndex = 0;
            //    for (i = 0; i < sf.RecordCount; i++)
            //    {
            //        if (Convert.ToInt32(rEdos[i]) == zona.EstadoId && Convert.ToInt32(rMun[i]) == zona.MunicipioId)
            //        {
            //            List<string> lstColsIds = zona.ListaColonias.Select(c => c.Id.ToString()).ToList();

            //            string colString = rCols[i].Replace("|", "").Trim();
            //            if (colString == "NA")
            //            {
            //                colString = rTipo[i].Trim() + rEdos[i].Trim().PadLeft(2, '0') + rMun[i].Trim().PadLeft(3, '0') + rLocs[i].Trim().PadLeft(4, '0') + rLocs2[i].Trim().PadLeft(5, '0');
            //            }
            //            else
            //            {
            //                colString = rTipo[i].Trim() + colString;
            //            }

            //            if (lstColsIds.Contains(colString))
            //            {
            //                lastIndex = i;
            //                sf.SelectRecord(i, true);
            //            }
            //        }
            //    }
            //    ReadOnlyCollection<EGIS.ShapeFileLib.PointD[]> puntos = sf.GetShapeDataD(lastIndex);
            //    sfmMainMap.SetZoomAndCentre(7000, puntos[0][0]);
            //}
        }

        private void ClearLayers(int minLayer, int maxLayer)
        {
            try
            {
                for (int i = minLayer; i < maxLayer; i++)
                {
                    this.sfmMainMap.RemoveShapeFile(this.sfmMainMap[i]);
                }
            }
            catch { }
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
                    this.cmbEstado.DataSource = objResponse.ListaEstados.ToList();
                    this.cmbEstado.DisplayMember = "Nombre";
                    this.cmbEstado.SelectedItem = null;
                    this.cmbEstado.Text = "--Seleccione un estado--";
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al extraer los datos de los estados [" + objResponse.Mensaje + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer los datos de los estados [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    this.cmbMunicipio.DataSource = objResponse.ListaMunicipios.ToList();
                    this.cmbMunicipio.DisplayMember = "Nombre";
                    this.cmbMunicipio.SelectedItem = null;
                    this.cmbMunicipio.Text = "--Seleccione un municipio--";
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al extraer los datos de los municipios [" + objResponse.Mensaje + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer los datos de los municipios [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            /*Se pintan las zonas*/
            if (LevelUpdate == 0)
            {
                SetupZonasCustomRenderSettings();
            }
            else if (LevelUpdate == 1 || LevelUpdate == 2 || LevelUpdate == 3)
            {
                SetupZonaSubzonasCustomRenderSettings();
            }
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

        private BE.Colonia GetNewCol(string edoId, string munId, string colId)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Colonia/GetColonia/2/" + edoId + "/" + munId + "/" + colId + "?type=json";
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

        #region Constructor
        private void SetUpdateLevel(int level)
        {
            BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
            BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
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
                    pnlHeadFields.Width = 425;
                    break;
                case 0:
                    txtCurrentZona.Text = "Nueva Zona";
                    txtCurrentZona.BackColor = Color.White;
                    btnNewZubZona.Visible = false;
                    btnDelete.Visible = false;
                    btnCanelZone.Location = new Point(233, btnCanelZone.Location.Y);
                    btnSaveZone.Location = new Point(128, btnCanelZone.Location.Y);
                    pnlHeadFields.Width = 585;
                    SetupZonasCustomRenderSettings();
                    ZoomToMunicipio("Municipios" + selEdo.Nombre, selMunicipio.Id.ToString());
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
                    pnlHeadFields.Width = 585;
                    break;
                case 2:
                    txtCurrentSubzona.Text = "Nueva Subzona";
                    txtCurrentSubzona.BackColor = Color.White;
                    btnNewZubZona.Visible = false;
                    btnDelete.Visible = false;
                    btnCanelZone.Location = new Point(233, btnCanelZone.Location.Y);
                    btnSaveZone.Location = new Point(128, btnCanelZone.Location.Y);
                    pnlHeadFields.Width = 740;
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
                    pnlHeadFields.Width = 740;
                    SetupZonaSubzonasCustomRenderSettings();
                    break;
            }
        }

        public ZoneAdmin()
        {
            InitializeComponent();
            IsFirstTime = true;
            SetUpdateLevel(-1);
            LocalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LoadEstados();
            LoadMunicipios("0");
            InitializeNewZone();
            PrintCurrentZona(this.CurrentZone);
            LoadZonas("0", "0");
            PrintZonas(this.ListZonas);
            LoadMainMap();
            ZoneCreatorForm_Resize(null, null);
            //splitterTreeView.ToggleState();
            IsFirstTime = false;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            panelCtrlMap.BackColor = Color.FromArgb(0, 0, 0, 0);
        }
        #endregion

        #region Cambio de Tamaño del Formulario
        private void ZoneCreatorForm_Resize(object sender, EventArgs e)
        {
            if (tabInfo.Visible)
            {
                sgReportZoneSingle.Height = tabInfo.Height - 285;
                pnlMap.Width = this.Width - tabInfo.Width - 26;
            }
            else
            {
                pnlMap.Width = this.Width - 26;
            }
        }

        private void splitterTreeView_SplitterMoving(object sender, SplitterEventArgs e)
        {
            ZoneCreatorForm_Resize(null, null);
        }

        private void splitterTreeView_Click(object sender, EventArgs e)
        {

        }

        private void tabInfo_Resize(object sender, EventArgs e)
        {
            ZoneCreatorForm_Resize(null, null);
        }

        private void tabInfo_VisibleChanged(object sender, EventArgs e)
        {
            ZoneCreatorForm_Resize(null, null);
        }

        private void lblCurrentZonaName_TextChanged(object sender, EventArgs e)
        {
            int newPos = Convert.ToInt32(tabPageZonas.Width / 2 - lblCurrentZonaName.Width / 2) - 15;
            lblCurrentZonaName.Location = new Point(newPos, lblCurrentZonaName.Location.Y);
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
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    PrintZonas(this.ListZonas);
                    ClearSelectedRecords(5);
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
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
                //url += "Zona/DeleteZona/1/" + this.CurrentZone.EstadoId.ToString() + "/" + this.CurrentZone.MunicipioId.ToString() + "/" + this.CurrentZone.Id.ToString() + "?type=json";
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
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    PrintZonas(this.ListZonas);
                    ClearSelectedRecords(5);
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
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
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    PrintZonas(this.ListZonas);
                    ClearSelectedRecords(5);
                    InitializeNewZone();
                    PrintCurrentZona(this.CurrentZone);
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
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    this.CurrentZone = this.ListZonas.Where(z => z.Id == this.CurrentZone.Id).FirstOrDefault();
                    this.CopyCurrentZone = (BE.Zona)this.CurrentZone;
                    ClearSelectedRecords(5);
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
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    this.CurrentZone = this.ListZonas.Where(z => z.Id == this.CurrentZone.Id).FirstOrDefault();
                    this.CopyCurrentZone = (BE.Zona)this.CurrentZone;
                    ClearSelectedRecords(5);
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
                //url += "Zona/DeleteZona/2/" + this.CurrentSubzone.EstadoId.ToString() + "/" + this.CurrentSubzone.MunicipioId.ToString() + "/" + this.CurrentSubzone.Id.ToString() + "?type=json";
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
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    this.CurrentZone = this.ListZonas.Where(z => z.Id == this.CurrentZone.Id).FirstOrDefault();
                    this.CopyCurrentZone = (BE.Zona)this.CurrentZone;
                    ClearSelectedRecords(5);
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
                                ClearSelectedRecords(5);
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
                                ClearSelectedRecords(5);
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

        #region Selección de Estado y Municipio
        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedItem != null && !IsFirstTime)
            {
                ClearSelectedRecords(sfmMainMap.ShapeFileCount - 1);
                ClearLayers(1, sfmMainMap.ShapeFileCount);
                BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                //Se carga el mapa
                LoadMap(selEdo.Id.ToString(), "Estado.shp", selEdo.Nombre, "NombreEsta", false, false, 0);
                LoadMap(selEdo.Id.ToString(), "Colonias.shp", "Colonias" + selEdo.Nombre, "Nombre", true, true, 0);
                LoadMap(selEdo.Id.ToString(), "Carreteras.shp", "Carreteras" + selEdo.Nombre, "Nombre", false, false, 60);
                LoadMap(selEdo.Id.ToString(), "Calles.shp", "Calles" + selEdo.Nombre, "Nombre", false, false, 60);
                LoadMap(selEdo.Id.ToString(), "Municipios.shp", "Municipios" + selEdo.Nombre, "NombreMuni", false, false, 0);
                //SetupColoniasCustomRenderSettings();
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                LoadZonas("0", "0");
                PrintZonas(this.ListZonas);
                //Se cargan los municipios en el combo
                IsFirstTime = true;
                LoadMunicipios(selEdo.Id.ToString());
                IsFirstTime = false;
                SetUpdateLevel(-1);
                cmbMunicipio.Focus();
            }
            else
            {
                InitializeNewZone();
                LoadZonas("0", "0");
                PrintZonas(this.ListZonas);
                SetUpdateLevel(-1);
                ClearSelectedRecords(sfmMainMap.ShapeFileCount - 1);
            }
        }

        private void cmbMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedItem != null && cmbMunicipio.SelectedItem != null && !IsFirstTime)
            {
                BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                SetupMunicipiosCustomRenderSettings();
                LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                PrintZonas(this.ListZonas);
                InitializeNewZone();
                PrintCurrentZona(this.CurrentZone);
                SetUpdateLevel(0);
                ClearSelectedRecords(sfmMainMap.ShapeFileCount - 1);
                sfmMainMap.CtrlDown = false;
                pbbZoomSelect.BorderStyle = BorderStyle.FixedSingle;
                sfmMainMap.Focus();
            }
            else
            {
                InitializeNewZone();
                ClearSelectedRecords(sfmMainMap.ShapeFileCount - 1);
                PrintCurrentZona(this.CurrentZone);
                LoadZonas("0", "0");
                PrintZonas(this.ListZonas);
                SetUpdateLevel(-1);
            }
        }
        #endregion

        #region Botones de Salvado  y Cancelado
        private void btnCanelZone_Click(object sender, EventArgs e)
        {
            BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
            BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
            switch (LevelUpdate)
            {
                case 1:
                case 0:
                    ClearSelectedRecords(sfmMainMap.ShapeFileCount - 1);
                    InitializeNewZone();
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    PrintZonas(this.ListZonas);
                    PrintCurrentZona(this.CurrentZone);
                    SetUpdateLevel(0);
                    break;
                case 2:
                case 3:
                    LoadZonas(selEdo.Id.ToString(), selMunicipio.Id.ToString());
                    ClearSelectedRecords(sfmMainMap.ShapeFileCount - 1);
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
            sfmMainMap.Focus();
        }

        private void btnCreteZone_Click(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedItem != null && cmbMunicipio.SelectedItem != null)
            {
                BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
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
                            //this.CurrentZone.EstadoId = selEdo.Id;
                            //this.CurrentZone.MunicipioId = selMunicipio.Id;
                            //this.CurrentZone.Nombre = oWindowZoneName.Nombre;
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
                            //this.CurrentSubzone.EstadoId = selEdo.Id;
                            //this.CurrentSubzone.MunicipioId = selMunicipio.Id;
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
                if (cmbEstado.SelectedItem != null && cmbMunicipio.SelectedItem != null)
                {
                    if (this.ListZonas.Count > 0)
                    {
                        DataTable dtInformationAll = ReportZonesConversor.ToGeneralDataTableAll(CurrentZone, ListZonas);
                        DetailView oWindowDetail = new DetailView();
                        oWindowDetail.Data = dtInformationAll;
                        //oWindowDetail.Estado = ((BE.Estado)cmbEstado.SelectedItem).Nombre;
                        //oWindowDetail.Municipio = ((BE.Municipio)cmbMunicipio.SelectedItem).Nombre;
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
                if (cmbEstado.SelectedItem != null && cmbMunicipio.SelectedItem != null)
                {
                    if (this.ListZonas.Count > 0)
                    {
                        DataTable dtInformationAll = ReportZonesConversor.ToGeneralDataTableNse(CurrentZone, ListZonas);
                        DetailView oWindowDetail = new DetailView();
                        oWindowDetail.Data = dtInformationAll;
                        //oWindowDetail.Estado = ((BE.Estado)cmbEstado.SelectedItem).Nombre;
                        //oWindowDetail.Municipio = ((BE.Municipio)cmbMunicipio.SelectedItem).Nombre;
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

        #region Selección de las colonias
        private bool ExistPreviosZona(List<BE.Zona> lstCurrentZonas, BE.Zona cZ, double colId)
        {
            bool result = false;
            List<BE.Zona> lstZonas = lstCurrentZonas.Where(z => z.Id != cZ.Id).ToList();
            foreach (BE.Zona zn in lstZonas)
            {
                List<double> lstColIds = zn.ListaColonias.Select(c => c.Id).ToList();
                if (lstColIds.Contains(colId))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void sfmMainMap_SelectedRecordsChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEstado.SelectedItem != null && cmbMunicipio.SelectedItem != null)
                {
                    BE.Estado selEdo = (BE.Estado)cmbEstado.SelectedItem;
                    BE.Municipio selMunicipio = (BE.Municipio)cmbMunicipio.SelectedItem;
                    List<double> newCols = new List<double>();
                    List<double> delCols = new List<double>();
                    if (LevelUpdate == 0 || LevelUpdate == 1)
                    {
                        GetNewColId(newCols, delCols, this.CurrentZone, null);
                        //Se eliminan las colonias
                        foreach (double colId in delCols)
                        {
                            BE.Colonia dCol = this.CurrentZone.ListaColonias.Where(c => c.Id == colId).FirstOrDefault();
                            if (dCol != null)
                            {
                                this.CurrentZone.ListaColonias.Remove(dCol);
                            }
                        }
                        foreach (double colId in newCols)
                        {
                            try
                            {
                                BE.Colonia newCol = GetNewCol(selEdo.Id.ToString(), selMunicipio.Id.ToString(), colId.ToString());
                                this.CurrentZone.ListaColonias.Add(newCol);
                            }
                            catch (Exception colEx)
                            {
                                MessageBox.Show(colEx.Message, "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        PrintCurrentZona(this.CurrentZone);
                    }
                    else if (LevelUpdate == 2 || LevelUpdate == 3)
                    {
                        GetNewColId(newCols, delCols, this.CurrentZone, this.CurrentSubzone);
                        //Se eliminan las colonias
                        foreach (double colId in delCols)
                        {
                            BE.Colonia dCol = this.CurrentSubzone.ListaColonias.Where(c => c.Id == colId).FirstOrDefault();
                            if (dCol != null)
                            {
                                this.CurrentSubzone.ListaColonias.Remove(dCol);
                            }
                        }
                        foreach (double colId in newCols)
                        {
                            try
                            {
                                BE.Colonia newCol = GetNewCol(selEdo.Id.ToString(), selMunicipio.Id.ToString(), colId.ToString());
                                this.CurrentSubzone.ListaColonias.Add(newCol);
                            }
                            catch (Exception colEx)
                            {
                                MessageBox.Show(colEx.Message, "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        PrintCurrentZona(this.CurrentSubzone);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un Estado - Municipio antes de interactuar con el mapa.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearSelectedRecords(LayerColonias);
                }
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer los datos de las colonias [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ClearSelectedRecords(LayerColonias);
                PrintCurrentZona(this.CurrentSubzone);
                SetUpdateLevel(2);
                sfmMainMap.Focus();
            }
            catch
            {

            }
        }
        #endregion

        #region Menejo del Panel de Zoom
        private void pbbZoomSelect_Click(object sender, EventArgs e)
        {
            if (sfmMainMap.CtrlDown)
            {
                pbbZoomSelect.BorderStyle = BorderStyle.FixedSingle;
                sfmMainMap.CtrlDown = false;
            }
            else
            {
                pbbZoomSelect.BorderStyle = BorderStyle.Fixed3D;
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
            if (trbZoom.Value >= 35)
            {
                trbZoom.Value = trbZoom.Value - 20;
            }
        }

        private void pbbZoomMax_Click(object sender, EventArgs e)
        {
            if (trbZoom.Value < 500000)
            {
                trbZoom.Value = trbZoom.Value + 20;
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

        private void sfmMainMap_KeyUp(object sender, KeyEventArgs e)
        {
            //if (sfmMainMap.CtrlDown)
            //{
            //    pbbZoomSelect.BorderStyle = BorderStyle.Fixed3D;
            //}
            //else
            //{
            //    pbbZoomSelect.BorderStyle = BorderStyle.FixedSingle;
            //}
        }
        #endregion

        #region Eliminaion de Zonas
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedItem != null && cmbMunicipio.SelectedItem != null)
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