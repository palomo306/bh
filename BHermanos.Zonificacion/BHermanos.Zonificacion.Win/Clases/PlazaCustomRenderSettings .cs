using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;
using System.Drawing;


namespace RegionDemo.Clases
{
    public class PlazaCustomRenderSettings  : ICustomRenderSettings
    {
        #region Propiedades
        private List<System.Drawing.Color> colorList;        
        RenderSettings defaultSettings;
        #endregion

        public PlazaCustomRenderSettings(RenderSettings defaultSettings, List<BE.Plaza> ListPlazas)
        {
            this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, ListPlazas);
        }

        private void BuildColorList(RenderSettings defaultSettings, List<BE.Plaza> ListPlazas)
        {
            colorList = new List<System.Drawing.Color>();

            int numRecords = defaultSettings.DbfReader.DbfRecordHeader.RecordCount;
            for (int n = 0; n < numRecords; ++n)
            {
                int estado = Convert.ToInt32(defaultSettings.DbfReader.GetField(n, 1).Trim());
                int municipio = Convert.ToInt32(defaultSettings.DbfReader.GetField(n, 2).Trim());
                string colString = defaultSettings.DbfReader.GetField(n, 7).Replace("|", "").Trim();
                if (colString == "NA")
                {
                    colString = defaultSettings.DbfReader.GetField(n, 4).Trim() + defaultSettings.DbfReader.GetField(n, 1).Trim().PadLeft(2, '0') + defaultSettings.DbfReader.GetField(n, 2).Trim().PadLeft(3, '0') + defaultSettings.DbfReader.GetField(n, 3).Trim().PadLeft(4, '0') + defaultSettings.DbfReader.GetField(n, 8).Trim().PadLeft(5, '0');
                }
                else
                {
                    colString = defaultSettings.DbfReader.GetField(n, 4).Trim() + colString;
                }
                double colonia = Convert.ToDouble(colString);

                //Se busca la colonia en alguna plaza
                bool existColony = false;
                List<BE.Plaza> plEstado = ListPlazas.Where(p => p.ListaEstados.Where(es => es.Id == estado).Any()).ToList();
                BE.Plaza plazaWithColony = null;
                foreach (BE.Plaza pl in plEstado)
                {
                    List<BE.Estado> plEstados = pl.ListaEstados.Where(es => es.ListaMunicipios.Where(mun => mun.Id == municipio).Any()).ToList();
                    foreach (BE.Estado es in plEstados)
                    {
                        existColony = es.ListaMunicipios.Where(mun => mun.ListaColonias.Where(col => col.Id == colonia).Any()).Any();
                        if (existColony)
                            break;
                    }
                    if (existColony)
                    {
                        plazaWithColony = pl;
                        break;
                    }
                }
                if (plazaWithColony != null)
                {
                    colorList.Add(plazaWithColony.RealColor);
                }
                else
                {
                    colorList.Add(defaultSettings.FillColor);
                }
            }
        }

        #region ICustomRenderSettings Members

        public System.Drawing.Color GetRecordFillColor(int recordNumber)
        {
            if (colorList != null)
            {
                return colorList[recordNumber];
            }
            return defaultSettings.FillColor;
        }

        public System.Drawing.Color GetRecordFontColor(int recordNumber)
        {
            return defaultSettings.FontColor;
        }

        public System.Drawing.Image GetRecordImageSymbol(int recordNumber)
        {
            return defaultSettings.GetImageSymbol();
        }

        public System.Drawing.Color GetRecordOutlineColor(int recordNumber)
        {
            return Color.Gray;
        }

        public Color GetRecordSelectColor(int recordNumber)
        {
            if (colorList != null && colorList[recordNumber] != defaultSettings.FillColor)
            {
                return colorList[recordNumber];
            }
            return defaultSettings.SelectFillColor;
        }
        

        public string GetRecordToolTip(int recordNumber)
        {
            return "";
        }

        public bool RenderShape(int recordNumber)
        {
            return true;
        }

        public bool UseCustomImageSymbols
        {
            get { return false; }
        }

        public bool UseCustomTooltips
        {
            get { return false; }
        }

        public float GetRecordOutlineWidth(int recordNumber)
        {
            return defaultSettings.PenWidthScale;
        }
        #endregion
    }
}