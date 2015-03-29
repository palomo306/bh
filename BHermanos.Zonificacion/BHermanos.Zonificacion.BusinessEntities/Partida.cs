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

        public string ToJSon()
        {
            try
            {
                string jSon = @"{""<Id>k__BackingField"":" + Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @",""<Valor>k__BackingField"":""" + Valor.ToString() + @",""<Orden>k__BackingField"":""" + Orden.ToString() + @",""<TieneHumbral>k__BackingField"":""" + TieneHumbral.ToString() + @",""<Color>k__BackingField"":""" + Color + @",""<ListaHumbrales>k__BackingField"":""" + "[]" + @"}";
                return jSon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }   
}