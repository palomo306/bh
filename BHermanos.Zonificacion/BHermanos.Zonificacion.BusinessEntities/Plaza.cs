using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Plaza : Base
    {
        public string Color { get; set; }
        public List<Estado> ListaEstados{ get; set; }
    }
}
