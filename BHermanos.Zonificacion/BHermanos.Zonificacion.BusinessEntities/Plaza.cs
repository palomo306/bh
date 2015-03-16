using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Plaza : Base
    {
        #region Propiedades
        public string Color { get; set; }

        public string Colonias { get; set; }
        
        public List<Estado> ListaEstados{ get; set; }
        #endregion

        #region Propiedades Dinámicas
        public Color RealColor
        {
            get
            {
                if (!string.IsNullOrEmpty(Color) && Color.Length == 6)
                {
                    string r = Color.Substring(0, 2);
                    string g = Color.Substring(2, 2);
                    string b = Color.Substring(4, 2);
                    return System.Drawing.Color.FromArgb(Convert.ToInt32(r, 16), Convert.ToInt32(g, 16), Convert.ToInt32(b, 16));
                }
                return System.Drawing.Color.Yellow;

            }
        }

        public string Editar
        {
            get
            {
                return "Editar";
            }
        }

        public string Eliminar
        {
            get
            {
                return "Eliminar";
            }
        }
        #endregion

        #region Métodos de Conversion
        private string GetListaEstadosToJson()
        {
            string jSon = string.Empty;
            if (ListaEstados == null || ListaEstados.Count == 0)
                return "[]";
            else
            {
                string[] arrEstadosJson = new string[ListaEstados.Count];
                for (int i = 0; i < ListaEstados.Count; i++)
                {
                    arrEstadosJson[i] = ListaEstados[i].ToJSon();
                }
                jSon = "[" + string.Join(",", arrEstadosJson) + "]";
            }
            return jSon;
        }

        private string GetColoniasIds()
        {
            List<string> colIds = new List<string>();
            foreach (Estado edo in this.ListaEstados)
            {
                foreach (Municipio mun in edo.ListaMunicipios)
                {
                    foreach (Colonia col in mun.ListaColonias)
                    {
                        colIds.Add(col.Id.ToString());
                    }
                }
            }
            return string.Join("|", colIds);
        }

        public string ToJSon(bool includeTree)
        {
            try
            {
                string jSon = @"{""<Id>k__BackingField"":" + Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @""",""<Color>k__BackingField"":""" + Color + @""",""<Colonias>k__BackingField"":""" + GetColoniasIds() + @""",""<ListaEstados>k__BackingField"":" + "[]" + @"}";
                if (includeTree)
                    jSon = @"{""<Id>k__BackingField"":" + Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @""",""<Color>k__BackingField"":""" + Color + @""",""<Colonias>k__BackingField"":""" + GetColoniasIds() + @""",""<ListaEstados>k__BackingField"":" + GetListaEstadosToJson() + @"}";
                return jSon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Constructor
        public Plaza()
        {
            this.ListaEstados = new List<Estado>();
        }
        #endregion
    }
}
