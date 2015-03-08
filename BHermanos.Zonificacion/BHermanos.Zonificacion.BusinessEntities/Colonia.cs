using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Colonia : ICloneable
    {
        #region Constructores
        public Colonia()
        {
         
        }
        #endregion

        #region Propiedades

        public double Id { get; set; }
        public string Nombre { get; set; }

        public List<GrupoRubros> ListaGrupoRubros { get; set; }

        public List<Partida> ListaPartidas { get; set; }

        #endregion

        #region Métodos de Conversion
        private string GetListaGrupoRubrosToJson()
        {
            string jSon = string.Empty;
            if (ListaGrupoRubros == null || ListaGrupoRubros.Count == 0)
                return "[]";
            else
            {
                string[] arrZonasJson = new string[ListaGrupoRubros.Count];
                for (int i = 0; i < ListaGrupoRubros.Count; i++)
                {
                    arrZonasJson[i] = ListaGrupoRubros[i].ToJSon();
                }
                jSon = "[" + string.Join(",", arrZonasJson) + "]";
            }
            return jSon;
        }

        public string ToJSon()
        {
            try
            {
                string jSon = @"{""<Id>k__BackingField"":" + Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @",""<ListaGrupoRubros>k__BackingField"":" + GetListaGrupoRubrosToJson() + @"}";
                return jSon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Implementación IClonable
        public object Clone()
        {
            Colonia cloneCol = new Colonia();
            cloneCol.Id = this.Id;
            cloneCol.Nombre = this.Nombre;
            cloneCol.ListaGrupoRubros = new List<GrupoRubros>();
            foreach (GrupoRubros gpRb in this.ListaGrupoRubros)
                cloneCol.ListaGrupoRubros.Add((GrupoRubros)gpRb.Clone());
            return cloneCol;
        }
        #endregion
    }
}