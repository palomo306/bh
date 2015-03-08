using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.DataAccess;

namespace BHermanos.Zonificacion.Seguridad
{
    public class ManejoAcceso : BaseUsuarios
    {

        //#region Atributos

        //private Usuario UsuarioEncontrado = null;        

        //#endregion

        #region Propiedades

        public Usuario UsuarioEncontrado{set;get;}

        #endregion       

        #region Metodos

        public bool Autenticar(string usuario, string password, byte aplicacionId)
        {
            try
            {
                ZonUsuario auxUsr = oDataAccess.ZonUsuarios.Where(usr => usr.fcUsuario == usuario && usr.fiEstatus != 0).FirstOrDefault();
                if (auxUsr == null)
                {
                    throw new ApplicationException("El usuario no existe o esta desactivado");
                }
                else
                {
                    this.UsuarioEncontrado = new Usuario();
                    this.UsuarioEncontrado.Usr = auxUsr.fcUsuario;
                    this.UsuarioEncontrado.Nombre = auxUsr.fcNombre;
                    this.UsuarioEncontrado.Mail = auxUsr.fcMail;
                    this.UsuarioEncontrado.Password = Encoding.ASCII.GetString(auxUsr.fvPassword.ToArray());
                    this.UsuarioEncontrado.Estatus = auxUsr.fiEstatus;
                    this.UsuarioEncontrado.UserRoles = new List<Rol>();

                    


                    //Contador menus    
                    int menuCount = 0;
                    foreach (ZonPerfile perfil in auxUsr.ZonPerfiles)
                    {
                        ZonRole rol = perfil.ZonRole;
                        if (rol.flEstatus == true)
                        {
                            Rol newRol = new Rol() { Id = rol.fiRolId, Nombre = rol.fcNombre, ListMenus = new List<Menu>() };
                            foreach (ZonPermiso permiso in rol.ZonPermisos)
                            {
                                ZonMenus menu = permiso.ZonMenus;
                                if (menu.fiAplicacionId == aplicacionId)
                                {
                                    newRol.ListMenus.Add(new Menu() { Id = menu.fiMenuId, Nombre = menu.fcDescripcion, Aplicacion = menu.fcAplicacion, Orden = menu.fiOrden, Dependencia = menu.fiDependeId, ListMenus = new List<Menu>() });
                                }

                            }
                            newRol.ListMenus = this.TransformaMenus(newRol.ListMenus);

                            if (newRol.ListMenus.Count > 0)
                            {
                                this.UsuarioEncontrado.UserRoles.Add(newRol);
                                menuCount = menuCount + newRol.ListMenus.Count;
                            }
                        }
                    }

                    //string p1 = Encrypt("Krishna13", usuario, base.vector);
                    //auxUsr.fvPassword = Encoding.ASCII.GetBytes(p1);
                    //oDataAccess.SubmitChanges();

                    string p = Decrypt(this.UsuarioEncontrado.Password, usuario, base.vector);
                    if (Encrypt(password, usuario, base.vector) == this.UsuarioEncontrado.Password)
                    {
                        if (menuCount <= 0)
                        {
                            throw new ApplicationException("El usuario no cuenta con menús asignados, favor de ponerte en contacto con el administrador del sistema");
                        }
                        return true;
                    }
                    else
                    {
                        throw new ApplicationException("La contraseña es incorrecta");
                    }
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