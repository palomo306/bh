using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Menu : Base
    {
        #region Propiedades
        public List<Menu> ListMenus { get; set; }
        public int Orden { get; set; }
        public int Dependencia { get; set; }
        public string Aplicacion { get; set; }
        #endregion
    }
}
