using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Partida : Base
    {

        #region Propiedades

        public double Valor { get; set; }

        public int Orden { get; set; }

        public bool TieneHumbral { get; set; }

        public string Color { get; set; }

        public List<Humbral> ListaHumbrales { get; set; }

        #endregion

    }   
}