using BHermanos.Zonificacion.BusinessMaps;
using BHermanos.Zonificacion.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BHermanos.Zonificacion.WebService.Controllers
{
    public class EstadoController : ApiController
    {

        #region Metodos

        public IHttpActionResult GetEstado()
        {
            EstadoModel rolModel = new EstadoModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaEstados = null
            };

            try
            {
                using (ManejadorEstados manejadorEstados = new ManejadorEstados())
                {
                    rolModel.ListaEstados = manejadorEstados.ObtenerEstados();
                    rolModel.Succes = true;
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
