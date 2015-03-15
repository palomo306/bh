using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Estado : Base
    {
        #region Propiedades
        public List<Municipio> ListaMunicipios { get; set; }
        #endregion

        #region Constructor
        public Estado()
        {
            ListaMunicipios = new List<Municipio>();
        }
        #endregion

    }
}