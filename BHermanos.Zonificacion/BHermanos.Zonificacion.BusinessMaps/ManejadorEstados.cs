using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorEstados : ManejadorBase
    {

        #region Atributos

        #endregion

        #region Constructores

        public ManejadorEstados() 
        {
 
        }

        #endregion        

        #region Metodos

        public IList<Estado> ObtenerEstados() 
        {
            List<Estado> estados = new List<Estado>();            
            try
            {
                var spConEstados = base.oDataAccess.spConEstados();
                foreach (var registro in spConEstados) 
                {
                    Estado estado = new Estado();
                    estado.Id = registro.fiEstadoId;
                    estado.Nombre = registro.fcNombre;
                    estados.Add(estado);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return estados;
        }
        #endregion

    }
}
