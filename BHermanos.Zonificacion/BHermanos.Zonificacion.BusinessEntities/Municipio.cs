using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Municipio : Base
    {
        #region Propiedades
        public List<Colonia> ListaColonias { get; set; }
        #endregion

        #region Constructor
        public Municipio() 
        {
            ListaColonias = new List<Colonia>();
        }
        #endregion
    }
}
