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
    public class PlazaController : ApiController
    {
        #region Metodos

        public IHttpActionResult GetPlaza([FromUri]byte vistaId, [FromUri]int plazaId)
        {
            PlazaModel zonaModel = new PlazaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaPlazas = null
            };

            try
            {
                using (ManejadorPlazas manejadorPlazas = new ManejadorPlazas())
                {
                    zonaModel.ListaPlazas = manejadorPlazas.ObtenerPlazas();
                    zonaModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                zonaModel.Mensaje = ex.Message;
            }
            return Ok(zonaModel);
        }

        #endregion
    }       
}
