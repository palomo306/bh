using BHermanos.Zonificacion.BusinessEntities;
using System;
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
                var spConPlazas = base.oDataAccess.spConPlazas(1, 0);
                foreach (var reg in spConPlazas)
                {
                    Plaza plaza = new Plaza();
                    plaza.Id = reg.fiPlazaId;
                    plaza.Nombre = reg.fcNombre;
                    plaza.Color = reg.fcColor;
                    var a = spConPlazas.Where(p => p.fiPlazaId == reg.fiPlazaId);
                    //plaza.ListaEstados = ObtenerEstados(null);
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
                var spConPlazas = base.oDataAccess.spConPlazas(2, plazaId);
                foreach (var reg in spConPlazas)
                {
                    Plaza plaza = new Plaza();
                    plaza.Id = reg.fiPlazaId;
                    listaPlazas.Add(plaza);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaPlazas;
        }

        public bool InsertaZona(Zona zona) 
        {
            string colonias = string.Empty;
            try
            {                
                base.oDataAccess.spInsZonas(zona.EstadoId, zona.MunicipioId, zona.Nombre, zona.Color, zona.Colonias);
                return true;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public bool InsertaSubZona(Zona zona, int zonaId)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spInsSubZonas(zona.EstadoId, zona.MunicipioId, zona.Nombre, zona.Color, zona.Colonias, zonaId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizaZona(Zona zona)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spUpdZona(zona.EstadoId,zona.MunicipioId,zona.Nombre, zona.Color, zona.Colonias, zona.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizaSubZona(Zona zona)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spUpdSubZonas(zona.EstadoId, zona.MunicipioId, zona.Nombre, zona.Color, zona.Colonias, zona.Id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarZona(int estadoId, int municipioId, int zonaId)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spDelZonas(estadoId, municipioId, zonaId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarSubZona(int estadoId, int municipioId, int subZonaId)
        {            
            try
            {
                base.oDataAccess.spDelSubZonas(estadoId, municipioId, subZonaId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Metodos privados"

        //private List<Estado> ObtenerEstados(IEnumerable<spConPlazasResult> reg)
        //{
        //    List<Estado> lista = new List<Estado>();
        //    try
        //    {
        //        foreach (var r in reg)
        //        {
        //            Estado estado = new Estado();
        //            estado.Id = r.fiEstadoId;
        //            estado.ListaMunicipios = this.ObtenerMunicipios(reg.Where(p => p.fiEstadoId == r.fiEstadoId));
        //            lista.Add(estado);
        //        }
        //        return lista;
        //    }
        //    catch (Exception)
        //    {
        //        return lista;
        //        throw;
        //    }
        //}

        //private List<Municipio> ObtenerMunicipios(IEnumerable<spConPlazasResult> reg)
        //{
        //    List<Municipio> lista = new List<Municipio>();
        //    try
        //    {
        //        foreach (var r in reg)
        //        {
        //            Municipio municipio = new Municipio();
        //            municipio.Id = r.fiMunicipioId;
        //            municipio.ListaColonias = this.ObtenerColonias(reg.Where(p => p.fiMunicipioId == r.fiMunicipioId));
        //            lista.Add(municipio);
        //        }
        //        return lista;
        //    }
        //    catch (Exception)
        //    {
        //        return lista;
        //        throw;
        //    }
        //}

        //private List<Colonia> ObtenerColonias(IEnumerable<DataAccess.spConPlazasResult> reg)
        //{
        //    List<Colonia> lista = new List<Colonia>();
        //    try
        //    {
        //        foreach (var r in reg)
        //        {
        //            Colonia colonia = new Colonia();
        //            colonia.Id = r.fiColoniaId;
        //            colonia.Tipo = r.fiTipoId;
        //            colonia.ListaGrupoRubros = new List<GrupoRubros>();
        //            colonia.ListaPartidas = new List<Partida>();
        //            lista.Add(colonia);
        //        }
        //        return lista;
        //    }
        //    catch (Exception)
        //    {
        //        return lista;
        //        throw;
        //    }
        //}

        #endregion

    }
}
