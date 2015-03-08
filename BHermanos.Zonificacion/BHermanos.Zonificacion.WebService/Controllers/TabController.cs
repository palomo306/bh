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
    public class TabController : ApiController
    {
        #region Metodos

        public IHttpActionResult GetTab()
        {
            TabModel tabModel = new TabModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaTabs = null
            };

            try
            {
                using (ManejadorTabs manejoTabs = new ManejadorTabs())
                {
                    tabModel.ListaTabs = manejoTabs.ObtenerTabs();
                    tabModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                tabModel.Mensaje = ex.Message;
            }
            return Ok(tabModel);
        }

        public IHttpActionResult GetTab([FromUri]int tabId, [FromUri]int estadoId, [FromUri]int municipioId)
        {
            TabModel tabModel = new TabModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaTabs = null
            };

            try
            {
                using (ManejadorTabs manejoTabs = new ManejadorTabs())
                {
                    tabModel.ListaTabs = manejoTabs.ObtenerTab(tabId, estadoId, municipioId);
                    tabModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                tabModel.Mensaje = ex.Message;
            }
            return Ok(tabModel);
        }

        #endregion
    }
}
