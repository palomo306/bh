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
    public class RolController : ApiController
    {

        #region Metodos

        public IHttpActionResult GetRol()
        {
            RolModel rolModel = new RolModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaRoles = null
            };

            try
            {
                using (ManejoRoles manejoRoles = new ManejoRoles())
                {
                    rolModel.ListaRoles = manejoRoles.ObtenerRoles();
                    rolModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                rolModel.Mensaje = ex.Message;
            }
            return Ok(rolModel);
        }

        public IHttpActionResult GetRol([FromUri]int id)
        {
            RolModel rolModel = new RolModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaRoles = null
            };

            try
            {
                using (ManejoRoles manejoRoles = new ManejoRoles())
                {
                    rolModel.ListaRoles = manejoRoles.ObtenerRol(id);
                    rolModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                rolModel.Mensaje = ex.Message;
            }
            return Ok(rolModel);
        }

        public IHttpActionResult PostRol([FromBody]Rol rol)
        {
            RolModel rolModel = new RolModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaRoles = null
            };

            try
            {
                if (rol != null)
                {
                    using (ManejoRoles manejoRoles = new ManejoRoles())
                    {
                        if (!manejoRoles.AltaRol(rol))
                        {
                            rolModel.Mensaje = "No fue posible dar de alta el rol";
                        }
                        else
                        {
                            rolModel.Succes = true;
                            rolModel.Mensaje = "El rol se dio de alta correctamente";

                        }
                    }
                }
                else
                {
                    rolModel.Mensaje = "El parametro de entrada(rol) es nulo";
                }
            }
            catch (Exception ex)
            {
                rolModel.Mensaje = ex.Message;
            }
            return Ok(rolModel);

        }

        public IHttpActionResult PutRol([FromUri] byte vistaId, [FromBody]Rol rol)
        {
            RolModel rolModel = new RolModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaRoles = null
            };

            try
            {
                if (rol != null)
                {

                    using (ManejoRoles manejoRoles = new ManejoRoles())
                    {
                        switch (vistaId)
                        {
                            case 1:
                                if (!manejoRoles.BajaRol(rol.Id))
                                {
                                    rolModel.Mensaje = "No fue posible dar de baja el rol";
                                }
                                else
                                {
                                    rolModel.Succes = true;
                                    rolModel.Mensaje = "La baja se realizó de forma correcta";

                                }
                                break;

                            default:
                                rolModel.Mensaje = "No se encontro el id de la vista de Actualización";
                                break;
                        }
                    }

                }
                else
                {
                    rolModel.Mensaje = "El parametro de entrada(rol) es nulo";
                }
            }
            catch (Exception ex)
            {
                rolModel.Mensaje = ex.Message;
            }
            return Ok(rolModel);
        }

        #endregion

    }
}
