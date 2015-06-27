using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorMunicipios : ManejadorBase
    {

        #region Atributos

        #endregion

        #region Constructores

        public ManejadorMunicipios() 
        {
 
        }

        #endregion        

        #region Metodos

        public IList<Municipio> ObtenerMunicipios(int estadoId) 
        {
            List<Municipio> municipios = new List<Municipio>();            
            try
            {
                var spConMunicipios = base.oDataAccess.spConMunicipios(estadoId);
                foreach (var registro in spConMunicipios) 
                {
                    Municipio municipio = new Municipio();
                    municipio.Id = registro.fiMunicipioId;
                    municipio.Nombre = registro.fcNombre;
                    municipios.Add(municipio);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return municipios;
        }
        #endregion

    }
}
