using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;
using System.Drawing;


namespace RegionDemo.Clases
{
    public class ColoniaCustomRenderSettings : ICustomRenderSettings
    {
        #region Propiedades
        private List<System.Drawing.Color> colorList;
        RenderSettings defaultSettings;        
        #endregion

        public ColoniaCustomRenderSettings (RenderSettings defaultSettings)
        {
            this.defaultSettings = defaultSettings;
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
            return Color.DarkRed;
        }

        public Color GetRecordSelectColor(int recordNumber)
        {
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