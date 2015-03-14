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
        public List<Municipio> ListaMunicipios { get; set; }
    }
}