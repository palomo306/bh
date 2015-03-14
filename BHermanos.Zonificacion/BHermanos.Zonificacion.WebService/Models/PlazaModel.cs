using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHermanos.Zonificacion.WebService.Models
{
    [Serializable]
    public class PlazaModel
    {
        public bool Succes { set; get; }
        public string Mensaje { set; get; }
        public IEnumerable<Plaza> ListaPlazas { set; get; }

    }
}