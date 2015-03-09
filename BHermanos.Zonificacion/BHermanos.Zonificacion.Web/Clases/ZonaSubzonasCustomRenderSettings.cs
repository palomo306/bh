using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;
using System.Drawing;


namespace BHermanos.Zonificacion.Web.Clases
{
    public class ColorRecord
    {
        public int Record { get; set; }
        public Color Color { get; set; }
    }

    public class ZonaSubzonasCustomRenderSettings : ICustomRenderSettings
    {
        #region Propiedades
        private List<ColorRecord> colorList;
        private List<ColorRecord> colorListOutLine;
        RenderSettings defaultSettings;
        #endregion

        public ZonaSubzonasCustomRenderSettings(RenderSettings defaultSettings, BE.Zona Zona)
        {
            this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, Zona);
            BuildBorderColorList(defaultSettings, Zona);
        }

        private void BuildColorList(RenderSettings defaultSettings, BE.Zona Zona)
        {
            try
            {
                List<BE.Zona> ListZonas = Zona.ListaSubzonas;
                colorList = new List<ColorRecord>();                

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
                    //Se revisan las subzonas
                    List<BE.Zona> lstZona = ListZonas.Where(z => z.EstadoId == estado && z.MunicipioId == municipio).ToList();
                    if (lstZona.Count > 0)
                    {
                        bool existColony = false;
                        BE.Zona colonyInSubZona = null;
                        //BE.Zona colonyInZona = null;
                        foreach (BE.Zona zon in lstZona)
                        {
                            if (zon.ListaColonias != null && zon.ListaColonias.Count > 0)
                            {
                                BE.Colonia col = zon.ListaColonias.Where(c => c.Id == colonia).FirstOrDefault();
                                if (col != null)
                                {
                                    colonyInSubZona = zon;
                                    existColony = true;
                                    break;
                                }
                            }
                        }
                        if (existColony)
                            colorList.Add(new ColorRecord() { Color = colonyInSubZona.RealColor, Record = n });
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BuildBorderColorList(RenderSettings defaultSettings, BE.Zona Zona) 
        {
            try
            {
                colorListOutLine = new List<ColorRecord>();
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

                    //Se revisa la zona
                    bool existColonyZona = false;
                    if (Zona.EstadoId == estado && Zona.MunicipioId == municipio)
                    {
                        if (Zona.ListaColonias != null && Zona.ListaColonias.Count > 0)
                        {

                            BE.Colonia col = Zona.ListaColonias.Where(c => c.Id == colonia).FirstOrDefault();
                            if (col != null)
                            {
                                existColonyZona = true;
                            }
                        }
                        if (existColonyZona)
                        {
                            colorListOutLine.Add(new ColorRecord() { Color = Zona.RealColor, Record = n });
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #region ICustomRenderSettings Members

        public System.Drawing.Color GetRecordFillColor(int recordNumber)
        {
            if (colorList != null)
            {
                ColorRecord record = colorList.Where(cl => cl.Record == recordNumber).FirstOrDefault();
                if (record != null)
                    return record.Color;
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
            if (colorListOutLine != null)
            {
                ColorRecord record = colorListOutLine.Where(cl => cl.Record == recordNumber).FirstOrDefault();
                if (record != null)
                    return record.Color;
            }
            return Color.Gray;
        }

        public Color GetRecordSelectColor(int recordNumber)
        {
            if (colorList != null)
            {
                ColorRecord record = colorList.Where(cl => cl.Record == recordNumber).FirstOrDefault();
                if (record != null)
                    return record.Color;
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