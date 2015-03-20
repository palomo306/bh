using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Municipio : Base
    {
        #region Propiedades
        public List<Colonia> ListaColonias { get; set; }
        public Estado ParentEstado { get; set; }
        #endregion

        #region Propiedades Dinamicas
        public string NombreWithEstado
        {
            get
            {
                string result=this.Nombre;
                if (ParentEstado != null)
                    result = ParentEstado.Nombre + " - " + result;
                return result;
            }
        }
        #endregion

        #region Constructor
        public Municipio() 
        {
            ListaColonias = new List<Colonia>();
        }
        #endregion

        #region Métodos de Conversion
        private string GetListaColoniasToJson()
        {
            string jSon = string.Empty;
            if (ListaColonias == null || ListaColonias.Count == 0)
                return "[]";
            else
            {
                string[] arrColoniasJson = new string[ListaColonias.Count];
                for (int i = 0; i < ListaColonias.Count; i++)
                {
                    arrColoniasJson[i] = ListaColonias[i].ToJSon();
                }
                jSon = "[" + string.Join(",", arrColoniasJson) + "]";
            }
            return jSon;
        }

        public string ToJSon()
        {
            try
            {
                string jSon = @"{""<Id>k__BackingField"":" + Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @""",""<ListaColonias>k__BackingField"":" + GetListaColoniasToJson() + @"}";
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
