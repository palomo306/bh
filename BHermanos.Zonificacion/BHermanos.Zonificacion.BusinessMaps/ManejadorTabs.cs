using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorTabs : ManejadorBase
    {

        #region Atributos

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
            try
            {
                var zonTabs = base.oDataAccess.ZonTabs;
                foreach (var registro in zonTabs)
                {
                    Tab tab = new Tab();
                    tab.Id = registro.fiTabId;
                    tab.Nombre = registro.fcDescripcion;
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

        public IList<Tab> ObtenerTab(int tabId, int estadoId, int municipioId)
        {
            List<Tab> vistas = new List<Tab>();
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
                        tab.ListaZonas = this.ObtenerZonas(estadoId, municipioId, zonTab);
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

        private List<Partida> ObtieneListaPartidas(int nivel, int estadoId, int municipioId, int zonaId, DataAccess.ZonTab zonTab, List<Colonia> listaColonias)
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
                    partida.ListaHumbrales = base.ObtieneHumbrales(nivel, estadoId, municipioId, zonaId, 0, partida.Valor, zonPartidaXTab);
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

        private List<Zona> ObtenerZonas(int estadoId, int municipioId, DataAccess.ZonTab zonTab)
        {
            List<Zona> listaZonas = new List<Zona>();
            try
            {
                var spConZonas = base.oDataAccess.spConZonas(1, estadoId, municipioId, 0);
                foreach (var spConZona in spConZonas)
                {
                    Zona zona = new Zona();
                    zona.Id = spConZona.fiZonaId;
                    zona.EstadoId = spConZona.fiEstadoId;
                    zona.MunicipioId = spConZona.fiMunicipioId;
                    zona.Nombre = spConZona.fcNombre;
                    zona.Colonias = string.Empty;
                    zona.Color = spConZona.fcColor;
                    zona.ListaSubzonas = this.ObtenerSubZonas(spConZona.fiEstadoId, spConZona.fiMunicipioId, spConZona.fiZonaId, zonTab);
                    zona.ListaColonias = this.ObtenerColoniasXZona(1,spConZona.fiEstadoId, spConZona.fiMunicipioId, spConZona.fiZonaId, zonTab);                   
                    zona.ListaPartidas = this.ObtieneListaPartidas(1,spConZona.fiEstadoId, spConZona.fiMunicipioId, spConZona.fiZonaId, zonTab, zona.ListaColonias);                   
                    listaZonas.Add(zona);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaZonas;
        }

        private List<Zona> ObtenerSubZonas(int estadoId, int municipioId, int zonaId, DataAccess.ZonTab zonTab)
        {
            List<Zona> listaSubZonas = new List<Zona>();
            try
            {
                var spConZonas = base.oDataAccess.spConZonas(1, estadoId, municipioId, zonaId);
                foreach (var spConZona in spConZonas)
                {
                    Zona zona = new Zona();
                    zona.Id = spConZona.fiZonaId;
                    zona.EstadoId = spConZona.fiEstadoId;
                    zona.MunicipioId = spConZona.fiMunicipioId;
                    zona.Nombre = spConZona.fcNombre;
                    zona.Color = spConZona.fcColor;
                    zona.Colonias = string.Empty;
                    zona.ListaSubzonas = new List<Zona>();
                    zona.ListaColonias = this.ObtenerColoniasXZona(1,spConZona.fiEstadoId, spConZona.fiMunicipioId, spConZona.fiZonaId, zonTab);
                    zona.ListaPartidas = this.ObtieneListaPartidas(2, spConZona.fiEstadoId, spConZona.fiMunicipioId, spConZona.fiZonaId, zonTab, zona.ListaColonias);
                    listaSubZonas.Add(zona);
                }
                if (listaSubZonas.Count > 0 )
                {
                    Zona zona = new Zona();
                    zona.Id = zonaId;
                    zona.EstadoId = estadoId;
                    zona.MunicipioId = municipioId;
                    zona.Nombre = "Sin-Clasificar";
                    zona.Color = "#FFFFFF";
                    zona.Colonias = string.Empty;
                    zona.ListaSubzonas = new List<Zona>();
                    zona.ListaColonias = this.ObtenerColoniasXZona(2,estadoId, municipioId, zonaId, zonTab);
                    zona.ListaPartidas = this.ObtieneListaPartidas(2, estadoId, municipioId, zonaId, zonTab, zona.ListaColonias);
                    listaSubZonas.Add(zona);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listaSubZonas;
        }

        private List<Colonia> ObtenerColoniasXZona(byte vista, int estadoId, int municipioId, int zonaId, DataAccess.ZonTab zonTab)
        {
            List<Colonia> listaColonias = new List<Colonia>();
            try
            {
                var spConColoniasXZona = base.oDataAccess.spConColoniasXZona(vista, estadoId, municipioId, zonaId);
                using (ManejadorColonias manejadorColonias = new ManejadorColonias())
                {
                    foreach (var spConColoniaXZona in spConColoniasXZona)
                    {
                        List<Colonia> col = manejadorColonias.ObtenerColoniasXPartida(2, estadoId, municipioId, zonaId, spConColoniaXZona.fiColoniaId, zonTab);
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