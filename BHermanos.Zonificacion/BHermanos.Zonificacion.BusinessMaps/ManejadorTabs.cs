using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorTabs : ManejadorBase
    {

        #region Atributos

        private DateTime fechaInicio;
        private DateTime fechaFin;
        private bool esBusquedaXFecha = false;

        #endregion

        #region Constructores

        public ManejadorTabs()
        {

        }

        #endregion

        #region Metodos

        public IList<Tab> ObtenerTabs()
        {
            List<Tab> vistas = new List<Tab>();
            List<DateTime> fechas = new List<DateTime>();
            try
            {
                var zonTabs = base.oDataAccess.ZonTabs;
                foreach (var zonTab in zonTabs)
                {
                    Tab tab = new Tab();
                    tab.Id = zonTab.fiTabId;
                    tab.Nombre = zonTab.fcDescripcion;
                    foreach (DataAccess.ZonNegocio negocio in zonTab.ZonNegocios.Where(w => w.flEstatus == true))
                    {                        
                        fechas = negocio.ZonDatosXNegocios.OrderByDescending(o => o.fdFecha).Select(d => DateTime.Parse(d.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX")))).Distinct().ToList<DateTime>();
                    };
                    if (fechas.Count == 0)
                    {
                        fechas.Add(DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("es-MX"))));
                    }  
                    tab.Fechas = fechas;     
                    tab.ListaZonas = new List<Zona>();
                    vistas.Add(tab);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vistas;
        }

        public IList<Tab> ObtenerTab(int tabId, int plazaId)
        {
            List<Tab> vistas = new List<Tab>();
            List<DateTime> fechas = new List<DateTime>();
            try
            {
                var zonTabs = base.oDataAccess.ZonTabs.Where(t => t.fiTabId == tabId);
                if (zonTabs != null && zonTabs.Count() > 0)
                {
                    foreach (var zonTab in zonTabs)
                    {
                        Tab tab = new Tab();
                        tab.Id = zonTab.fiTabId;
                        tab.Nombre = zonTab.fcDescripcion;
                        foreach (DataAccess.ZonNegocio negocio in zonTab.ZonNegocios.Where(w => w.flEstatus == true))
                        {
                            fechas = negocio.ZonDatosXNegocios.OrderByDescending(o => o.fdFecha).Select(d => DateTime.Parse(d.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX")))).Distinct().ToList<DateTime>();
                        };
                        if (fechas.Count == 0)
                        {
                            fechas.Add(DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("es-MX"))));
                        }
                        tab.Fechas = fechas;
                        tab.ListaZonas = this.ObtenerZonas(plazaId, zonTab);
                        vistas.Add(tab);
                    }
                }
                else
                {
                    throw new ApplicationException("El tabId(" + tabId + ") no existe");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vistas;
        }

        public IList<Tab> ObtenerTab(int tabId, int plazaId, DateTime fechaInicio, DateTime fechaFin)
        {
            List<Tab> vistas = new List<Tab>();
            List<DateTime> fechas = new List<DateTime>();
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.esBusquedaXFecha = true;
            try
            {
                List<DataAccess.ZonTab> zonTabs = base.oDataAccess.ZonTabs.Where(t => t.fiTabId == tabId).ToList<DataAccess.ZonTab>();
                if (zonTabs != null && zonTabs.Count() > 0)
                {
                    foreach (DataAccess.ZonTab zonTab in zonTabs)
                    {
                        Tab tab = new Tab();
                        tab.Id = zonTab.fiTabId;
                        tab.Nombre = zonTab.fcDescripcion;
                        foreach (DataAccess.ZonNegocio negocio in zonTab.ZonNegocios.Where(w => w.flEstatus == true))
                        {
                            fechas = negocio.ZonDatosXNegocios.Select(d => DateTime.Parse(d.fdFecha.ToString("dd-MM-yyyy", new CultureInfo("es-MX")))).Distinct().ToList<DateTime>();
                        };
                        if (fechas.Count == 0)
                        {
                            fechas.Add(DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("es-MX"))));
                        }
                        tab.Fechas = fechas;
                        tab.ListaZonas = this.ObtenerZonas(plazaId, zonTab);
                        vistas.Add(tab);
                    }
                }
                else
                {
                    throw new ApplicationException("El tabId(" + tabId + ") no existe");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vistas;
        }

        #endregion

        #region Metodos privados

        private List<Partida> ObtieneListaPartidas(int nivel, int plazaId, int zonaId, DataAccess.ZonTab zonTab, List<Colonia> listaColonias)
        {
            List<Partida> listaPartidas = new List<Partida>();

            try
            {
                foreach (var zonPartidaXTab in zonTab.ZonPartidasXTabs)
                {
                    Partida partida = new Partida();
                    partida.Id = zonPartidaXTab.fiPartidaId;
                    partida.Nombre = zonPartidaXTab.fcDescripcion;
                    partida.TieneHumbral = zonPartidaXTab.flTieneUmbral;
                    partida.Orden = zonPartidaXTab.fiOrden;
                    partida.Valor = 0D;                    
                    foreach (Colonia colonia in listaColonias)
                    {
                        Partida part = colonia.ListaPartidas.Where(p => p.Id == partida.Id).FirstOrDefault();
                        if (part != null)
                        {
                            partida.Valor += part.Valor;
                        }

                    }
                    partida.ListaHumbrales = base.ObtieneHumbrales(nivel, plazaId,zonaId, 0, partida.Valor, zonPartidaXTab);
                    partida.Color = base.ObtieneColor(partida.Valor, partida.ListaHumbrales);
                    listaPartidas.Add(partida);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaPartidas;
        }

        private List<Zona> ObtenerZonas(int plazaId, DataAccess.ZonTab zonTab)
        {
            List<Zona> listaZonas = new List<Zona>();
            try
            {
                var spConZonas = base.oDataAccess.spConZonas(1, plazaId, 0);
                foreach (var spConZona in spConZonas)
                {
                    Zona zona = new Zona();
                    zona.Id = spConZona.fiZonaId;
                    zona.PlazaId = spConZona.fiPlazaId;                    
                    zona.Nombre = spConZona.fcNombre;
                    zona.Colonias = string.Empty;
                    zona.Color = spConZona.fcColor;
                    zona.ListaSubzonas = this.ObtenerSubZonas(spConZona.fiPlazaId, spConZona.fiZonaId, zonTab);
                    zona.ListaColonias = this.ObtenerColoniasXZona(1,spConZona.fiPlazaId, spConZona.fiZonaId, zonTab);                   
                    zona.ListaPartidas = this.ObtieneListaPartidas(1,spConZona.fiPlazaId, spConZona.fiZonaId, zonTab, zona.ListaColonias);                   
                    listaZonas.Add(zona);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaZonas;
        }

        private List<Zona> ObtenerSubZonas(int plazaId, int zonaId, DataAccess.ZonTab zonTab)
        {
            List<Zona> listaSubZonas = new List<Zona>();
            try
            {
                var spConZonas = base.oDataAccess.spConZonas(1, plazaId, zonaId);
                foreach (var spConZona in spConZonas)
                {
                    Zona zona = new Zona();
                    zona.Id = spConZona.fiZonaId;
                    zona.PlazaId = spConZona.fiPlazaId;                    
                    zona.Nombre = spConZona.fcNombre;
                    zona.Color = spConZona.fcColor;
                    zona.Colonias = string.Empty;
                    zona.ListaSubzonas = new List<Zona>();
                    zona.ListaColonias = this.ObtenerColoniasXZona(1,spConZona.fiPlazaId,  spConZona.fiZonaId, zonTab);
                    zona.ListaPartidas = this.ObtieneListaPartidas(2, spConZona.fiPlazaId, spConZona.fiZonaId, zonTab, zona.ListaColonias);
                    listaSubZonas.Add(zona);
                }
                if (listaSubZonas.Count > 0 )
                {
                    List<Colonia> listCol = this.ObtenerColoniasXZona(2, plazaId, zonaId, zonTab);
                    if (listCol != null && listCol.Count > 0)
                    {
                        Zona zona = new Zona();
                        zona.Id = zonaId;
                        zona.PlazaId = plazaId;
                        zona.Nombre = "Sin-Clasificar";
                        zona.Color = "#EFEFEF";
                        zona.Colonias = string.Empty;
                        zona.ListaSubzonas = new List<Zona>();
                        zona.ListaColonias = listCol;
                        zona.ListaPartidas = this.ObtieneListaPartidas(2, plazaId, zonaId, zonTab, zona.ListaColonias);
                        listaSubZonas.Add(zona);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listaSubZonas;
        }

        private List<Colonia> ObtenerColoniasXZona(byte vista, int plazaId, int zonaId, DataAccess.ZonTab zonTab)
        {
            List<Colonia> listaColonias = new List<Colonia>();
            try
            {
                var spConColoniasXZona = base.oDataAccess.spConColoniasXZona(vista, plazaId, zonaId);
                using (ManejadorColonias manejadorColonias = new ManejadorColonias())
                {
                    foreach (var spConColoniaXZona in spConColoniasXZona)
                    {
                        List<Colonia> col = manejadorColonias.ObtenerColoniasXPartida(2, plazaId, zonaId, spConColoniaXZona.fiColoniaId, zonTab,this.fechaInicio,this.fechaFin,this.esBusquedaXFecha);
                        if (col != null && col.Count > 0)
                        {
                            listaColonias.Add(col[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listaColonias;
        }

        #endregion

    }
}