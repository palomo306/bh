using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorColonias : ManejadorBase
    {

        #region Atributos

        #endregion

        #region Constructores

        public ManejadorColonias()
        {

        }

        #endregion

        #region "Propiedades"


        #endregion

        #region Metodos publicos

        public List<Colonia> ObtenerColonias(byte vistaId, int plazaId, double coloniaId)
        {
            List<Colonia> listacolonias = new List<Colonia>();
            try
            {
                var spConColonias = base.oDataAccess.spConColonias(vistaId, plazaId, coloniaId);
                foreach (var registro in spConColonias)
                {
                    Colonia colonia = new Colonia();
                    colonia.Id = (double)registro.IdColonia;
                    colonia.Nombre = registro.Nombre;
                    colonia.Tipo = (byte)registro.IdTipo;
                    colonia.ListaGrupoRubros = this.ObtenerListaGrupoRubros(registro);
                    colonia.ListaPartidas = new List<Partida>();
                    listacolonias.Add(colonia);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listacolonias;
        }

        public List<Colonia> ObtenerColoniaVacia(DataAccess.spConColoniasResult coloniasResult)
        {
            List<Colonia> listacolonias = new List<Colonia>();
            try
            {
                Colonia colonia = new Colonia();
                colonia.Id = 0;
                colonia.Nombre = "Vacía";
                colonia.ListaGrupoRubros = this.ObtenerListaGrupoRubros(coloniasResult);
                colonia.ListaPartidas = new List<Partida>();
                listacolonias.Add(colonia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listacolonias;
        }

        public List<Colonia> ObtenerColoniasXPartida(byte vistaId, int plazaId, int zonaId, double coloniaId, DataAccess.ZonTab zonTab, DateTime fechaInicio, DateTime fechaFin, bool esBusquedaXFecha)
        {
            List<Colonia> listacolonias = new List<Colonia>();
            try
            {
                var spConColonias = base.oDataAccess.spConColonias(vistaId, plazaId, coloniaId);
                foreach (var registro in spConColonias)
                {
                    Colonia colonia = new Colonia();
                    colonia.Id = (double)registro.IdColonia;
                    colonia.Nombre = registro.Nombre;
                    colonia.Tipo = (byte)registro.IdTipo;
                    colonia.ListaGrupoRubros = new List<GrupoRubros>();
                    colonia.ListaPartidas = this.ObtenerListaDePartidasDeColonia(3,plazaId, zonaId, coloniaId, registro, zonTab,fechaInicio,fechaFin,esBusquedaXFecha);
                    listacolonias.Add(colonia);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listacolonias;
        }

        #endregion

        #region Metodos privados

        private List<GrupoRubros> ObtenerListaGrupoRubros(DataAccess.spConColoniasResult coloniasResult)
        {
            List<GrupoRubros> listaGrupoRubros = new List<GrupoRubros>();

            try
            {
                var spConGrupoRubros = base.oDataAccess.spConGrupoRubros();
                foreach (var reg in spConGrupoRubros)
                {
                    GrupoRubros grupoRubros = new GrupoRubros();
                    grupoRubros.Id = reg.fiGrupoId;
                    grupoRubros.Nombre = reg.fcNombre;
                    grupoRubros.ListaRubros = this.ObtenerListaRubros(reg.fiGrupoId, coloniasResult);
                    listaGrupoRubros.Add(grupoRubros);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return listaGrupoRubros;
        }

        private List<Rubro> ObtenerListaRubros(int grupoId, DataAccess.spConColoniasResult coloniasResult)
        {
            List<Rubro> listaRubros = new List<Rubro>();

            try
            {
                var spConRubrosXGrupo = base.oDataAccess.spConRubrosXGrupo(grupoId);
                foreach (var reg in spConRubrosXGrupo)
                {
                    Rubro rubro = new Rubro();
                    rubro.Id = reg.fiRubroId;
                    rubro.Nombre = reg.fcDescripcion;
                    rubro.Expresion = reg.fcExpresion;
                    rubro.Orden = reg.fiOrden;
                    rubro.Main = reg.flMain;
                    rubro.Estatus = true;
                    rubro.SignoAcumulado = reg.fcSignoAcumulado;
                    rubro.Formato = reg.fcFormato;
                    rubro.Valor = this.AsignaValor(rubro.Expresion, coloniasResult,1,null,DateTime.Now,DateTime.Now,false);
                    listaRubros.Add(rubro);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return listaRubros;
        }

        private List<Partida> ObtenerListaDePartidasDeColonia(int nivel, int plazaId, int zonaId, double coloniaId, DataAccess.spConColoniasResult coloniasResult, DataAccess.ZonTab zonTab, DateTime fechaInicio, DateTime fechaFin, bool esBusquedaXFecha)
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
                    partida.Valor = (double)this.AsignaValor(zonPartidaXTab.fcExpresion, coloniasResult, zonPartidaXTab.fiTipo,zonTab,fechaInicio,fechaFin,esBusquedaXFecha);
                    partida.ListaHumbrales = this.ObtieneHumbrales(nivel, plazaId, zonaId, coloniaId, partida.Valor, zonPartidaXTab);
                    partida.Color = this.ObtieneColor(partida.Valor, partida.ListaHumbrales);
                    listaPartidas.Add(partida);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaPartidas;
        }            

        #endregion

    }
}