using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;
using System.Drawing;


namespace BHermanos.Zonificacion.Web
{
    public class ZonaCustomRenderSettings : ICustomRenderSettings
    {
        #region Propiedades
        private List<System.Drawing.Color> colorList;
        private List<System.Drawing.Color> colorBorderList;
        RenderSettings defaultSettings;
        #endregion

        public ZonaCustomRenderSettings(RenderSettings defaultSettings, List<BE.Zona> ListZonas)
        {
            this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, ListZonas);
        }

        private void BuildColorList(RenderSettings defaultSettings, List<BE.Zona> ListZonas)
        {
            colorList = new List<System.Drawing.Color>();
            colorBorderList = new List<Color>();

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

                //double colonia = Convert.ToDouble(defaultSettings.DbfReader.GetField(n, 7).Replace("|", "").Trim());

                List<BE.Zona> lstZona = ListZonas.Where(z => z.EstadoId == estado && z.MunicipioId == municipio).ToList();
                if (lstZona.Count > 0)
                {
                    bool existColony = false;
                    BE.Zona colonyInZona = null;
                    foreach (BE.Zona zon in lstZona)
                    {
                        if (zon.ListaColonias != null && zon.ListaColonias.Count > 0)
                        {
                            BE.Colonia col = zon.ListaColonias.Where(c => c.Id == colonia).FirstOrDefault();
                            if (col != null)
                            {
                                colonyInZona = zon;
                                existColony = true;
                                break;
                            }
                        }
                    }
                    if (existColony)
                        colorList.Add(colonyInZona.RealColor);
                    else
                        colorList.Add(defaultSettings.FillColor);
                }
                else
                    colorList.Add(defaultSettings.FillColor);
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
            if (colorList != null)
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