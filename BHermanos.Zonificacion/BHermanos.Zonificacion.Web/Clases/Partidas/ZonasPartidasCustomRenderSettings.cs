using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;
using System.Drawing;


namespace BHermanos.Zonificacion.Web.Clases
{
    public class ZonasPartidasCustomRenderSettings : ICustomRenderSettings
    {
        #region Propiedades
        private List<ColorRecord> colorList;
        private List<ColorRecord> colorListOutLine;
        RenderSettings defaultSettings;
        #endregion

        public ZonasPartidasCustomRenderSettings(RenderSettings defaultSettings, List<BE.Zona> Zonas)
        {
            this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, Zonas);
            BuildBorderColorList(defaultSettings, Zonas);
        }

        private Color GetColorBasedUmbral(List<BE.Humbral> lstUmbrales, double valor)
        {
            BE.Humbral selectedUmbral = null;
            foreach (BE.Humbral umbral in lstUmbrales.OrderBy(u => u.Valor))
            {
                bool breakUmbral = false;
                switch (umbral.Operador)
                {
                    case "<":
                        if (valor < umbral.Valor)
                        {
                            selectedUmbral = umbral;
                            breakUmbral = true;
                        }
                        break;
                    case ">":
                        if (valor > umbral.Valor)
                        {
                            selectedUmbral = umbral;
                            breakUmbral = true;
                        }
                        break;
                    case "=":
                        if (valor == umbral.Valor)
                        {
                            selectedUmbral = umbral;
                            breakUmbral = true;
                        }
                        break;
                    case "<=":
                        if (valor <= umbral.Valor)
                        {
                            selectedUmbral = umbral;
                            breakUmbral = true;
                        }
                        break;
                    case ">=":
                        if (valor >= umbral.Valor)
                        {
                            selectedUmbral = umbral;
                            breakUmbral = true;
                        }
                        break;
                }
                if (breakUmbral)
                    break;
            }
            if (selectedUmbral != null)
            {
                return BE.Cast.ColorConverter.GetColor(selectedUmbral.Color.Replace("#", ""));
            }
            return this.defaultSettings.FillColor;
        }

        private void BuildColorList(RenderSettings defaultSettings, List<BE.Zona> ListZonas)
        {
            try
            {
                colorList = new List<ColorRecord>();
                if (ListZonas.Count > 0)
                {
                    BE.Zona zonaBase = ListZonas[0];
                    //Se obtienen los límites base
                    BE.Partida partidaBase = zonaBase.ListaPartidas.Where(z => z.TieneHumbral).FirstOrDefault();
                    if (partidaBase != null)
                    {
                        List<BE.Humbral> lstUmbrales = partidaBase.ListaHumbrales;
                        //Se leen los shapes (records)
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
                            //Se revisa si la colonia está en alguna zona (primero por estado / municipio)
                            List<BE.Zona> lstZona = ListZonas.Where(z => z.EstadoId == estado && z.MunicipioId == municipio).ToList();
                            if (lstZona.Count > 0)
                            {
                                bool existColony = false;
                                BE.Zona colonyInZona = null;
                                //BE.Zona colonyInZona = null;
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
                                {
                                    double valor = 0;
                                    BE.Partida localPartida = colonyInZona.ListaPartidas.Where(p => p.TieneHumbral).FirstOrDefault();
                                    if (localPartida != null)
                                        valor = localPartida.Valor;
                                    colorList.Add(new ColorRecord() { Color = GetColorBasedUmbral(lstUmbrales, valor), Record = n });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void BuildBorderColorList(RenderSettings defaultSettings, List<BE.Zona> ListZonas)
        {
            try
            {
                colorListOutLine = new List<ColorRecord>();
                if (ListZonas.Count > 0)
                {
                    //Se leen los shapes (records)
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
                        //Se revisa si la colonia está en alguna zona (primero por estado / municipio)
                        List<BE.Zona> lstZona = ListZonas.Where(z => z.EstadoId == estado && z.MunicipioId == municipio).ToList();
                        if (lstZona.Count > 0)
                        {
                            bool existColony = false;
                            BE.Zona colonyInZona = null;
                            //BE.Zona colonyInZona = null;
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
                            {
                                colorListOutLine.Add(new ColorRecord() { Color = colonyInZona.RealColor, Record = n });
                            }
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

        #endregion
    }
}