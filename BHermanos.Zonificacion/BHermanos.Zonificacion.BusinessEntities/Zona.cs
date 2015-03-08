using BHermanos.Zonificacion.BusinessEntities.Cast;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Zona : Base,ICloneable
    {

         #region Constructores
        public Zona()
        {
            
        }
        #endregion

        #region Propiedades
        public int EstadoId { set; get; }
        public int MunicipioId { set; get; }
        public string Color { set; get; }
        public string Colonias { get; set; }
        public List<Zona> ListaSubzonas { set; get; }
        public List<Colonia> ListaColonias { set; get; }
        public List<Partida> ListaPartidas { set; get; }
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
        #endregion

        #region Métodos públicos
        private void ClearColoniaCtrl(Colonia colCtrl)
        {
            foreach (GrupoRubros gpr in colCtrl.ListaGrupoRubros)
            {
                foreach (Rubro rb in gpr.ListaRubros)
                {
                    rb.Valor = 0;
                }
            }
        }

        public Colonia GetColoniaTotal()
        {
            try
            {
                if (ListaColonias != null)
                {
                    Colonia auxCol = ListaColonias.Where(col => col.Id == 0).FirstOrDefault();
                    Colonia colCtrl = (Colonia)auxCol.Clone();
                    ClearColoniaCtrl(colCtrl);
                    if (colCtrl != null)
                    {
                        for (int i = 1; i < ListaColonias.Count; i++)
                        {
                            foreach (GrupoRubros gpr in colCtrl.ListaGrupoRubros)
                            {
                                GrupoRubros gprInZona = ListaColonias[i].ListaGrupoRubros.Where(gprIZ => gprIZ.Id == gpr.Id).FirstOrDefault();
                                if (gprInZona != null)
                                {
                                    foreach (Rubro rb in gpr.ListaRubros)
                                    {
                                        Rubro rbInZona = gprInZona.ListaRubros.Where(rvIZ => rvIZ.Id == rb.Id).FirstOrDefault();
                                        if (rbInZona != null)
                                        {
                                            rb.Valor += rbInZona.Valor;
                                        }
                                    }                                    
                                }
                            }
                        }
                        //Se sacan los promedios de ser necesario
                        foreach (GrupoRubros gpr in colCtrl.ListaGrupoRubros)
                        {
                            foreach (Rubro rb in gpr.ListaRubros)
                            {
                                if (rb.SignoAcumulado == "P")
                                {
                                    if (this.ListaColonias.Count > 1)
                                        rb.Valor = rb.Valor / (this.ListaColonias.Count - 1);
                                    else
                                        rb.Valor = rb.Valor;
                                }
                            }
                        }
                        return colCtrl;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Colonia GetColoniaExceptLast()
        {
            try
            {
                if (ListaColonias != null)
                {
                    //Última colonia
                    Colonia lastCol = ListaColonias[ListaColonias.Count - 1];
                    //Colonia de control
                    Colonia auxCol = ListaColonias.Where(col => col.Id == 0).FirstOrDefault();
                    Colonia colCtrl = (Colonia)auxCol.Clone();
                    ClearColoniaCtrl(colCtrl);
                    if (colCtrl != null)
                    {
                        for (int i = 1; i < ListaColonias.Count; i++)
                        {
                            if (lastCol.Id != ListaColonias[i].Id)
                            {
                                foreach (GrupoRubros gpr in colCtrl.ListaGrupoRubros)
                                {
                                    GrupoRubros gprInZona = ListaColonias[i].ListaGrupoRubros.Where(gprIZ => gprIZ.Id == gpr.Id).FirstOrDefault();
                                    if (gprInZona != null)
                                    {
                                        foreach (Rubro rb in gpr.ListaRubros)
                                        {
                                            Rubro rbInZona = gprInZona.ListaRubros.Where(rvIZ => rvIZ.Id == rb.Id).FirstOrDefault();
                                            if (rbInZona != null)
                                            {
                                                rb.Valor += rbInZona.Valor;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Se sacan los promedios de ser necesario
                        foreach (GrupoRubros gpr in colCtrl.ListaGrupoRubros)
                        {
                            foreach (Rubro rb in gpr.ListaRubros)
                            {
                                if (rb.SignoAcumulado == "P")
                                {
                                    if (this.ListaColonias.Count > 2)
                                        rb.Valor = rb.Valor / (this.ListaColonias.Count - 2);
                                    else
                                        rb.Valor = rb.Valor;
                                }
                            }
                        }
                        return colCtrl;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Colonia GetColoniaLast()
        {
            try
            {
                if (ListaColonias != null)
                {
                    //Última colonia
                    Colonia lastCol = ListaColonias[ListaColonias.Count - 1];
                    if (lastCol != null)
                    {
                        return lastCol;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Métodos de Conversion
        private string ListaSubzonasToJSon()
        {
            string jSon = string.Empty;
            if (ListaSubzonas == null || ListaSubzonas.Count == 0)
                return "[]";
            else
            {
                string[] arrZonasJson = new string[ListaSubzonas.Count];
                for (int i = 0; i < ListaSubzonas.Count; i++)
                {
                    arrZonasJson[i] = ListaSubzonas[i].ToJSon(false);
                }
                jSon = "[" + string.Join(",", arrZonasJson) + "]";
            }
            return jSon;
        }

        private string ListaColoniasToJSon()
        {
            string jSon = string.Empty;
            if (ListaColonias == null || ListaColonias.Count == 0)
                return "[]";
            else
            {
                string[] arrColoniasJson = new string[ListaColonias.Count];
                for (int i = 0; i < ListaColonias.Count; i++)
                {
                    if (ListaColonias[i].Id != 0)
                        arrColoniasJson[i] = ListaColonias[i].ToJSon();
                }
                jSon = "[" + string.Join(",", arrColoniasJson) + "]";
            }
            return jSon;
        }

        public string ToJSon(bool includeCol)
        {
            try
            {
                string jSon;
                if (includeCol)
                    jSon = @"{""<Id>k__BackingField"":" + this.Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @""",""<EstadoId>k__BackingField"":" + EstadoId.ToString() + @",""<MunicipioId>k__BackingField"":" + MunicipioId.ToString() + @",""<Color>k__BackingField"":""" + Color + @""",""<Colonias>k__BackingField"":" + Colonias + @",""<ListaSubzonas>k__BackingField"":" + ListaSubzonasToJSon() + @",""<ListaColonias>k__BackingField"":" + ListaColoniasToJSon() + @",""<Colonias>k__BackingField"":""" + string.Join("|", ListaColonias.Where(c => c.Id != 0).Select(c => c.Id.ToString()).Distinct().ToArray()) + @"""}";
                else
                    jSon = @"{""<Id>k__BackingField"":" + this.Id.ToString() + @",""<Nombre>k__BackingField"":""" + Nombre + @""",""<EstadoId>k__BackingField"":" + EstadoId.ToString() + @",""<MunicipioId>k__BackingField"":" + MunicipioId.ToString() + @",""<Color>k__BackingField"":""" + Color + @""",""<Colonias>k__BackingField"":" + Colonias + @",""<ListaSubzonas>k__BackingField"":" + ListaSubzonasToJSon() + @",""<ListaColonias>k__BackingField"":[],""<Colonias>k__BackingField"":""" + string.Join("|", ListaColonias.Where(c => c.Id != 0).Select(c => c.Id.ToString()).Distinct().ToArray()) + @"""}";
                return jSon;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Métodos de la Interfaz IClonable
        public object Clone()
        {
            Zona copyZona = ObjectCopier.Clone<Zona>(this);
            return copyZona;
        }
        #endregion
    }
}