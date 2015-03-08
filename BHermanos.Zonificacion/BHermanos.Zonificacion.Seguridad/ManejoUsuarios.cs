using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.DataAccess;

namespace BHermanos.Zonificacion.Seguridad
{
    public class ManejoUsuarios : BaseUsuarios
    {
        
        #region Metodos

        public bool AltaUsuario(Usuario usuario)
        {
            ZonUsuario zonUsuario = null;
            try
            {
                ZonUsuario usr = oDataAccess.ZonUsuarios.Where(u => u.fcUsuario == usuario.Usr).FirstOrDefault();
                if (usr != null)
                {
                    throw new ApplicationException("El usuario ya existe");
                }
                else
                {
                    zonUsuario = new ZonUsuario();
                    zonUsuario.fcUsuario = usuario.Usr;
                    zonUsuario.fcNombre = usuario.Nombre;
                    zonUsuario.fcMail = usuario.Mail;
                    zonUsuario.fiEstatus = 2;
                    zonUsuario.fvPassword = Encoding.ASCII.GetBytes(base.Encrypt(usuario.Usr, usuario.Usr, vector));
                    oDataAccess.ZonUsuarios.InsertOnSubmit(zonUsuario);
                    oDataAccess.SubmitChanges();
                    foreach (Rol r in usuario.UserRoles)
                    {
                        ZonPerfile zp = new ZonPerfile();
                        zp.fcUsuario = usuario.Usr;
                        zp.fiRolId = r.Id;
                        oDataAccess.ZonPerfiles.InsertOnSubmit(zp);
                    }
                    oDataAccess.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            List<Usuario> listaUsuario = new List<Usuario>();
            try
            {
                oDataAccess.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, oDataAccess.ZonPerfiles);
                oDataAccess.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, oDataAccess.ZonUsuarios);                
                List<ZonUsuario> usuarios = oDataAccess.ZonUsuarios.ToList<ZonUsuario>();
                foreach (ZonUsuario zonUsr in usuarios)
                {
                    Usuario usrAux = new Usuario();
                    usrAux.Usr = zonUsr.fcUsuario;
                    usrAux.Nombre = zonUsr.fcNombre;
                    usrAux.Mail = zonUsr.fcMail;
                    usrAux.Estatus = zonUsr.fiEstatus;
                    usrAux.Password = string.Empty;
                    usrAux.UserRoles = new List<Rol>();
                    foreach (ZonPerfile perfil in zonUsr.ZonPerfiles)
                    {
                        ZonRole rol = perfil.ZonRole;
                        Rol newRol = new Rol() { Id = rol.fiRolId, Nombre = rol.fcNombre, ListMenus = new List<Menu>() };
                        usrAux.UserRoles.Add(newRol);
                    }
                    listaUsuario.Add(usrAux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listaUsuario;
        }        

        public bool CambiarPassword(string usuario, string passwordNuevo)
        {
            string passEncriptado = string.Empty;
            try
            {
                ZonUsuario usr = oDataAccess.ZonUsuarios.Where(u => u.fcUsuario == usuario).FirstOrDefault();
                if (usr == null)
                {
                    throw new ApplicationException("El usuario no se encuentra registrado");
                }
                else
                {
                    passEncriptado = base.Encrypt(passwordNuevo, usuario, vector);
                    if (passEncriptado == Encoding.UTF8.GetString(usr.fvPassword.ToArray()))
                    {
                        throw new ApplicationException("El nuevo password no puede ser igual al password actual");
                    }
                    else
                    {
                        usr.fvPassword = Encoding.ASCII.GetBytes(passEncriptado);
                        usr.fiEstatus = 1;
                        oDataAccess.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool CambiarDatosGenerales(Usuario usuario)
        {
            try
            {
                ZonUsuario usr = oDataAccess.ZonUsuarios.Where(u => u.fcUsuario == usuario.Usr).FirstOrDefault();
                if (usr == null)
                {
                    throw new ApplicationException("El usuario no se encuentra registrado");
                }
                else
                {
                    oDataAccess.ZonPerfiles.DeleteAllOnSubmit(usr.ZonPerfiles);
                    oDataAccess.SubmitChanges();

                    foreach (Rol r in usuario.UserRoles)
                    {
                        ZonPerfile zp = new ZonPerfile();
                        zp.fcUsuario = usuario.Usr;
                        zp.fiRolId = r.Id;
                        oDataAccess.ZonPerfiles.InsertOnSubmit(zp);
                    }
                    oDataAccess.SubmitChanges();


                    usr.fcNombre = usuario.Nombre;
                    usr.fcMail = usuario.Mail;

                    oDataAccess.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ResetPassword(string usuario)
        {
            string passEncriptado = string.Empty;
            try
            {
                ZonUsuario usr = oDataAccess.ZonUsuarios.Where(u => u.fcUsuario == usuario).FirstOrDefault();
                if (usr == null)
                {
                    throw new ApplicationException("El usuario no se encuentra registrado");
                }
                else
                {
                    passEncriptado = base.Encrypt(usuario, usuario, vector);
                    usr.fvPassword = Encoding.ASCII.GetBytes(passEncriptado);
                    usr.fiEstatus = 2;
                    oDataAccess.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool BajaUsuario(string usuario)
        {            
            try
            {
                ZonUsuario usr = oDataAccess.ZonUsuarios.Where(u => u.fcUsuario == usuario).FirstOrDefault();
                if (usr == null)
                {
                    throw new ApplicationException("El usuario no se encuentra registrado");
                }
                else
                {                    
                    usr.fiEstatus = 0;                    
                    oDataAccess.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool ActivarUsuario(string usuario)
        {
            string passEncriptado = string.Empty;
            try
            {
                ZonUsuario usr = oDataAccess.ZonUsuarios.Where(u => u.fcUsuario == usuario).FirstOrDefault();
                if (usr == null)
                {
                    throw new ApplicationException("El usuario no se encuentra registrado");
                }
                else
                {
                    passEncriptado = base.Encrypt(usuario, usuario, vector);
                    usr.fvPassword = Encoding.ASCII.GetBytes(passEncriptado);
                    usr.fiEstatus = 2;
                    oDataAccess.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }       
        #endregion
    
    }
}