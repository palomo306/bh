using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Usuario
    {
        public string Usr { set; get; }
        public string Nombre { set; get; }
        public string Mail { set; get; }
        public string Password { set; get; }
        public byte Estatus { set; get; }

        public List<Rol> UserRoles{ get; set; }

    }
}
