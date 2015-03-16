using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Estado : Base
    {
        #region Propiedades
        public List<Municipio> ListaMunicipios { get; set; }
        #endregion

        #region Constructor
        public Estado()
        {
            ListaMunicipios = new List<Municipio>();
        }
        #endregion

        #region Métodos de Conversion
        private string GetListaMunicipiosToJson()
        {
            string jSon = string.Empty;
            if (ListaMunicipios == null || ListaMunicipios.Count == 0)
                return "[]";
            else
            {
                string[] arrMunicipiosJson = new string[ListaMunicipios.Count];
                for (int i = 0; i < ListaMunicipios.Count; i++)
                {
                    arrMunicipiosJson[i] = ListaMunicipios[i].ToJSon();
                }
                jSon = "[" + string.Join(",", arrMunicipiosJson) + "]";
            }
            return jSon;
        }

        public string ToJSon()
        {
            try
            {
                string jSon = @"{""<Id>k__BackingField"":" + Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @""",""<ListaMunicipios>k__BackingField"":" + GetListaMunicipiosToJson() + @"}";
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