using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;
using System.Drawing;


namespace BHermanos.Zonificacion.Web.Clases
{
    public class MunicipioCustomRenderSettings : ICustomRenderSettings
    {
        #region Propiedades
        private List<int> indexBorderShapes;
        private List<System.Drawing.Color> colorList;
        RenderSettings defaultSettings;        
        #endregion

        public MunicipioCustomRenderSettings(RenderSettings defaultSettings, int municipioId)
        {
            this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, municipioId);
        }

        private void BuildColorList(RenderSettings defaultSettings, int municipioId)
        {
            colorList = new List<System.Drawing.Color>();
            indexBorderShapes = new List<int>();
            int numRecords = defaultSettings.DbfReader.DbfRecordHeader.RecordCount;
            for (int n = 0; n < numRecords; ++n)
            {
                int municipio = Convert.ToInt32(defaultSettings.DbfReader.GetField(n, 2).Trim());
                if (municipioId == municipio)
                {
                    colorList.Add(Color.DarkRed);
                    indexBorderShapes.Add(n);
                }
                else
                    colorList.Add(defaultSettings.FillColor);
            }
        }

        #region ICustomRenderSettings Members

        public System.Drawing.Color GetRecordFillColor(int recordNumber)
        {
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
            if (colorList != null)
            {
                return colorList[recordNumber];
            }
            return defaultSettings.OutlineColor;
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

        public Color GetRecordSelectColor(int recordNumber)
        {
            return defaultSettings.SelectFillColor;
        }

        public float GetRecordOutlineWidth(int recordNumber)
        {
            if (indexBorderShapes.Where(r => r == recordNumber).Any())
                return 2;
            return defaultSettings.PenWidthScale;
        }
        #endregion
    }
}