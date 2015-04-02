using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace BHermanos.Zonificacion.BusinessEntities.Cast
{
    public static class ReportZonesConversor
    {
        #region Propiedades
        public static string BtnName { get; set; }
        public static string Hdn1Name { get; set; }
        public static string Hdn2Name { get; set; }
        #endregion

        public static DataTable ToGeneralDataTable(Zona prmZonaBase, List<Zona> lstZonas, int appId, int level)
        {
            //Se obtiene la colonia base
            Colonia colBase = prmZonaBase.GetColoniaTotal();
            //Se agrega la primera columna con valores
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Dato");
            foreach (GrupoRubros gpo in colBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "general"))
            {
                foreach (Rubro rbo in gpo.ListaRubros.Where(r => r.Main))
                {
                    DataRow newRow = dtResult.NewRow();
                    newRow["Dato"] = rbo.Nombre;
                    dtResult.Rows.Add(newRow);
                }
            }
            if (lstZonas != null && lstZonas.Count > 0)
            {
                //Se agrega la fila para los botones
                DataRow newRowBotones = dtResult.NewRow();
                dtResult.Rows.Add(newRowBotones);
                //Se colocan los datos de cada zona para cada rubro
                foreach (Zona zno in lstZonas)
                {
                    int row = 0;
                    //Se agrega la columna con la zona
                    dtResult.Columns.Add(zno.Nombre, typeof(string));
                    //Se agregan los rubros para esta zona
                    Colonia colZnBase = zno.GetColoniaTotal();
                    foreach (GrupoRubros gpo in colZnBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "general"))
                    {
                        GrupoRubros gpoInBase = colZnBase.ListaGrupoRubros.Where(gIB => gIB.Id == gpo.Id).FirstOrDefault();
                        if (gpoInBase != null)
                        {
                            foreach (Rubro rbo in gpo.ListaRubros.Where(r => r.Main))
                            {
                                Rubro rb = gpoInBase.ListaRubros.Where(rIB => rIB.Id == rbo.Id).FirstOrDefault();
                                if (appId == 1)
                                {
                                    if (rb != null)
                                        dtResult.Rows[row][zno.Nombre] = string.Format(rb.Formato, rb.Valor);
                                    else
                                        dtResult.Rows[row][zno.Nombre] = "0";
                                }
                                else
                                {
                                    if (rb != null)
                                        dtResult.Rows[row][zno.Nombre] = string.Format(rb.Formato, rb.Valor) + "|" + zno.Color;
                                    else
                                        dtResult.Rows[row][zno.Nombre] = "0" + "|" + zno.Color; ;
                                }
                                row++;
                            }
                            if (appId == 1)
                                dtResult.Rows[row][zno.Nombre] = "Editar";
                            else
                            {
                                dtResult.Rows[row][zno.Nombre] = @"Editar|<input type=""button"" value=""Ver"" onclick=""SelectZone('" + zno.Id.ToString() + @"','" + level + @"','" +  Hdn1Name + "','" + Hdn2Name + "','" + BtnName + @"');"" class=""BotonChico"" />|" + zno.Color;
                            }
                        }
                    }
                }
            }
            return dtResult;
        }

        public static DataTable ToGeneralDataTableAll(Zona prmZonaBase, List<Zona> lstZonas)
        {
            //Se obtiene la colonia base
            Colonia colBase = prmZonaBase.GetColoniaTotal();
            //Se agrega la primera columna con valores
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Dato");
            foreach (GrupoRubros gpo in colBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "general"))
            {
                foreach (Rubro rbo in gpo.ListaRubros)
                {
                    DataRow newRow = dtResult.NewRow();
                    newRow["Dato"] = rbo.Nombre;
                    dtResult.Rows.Add(newRow);
                }
            }
            if (lstZonas != null && lstZonas.Count > 0)
            {
                //Se colocan los datos de cada zona para cada rubro
                foreach (Zona zno in lstZonas)
                {
                    int row = 0;
                    //Se agrega la columna con la zona
                    dtResult.Columns.Add(zno.Nombre, typeof(string));
                    //Se agregan los rubros para esta zona
                    Colonia colZnBase = zno.GetColoniaTotal();
                    foreach (GrupoRubros gpo in colZnBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "general"))
                    {
                        GrupoRubros gpoInBase = colZnBase.ListaGrupoRubros.Where(gIB => gIB.Id == gpo.Id).FirstOrDefault();
                        if (gpoInBase != null)
                        {
                            foreach (Rubro rbo in gpo.ListaRubros.Where(r => r.Main))
                            {
                                Rubro rb = gpoInBase.ListaRubros.Where(rIB => rIB.Id == rbo.Id).FirstOrDefault();
                                if (rb != null)
                                    dtResult.Rows[row][zno.Nombre] = string.Format(rb.Formato, rb.Valor);
                                else
                                    dtResult.Rows[row][zno.Nombre] = "0";
                                row++;
                            }
                        }
                    }
                }
            }
            return dtResult;
        }

        public static DataTable ToGeneralDataTableNse(Zona prmZonaBase, List<Zona> lstZonas)
        {
            //Se obtiene la colonia base
            Colonia colBase = prmZonaBase.GetColoniaTotal();
            //Se agrega la primera columna con valores
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Dato");
            foreach (GrupoRubros gpo in colBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "nse"))
            {
                foreach (Rubro rbo in gpo.ListaRubros)
                {
                    DataRow newRow = dtResult.NewRow();
                    newRow["Dato"] = rbo.Nombre;
                    dtResult.Rows.Add(newRow);
                }
            }
            if (lstZonas != null && lstZonas.Count > 0)
            {
                //Se colocan los datos de cada zona para cada rubro
                foreach (Zona zno in lstZonas)
                {
                    int row = 0;
                    //Se agrega la columna con la zona
                    dtResult.Columns.Add(zno.Nombre, typeof(string));
                    //Se agregan los rubros para esta zona
                    Colonia colZnBase = zno.GetColoniaTotal();
                    foreach (GrupoRubros gpo in colZnBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "nse"))
                    {
                        GrupoRubros gpoInBase = colZnBase.ListaGrupoRubros.Where(gIB => gIB.Id == gpo.Id).FirstOrDefault();
                        if (gpoInBase != null)
                        {
                            foreach (Rubro rbo in gpo.ListaRubros)
                            {
                                Rubro rb = gpoInBase.ListaRubros.Where(rIB => rIB.Id == rbo.Id).FirstOrDefault();
                                if (rb != null)
                                    dtResult.Rows[row][zno.Nombre] = string.Format(rb.Formato, rb.Valor);
                                else
                                    dtResult.Rows[row][zno.Nombre] = "0.00";
                                row++;
                            }
                        }
                    }
                }
            }
            return dtResult;
        }


        public static DataTable ToZoneDataTable(Zona zoneBase, bool isWeb)
        {
            //Se agrega la primera columna con valores
            DataTable dtResult = new DataTable();
            string colDat = "Dato";
            string colGroup = "Agrupación";
            string colLastCol = "Última Colonia";
            string colTotal = "Total";
            if (isWeb)
            {
                colDat = "Dato";
                colGroup = "Agrup.";
                colLastCol = "Última";
                colTotal = "Total";
            }
            dtResult.Columns.Add(colDat);
            dtResult.Columns.Add(colGroup, typeof(string));
            dtResult.Columns.Add(colLastCol, typeof(string));
            dtResult.Columns.Add(colTotal, typeof(string));
            Colonia colBase = zoneBase.GetColoniaTotal();
            Colonia colExceptLast = zoneBase.GetColoniaExceptLast();
            Colonia colLast = zoneBase.GetColoniaLast();
            foreach (GrupoRubros gpo in colBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "general"))
            {
                GrupoRubros gpoExLast = colExceptLast.ListaGrupoRubros.Where(gpEL => gpEL.Id == gpo.Id).FirstOrDefault();
                GrupoRubros gpoLast = colLast.ListaGrupoRubros.Where(gpEL => gpEL.Id == gpo.Id).FirstOrDefault();
                foreach (Rubro rbo in gpo.ListaRubros.Where(r => r.Main))
                {
                    Rubro rboExLast = gpoExLast.ListaRubros.Where(rbEL => rbEL.Id == rbo.Id).FirstOrDefault();
                    Rubro rboLast = gpoLast.ListaRubros.Where(rbEL => rbEL.Id == rbo.Id).FirstOrDefault();
                    DataRow newRow = dtResult.NewRow();
                    newRow[colDat] = rbo.Nombre;
                    newRow[colGroup] = string.Format(rboExLast.Formato, rboExLast.Valor);
                    newRow[colLastCol] = string.Format(rboLast.Formato, rboLast.Valor);
                    newRow[colTotal] = string.Format(rbo.Formato, rbo.Valor);
                    dtResult.Rows.Add(newRow);
                }
            }
            return dtResult;
        }

        public static DataTable ToListZonesReport(List<Zona> lstZonas)
        {
            //Se agrega la primera columna con valores
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Zona");
            dtResult.Columns.Add("Ver", typeof(string));
            dtResult.Columns.Add("ZoneId", typeof(string));

            foreach (Zona zn in lstZonas)
            {
                DataRow oNewRow = dtResult.NewRow();
                oNewRow["Zona"] = zn.Nombre;
                oNewRow["Ver"] = "Ver";
                oNewRow["ZoneId"] = zn.Id.ToString();
                dtResult.Rows.Add(oNewRow);
            }
            return dtResult;
        }

        public static DataTable ToZoneDetailDataTable(Zona zoneBase)
        {
            //Se agrega la primera columna con valores
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Dato");
            dtResult.Columns.Add("Total", typeof(string));
            Colonia colBase = zoneBase.GetColoniaTotal();
            Colonia colExceptLast = zoneBase.GetColoniaExceptLast();
            Colonia colLast = zoneBase.GetColoniaLast();
            foreach (GrupoRubros gpo in colBase.ListaGrupoRubros.Where(g => g.Nombre.ToLower() == "general"))
            {
                GrupoRubros gpoExLast = colExceptLast.ListaGrupoRubros.Where(gpEL => gpEL.Id == gpo.Id).FirstOrDefault();
                GrupoRubros gpoLast = colLast.ListaGrupoRubros.Where(gpEL => gpEL.Id == gpo.Id).FirstOrDefault();
                foreach (Rubro rbo in gpo.ListaRubros.Where(r => r.Main))
                {
                    Rubro rboExLast = gpoExLast.ListaRubros.Where(rbEL => rbEL.Id == rbo.Id).FirstOrDefault();
                    Rubro rboLast = gpoLast.ListaRubros.Where(rbEL => rbEL.Id == rbo.Id).FirstOrDefault();
                    DataRow newRow = dtResult.NewRow();
                    newRow["Dato"] = rbo.Nombre;
                    newRow["Total"] = string.Format(rbo.Formato, rbo.Valor);
                    dtResult.Rows.Add(newRow);
                }
            }
            return dtResult;
        }


        public static DataTable ToListPartidas(List<Zona> lstZonas, string level)
        {
            DataTable dtResult = new DataTable();
            if (lstZonas != null && lstZonas.Count > 0)
            {
                //Se revisa el nivel
                string columnDato = "Zona";
                string columnId = "ZonaId";
                if (level == "1")
                {
                    columnDato = "Subzona";
                    columnId = "SubzonaId";
                }
                //Se crea la estructura del DataTable
                dtResult.Columns.Add(columnDato);
                Zona zonaRef = lstZonas[0];    //Zona referencia (base)
                foreach (Partida partida in zonaRef.ListaPartidas)
                {
                    dtResult.Columns.Add(partida.Nombre);
                }
                dtResult.Columns.Add("Ver");
                //Se insertan los datos
                foreach (Zona zn in lstZonas)
                {
                    DataRow oNewRow = dtResult.NewRow();
                    oNewRow[columnDato] = zn.Nombre;
                    foreach (Partida partida in zn.ListaPartidas)
                    {
                        if (partida.TieneHumbral)
                            oNewRow[partida.Nombre] = "|||" + partida.Valor;
                        else
                            oNewRow[partida.Nombre] = partida.Valor;
                    }
                    oNewRow["Ver"] = @"Editar|<input type=""button"" value=""Ver"" onclick=""SelectZone('" + zn.Id.ToString() + @"','" + level + @"','" + Hdn1Name + "','" + Hdn2Name + "','" + BtnName + @"');"" class=""BotonChico"" />";
                    dtResult.Rows.Add(oNewRow);
                }
            }
            return dtResult;
        }

        public static DataTable ToListPartidas(List<Colonia> lstColonias)
        {
            DataTable dtResult = new DataTable();
            if ( lstColonias.Count > 0)
            {
                //Se revisa el nivel
                string columnDato = "Colonia";
                string columnId = "ColoniaId";
                //Se crea la estructura del DataTable
                dtResult.Columns.Add(columnDato);
                Colonia zonaRef = lstColonias[0];    //Zona referencia (base)
                foreach (Partida partida in zonaRef.ListaPartidas)
                {
                    dtResult.Columns.Add(partida.Nombre);
                }                
                //Se insertan los datos
                foreach (Colonia col in lstColonias)
                {
                    DataRow oNewRow = dtResult.NewRow();
                    oNewRow[columnDato] = col.Nombre;
                    foreach (Partida partida in col.ListaPartidas)
                    {
                        if (partida.TieneHumbral)
                            oNewRow[partida.Nombre] = "|||" + partida.Valor;
                        else
                            oNewRow[partida.Nombre] = partida.Valor;
                    }
                    dtResult.Rows.Add(oNewRow);
                }
            }
            return dtResult;
        }


        private static string GetColorIndicator(string color)
        {
            string resultHtml = @"<table border=""1"" style=""width:20px; height:15px;""><tr><td style=""background-color:#" + color + @""">&nbsp;</td></tr></table>";
            return resultHtml;
        }

        public static string ToHtmlUmbrales(List<Zona> lstZonas)
        {
            string resultHtml = string.Empty;
            if (lstZonas != null && lstZonas.Count > 0)
            {
                resultHtml = "<table>" + Environment.NewLine;
                Zona zonaRef = lstZonas[0];    //Zona referencia (base)
                foreach (Partida partida in zonaRef.ListaPartidas)
                {
                    if (partida.TieneHumbral)
                    {
                        resultHtml += @"<tr><td colspan=""2"" class=""HeaderText"">" + partida.Nombre + "</td></tr>" + Environment.NewLine;
                        resultHtml += @"<tr><td colspan=""2"">&nbsp;</td></tr>" + Environment.NewLine;
                        foreach (Humbral umb in partida.ListaHumbrales)
                        {
                            resultHtml += @"<tr><td>" + GetColorIndicator(umb.Color.Replace("#", "")) + @"</td><td class=""NormalText"">" + umb.Operador + " " + umb.Valor + "</td></tr>" + Environment.NewLine; ;
                        }
                    }
                }
                resultHtml += "</table>";
            }
            return resultHtml;
        }

        public static string ToHtmlUmbrales(List<Colonia> lstColonias)
        {
            string resultHtml = string.Empty;
            if (lstColonias.Count > 0)
            {
                resultHtml = "<table>" + Environment.NewLine;
                Colonia colRef = lstColonias[0];    //Zona referencia (base)
                foreach (Partida partida in colRef.ListaPartidas)
                {
                    if (partida.TieneHumbral)
                    {
                        resultHtml += @"<tr><td colspan=""2"" class=""HeaderText"">" + partida.Nombre + "</td></tr>" + Environment.NewLine;
                        resultHtml += @"<tr><td colspan=""2"">&nbsp;</td></tr>" + Environment.NewLine;
                        foreach (Humbral umb in partida.ListaHumbrales)
                        {
                            resultHtml += @"<tr><td>" + GetColorIndicator(umb.Color.Replace("#", "")) + @"</td><td class=""NormalText"">" + umb.Operador + " " + umb.Valor + "</td></tr>" + Environment.NewLine; ;
                        }
                    }
                }
                resultHtml += "</table>";
            }
            return resultHtml;
        }
    }
}