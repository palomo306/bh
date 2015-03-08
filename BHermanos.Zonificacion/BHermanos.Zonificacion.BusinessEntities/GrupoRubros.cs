using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class GrupoRubros : Base, ICloneable
    {
        public int Orden { get; set; }
        public List<Rubro> ListaRubros { get; set; }


        #region Métodos de Conversion
        private string GetListaRubrosToJson()
        {
            string jSon = string.Empty;
            if (ListaRubros == null || ListaRubros.Count == 0)
                return "[]";
            else
            {
                string[] arrZonasJson = new string[ListaRubros.Count];
                for (int i = 0; i < ListaRubros.Count; i++)
                {
                    arrZonasJson[i] = ListaRubros[i].ToJSon();
                }
                jSon = "[" + string.Join(",", arrZonasJson) + "]";
            }
            return jSon;
        }

        public string ToJSon()
        {
            try
            {
                string jSon = @"{""<Id>k__BackingField"":" + Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @",""<ListaRubros>k__BackingField"":" + GetListaRubrosToJson() + @"}";
                return jSon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public object Clone()
        {
            GrupoRubros cloneGrupoRubros = new GrupoRubros();
            cloneGrupoRubros.Id = this.Id;
            cloneGrupoRubros.Nombre = this.Nombre;
            cloneGrupoRubros.Orden = this.Orden;
            cloneGrupoRubros.ListaRubros = new List<Rubro>();
            foreach (Rubro rb in ListaRubros)
            {
                cloneGrupoRubros.ListaRubros.Add((Rubro)rb.Clone());
            }
            return cloneGrupoRubros;
        }
    }
}