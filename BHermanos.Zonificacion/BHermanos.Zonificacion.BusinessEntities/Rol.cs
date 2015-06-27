using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Rol : Base
    {
        #region Propiedades
        public List<Menu> ListMenus { get; set; }
        #endregion
    }
}