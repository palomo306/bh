using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHermanos.Zonificacion.BusinessEntities
{
    [Serializable]
    public class Rubro : Base, ICloneable
    {
        public int Orden { get; set; }
        public bool Main { get; set; }
        public string Expresion { get; set; }
        public double Valor { get; set; }
        public string SignoAcumulado { get; set; }
        public string Formato { get; set; }
        public bool Estatus { get; set; }

        #region Métodos
        public string ToJSon()
        {
            try
            {
                string jSon = @"{""<Orden>k__BackingField"":" + Orden.ToString() + @",""<Main>k__BackingField"":""" + Main.ToString() + @""",""<Expresion>k__BackingField"":""" + Expresion + @""",""<Valor>k__BackingField"":" + Valor.ToString() + @",""<SignoAcumulado>k__BackingField"":""" + SignoAcumulado + @"""}";
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
            Rubro cloneRubro = new Rubro();
            cloneRubro.Id = this.Id;
            cloneRubro.Nombre = this.Nombre;
            cloneRubro.Orden = this.Orden;
            cloneRubro.Main = this.Main;
            cloneRubro.Expresion = this.Expresion;
            cloneRubro.Valor = this.Valor;
            cloneRubro.SignoAcumulado = this.SignoAcumulado;
            cloneRubro.Formato = this.Formato;
            cloneRubro.Estatus = this.Estatus;            

            return cloneRubro;
        }
    }
}