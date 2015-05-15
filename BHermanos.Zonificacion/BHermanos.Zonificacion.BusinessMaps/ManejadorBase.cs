using BHermanos.Zonificacion.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using BHermanos.Zonificacion.BusinessEntities;
using System.Globalization;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public abstract class ManejadorBase : IDisposable
    {

        #region Atributos

        protected DataAccessDataContext oDataAccess;

        #endregion

        #region Constructores

        protected ManejadorBase()
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["ConexionZonificacion"].ToString();
                int commandTimeOut = int.Parse(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                //this.oDataAccess = new DataAccessDataContext(connString, commandTimeOut);
                this.oDataAccess = new DataAccessDataContext(connString);
                this.oDataAccess.CommandTimeout = commandTimeOut;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Metodos

        public void Dispose()
        {
            if (oDataAccess != null)
                oDataAccess.Dispose();
        }

        protected virtual double AsignaValor(String expresion, DataAccess.spConColoniasResult coloniasResult, byte tipo, DataAccess.ZonTab zonTab, DateTime fechaInicio, DateTime fechaFin, bool esBusquedaXFecha)
        {
            double valor = 0.0D;
            try
            {
                string cadena = string.Empty;
                if (coloniasResult != null)
                {

                    for (int i = 0; i < expresion.Length; i++)
                    {
                        string caracter = expresion.Substring(i, 1);
                        if (caracter == "|")
                        {
                            int ini = i;
                            int fin = expresion.IndexOf("|", i + 1);
                            string subExpresion = expresion.Substring(ini + 1, fin - ini - 1);

                            if (subExpresion.IndexOf("==") > 0)
                            {
                                string[] array = subExpresion.Replace("==", "=").Split("=".ToCharArray());
                                object obj = this.BuscaValor(coloniasResult, array[0]);
                                string val = string.Empty;

                                //if (obj.GetType() == typeof(string))
                                val = (string)obj.ToString();
                                //if (obj.GetType() == typeof(double))
                                //val = ((double)obj).ToString();

                                if (val == array[1])
                                {
                                    cadena += "1";
                                }
                                else
                                {
                                    cadena += "0";
                                }
                            }
                            else
                            {
                                object obj;
                                if (tipo == 1)
                                {
                                    obj = this.BuscaValor(coloniasResult, subExpresion);
                                }
                                else
                                {
                                    DataAccess.ZonNegocio negocio = zonTab.ZonNegocios.Where(w => w.flEstatus == true && w.fiNegocioId == int.Parse(subExpresion)).FirstOrDefault();
                                    if (negocio != null)
                                    {
                                        if (!esBusquedaXFecha)
                                        {
                                            fechaInicio = negocio.ZonDatosXNegocios.Where(w => w.fiNegocioId == negocio.fiNegocioId && w.fiColoniaId == coloniasResult.IdColonia).OrderByDescending(o => o.fdFecha).Select(d => DateTime.Parse(d.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX")))).Distinct().ToList<DateTime>().FirstOrDefault();
                                            fechaFin = fechaInicio;
                                        }
                                        obj = negocio.ZonDatosXNegocios.Where(w => w.fiNegocioId == negocio.fiNegocioId && w.fiColoniaId == coloniasResult.IdColonia && DateTime.Parse(w.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX"))) >= fechaInicio && DateTime.Parse(w.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX"))) <= fechaFin).Select(s => s.fiValor).Sum();
                                    }
                                    else {
                                        obj = 0.0D;
                                    }
                                    
                                }                            
                                if (obj.GetType() == typeof(double))
                                    cadena += (double)obj;
                                else
                                    cadena += 0.0D;
                            }
                            i = fin;
                        }
                        else
                        {
                            cadena += caracter;
                        }
                    }
                    Evaluador e = new Evaluador(cadena);
                    valor = e.F();

                }
            }
            catch (Exception)
            {
            }
            return valor;
        }

        protected virtual double AsignaValor(String expresion, DataAccess.ZonColoniasLocalidade zonColoniasLocalidade, byte tipo, DataAccess.ZonTab zonTab, DateTime fechaInicio, DateTime fechaFin, bool esBusquedaXFecha)
        {
            double valor = 0.0D;
            try
            {
                string cadena = string.Empty;
                if (zonColoniasLocalidade != null)
                {

                    for (int i = 0; i < expresion.Length; i++)
                    {
                        string caracter = expresion.Substring(i, 1);
                        if (caracter == "|")
                        {
                            int ini = i;
                            int fin = expresion.IndexOf("|", i + 1);
                            string subExpresion = expresion.Substring(ini + 1, fin - ini - 1);

                            if (subExpresion.IndexOf("==") > 0)
                            {
                                string[] array = subExpresion.Replace("==", "=").Split("=".ToCharArray());
                                object obj = this.BuscaValor(zonColoniasLocalidade, array[0]);
                                string val = string.Empty;
                                
                                val = (string)obj.ToString();

                                if (val == array[1])
                                {
                                    cadena += "1";
                                }
                                else
                                {
                                    cadena += "0";
                                }
                            }
                            else
                            {
                                object obj;
                                if (tipo == 1)
                                {
                                    //if (subExpresion.ToUpper() == "DIST_TOT")
                                    //{
                                    //    obj = 0;
                                    //}
                                    //else
                                    //{
                                        obj = this.BuscaValor(zonColoniasLocalidade, subExpresion);
                                    //}
                                }
                                else
                                {
                                    DataAccess.ZonNegocio negocio = zonTab.ZonNegocios.Where(w => w.flEstatus == true && w.fiNegocioId == int.Parse(subExpresion)).FirstOrDefault();
                                    if (negocio != null)
                                    {
                                        if (!esBusquedaXFecha)
                                        {
                                            fechaInicio = negocio.ZonDatosXNegocios.Where(w => w.fiNegocioId == negocio.fiNegocioId && w.fiColoniaId == zonColoniasLocalidade.idColonia).OrderByDescending(o => o.fdFecha).Select(d => DateTime.Parse(d.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX")))).Distinct().ToList<DateTime>().FirstOrDefault();
                                            fechaFin = fechaInicio;
                                        }
                                        obj = negocio.ZonDatosXNegocios.Where(w => w.fiNegocioId == negocio.fiNegocioId && w.fiColoniaId == zonColoniasLocalidade.idColonia && DateTime.Parse(w.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX"))) >= fechaInicio && DateTime.Parse(w.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX"))) <= fechaFin).Select(s => s.fiValor).Sum();
                                    }
                                    else
                                    {
                                        obj = 0.0D;
                                    }

                                }
                                if (obj.GetType() == typeof(double))
                                    cadena += (double)obj;
                                else
                                    cadena += 0.0D;
                            }
                            i = fin;
                        }
                        else
                        {
                            cadena += caracter;
                        }
                    }
                    Evaluador e = new Evaluador(cadena);
                    valor = e.F();

                }
            }
            catch (Exception)
            {
            }
            return valor;
        }

        protected virtual List<Humbral> ObtieneHumbrales(int nivel, int plazaId, int zonaId, double coloniaId, double valor, DataAccess.ZonPartidasXTab zonPartidaXTab)
        {
            List<Humbral> listaHumbrales = new List<Humbral>();
            List<DataAccess.ZonUmbralesXPartida> listaZonUmbralesXPartida = null;
            try
            {
                listaZonUmbralesXPartida = zonPartidaXTab.ZonUmbralesXPartidas.Where(h => h.fiNivel == nivel && h.fiPlazaId == plazaId && h.fiZonaId == zonaId && h.fiColoniaId == coloniaId).OrderBy(hh => hh.fiConsecutivo).ToList<DataAccess.ZonUmbralesXPartida>();
                if (listaZonUmbralesXPartida == null || listaZonUmbralesXPartida.Count() == 0)
                {
                    listaZonUmbralesXPartida = zonPartidaXTab.ZonUmbralesXPartidas.Where(h => h.fiNivel == nivel && h.fiPlazaId == plazaId && h.fiZonaId == zonaId).OrderBy(hh => hh.fiConsecutivo).ToList<DataAccess.ZonUmbralesXPartida>();
                    if (listaZonUmbralesXPartida == null || listaZonUmbralesXPartida.Count() == 0)
                    {
                        listaZonUmbralesXPartida = zonPartidaXTab.ZonUmbralesXPartidas.Where(h => h.fiNivel == nivel && h.fiPlazaId == plazaId).OrderBy(hh => hh.fiConsecutivo).ToList<DataAccess.ZonUmbralesXPartida>();
                        if (listaZonUmbralesXPartida == null || listaZonUmbralesXPartida.Count() == 0)
                        {
                            listaZonUmbralesXPartida = zonPartidaXTab.ZonUmbralesXPartidas.Where(h => h.fiNivel == nivel && h.fiPlazaId == plazaId).OrderBy(hh => hh.fiConsecutivo).ToList<DataAccess.ZonUmbralesXPartida>();
                            if (listaZonUmbralesXPartida == null || listaZonUmbralesXPartida.Count() == 0)
                            {
                                listaZonUmbralesXPartida = zonPartidaXTab.ZonUmbralesXPartidas.Where(h => h.fiNivel == nivel).OrderBy(hh => hh.fiConsecutivo).ToList<DataAccess.ZonUmbralesXPartida>();
                            }
                        }
                    }
                }

                foreach (DataAccess.ZonUmbralesXPartida zonUmbralXPartida in listaZonUmbralesXPartida)
                {
                    Humbral humbral = new Humbral();
                    humbral.Consucutivo = zonUmbralXPartida.fiConsecutivo;
                    humbral.Operador = zonUmbralXPartida.fcOperador;
                    humbral.Color = zonUmbralXPartida.fcColor;
                    humbral.Valor = zonUmbralXPartida.fiValor;
                    listaHumbrales.Add(humbral);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listaHumbrales;
        }

        protected virtual string ObtieneColor(double valor, List<Humbral> humbrales)
        {
            string color = string.Empty;
            try
            {
                foreach (Humbral humbral in humbrales)
                {
                    switch (humbral.Operador)
                    {
                        case "==":
                            if (valor == humbral.Valor)
                            {
                                color = humbral.Color;
                                return color;
                            }
                            break;
                        case "<":
                            if (valor < humbral.Valor)
                            {
                                color = humbral.Color;
                                return color;
                            }
                            break;
                        case "<=":
                            if (valor <= humbral.Valor)
                            {
                                color = humbral.Color;
                                return color;
                            }
                            break;
                        case ">":
                            if (valor > humbral.Valor)
                            {
                                color = humbral.Color;
                                return color;
                            }
                            break;
                        case ">=":
                            if (valor >= humbral.Valor)
                            {
                                color = humbral.Color;
                                return color;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return color;
        }

        protected virtual object BuscaValor(DataAccess.spConColoniasResult coloniasResult, string key)
        {
            object valor = 0;
            switch (key.ToUpper())
            {
                case "IDTIPO": valor = (double)coloniasResult.IdTipo; break;
                case "NSE_PREDO": valor = ((string)coloniasResult.NSE_Predo).ToUpper(); break;
                case "AB": valor = (double)coloniasResult.ab; break;
                case "CMAS": valor = (double)coloniasResult.cmas; break;
                case "C": valor = (double)coloniasResult.c; break;
                case "CMENOS": valor = (double)coloniasResult.cmenos; break;
                case "DMAS": valor = (double)coloniasResult.dmas; break;
                case "D": valor = (double)coloniasResult.d; break;
                case "E": valor = (double)coloniasResult.e; break;
                case "NC": valor = (double)coloniasResult.nc; break;
                case "POBTOT": valor = (double)coloniasResult.pobtot; break;
                case "POBMAS": valor = (double)coloniasResult.pobmas; break;
                case "POBFEM": valor = (double)coloniasResult.pobfem; break;
                case "P_0A2": valor = (double)coloniasResult.p_0a2; break;
                case "P_0A2_M": valor = (double)coloniasResult.p_0a2_m; break;
                case "P_0A2_F": valor = (double)coloniasResult.p_0a2_f; break;
                case "P_3YMAS": valor = (double)coloniasResult.p_3ymas; break;
                case "P_3YMAS_M": valor = (double)coloniasResult.p_3ymas_m; break;
                case "P_3YMAS_F": valor = (double)coloniasResult.p_3ymas_f; break;
                case "P_5YMAS": valor = (double)coloniasResult.p_5ymas; break;
                case "P_5YMAS_M": valor = (double)coloniasResult.p_5ymas_m; break;
                case "P_5YMAS_F": valor = (double)coloniasResult.p_5ymas_f; break;
                case "P_12YMAS": valor = (double)coloniasResult.p_12ymas; break;
                case "P_12YMAS_M": valor = (double)coloniasResult.p_12ymas_m; break;
                case "P_12YMAS_F": valor = (double)coloniasResult.p_12ymas_f; break;
                case "P_15YMAS": valor = (double)coloniasResult.p_15ymas; break;
                case "P_15YMAS_M": valor = (double)coloniasResult.p_15ymas_m; break;
                case "P_15YMAS_F": valor = (double)coloniasResult.p_15ymas_f; break;
                case "P_18YMAS": valor = (double)coloniasResult.p_18ymas; break;
                case "P_18YMAS_M": valor = (double)coloniasResult.p_18ymas_m; break;
                case "P_18YMAS_F": valor = (double)coloniasResult.p_18ymas_f; break;
                case "P_3A5": valor = (double)coloniasResult.p_3a5; break;
                case "P_3A5_M": valor = (double)coloniasResult.p_3a5_m; break;
                case "P_3A5_F": valor = (double)coloniasResult.p_3a5_f; break;
                case "P_6A11": valor = (double)coloniasResult.p_6a11; break;
                case "P_6A11_M": valor = (double)coloniasResult.p_6a11_m; break;
                case "P_6A11_F": valor = (double)coloniasResult.p_6a11_f; break;
                case "P_8A14": valor = (double)coloniasResult.p_8a14; break;
                case "P_8A14_M": valor = (double)coloniasResult.p_8a14_m; break;
                case "P_8A14_F": valor = (double)coloniasResult.p_8a14_f; break;
                case "P_12A14": valor = (double)coloniasResult.p_12a14; break;
                case "P_12A14_M": valor = (double)coloniasResult.p_12a14_m; break;
                case "P_12A14_F": valor = (double)coloniasResult.p_12a14_f; break;
                case "P_15A17": valor = (double)coloniasResult.p_15a17; break;
                case "P_15A17_M": valor = (double)coloniasResult.p_15a17_m; break;
                case "P_15A17_F": valor = (double)coloniasResult.p_15a17_f; break;
                case "P_18A24": valor = (double)coloniasResult.p_18a24; break;
                case "P_18A24_M": valor = (double)coloniasResult.p_18a24_m; break;
                case "P_18A24_F": valor = (double)coloniasResult.p_18a24_f; break;
                case "P_15A49_F": valor = (double)coloniasResult.p_15a49_f; break;
                case "P_60YMAS": valor = (double)coloniasResult.p_60ymas; break;
                case "P_60YMAS_M": valor = (double)coloniasResult.p_60ymas_m; break;
                case "P_60YMAS_F": valor = (double)coloniasResult.p_60ymas_f; break;
                case "REL_H_M": valor = (double)coloniasResult.rel_h_m; break;
                case "POB0_14": valor = (double)coloniasResult.pob0_14; break;
                case "POB15_64": valor = (double)coloniasResult.pob15_64; break;
                case "POB65_MAS": valor = (double)coloniasResult.pob65_mas; break;
                case "PNACENT": valor = (double)coloniasResult.pnacent; break;
                case "PNACENT_M": valor = (double)coloniasResult.pnacent_m; break;
                case "PNACENT_F": valor = (double)coloniasResult.pnacent_f; break;
                case "PNACOE": valor = (double)coloniasResult.pnacoe; break;
                case "PNACOE_M": valor = (double)coloniasResult.pnacoe_m; break;
                case "PNACOE_F": valor = (double)coloniasResult.pnacoe_f; break;
                case "PRES2005": valor = (double)coloniasResult.pres2005; break;
                case "PRES2005_M": valor = (double)coloniasResult.pres2005_m; break;
                case "PRES2005_F": valor = (double)coloniasResult.pres2005_f; break;
                case "PRESOE05": valor = (double)coloniasResult.presoe05; break;
                case "PRESOE05_M": valor = (double)coloniasResult.presoe05_m; break;
                case "PRESOE05_F": valor = (double)coloniasResult.presoe05_f; break;
                case "P3YM_HLI": valor = (double)coloniasResult.p3ym_hli; break;
                case "P3YM_HLI_M": valor = (double)coloniasResult.p3ym_hli_m; break;
                case "P3YM_HLI_F": valor = (double)coloniasResult.p3ym_hli_f; break;
                case "P3HLINHE": valor = (double)coloniasResult.p3hlinhe; break;
                case "P3HLINHE_M": valor = (double)coloniasResult.p3hlinhe_m; break;
                case "P3HLINHE_F": valor = (double)coloniasResult.p3hlinhe_f; break;
                case "P3HLI_HE": valor = (double)coloniasResult.p3hli_he; break;
                case "P3HLI_HE_M": valor = (double)coloniasResult.p3hli_he_m; break;
                case "P3HLI_HE_F": valor = (double)coloniasResult.p3hli_he_f; break;
                case "P5_HLI": valor = (double)coloniasResult.p5_hli; break;
                case "P5_HLI_NHE": valor = (double)coloniasResult.p5_hli_nhe; break;
                case "P5_HLI_HE": valor = (double)coloniasResult.p5_hli_he; break;
                case "PHOG_IND": valor = (double)coloniasResult.phog_ind; break;
                case "PCON_LIM": valor = (double)coloniasResult.pcon_lim; break;
                case "PCLIM_MOT": valor = (double)coloniasResult.pclim_mot; break;
                case "PCLIM_VIS": valor = (double)coloniasResult.pclim_vis; break;
                case "PCLIM_LENG": valor = (double)coloniasResult.pclim_leng; break;
                case "PCLIM_AUD": valor = (double)coloniasResult.pclim_aud; break;
                case "PCLIM_MOT2": valor = (double)coloniasResult.pclim_mot2; break;
                case "PCLIM_MEN": valor = (double)coloniasResult.pclim_men; break;
                case "PCLIM_MEN2": valor = (double)coloniasResult.pclim_men2; break;
                case "PSIN_LIM": valor = (double)coloniasResult.psin_lim; break;
                case "P3A5_NOA": valor = (double)coloniasResult.p3a5_noa; break;
                case "P3A5_NOA_M": valor = (double)coloniasResult.p3a5_noa_m; break;
                case "P3A5_NOA_F": valor = (double)coloniasResult.p3a5_noa_f; break;
                case "P6A11_NOA": valor = (double)coloniasResult.p6a11_noa; break;
                case "P6A11_NOAM": valor = (double)coloniasResult.p6a11_noam; break;
                case "P6A11_NOAF": valor = (double)coloniasResult.p6a11_noaf; break;
                case "P12A14NOA": valor = (double)coloniasResult.p12a14noa; break;
                case "P12A14NOAM": valor = (double)coloniasResult.p12a14noam; break;
                case "P12A14NOAF": valor = (double)coloniasResult.p12a14noaf; break;
                case "P15A17A": valor = (double)coloniasResult.p15a17a; break;
                case "P15A17A_M": valor = (double)coloniasResult.p15a17a_m; break;
                case "P15A17A_F": valor = (double)coloniasResult.p15a17a_f; break;
                case "P18A24A": valor = (double)coloniasResult.p18a24a; break;
                case "P18A24A_M": valor = (double)coloniasResult.p18a24a_m; break;
                case "P18A24A_F": valor = (double)coloniasResult.p18a24a_f; break;
                case "P8A14AN": valor = (double)coloniasResult.p8a14an; break;
                case "P8A14AN_M": valor = (double)coloniasResult.p8a14an_m; break;
                case "P8A14AN_F": valor = (double)coloniasResult.p8a14an_f; break;
                case "P15YM_AN": valor = (double)coloniasResult.p15ym_an; break;
                case "P15YM_AN_M": valor = (double)coloniasResult.p15ym_an_m; break;
                case "P15YM_AN_F": valor = (double)coloniasResult.p15ym_an_f; break;
                case "P15YM_SE": valor = (double)coloniasResult.p15ym_se; break;
                case "P15YM_SE_M": valor = (double)coloniasResult.p15ym_se_m; break;
                case "P15YM_SE_F": valor = (double)coloniasResult.p15ym_se_f; break;
                case "P15PRI_IN": valor = (double)coloniasResult.p15pri_in; break;
                case "P15PRI_INM": valor = (double)coloniasResult.p15pri_inm; break;
                case "P15PRI_INF": valor = (double)coloniasResult.p15pri_inf; break;
                case "P15PRI_CO": valor = (double)coloniasResult.p15pri_co; break;
                case "P15PRI_COM": valor = (double)coloniasResult.p15pri_com; break;
                case "P15PRI_COF": valor = (double)coloniasResult.p15pri_cof; break;
                case "P15SEC_IN": valor = (double)coloniasResult.p15sec_in; break;
                case "P15SEC_INM": valor = (double)coloniasResult.p15sec_inm; break;
                case "P15SEC_INF": valor = (double)coloniasResult.p15sec_inf; break;
                case "P15SEC_CO": valor = (double)coloniasResult.p15sec_co; break;
                case "P15SEC_COM": valor = (double)coloniasResult.p15sec_com; break;
                case "P15SEC_COF": valor = (double)coloniasResult.p15sec_cof; break;
                case "P18YM_PB": valor = (double)coloniasResult.p18ym_pb; break;
                case "P18YM_PB_M": valor = (double)coloniasResult.p18ym_pb_m; break;
                case "P18YM_PB_F": valor = (double)coloniasResult.p18ym_pb_f; break;
                case "PEA": valor = (double)coloniasResult.pea; break;
                case "PEA_M": valor = (double)coloniasResult.pea_m; break;
                case "PEA_F": valor = (double)coloniasResult.pea_f; break;
                case "PE_INAC": valor = (double)coloniasResult.pe_inac; break;
                case "PE_INAC_M": valor = (double)coloniasResult.pe_inac_m; break;
                case "PE_INAC_F": valor = (double)coloniasResult.pe_inac_f; break;
                case "POCUPADA": valor = (double)coloniasResult.pocupada; break;
                case "POCUPADA_M": valor = (double)coloniasResult.pocupada_m; break;
                case "POCUPADA_F": valor = (double)coloniasResult.pocupada_f; break;
                case "PSINDER": valor = (double)coloniasResult.psinder; break;
                case "PDESOCUP": valor = (double)coloniasResult.pdesocup; break;
                case "PDESOCUP_M": valor = (double)coloniasResult.pdesocup_m; break;
                case "PDESOCUP_F": valor = (double)coloniasResult.pdesocup_f; break;
                case "PDER_SS": valor = (double)coloniasResult.pder_ss; break;
                case "PDER_IMSS": valor = (double)coloniasResult.pder_imss; break;
                case "PDER_ISTE": valor = (double)coloniasResult.pder_iste; break;
                case "PDER_ISTEE": valor = (double)coloniasResult.pder_istee; break;
                case "PDER_SEGP": valor = (double)coloniasResult.pder_segp; break;
                case "P12YM_SOLT": valor = (double)coloniasResult.p12ym_solt; break;
                case "P12YM_CASA": valor = (double)coloniasResult.p12ym_casa; break;
                case "P12YM_SEPA": valor = (double)coloniasResult.p12ym_sepa; break;
                case "PCATOLICA": valor = (double)coloniasResult.pcatolica; break;
                case "PNCATOLICA": valor = (double)coloniasResult.pncatolica; break;
                case "POTRAS_REL": valor = (double)coloniasResult.potras_rel; break;
                case "PSIN_RELIG": valor = (double)coloniasResult.psin_relig; break;
                case "TOTHOG": valor = (double)coloniasResult.tothog; break;
                case "HOGJEF_M": valor = (double)coloniasResult.hogjef_m; break;
                case "HOGJEF_F": valor = (double)coloniasResult.hogjef_f; break;
                case "POBHOG": valor = (double)coloniasResult.pobhog; break;
                case "PHOGJEF_M": valor = (double)coloniasResult.phogjef_m; break;
                case "PHOGJEF_F": valor = (double)coloniasResult.phogjef_f; break;
                case "VIVTOT": valor = (double)coloniasResult.vivtot; break;
                case "TVIVHAB": valor = (double)coloniasResult.tvivhab; break;
                case "TVIVPAR": valor = (double)coloniasResult.tvivpar; break;
                case "VIVPAR_HAB": valor = (double)coloniasResult.vivpar_hab; break;
                case "TVIVPARHAB": valor = (double)coloniasResult.tvivparhab; break;
                case "VIVPAR_DES": valor = (double)coloniasResult.vivpar_des; break;
                case "VIVPAR_UT": valor = (double)coloniasResult.vivpar_ut; break;
                case "OCUPVIVPAR": valor = (double)coloniasResult.ocupvivpar; break;
                case "VPH_PISODT": valor = (double)coloniasResult.vph_pisodt; break;
                case "VPH_PISOTI": valor = (double)coloniasResult.vph_pisoti; break;
                case "VPH_1DOR": valor = (double)coloniasResult.vph_1dor; break;
                case "VPH_2YMASD": valor = (double)coloniasResult.vph_2ymasd; break;
                case "VPH_1CUART": valor = (double)coloniasResult.vph_1cuart; break;
                case "VPH_2CUART": valor = (double)coloniasResult.vph_2cuart; break;
                case "VPH_3YMASC": valor = (double)coloniasResult.vph_3ymasc; break;
                case "VPH_C_ELEC": valor = (double)coloniasResult.vph_c_elec; break;
                case "VPH_S_ELEC": valor = (double)coloniasResult.vph_s_elec; break;
                case "VPH_AGUADV": valor = (double)coloniasResult.vph_aguadv; break;
                case "VPH_AGUAFV": valor = (double)coloniasResult.vph_aguafv; break;
                case "VPH_EXCSA": valor = (double)coloniasResult.vph_excsa; break;
                case "VPH_DRENAJ": valor = (double)coloniasResult.vph_drenaj; break;
                case "VPH_NODREN": valor = (double)coloniasResult.vph_nodren; break;
                case "VPH_C_SERV": valor = (double)coloniasResult.vph_c_serv; break;
                case "VPH_SNBIEN": valor = (double)coloniasResult.vph_snbien; break;
                case "VPH_RADIO": valor = (double)coloniasResult.vph_radio; break;
                case "VPH_TV": valor = (double)coloniasResult.vph_tv; break;
                case "VPH_REFRI": valor = (double)coloniasResult.vph_refri; break;
                case "VPH_LAVAD": valor = (double)coloniasResult.vph_lavad; break;
                case "VPH_AUTOM": valor = (double)coloniasResult.vph_autom; break;
                case "VPH_PC": valor = (double)coloniasResult.vph_pc; break;
                case "VPH_TELEF": valor = (double)coloniasResult.vph_telef; break;
                case "VPH_CEL": valor = (double)coloniasResult.vph_cel; break;
                case "VPH_INTER": valor = (double)coloniasResult.vph_inter; break;
                case "GRAPROES": valor = (double)coloniasResult.graproes; break;
                case "GRAPROES_M": valor = (double)coloniasResult.graproes_m; break;
                case "GRAPROES_F": valor = (double)coloniasResult.graproes_f; break;
                case "PROM_OCUP": valor = (double)coloniasResult.prom_ocup; break;
                case "PRO_OCUP_C": valor = (double)coloniasResult.pro_ocup_c; break;
                case "DIST_TOT": valor = (double)coloniasResult.dist_tot; break;
                //case "DIST_PSIBLS": valor = (double)coloniasResult.dist_psibls; break;
                //case "VTAS_TOT": valor = (double)coloniasResult.vtas_tot; break;
            }
            return valor;
        }

        protected virtual object BuscaValor(DataAccess.ZonColoniasLocalidade zonColoniasLocalidade, string key)
        {
            object valor = 0;
            switch (key.ToUpper())
            {
                case "IDTIPO": valor = (double)zonColoniasLocalidade.IdTipo; break;
                case "NSE_PREDO": valor = ((string)zonColoniasLocalidade.Nse_Predo).ToUpper(); break;
                case "AB": valor = (double)zonColoniasLocalidade.ab; break;
                case "CMAS": valor = (double)zonColoniasLocalidade.cmas; break;
                case "C": valor = (double)zonColoniasLocalidade.c; break;
                case "CMENOS": valor = (double)zonColoniasLocalidade.cmenos; break;
                case "DMAS": valor = (double)zonColoniasLocalidade.dmas; break;
                case "D": valor = (double)zonColoniasLocalidade.d; break;
                case "E": valor = (double)zonColoniasLocalidade.e; break;
                case "NC": valor = (double)zonColoniasLocalidade.nc; break;
                case "POBTOT": valor = (double)zonColoniasLocalidade.POBTOT; break;
                case "POBMAS": valor = (double)zonColoniasLocalidade.POBMAS; break;
                case "POBFEM": valor = (double)zonColoniasLocalidade.POBFEM; break;
                case "P_0A2": valor = (double)zonColoniasLocalidade.P_0A2; break;
                case "P_0A2_M": valor = (double)zonColoniasLocalidade.P_0A2_M; break;
                case "P_0A2_F": valor = (double)zonColoniasLocalidade.P_0A2_F; break;
                case "P_3YMAS": valor = (double)zonColoniasLocalidade.P_3YMAS; break;
                case "P_3YMAS_M": valor = (double)zonColoniasLocalidade.P_3YMAS_M; break;
                case "P_3YMAS_F": valor = (double)zonColoniasLocalidade.P_3YMAS_F; break;
                case "P_5YMAS": valor = (double)zonColoniasLocalidade.P_5YMAS; break;
                case "P_5YMAS_M": valor = (double)zonColoniasLocalidade.P_5YMAS_M; break;
                case "P_5YMAS_F": valor = (double)zonColoniasLocalidade.P_5YMAS_F; break;
                case "P_12YMAS": valor = (double)zonColoniasLocalidade.P_12YMAS; break;
                case "P_12YMAS_M": valor = (double)zonColoniasLocalidade.P_12YMAS_M; break;
                case "P_12YMAS_F": valor = (double)zonColoniasLocalidade.P_12YMAS_F; break;
                case "P_15YMAS": valor = (double)zonColoniasLocalidade.P_15YMAS; break;
                case "P_15YMAS_M": valor = (double)zonColoniasLocalidade.P_15YMAS_M; break;
                case "P_15YMAS_F": valor = (double)zonColoniasLocalidade.P_15YMAS_F; break;
                case "P_18YMAS": valor = (double)zonColoniasLocalidade.P_18YMAS; break;
                case "P_18YMAS_M": valor = (double)zonColoniasLocalidade.P_18YMAS_M; break;
                case "P_18YMAS_F": valor = (double)zonColoniasLocalidade.P_18YMAS_F; break;
                case "P_3A5": valor = (double)zonColoniasLocalidade.P_3A5; break;
                case "P_3A5_M": valor = (double)zonColoniasLocalidade.P_3A5_M; break;
                case "P_3A5_F": valor = (double)zonColoniasLocalidade.P_3A5_F; break;
                case "P_6A11": valor = (double)zonColoniasLocalidade.P_6A11; break;
                case "P_6A11_M": valor = (double)zonColoniasLocalidade.P_6A11_M; break;
                case "P_6A11_F": valor = (double)zonColoniasLocalidade.P_6A11_F; break;
                case "P_8A14": valor = (double)zonColoniasLocalidade.P_8A14; break;
                case "P_8A14_M": valor = (double)zonColoniasLocalidade.P_8A14_M; break;
                case "P_8A14_F": valor = (double)zonColoniasLocalidade.P_8A14_F; break;
                case "P_12A14": valor = (double)zonColoniasLocalidade.P_12A14; break;
                case "P_12A14_M": valor = (double)zonColoniasLocalidade.P_12A14_M; break;
                case "P_12A14_F": valor = (double)zonColoniasLocalidade.P_12A14_F; break;
                case "P_15A17": valor = (double)zonColoniasLocalidade.P_15A17; break;
                case "P_15A17_M": valor = (double)zonColoniasLocalidade.P_15A17_M; break;
                case "P_15A17_F": valor = (double)zonColoniasLocalidade.P_15A17_F; break;
                case "P_18A24": valor = (double)zonColoniasLocalidade.P_18A24; break;
                case "P_18A24_M": valor = (double)zonColoniasLocalidade.P_18A24_M; break;
                case "P_18A24_F": valor = (double)zonColoniasLocalidade.P_18A24_F; break;
                case "P_15A49_F": valor = (double)zonColoniasLocalidade.P_15A49_F; break;
                case "P_60YMAS": valor = (double)zonColoniasLocalidade.P_60YMAS; break;
                case "P_60YMAS_M": valor = (double)zonColoniasLocalidade.P_60YMAS_M; break;
                case "P_60YMAS_F": valor = (double)zonColoniasLocalidade.P_60YMAS_F; break;
                case "REL_H_M": valor = (double)zonColoniasLocalidade.REL_H_M; break;
                case "POB0_14": valor = (double)zonColoniasLocalidade.POB0_14; break;
                case "POB15_64": valor = (double)zonColoniasLocalidade.POB15_64; break;
                case "POB65_MAS": valor = (double)zonColoniasLocalidade.POB65_MAS; break;
                case "PNACENT": valor = (double)zonColoniasLocalidade.PNACENT; break;
                case "PNACENT_M": valor = (double)zonColoniasLocalidade.PNACENT_M; break;
                case "PNACENT_F": valor = (double)zonColoniasLocalidade.PNACENT_F; break;
                case "PNACOE": valor = (double)zonColoniasLocalidade.PNACOE; break;
                case "PNACOE_M": valor = (double)zonColoniasLocalidade.PNACOE_M; break;
                case "PNACOE_F": valor = (double)zonColoniasLocalidade.PNACOE_F; break;
                case "PRES2005": valor = (double)zonColoniasLocalidade.PRES2005; break;
                case "PRES2005_M": valor = (double)zonColoniasLocalidade.PRES2005_M; break;
                case "PRES2005_F": valor = (double)zonColoniasLocalidade.PRES2005_F; break;
                case "PRESOE05": valor = (double)zonColoniasLocalidade.PRESOE05; break;
                case "PRESOE05_M": valor = (double)zonColoniasLocalidade.PRESOE05_M; break;
                case "PRESOE05_F": valor = (double)zonColoniasLocalidade.PRESOE05_F; break;
                case "P3YM_HLI": valor = (double)zonColoniasLocalidade.P3YM_HLI; break;
                case "P3YM_HLI_M": valor = (double)zonColoniasLocalidade.P3YM_HLI_M; break;
                case "P3YM_HLI_F": valor = (double)zonColoniasLocalidade.P3YM_HLI_F; break;
                case "P3HLINHE": valor = (double)zonColoniasLocalidade.P3HLINHE; break;
                case "P3HLINHE_M": valor = (double)zonColoniasLocalidade.P3HLINHE_M; break;
                case "P3HLINHE_F": valor = (double)zonColoniasLocalidade.P3HLINHE_F; break;
                case "P3HLI_HE": valor = (double)zonColoniasLocalidade.P3HLI_HE; break;
                case "P3HLI_HE_M": valor = (double)zonColoniasLocalidade.P3HLI_HE_M; break;
                case "P3HLI_HE_F": valor = (double)zonColoniasLocalidade.P3HLI_HE_F; break;
                case "P5_HLI": valor = (double)zonColoniasLocalidade.P5_HLI; break;
                case "P5_HLI_NHE": valor = (double)zonColoniasLocalidade.P5_HLI_NHE; break;
                case "P5_HLI_HE": valor = (double)zonColoniasLocalidade.P5_HLI_HE; break;
                case "PHOG_IND": valor = (double)zonColoniasLocalidade.PHOG_IND; break;
                case "PCON_LIM": valor = (double)zonColoniasLocalidade.PCON_LIM; break;
                case "PCLIM_MOT": valor = (double)zonColoniasLocalidade.PCLIM_MOT; break;
                case "PCLIM_VIS": valor = (double)zonColoniasLocalidade.PCLIM_VIS; break;
                case "PCLIM_LENG": valor = (double)zonColoniasLocalidade.PCLIM_LENG; break;
                case "PCLIM_AUD": valor = (double)zonColoniasLocalidade.PCLIM_AUD; break;
                case "PCLIM_MOT2": valor = (double)zonColoniasLocalidade.PCLIM_MOT2; break;
                case "PCLIM_MEN": valor = (double)zonColoniasLocalidade.PCLIM_MEN; break;
                case "PCLIM_MEN2": valor = (double)zonColoniasLocalidade.PCLIM_MEN2; break;
                case "PSIN_LIM": valor = (double)zonColoniasLocalidade.PSIN_LIM; break;
                case "P3A5_NOA": valor = (double)zonColoniasLocalidade.P3A5_NOA; break;
                case "P3A5_NOA_M": valor = (double)zonColoniasLocalidade.P3A5_NOA_M; break;
                case "P3A5_NOA_F": valor = (double)zonColoniasLocalidade.P3A5_NOA_F; break;
                case "P6A11_NOA": valor = (double)zonColoniasLocalidade.P6A11_NOA; break;
                case "P6A11_NOAM": valor = (double)zonColoniasLocalidade.P6A11_NOAM; break;
                case "P6A11_NOAF": valor = (double)zonColoniasLocalidade.P6A11_NOAF; break;
                case "P12A14NOA": valor = (double)zonColoniasLocalidade.P12A14NOA; break;
                case "P12A14NOAM": valor = (double)zonColoniasLocalidade.P12A14NOAM; break;
                case "P12A14NOAF": valor = (double)zonColoniasLocalidade.P12A14NOAF; break;
                case "P15A17A": valor = (double)zonColoniasLocalidade.P15A17A; break;
                case "P15A17A_M": valor = (double)zonColoniasLocalidade.P15A17A_M; break;
                case "P15A17A_F": valor = (double)zonColoniasLocalidade.P15A17A_F; break;
                case "P18A24A": valor = (double)zonColoniasLocalidade.P18A24A; break;
                case "P18A24A_M": valor = (double)zonColoniasLocalidade.P18A24A_M; break;
                case "P18A24A_F": valor = (double)zonColoniasLocalidade.P18A24A_F; break;
                case "P8A14AN": valor = (double)zonColoniasLocalidade.P8A14AN; break;
                case "P8A14AN_M": valor = (double)zonColoniasLocalidade.P8A14AN_M; break;
                case "P8A14AN_F": valor = (double)zonColoniasLocalidade.P8A14AN_F; break;
                case "P15YM_AN": valor = (double)zonColoniasLocalidade.P15YM_AN; break;
                case "P15YM_AN_M": valor = (double)zonColoniasLocalidade.P15YM_AN_M; break;
                case "P15YM_AN_F": valor = (double)zonColoniasLocalidade.P15YM_AN_F; break;
                case "P15YM_SE": valor = (double)zonColoniasLocalidade.P15YM_SE; break;
                case "P15YM_SE_M": valor = (double)zonColoniasLocalidade.P15YM_SE_M; break;
                case "P15YM_SE_F": valor = (double)zonColoniasLocalidade.P15YM_SE_F; break;
                case "P15PRI_IN": valor = (double)zonColoniasLocalidade.P15PRI_IN; break;
                case "P15PRI_INM": valor = (double)zonColoniasLocalidade.P15PRI_INM; break;
                case "P15PRI_INF": valor = (double)zonColoniasLocalidade.P15PRI_INF; break;
                case "P15PRI_CO": valor = (double)zonColoniasLocalidade.P15PRI_CO; break;
                case "P15PRI_COM": valor = (double)zonColoniasLocalidade.P15PRI_COM; break;
                case "P15PRI_COF": valor = (double)zonColoniasLocalidade.P15PRI_COF; break;
                case "P15SEC_IN": valor = (double)zonColoniasLocalidade.P15SEC_IN; break;
                case "P15SEC_INM": valor = (double)zonColoniasLocalidade.P15SEC_INM; break;
                case "P15SEC_INF": valor = (double)zonColoniasLocalidade.P15SEC_INF; break;
                case "P15SEC_CO": valor = (double)zonColoniasLocalidade.P15SEC_CO; break;
                case "P15SEC_COM": valor = (double)zonColoniasLocalidade.P15SEC_COM; break;
                case "P15SEC_COF": valor = (double)zonColoniasLocalidade.P15SEC_COF; break;
                case "P18YM_PB": valor = (double)zonColoniasLocalidade.P18YM_PB; break;
                case "P18YM_PB_M": valor = (double)zonColoniasLocalidade.P18YM_PB_M; break;
                case "P18YM_PB_F": valor = (double)zonColoniasLocalidade.P18YM_PB_F; break;
                case "PEA": valor = (double)zonColoniasLocalidade.PEA; break;
                case "PEA_M": valor = (double)zonColoniasLocalidade.PEA_M; break;
                case "PEA_F": valor = (double)zonColoniasLocalidade.PEA_F; break;
                case "PE_INAC": valor = (double)zonColoniasLocalidade.PE_INAC; break;
                case "PE_INAC_M": valor = (double)zonColoniasLocalidade.PE_INAC_M; break;
                case "PE_INAC_F": valor = (double)zonColoniasLocalidade.PE_INAC_F; break;
                case "POCUPADA": valor = (double)zonColoniasLocalidade.POCUPADA; break;
                case "POCUPADA_M": valor = (double)zonColoniasLocalidade.POCUPADA_M; break;
                case "POCUPADA_F": valor = (double)zonColoniasLocalidade.POCUPADA_F; break;
                case "PSINDER": valor = (double)zonColoniasLocalidade.PSINDER; break;
                case "PDESOCUP": valor = (double)zonColoniasLocalidade.PDESOCUP; break;
                case "PDESOCUP_M": valor = (double)zonColoniasLocalidade.PDESOCUP_M; break;
                case "PDESOCUP_F": valor = (double)zonColoniasLocalidade.PDESOCUP_F; break;
                case "PDER_SS": valor = (double)zonColoniasLocalidade.PDER_SS; break;
                case "PDER_IMSS": valor = (double)zonColoniasLocalidade.PDER_IMSS; break;
                case "PDER_ISTE": valor = (double)zonColoniasLocalidade.PDER_ISTE; break;
                case "PDER_ISTEE": valor = (double)zonColoniasLocalidade.PDER_ISTEE; break;
                case "PDER_SEGP": valor = (double)zonColoniasLocalidade.PDER_SEGP; break;
                case "P12YM_SOLT": valor = (double)zonColoniasLocalidade.P12YM_SOLT; break;
                case "P12YM_CASA": valor = (double)zonColoniasLocalidade.P12YM_CASA; break;
                case "P12YM_SEPA": valor = (double)zonColoniasLocalidade.P12YM_SEPA; break;
                case "PCATOLICA": valor = (double)zonColoniasLocalidade.PCATOLICA; break;
                case "PNCATOLICA": valor = (double)zonColoniasLocalidade.PNCATOLICA; break;
                case "POTRAS_REL": valor = (double)zonColoniasLocalidade.POTRAS_REL; break;
                case "PSIN_RELIG": valor = (double)zonColoniasLocalidade.PSIN_RELIG; break;
                case "TOTHOG": valor = (double)zonColoniasLocalidade.TOTHOG; break;
                case "HOGJEF_M": valor = (double)zonColoniasLocalidade.HOGJEF_M; break;
                case "HOGJEF_F": valor = (double)zonColoniasLocalidade.HOGJEF_F; break;
                case "POBHOG": valor = (double)zonColoniasLocalidade.POBHOG; break;
                case "PHOGJEF_M": valor = (double)zonColoniasLocalidade.PHOGJEF_M; break;
                case "PHOGJEF_F": valor = (double)zonColoniasLocalidade.PHOGJEF_F; break;
                case "VIVTOT": valor = (double)zonColoniasLocalidade.VIVTOT; break;
                case "TVIVHAB": valor = (double)zonColoniasLocalidade.TVIVHAB; break;
                case "TVIVPAR": valor = (double)zonColoniasLocalidade.TVIVPAR; break;
                case "VIVPAR_HAB": valor = (double)zonColoniasLocalidade.VIVPAR_HAB; break;
                case "TVIVPARHAB": valor = (double)zonColoniasLocalidade.TVIVPARHAB; break;
                case "VIVPAR_DES": valor = (double)zonColoniasLocalidade.VIVPAR_DES; break;
                case "VIVPAR_UT": valor = (double)zonColoniasLocalidade.VIVPAR_UT; break;
                case "OCUPVIVPAR": valor = (double)zonColoniasLocalidade.OCUPVIVPAR; break;
                case "VPH_PISODT": valor = (double)zonColoniasLocalidade.VPH_PISODT; break;
                case "VPH_PISOTI": valor = (double)zonColoniasLocalidade.VPH_PISOTI; break;
                case "VPH_1DOR": valor = (double)zonColoniasLocalidade.VPH_1DOR; break;
                case "VPH_2YMASD": valor = (double)zonColoniasLocalidade.VPH_2YMASD; break;
                case "VPH_1CUART": valor = (double)zonColoniasLocalidade.VPH_1CUART; break;
                case "VPH_2CUART": valor = (double)zonColoniasLocalidade.VPH_2CUART; break;
                case "VPH_3YMASC": valor = (double)zonColoniasLocalidade.VPH_3YMASC; break;
                case "VPH_C_ELEC": valor = (double)zonColoniasLocalidade.VPH_C_ELEC; break;
                case "VPH_S_ELEC": valor = (double)zonColoniasLocalidade.VPH_S_ELEC; break;
                case "VPH_AGUADV": valor = (double)zonColoniasLocalidade.VPH_AGUADV; break;
                case "VPH_AGUAFV": valor = (double)zonColoniasLocalidade.VPH_AGUAFV; break;
                case "VPH_EXCSA": valor = (double)zonColoniasLocalidade.VPH_EXCSA; break;
                case "VPH_DRENAJ": valor = (double)zonColoniasLocalidade.VPH_DRENAJ; break;
                case "VPH_NODREN": valor = (double)zonColoniasLocalidade.VPH_NODREN; break;
                case "VPH_C_SERV": valor = (double)zonColoniasLocalidade.VPH_C_SERV; break;
                case "VPH_SNBIEN": valor = (double)zonColoniasLocalidade.VPH_SNBIEN; break;
                case "VPH_RADIO": valor = (double)zonColoniasLocalidade.VPH_RADIO; break;
                case "VPH_TV": valor = (double)zonColoniasLocalidade.VPH_TV; break;
                case "VPH_REFRI": valor = (double)zonColoniasLocalidade.VPH_REFRI; break;
                case "VPH_LAVAD": valor = (double)zonColoniasLocalidade.VPH_LAVAD; break;
                case "VPH_AUTOM": valor = (double)zonColoniasLocalidade.VPH_AUTOM; break;
                case "VPH_PC": valor = (double)zonColoniasLocalidade.VPH_PC; break;
                case "VPH_TELEF": valor = (double)zonColoniasLocalidade.VPH_TELEF; break;
                case "VPH_CEL": valor = (double)zonColoniasLocalidade.VPH_CEL; break;
                case "VPH_INTER": valor = (double)zonColoniasLocalidade.VPH_INTER; break;
                case "GRAPROES": valor = (double)zonColoniasLocalidade.GRAPROES; break;
                case "GRAPROES_M": valor = (double)zonColoniasLocalidade.GRAPROES_M; break;
                case "GRAPROES_F": valor = (double)zonColoniasLocalidade.GRAPROES_F; break;
                case "PROM_OCUP": valor = (double)zonColoniasLocalidade.PROM_OCUP; break;
                case "PRO_OCUP_C": valor = (double)zonColoniasLocalidade.PRO_OCUP_C; break;
                case "DIST_TOT": {
                    var col = oDataAccess.ZonDatosXNegocios.Where(w => w.fiColoniaId == zonColoniasLocalidade.idColonia && w.fiNegocioId == 1).OrderByDescending(o => o.fdFecha).FirstOrDefault();
                    if (col != null)
                    {
                        valor = col.fiValor;
                    }
                    else
                    {
                        valor = 0;
                    }
                } break;

                //case "DIST_PSIBLS": valor = (double)coloniasResult.dist_psibls; break;
                //case "VTAS_TOT": valor = (double)coloniasResult.vtas_tot; break;
            }
            return valor;
        }


        #endregion

    }
}
