using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{

    [Serializable]
    public class Humbral
    {

        #region Propiedades

        public int Consucutivo { get; set; }

        public string Operador { get; set; }

        public double Valor { get; set; }

        public string Color { get; set; }

        #endregion

    }
}