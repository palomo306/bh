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
    public class MunicipioController : ApiController
    {

        #region Metodos

        public IHttpActionResult GetMunicipio([FromUri]int id)
        {
            MunicipioModel municipioModel = new MunicipioModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaMunicipios = null
            };

            try
            {
                using (ManejadorMunicipios manejadorMunicipios = new ManejadorMunicipios())
                {
                    municipioModel.ListaMunicipios = manejadorMunicipios.ObtenerMunicipios(id);
                    municipioModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                municipioModel.Mensaje = ex.Message;
            }
            return Ok(municipioModel);
        }

        #endregion

    }
}
