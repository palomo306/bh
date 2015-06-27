using BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.Seguridad;
using BHermanos.Zonificacion.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BHermanos.Zonificacion.WebService.Controllers
{
    public class UsuarioController : ApiController
    {

        #region Metodos

        public IHttpActionResult GetUsuario()
        {
            UsuarioModel usrModel = new UsuarioModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaUsuarios = null
            };

            try
            {
                using (ManejoUsuarios manejoUsuarios = new ManejoUsuarios())
                {
                    usrModel.ListaUsuarios = manejoUsuarios.ObtenerUsuarios();
                    usrModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                usrModel.Mensaje = ex.Message;
            }
            return Ok(usrModel);
        }

        // POST /WebApi/Usuario/PostUsuario?type=json        
        public IHttpActionResult PostUsuario([FromBody]Usuario usuario)
        {
            UsuarioModel usrModel = new UsuarioModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaUsuarios = null
            };

            try
            {
                if (!usuario.Equals(null))
                {
                    using (ManejoUsuarios manejoUsuarios = new ManejoUsuarios())
                    {
                        if (!manejoUsuarios.AltaUsuario(usuario))
                        {
                            usrModel.Mensaje = "No fue posible dar de alta el usuario";
                        }
                        else
                        {
                            usrModel.Succes = true;
                            usrModel.ListaUsuarios = manejoUsuarios.ObtenerUsuarios();
                            usrModel.Mensaje = "El usuario se dio de alta correctamente";

                        }
                    }
                }
                else
                {
                    usrModel.Mensaje = "El parametro de entrada(cliente) es nulo";
                }
            }
            catch (Exception ex)
            {
                usrModel.Mensaje = ex.Message;
            }
            return Ok(usrModel);

        }

        public IHttpActionResult PutUsuario([FromUri]byte vistaId, [FromBody]Usuario usuario)
        {
            UsuarioModel usrModel = new UsuarioModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaUsuarios = null
            };

            try
            {
                if (!usuario.Equals(null))
                {
                    if (!vistaId.Equals(null))
                    {
                        using (ManejoUsuarios manejoUsuarios = new ManejoUsuarios())
                        {
                            switch (vistaId)
                            {
                                case 1:
                                    if (!manejoUsuarios.ResetPassword(usuario.Usr))
                                    {
                                        usrModel.Mensaje = "No fue posible reiniciar la contraseña del usuario";
                                    }
                                    else
                                    {
                                        usrModel.Succes = true;
                                        usrModel.ListaUsuarios = manejoUsuarios.ObtenerUsuarios();
                                        usrModel.Mensaje = "La contraseña se reinicio de forma correcta";

                                    }
                                    break;
                                case 2:
                                    if (!manejoUsuarios.BajaUsuario(usuario.Usr))
                                    {
                                        usrModel.Mensaje = "No fue posible dar de baja al usuario";
                                    }
                                    else
                                    {
                                        usrModel.Succes = true;
                                        usrModel.ListaUsuarios = manejoUsuarios.ObtenerUsuarios();
                                        usrModel.Mensaje = "La baja se realizó de forma correcta";

                                    }
                                    break;
                                case 3:
                                    if (!manejoUsuarios.CambiarDatosGenerales(usuario))
                                    {
                                        usrModel.Mensaje = "No fue posible actualizar los datos del usuario";
                                    }
                                    else
                                    {
                                        usrModel.Succes = true;
                                        using (ManejoUsuarios manejoUsuariosAux = new ManejoUsuarios())
                                        {
                                            usrModel.ListaUsuarios = manejoUsuariosAux.ObtenerUsuarios();
                                        }
                                        usrModel.Mensaje = "La actualización se realizó de forma correcta";

                                    }
                                    break;
                                case 4:
                                    if (!manejoUsuarios.CambiarPassword(usuario.Usr, usuario.Password))
                                    {
                                        usrModel.Mensaje = "No fue posible asignar la nueva contraseña al usuario";
                                    }
                                    else
                                    {
                                        usrModel.Succes = true;
                                        usrModel.ListaUsuarios = manejoUsuarios.ObtenerUsuarios();
                                        usrModel.Mensaje = "El cambio de contraseña se realizó de forma correcta";

                                    }
                                    break;
                                case 5:
                                    if (!manejoUsuarios.ActivarUsuario(usuario.Usr))
                                    {
                                        usrModel.Mensaje = "No fue posible activar al usuario";
                                    }
                                    else
                                    {
                                        usrModel.Succes = true;
                                        usrModel.ListaUsuarios = manejoUsuarios.ObtenerUsuarios();
                                        usrModel.Mensaje = "La activación se realizó de forma correcta";

                                    }
                                    break;

                                default:
                                    usrModel.Mensaje = "No se encontro el id de la vista de Actualización";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        usrModel.Mensaje = "El parametro de entrada(vista) es nulo";
                    }
                }
                else
                {
                    usrModel.Mensaje = "El parametro de entrada(cliente) es nulo";
                }
            }
            catch (Exception ex)
            {
                usrModel.Mensaje = ex.Message;
            }
            return Ok(usrModel);
        }

        #endregion

    }
}
