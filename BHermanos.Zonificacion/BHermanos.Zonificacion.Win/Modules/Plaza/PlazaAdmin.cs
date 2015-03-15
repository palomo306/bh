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

namespace BHermanos.Zonificacion.Win.Modules.Plaza
{
    public partial class PlazaAdmin : Form
    {
        #region Campos
        private string LocalPath { get; set; }
        private List<BE.Plaza> ListPlazas { get; set; }
        private BE.Plaza CurrentPlaza { get; set; }
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
                    foreach (BE.Estado edo in objResponse.ListaEstados)
                    {
                        ccbEstados.Items.Add(new CheckComboBoxItem(edo.Nombre, edo, false));
                    }
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
        #endregion

        #region Manejo de los Mapas
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

        #region Constructor
        public PlazaAdmin()
        {
            InitializeComponent();
            LocalPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LoadEstados();
            LoadPlazas();
            LoadMainMap();
        }
        #endregion

        #region Seleccion de los Estados
        private void ccbEstados_CheckStateChanged(object sender, EventArgs e)
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

    }
}