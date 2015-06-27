using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHermanos.Zonificacion.WebService.Models
{
    [Serializable]
    public class AccesoModel
    {
        public bool Accesa { set; get; }
        public string Mensaje { set; get; }
        public Usuario DatosUsuario { set; get; }
    }
}