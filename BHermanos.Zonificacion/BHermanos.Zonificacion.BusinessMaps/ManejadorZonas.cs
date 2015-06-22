using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorZonas : ManejadorBase
    {

        #region Atributos

        #endregion

        #region Constructores

        public ManejadorZonas()
        {

        }

        #endregion

        #region Metodos publicos

        public List<Zona> ObtenerZonas(byte vistaId, int plazaId, int zonaId)
        {
            List<Zona> listaZonas = new List<Zona>();
            try
            {
                switch (vistaId)
                {                       
                    case 0: //zona vacia
                        Zona zona1 = new Zona();
                        zona1.Id = 0;
                        zona1.PlazaId = plazaId;                        
                        zona1.Nombre = "";
                        zona1.Color = "";
                        zona1.ListaSubzonas = new List<Zona>();
                        zona1.ListaColonias = this.ObtenerColoniasVacia();
                        zona1.ListaPartidas = new List<Partida>();
                        listaZonas.Add(zona1);
                        break;
                    case 1: //obtener zona
                        var spConZonas1 = base.oDataAccess.spConZonas(2, plazaId, zonaId);
                        foreach (var reg in spConZonas1)
                        {
                            Zona zona2 = new Zona();
                            zona2.Id = reg.fiZonaId;
                            zona2.PlazaId = reg.fiPlazaId;                            
                            zona2.Nombre = reg.fcNombre;
                            zona2.Color = reg.fcColor;
                            //Se agregan las colonias base
                            zona2.ListaSubzonas = this.ObtenerSubZonas(reg.fiPlazaId, reg.fiZonaId);
                            zona2.ListaColonias = this.ObtenerColoniasXZona(1,reg.fiPlazaId, reg.fiZonaId);
                            zona2.ListaPartidas = new List<Partida>();
                            //Se agrega la colonia vacía
                            List<Colonia> latAuxColonias = this.ObtenerColoniasVacia();
                            zona2.ListaColonias.Insert(0, latAuxColonias[0]);
                            listaZonas.Add(zona2);
                        }
                        break;
                    default:
                        var spConZonas2 = base.oDataAccess.spConZonas(1, plazaId, zonaId);
                        foreach (var reg in spConZonas2)
                        {
                            Zona zona2 = new Zona();
                            zona2.Id = reg.fiZonaId;
                            zona2.PlazaId = reg.fiPlazaId;                            
                            zona2.Nombre = reg.fcNombre;
                            zona2.Color = reg.fcColor;
                            zona2.ListaSubzonas = this.ObtenerSubZonas(reg.fiPlazaId,reg.fiZonaId);
                            zona2.ListaColonias = this.ObtenerColoniasXZona(1,reg.fiPlazaId,reg.fiZonaId);
                            zona2.ListaPartidas = new List<Partida>();
                            //Se agrega la colonia vacía
                            List<Colonia> latAuxColonias = this.ObtenerColoniasVacia();
                            zona2.ListaColonias.Insert(0, latAuxColonias[0]);
                            listaZonas.Add(zona2);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaZonas;
        }

        public bool InsertaZona(Zona zona) 
        {
            string colonias = string.Empty;
            try
            {                
                base.oDataAccess.spInsZona(zona.PlazaId, zona.Nombre, zona.Color, zona.Colonias);
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
                base.oDataAccess.spInsSubZona(zona.PlazaId, zonaId, zona.Nombre, zona.Color, zona.Colonias);
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
                base.oDataAccess.spUpdZona(zona.PlazaId,zona.Id,zona.Nombre, zona.Color, zona.Colonias);
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
                base.oDataAccess.spUpdSubZona(zona.PlazaId, zona.Id, zona.Nombre, zona.Color, zona.Colonias);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarZona(int plazaId, int zonaId)
        {
            string colonias = string.Empty;
            try
            {
                base.oDataAccess.spDelZona(plazaId, zonaId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarSubZona(int plazaId, int subZonaId)
        {            
            try
            {
                base.oDataAccess.spDelSubZona(plazaId, subZonaId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Metodos privados"

        private List<Zona> ObtenerSubZonas(int plazaId, int zonaId) 
        {
            List<Zona> listaSubZonas = new List<Zona>();
            try
            {
                var spConZonas = base.oDataAccess.spConZonas(1, plazaId, zonaId);
                foreach (var reg in spConZonas)
                {
                    Zona zona = new Zona();
                    zona.Id = reg.fiZonaId;
                    zona.PlazaId = reg.fiPlazaId;                    
                    zona.Nombre = reg.fcNombre;
                    zona.Color = reg.fcColor;
                    zona.ListaSubzonas = new List<Zona>();                    
                    zona.ListaColonias = this.ObtenerColoniasXZona(1,reg.fiPlazaId,reg.fiZonaId);
                    //Se agrega la colonia vacía
                    List<Colonia> latAuxColonias = this.ObtenerColoniasVacia();
                    zona.ListaColonias.Insert(0, latAuxColonias[0]);
                    listaSubZonas.Add(zona);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listaSubZonas;
        }

        private List<Colonia> ObtenerColoniasXZona(byte vista, int plazaId, int zonaId)
        {
            List<Colonia> listaColonias = new List<Colonia>();
            try
            {
                var spConColoniasXZona = base.oDataAccess.spConColoniasXZona(vista, plazaId, zonaId);
                using (ManejadorColonias manejadorColonias = new ManejadorColonias())
                {
                    foreach (var reg in spConColoniasXZona)
                    {
                        List<Colonia> col = manejadorColonias.ObtenerColonias(2, plazaId, reg.fiColoniaId);
                        if (col != null && col.Count > 0)

                            listaColonias.Add(col[0]);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listaColonias;
        }

        private List<Colonia> ObtenerColoniasVacia()
        {
            List<Colonia> listaColonias = new List<Colonia>();
            try
            {                
                using (ManejadorColonias manejadorColonias = new ManejadorColonias())
                {
                    listaColonias = manejadorColonias.ObtenerColoniaVacia(null);  
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
