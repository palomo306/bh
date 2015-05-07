using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;
using System.Drawing;


namespace RegionDemo.Clases
{
    public class SinglePlazaCustomRenderSettings  : ICustomRenderSettings
    {
        #region Propiedades
        private List<ColorRecord> colorList;
        private List<ColorRecord> zoneColorList;                
        RenderSettings defaultSettings;
        #endregion

        public SinglePlazaCustomRenderSettings(RenderSettings defaultSettings, BE.Plaza plaza, List<BE.Zona> lstZonas)
        {
            this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, plaza,lstZonas);
        }

        private void BuildColorList(RenderSettings defaultSettings, BE.Plaza plaza, List<BE.Zona> lstZonas)
        {
            colorList = new List<ColorRecord>();
            zoneColorList = new List<ColorRecord>();
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
                //if (plaza.ListaEstados.Where(le => le.ListaMunicipios.Where(lm => lm.ListaColonias.Where(lc => lc.Id == colonia).Any()).Any()).Any())
                if (plaza.ListaColonias.Where(lc => lc.Id == colonia).Any())
                    colorList.Add(new ColorRecord() { Color = plaza.RealColor, Record = n });
                //Se busca si la colonia existe en alguna zona
                BE.Zona zone = lstZonas.Where(lz => lz.ListaColonias.Where(c => c.Id == colonia).Any()).FirstOrDefault();
                if (zone != null)
                    zoneColorList.Add(new ColorRecord() { Color = zone.RealColor, Record = n });
            }
        }

        #region ICustomRenderSettings Members

        public System.Drawing.Color GetRecordFillColor(int recordNumber)
        {
            ColorRecord cRecord = zoneColorList.Where(cr => cr.Record == recordNumber).FirstOrDefault();
            if (cRecord != null)
            {
                return cRecord.Color;
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
            ColorRecord cRecord = colorList.Where(cr => cr.Record == recordNumber).FirstOrDefault();
            if (cRecord != null)
            {
                return cRecord.Color;
            }
            return Color.Gray;
        }

        public Color GetRecordSelectColor(int recordNumber)
        {
            ColorRecord cRecord = zoneColorList.Where(cr => cr.Record == recordNumber).FirstOrDefault();
            if (cRecord != null)
            {
                return cRecord.Color;
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
            ColorRecord cRecord = colorList.Where(cr => cr.Record == recordNumber).FirstOrDefault();
            if (cRecord != null)
            {
                return 2;
            }
            return defaultSettings.PenWidthScale;
        }
        #endregion
    }
}