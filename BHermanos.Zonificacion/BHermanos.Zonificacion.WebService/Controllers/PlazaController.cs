using BHermanos.Zonificacion.BusinessEntities;
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
            PlazaModel plazaModel = new PlazaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaPlazas = null
            };

            try
            {
                using (ManejadorPlazas manejadorPlazas = new ManejadorPlazas())
                {
                    switch (vistaId)
                    {
                        case 1:
                            plazaModel.ListaPlazas = manejadorPlazas.ObtenerPlazas();
                            plazaModel.Succes = true;
                            break;
                        case 2:
                            plazaModel.ListaPlazas = manejadorPlazas.ObtenerPlaza(plazaId);
                            plazaModel.Succes = true;
                            break;
                        default:
                            new ApplicationException("El id de la vista no existe");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                plazaModel.Mensaje = ex.Message;
            }
            return Ok(plazaModel);
        }

        public IHttpActionResult PostPlaza([FromBody]Plaza plaza)
        {
            PlazaModel plazaModel = new PlazaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaPlazas = null
            };

            try
            {
                if (plaza != null)
                {
                    using (ManejadorPlazas manejadorPlaza = new ManejadorPlazas())
                    {
                        if (!manejadorPlaza.InsertaPlaza(plaza))
                        {
                            plazaModel.Mensaje = "No fue posible dar de alta la plaza";
                        }
                        else
                        {
                            plazaModel.Mensaje = "La plaza se dio de alta correctamente";
                            plazaModel.Succes = true;
                        }
                    }
                }
                else
                {
                    plazaModel.Mensaje = "El parametro de entrada(plaza) es nulo";
                }
            }
            catch (Exception ex)
            {
                plazaModel.Mensaje = ex.Message;
            }
            return Ok(plazaModel);
        }

        public IHttpActionResult DeletePlaza([FromUri]int plazaId)
        {
            PlazaModel plazaModel = new PlazaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaPlazas = null
            };

            try
            {
                using (ManejadorPlazas manejadorPlazas = new ManejadorPlazas())
                {
                    if (!manejadorPlazas.EliminarPlaza(plazaId))
                    {
                        plazaModel.Mensaje = "No fue posible eliminar de la plaza";
                    }
                    else
                    {
                        plazaModel.Succes = true;
                        plazaModel.Mensaje = "La plaza se eliminó correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                plazaModel.Mensaje = ex.Message;
            }
            return Ok(plazaModel);
        }

        public IHttpActionResult PutPlaza([FromBody]Plaza plaza)
        {
            PlazaModel plazaModel = new PlazaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaPlazas = null
            };

            try
            {
                if (plaza != null)
                {
                    using (ManejadorPlazas manejadorPlazas = new ManejadorPlazas())
                    {
                        if (!manejadorPlazas.ActualizaPlaza(plaza))
                        {
                            plazaModel.Mensaje = "No fue posible realizar la actualización de la plaza";
                        }
                        else
                        {
                            plazaModel.Succes = true;
                            plazaModel.Mensaje = "La actualización de la plaza se realizó correctamente";
                        }
                    }
                }
                else
                {
                    plazaModel.Mensaje = "El parametro de entrada(plaza) es nulo";
                }
            }
            catch (Exception ex)
            {
                plazaModel.Mensaje = ex.Message;
            }
            return Ok(plazaModel);
        }

        #endregion
    }
}