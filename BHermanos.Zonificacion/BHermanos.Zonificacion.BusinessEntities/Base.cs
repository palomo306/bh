using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Base
    {
        #region Propiedades
        public int Id { get; set; }
        public string Nombre { get; set; }
        #endregion
    }
}
