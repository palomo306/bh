using BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorPlazas : ManejadorBase
    {

        #region Atributos

        #endregion

        #region Constructores

        public ManejadorPlazas()
        {

        }

        #endregion

        #region Metodos publicos

        public List<Plaza> ObtenerPlazas()
        {
            List<Plaza> listaPlazas = new List<Plaza>();
            try
            {
                var spPlazas = base.oDataAccess.spConPlazas(1, 0).ToList();
                foreach (var reg in (from p in spPlazas select new { p.fcNombre, p.fiPlazaId, p.fcColor }).Distinct().ToList())
                {
                    Plaza plaza = new Plaza();
                    plaza.Id = reg.fiPlazaId;
                    plaza.Nombre = reg.fcNombre;
                    plaza.Color = reg.fcColor;
                    plaza.ListaEstados = this.ObtenerEstadosXPlaza(reg.fiPlazaId, spPlazas);                   
                    listaPlazas.Add(plaza);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaPlazas;
        }

        public List<Plaza> ObtenerPlaza(int plazaId)
        {
            List<Plaza> listaPlazas = new List<Plaza>();
            try
            {
                var spPlazas = base.oDataAccess.spConPlazas(2, plazaId).ToList();
                foreach (var reg in (from p in spPlazas select new { p.fcNombre, p.fiPlazaId, p.fcColor }).Distinct().ToList())
                {
                    Plaza plaza = new Plaza();
                    plaza.Id = reg.fiPlazaId;
                    plaza.Nombre = reg.fcNombre;
                    plaza.Color = reg.fcColor;
                    plaza.ListaEstados = this.ObtenerEstadosXPlaza(reg.fiPlazaId, spPlazas);
                    listaPlazas.Add(plaza);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaPlazas;
        }

        public bool InsertaPlaza(Plaza plaza)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spInsPlaza(plaza.Nombre, plaza.Color, plaza.Colonias);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizaPlaza(Plaza plaza)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spUpdPlaza(plaza.Id ,plaza.Nombre, plaza.Color, plaza.Colonias);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarPlaza(int plazaId)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spDelPlaza(plazaId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Metodos privados"

        private List<Estado> ObtenerEstadosXPlaza(int plazaId, List<spConPlazasResult> listaRegistros)
        {
            List<Estado> lista = new List<Estado>();
            try
            {
                var spPlazas = base.oDataAccess.spConPlazas(1, 0);
                foreach (int estadoId in (from li in listaRegistros where li.fiPlazaId == plazaId select li.fiEstadoId).Distinct().ToList())
                {
                    Estado estado = new Estado();
                    estado.Id = estadoId;
                    estado.ListaMunicipios = this.ObtenerMunicipiosXPlaza(plazaId, estadoId, listaRegistros);
                    lista.Add(estado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        private List<Municipio> ObtenerMunicipiosXPlaza(int plazaId, int estadoId, List<spConPlazasResult> listaRegistros)
        {
            List<Municipio> lista = new List<Municipio>();
            try
            {
                foreach (int m in listaRegistros.Where(p => p.fiEstadoId == estadoId && p.fiPlazaId == plazaId).Select(s => s.fiMunicipioId).Distinct().ToList())
                {
                    Municipio municipio = new Municipio();
                    municipio.Id = m;
                    municipio.ListaColonias = this.ObtenerColoniasXPlaza(plazaId, estadoId, m, listaRegistros);
                    lista.Add(municipio);
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
                throw ex;
            }
        }

        private List<Colonia> ObtenerColoniasXPlaza(int plazaId, int estadoId, int municipioId, List<spConPlazasResult> listaRegistros)
        {
            List<Colonia> lista = new List<Colonia>();
            try
            {
                foreach (var c in listaRegistros.Where(p => p.fiEstadoId == estadoId && p.fiPlazaId == plazaId && p.fiMunicipioId == municipioId).ToList())
                {
                    Colonia colonia = new Colonia();
                    colonia.Id = c.fiColoniaId;
                    colonia.Tipo = c.fiTipo;
                    colonia.ListaGrupoRubros = new List<GrupoRubros>();
                    colonia.ListaPartidas = new List<Partida>();
                    lista.Add(colonia);
                }
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
                throw ex;
            }
        }

        #endregion

    }
}