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
    public class ColoniaController : ApiController
    {
        #region Metodos

        public IHttpActionResult GetColonia([FromUri]byte vistaId, [FromUri]int plazaId, [FromUri]double coloniaId)
        {
            ColoniaModel coloniaModel = new ColoniaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaColonias = null
            };

            try
            {
                using (ManejadorColonias manejadorColonias = new ManejadorColonias())
                {
                    coloniaModel.ListaColonias = manejadorColonias.ObtenerColonias(vistaId, plazaId,coloniaId);
                    coloniaModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                coloniaModel.Mensaje = ex.Message;
            }
            return Ok(coloniaModel);
        }

        #endregion

    }
}
