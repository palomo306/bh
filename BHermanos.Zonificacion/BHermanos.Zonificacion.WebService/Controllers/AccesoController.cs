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
    public class AccesoController : ApiController
    {


        #region Metodos

        public IHttpActionResult GetAcceso(string usuario, string contrasena, byte aplicacionId)
        {
            AccesoModel acceso = new AccesoModel()
            {
                Accesa = false,
                Mensaje = string.Empty,
                DatosUsuario = null
            };
            try
            {
                using (ManejoAcceso login = new ManejoAcceso())
                {
                    if (login.Autenticar(usuario, contrasena, aplicacionId))
                    {
                        acceso.Accesa = true;
                        acceso.DatosUsuario = login.UsuarioEncontrado;
                    }
                }
            }
            catch (Exception ex)
            {
                acceso.Mensaje = ex.Message;
            }
            return Ok(acceso);
        }

        #endregion Metodos
    
    }
}
