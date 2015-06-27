using BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHermanos.Zonificacion.DataAccess;


namespace BHermanos.Zonificacion.BusinessMaps
{
    public class ManejadorGrupoRubros : ManejadorBase
    {

        #region Atributos

        #endregion

        #region Constructores

        public ManejadorGrupoRubros()
        {

        }

        #endregion

        #region Metodos publicos

        public List<GrupoRubros> ObtenerGrupoDeRubros()
        {
            List<GrupoRubros> listaGrupos = new List<GrupoRubros>();
            try
            {
                foreach (ZonGrupoRubro reg1 in base.oDataAccess.ZonGrupoRubros.ToList<ZonGrupoRubro>())
                {
                    GrupoRubros grupo = new GrupoRubros();
                    grupo.Id = reg1.fiGrupoId;
                    grupo.Nombre = reg1.fcNombre;
                    grupo.ListaRubros = new List<Rubro>();
                    foreach (ZonRubro reg2 in reg1.ZonRubros)
                    {
                        Rubro rubro = new Rubro();
                        rubro.Id = reg2.fiRubroId;
                        rubro.Main = reg2.flMain;
                        rubro.Nombre = reg2.fcDescripcion;
                        rubro.Orden = reg2.fiOrden;
                        rubro.SignoAcumulado = reg2.fcSignoAcumulado;
                        rubro.Estatus = reg2.flEstatus;
                        rubro.Expresion = reg2.fcExpresion;
                        rubro.Formato = reg2.fcFormato;
                        grupo.ListaRubros.Add(rubro);
                    }
                    listaGrupos.Add(grupo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaGrupos;
        }

        public List<GrupoRubros> ObtenerGrupoDeRubros(int grupoId)
        {
            List<GrupoRubros> listaGrupos = new List<GrupoRubros>();
            try
            {
                foreach (ZonGrupoRubro reg1 in base.oDataAccess.ZonGrupoRubros.Where(g => g.fiGrupoId == grupoId).ToList<ZonGrupoRubro>())
                {
                    GrupoRubros grupo = new GrupoRubros();
                    grupo.Id = reg1.fiGrupoId;
                    grupo.Nombre = reg1.fcNombre;
                    grupo.ListaRubros = new List<Rubro>();
                    foreach (ZonRubro reg2 in reg1.ZonRubros)
                    {
                        Rubro rubro = new Rubro();
                        rubro.Id = reg2.fiRubroId;
                        rubro.Main = reg2.flMain;
                        rubro.Nombre = reg2.fcDescripcion;
                        rubro.Orden = reg2.fiOrden;
                        rubro.SignoAcumulado = reg2.fcSignoAcumulado;
                        rubro.Estatus = reg2.flEstatus;
                        rubro.Expresion = reg2.fcExpresion;
                        rubro.Formato = reg2.fcFormato;
                        grupo.ListaRubros.Add(rubro);
                    }
                    listaGrupos.Add(grupo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaGrupos;
        }

        public bool ActualizaGrupoRubros(GrupoRubros grupo)
        {
            try
            {
                ZonGrupoRubro zonGrupoRubro = base.oDataAccess.ZonGrupoRubros.Where(r => r.fiGrupoId == grupo.Id).FirstOrDefault<ZonGrupoRubro>();
                if (zonGrupoRubro != null)
                {
                    zonGrupoRubro.ZonRubros.Clear();
                    int id = 1;
                    foreach (Rubro reg1 in grupo.ListaRubros)
                    {
                        ZonRubro zonRubro = new ZonRubro();
                        zonRubro.fiRubroId = id;
                        zonRubro.flMain = reg1.Main;
                        zonRubro.fcDescripcion = reg1.Nombre;
                        zonRubro.fiOrden = reg1.Orden;
                        zonRubro.fcSignoAcumulado = reg1.SignoAcumulado;
                        zonRubro.flEstatus = reg1.Estatus;
                        zonRubro.fcExpresion = reg1.Expresion;
                        zonRubro.fcFormato = reg1.Formato;
                        zonRubro.fiGrupoId = grupo.Id;
                        zonRubro.fiOrden = id;
                        zonGrupoRubro.ZonRubros.Add(zonRubro);
                        id++;
                    }
                    base.oDataAccess.SubmitChanges();
                    return true;
                }

                else
                {
                    throw new ApplicationException("El id " + grupo.Id + " del grupo no existe");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
