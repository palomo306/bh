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

namespace BHermanos.Zonificacion.Win.Modules.Plaza
{
    public partial class PlazaAdmin : Form
    {
        #region Campos
        private string LocalPath { get; set; }
        private List<BE.Plaza> ListPlazas { get; set; }
        private BE.Plaza CurrentPlaza { get; set; }
        private bool IsUpdate;
        private bool IsFirsTime;
        private bool updateZoomLevel;
        #endregion

        #region Carga y Manejo de Datos
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
                    foreach (BE.Estado edo in objResponse.ListaEstados)
                    {
                        ccbEstados.Items.Add(new CheckComboBoxItem(edo.Nombre, edo, false));
                    }
                    ccbEstados.SelectedItem = null;
                    ccbEstados.Text = "--Seleccione los estados--";
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

        private void LoadPlazas()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Plaza/GetPlaza/2/0?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                PlazaModel objResponse = JsonSerializer.Parse<PlazaModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    ListPlazas = objResponse.ListaPlazas.ToList();
                    dgPlazas.DataSource = ListPlazas;
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

        private void SavePlaza()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Plaza/PostPlaza?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "POST";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentPlaza.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                PlazaModel objResponse = JsonSerializer.Parse<PlazaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    ClearForm();
                    LoadPlazas();
                    MessageBox.Show("Se ha almacenado correctamente la plaza.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guardar la plaza [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar la plaza [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeletePlaza()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Plaza/DeletePlaza/" + this.CurrentPlaza.Id.ToString() + "?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "DELETE";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentPlaza.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                PlazaModel objResponse = JsonSerializer.Parse<PlazaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    ClearForm();
                    LoadPlazas();
                    MessageBox.Show("Se ha eliminado correctamente la plaza.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al eliminar la plaza [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al eliminar la plaza [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePlaza()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Plaza/PutPlaza?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "PUT";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = this.CurrentPlaza.ToJSon(false);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                PlazaModel objResponse = JsonSerializer.Parse<PlazaModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    ClearForm();
                    LoadPlazas();
                    MessageBox.Show("Se ha almacenado correctamente la plaza.", "Operación existosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al guardar la plaza [" + objResponse.Mensaje + "].");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar la plaza [" + ex.Message + "].", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            sf.RenderSettings.OutlineColor = Color.FromArgb(transparency, sf.RenderSettings.OutlineColor);
            sf.RenderSettings.MinZoomLevel = 15;
            sf.RenderSettings.SelectFillColor = Color.FromArgb(0, 55, 33, 22);            
        }

        private void LoadPlazaRenderSetting()
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
                    PlazaCustomRenderSettings crsPl = new PlazaCustomRenderSettings(sf.RenderSettings, this.ListPlazas);
                    sf.RenderSettings.CustomRenderSettings = crsPl;
                    layerIndex += 5;                
                }
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
        }

        private void LoadMapsByEdo(BE.Estado selEdo)
        {
            LoadMap(selEdo.Id.ToString(), "Estado.shp", selEdo.Nombre, "NombreEsta", false, false, 0);
            LoadMap(selEdo.Id.ToString(), "Colonias.shp", "Colonias" + selEdo.Nombre, "Nombre", true, true, 0);
            LoadMap(selEdo.Id.ToString(), "Carreteras.shp", "Carreteras" + selEdo.Nombre, "Nombre", false, false, 60);
            LoadMap(selEdo.Id.ToString(), "Calles.shp", "Calles" + selEdo.Nombre, "Nombre", false, false, 60);
            LoadMap(selEdo.Id.ToString(), "Municipios.shp", "Municipios" + selEdo.Nombre, "NombreMuni", false, false, 0);
        }

        private void RemoveMapsByEdo(BE.Estado selEdo)
        {
            List<int> indexToRemove = new List<int>();
            for (int i = 1; i < this.sfmMainMap.ShapeFileCount; i++)
            {
                if (this.sfmMainMap[i].Name.EndsWith(selEdo.Nombre))
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

        private void ChangePlazaSelections()
        {
            try
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
                            //Se busca en la plaza si existen esa colonia seleccionada para otra plaza
                            if (!ExistPreviosPlaza(estadoId, municipioId, coldId))
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
                                MessageBox.Show("Se han seleccionado colonias que pertenecen a otra Plaza", "Error de selección ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sf.SelectRecord(selIndex, false);
                            }
                        }
                        layerIndex += 5;
                    }
                    //Se actualiza la plaza actual                   
                    List<BE.Estado> treeEdos = new List<BE.Estado>();
                    foreach (BE.Colonia selCol in lstSelColonias)
                    {
                        BE.Estado currentEdo = treeEdos.Where(edo => edo.Id == selCol.EstadoId).FirstOrDefault();
                        if (currentEdo == null)
                        {
                            currentEdo = new BE.Estado() { Id = selCol.EstadoId };
                            treeEdos.Add(currentEdo);
                        }
                        BE.Municipio currentMun = currentEdo.ListaMunicipios.Where(mun => mun.Id == selCol.MunicipioId).FirstOrDefault();
                        if (currentMun == null)
                        {
                            currentMun = new BE.Municipio() { Id = selCol.MunicipioId };
                            currentEdo.ListaMunicipios.Add(currentMun);
                        }
                        BE.Colonia currentCol = currentMun.ListaColonias.Where(col => col.Id == selCol.Id).FirstOrDefault();
                        if (currentCol == null)
                        {
                            currentCol = new BE.Colonia() { Id = selCol.Id };
                            currentMun.ListaColonias.Add(currentCol);
                        }
                    }
                    CurrentPlaza.ListaEstados = treeEdos;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Limpieza de Datos
        private void ClearForm()
        {
            IsUpdate = false;
            foreach (CheckComboBoxItem item in ccbEstados.Items)
            {
                item.CheckState = false;
            }
            btnSaveZone.Text = "Guardar plaza";
            this.CurrentPlaza = new BE.Plaza();
            txtCurrentPlaza.Text = "Nueva plaza";
            txtCurrentPlaza.BackColor = Color.White;
        }
        #endregion

        #region Constructor
        public PlazaAdmin()
        {
            InitializeComponent();
            IsFirsTime = true;
            LocalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);            
            LoadEstados();
            LoadPlazas();
            LoadMainMap();
            ClearForm();
            IsFirsTime = false;
        }
        #endregion

        #region Seleccion de los Estados
        private void ccbEstados_CheckStateChanged(object sender, EventArgs e)
        {
            if (!IsFirsTime)
            {
                if (sender is CheckComboBoxItem)
                {
                    CheckComboBoxItem item = (CheckComboBoxItem)sender;
                    BE.Estado selEdo = (BE.Estado)item.Tag;
                    if (item.CheckState)
                    {
                        LoadMapsByEdo(selEdo);
                    }
                    else
                    {
                        RemoveMapsByEdo(selEdo);
                    }
                }
            }
        }
        #endregion

        #region Selección de las colonias
        private bool ExistPreviosPlaza(int edoId, int munId, double colId)
        {
            bool result = false;
            foreach (BE.Plaza pl in this.ListPlazas.Where(p => p.Id != CurrentPlaza.Id))
            {
                BE.Estado edo = pl.ListaEstados.Where(e => e.Id == edoId).FirstOrDefault();
                if (edo != null)
                {
                    BE.Municipio mun = edo.ListaMunicipios.Where(m => m.Id == munId).FirstOrDefault();
                    if (mun != null)
                    {
                        return mun.ListaColonias.Where(c => c.Id == colId).Any();
                    }
                }
            }
            return result;
        }

        private void sfmMainMap_SelectedRecordsChanged(object sender, EventArgs e)
        {
            try
            {
                ChangePlazaSelections();
                sfmMainMap.ZoomLevel = sfmMainMap.ZoomLevel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al extraer los datos de las colonias [" + ex.Message + "]", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Eventos del Grid
        private void dgPlazas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                int idPlaza = Convert.ToInt32(dgPlazas.Rows[e.RowIndex].Cells[3].Value);
                BE.Plaza clickPlaza = ListPlazas.Where(p => p.Id == idPlaza).FirstOrDefault();
                if (clickPlaza != null)
                {
                    this.CurrentPlaza = clickPlaza;
                    if (e.ColumnIndex == 1)
                    {
                        txtCurrentPlaza.Text = this.CurrentPlaza.Nombre;
                        txtCurrentPlaza.BackColor = this.CurrentPlaza.RealColor;
                        btnSaveZone.Text = "Act. plaza";
                        IsUpdate = true;
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        if (MessageBox.Show("¿Está seguro que desea eliminar la plaza [" + clickPlaza.Nombre + "]?", "Confirmación de eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DeletePlaza();
                        }
                    }
                }
            }
        }

        private void dgPlazas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow fila in dgPlazas.Rows)
            {
                int idPlaza = Convert.ToInt32(fila.Cells[3].Value);
                BE.Plaza rowPlaza = ListPlazas.Where(p => p.Id == idPlaza).FirstOrDefault();
                if (rowPlaza != null)
                {
                    fila.Cells[0].Style.BackColor = rowPlaza.RealColor;
                }
            }
        }
        #endregion

        #region Actualización de la Información
        private void btnSaveZone_Click(object sender, EventArgs e)
        {
            if (!IsUpdate)
            {
                CreatePlaza oWindowZoneName = new CreatePlaza();
                oWindowZoneName.ListPlazas = this.ListPlazas;
                oWindowZoneName.ZoneEditId = -1;
                oWindowZoneName.ShowDialog();
                if (oWindowZoneName.DialogResult == DialogResult.OK)
                {
                    this.CurrentPlaza.Nombre = oWindowZoneName.Nombre;
                    this.CurrentPlaza.Color = oWindowZoneName.Color;
                    SavePlaza();
                }
            }
            else
            {
                CreatePlaza oWindowZoneName = new CreatePlaza();
                oWindowZoneName.ListPlazas = this.ListPlazas;
                oWindowZoneName.ZoneEditId = this.CurrentPlaza.Id;
                oWindowZoneName.Color = this.CurrentPlaza.Color;
                oWindowZoneName.Nombre = this.CurrentPlaza.Nombre;
                oWindowZoneName.ShowDialog();
                if (oWindowZoneName.DialogResult == DialogResult.OK)
                {
                    this.CurrentPlaza.Nombre = oWindowZoneName.Nombre;
                    this.CurrentPlaza.Color = oWindowZoneName.Color;
                    UpdatePlaza();
                }
            }
        }        

        private void btnCanelZone_Click(object sender, EventArgs e)
        {
            ClearForm();
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
    }
}