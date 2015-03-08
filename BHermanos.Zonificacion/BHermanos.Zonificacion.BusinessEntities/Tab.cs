using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Tab : Base
    {
        #region Constructores
     
        #endregion

        #region Propiedades

        public List<Zona> ListaZonas { get; set; }

        #endregion
    }
}