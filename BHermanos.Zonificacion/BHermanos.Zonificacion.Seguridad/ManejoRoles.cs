using BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BHermanos.Zonificacion.Seguridad
{
    public class ManejoRoles : IDisposable
    {

        #region Atributos

        private DataAccessDataContext oDataAccess = null;

        #endregion

        #region Constructores

        public ManejoRoles()
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["ConexionZonificacion"].ToString();
                oDataAccess = new DataAccessDataContext(connString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Metodos

        public bool AltaRol(Rol rol)
        {
            ZonRole zonRol = null;
            try
            {
                if (oDataAccess.ZonRoles.Where(r => r.fiRolId == rol.Id).FirstOrDefault() != null)
                {
                    throw new ApplicationException("El rol ya existe");
                }
                else
                {
                    zonRol = new ZonRole();
                    zonRol.fiRolId = rol.Id;
                    zonRol.fcNombre = rol.Nombre;
                    zonRol.flEstatus = true;
                    if (rol.ListMenus != null)
                    {
                        foreach (Menu m in rol.ListMenus)
                        {
                            ZonPermiso zonPermiso = new ZonPermiso();
                            zonPermiso.fiMenuId = m.Id;
                            zonPermiso.fiRoldId = rol.Id;
                            zonRol.ZonPermisos.Add(zonPermiso);
                        }
                    }
                    oDataAccess.ZonRoles.InsertOnSubmit(zonRol);
                    oDataAccess.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool BajaRol(int id)
        {
            try
            {
                ZonRole rol = oDataAccess.ZonRoles.Where(u => u.fiRolId == id).FirstOrDefault();
                if (rol == null)
                {
                    throw new ApplicationException("El rol no se encuentra registrado");
                }
                else
                {
                    rol.flEstatus = false;
                    oDataAccess.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public IEnumerable<Rol> ObtenerRoles()
        {
            List<Rol> listaRoles = new List<Rol>();
            try
            {
                List<ZonRole> roles = oDataAccess.ZonRoles.ToList<ZonRole>();
                foreach (ZonRole zr in roles)
                {
                    Rol r = new Rol();
                    r.Id = zr.fiRolId;
                    r.Nombre = zr.fcNombre;
                    r.ListMenus = new List<Menu>();
                    foreach (ZonPermiso p in zr.ZonPermisos)
                    {
                        ZonMenus m = p.ZonMenus;
                        r.ListMenus.Add(new Menu() { Id = m.fiMenuId, Nombre = m.fcDescripcion, Aplicacion = m.fcAplicacion, Orden = m.fiOrden, Dependencia = m.fiDependeId, ListMenus = new List<Menu>() });
                    }
                    listaRoles.Add(r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaRoles;
        }

        public IEnumerable<Rol> ObtenerRol(int id)
        {
            List<Rol> listaRol = new List<Rol>();
            try
            {
                ZonRole zr = oDataAccess.ZonRoles.Where(r => r.fiRolId == id).FirstOrDefault();
                if (zr != null)
                {
                    Rol rol = new Rol();
                    rol.Id = zr.fiRolId;
                    rol.Nombre = zr.fcNombre;
                    rol.ListMenus = new List<Menu>();
                    foreach (ZonPermiso p in zr.ZonPermisos)
                    {
                        ZonMenus m = p.ZonMenus;
                        rol.ListMenus.Add(new Menu() { Id = m.fiMenuId, Nombre = m.fcDescripcion, Aplicacion = m.fcAplicacion, Orden = m.fiOrden, Dependencia = m.fiDependeId, ListMenus = new List<Menu>() });
                    }
                    listaRol.Add(rol);
                }
                else
                {
                    throw new ApplicationException("El id del rol no existe");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaRol;
        }

        public void Dispose()
        {
            if (oDataAccess != null)
                oDataAccess.Dispose();
        }

        #endregion

    }
}