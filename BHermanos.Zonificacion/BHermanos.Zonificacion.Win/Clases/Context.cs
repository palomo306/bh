using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.Win.Clases
{
    public static class Context
    {
        public static BE.Usuario CurrentUser { get; set; }
    }

    public delegate void GeneralDelegate(object sender, EventArgs args);
}
