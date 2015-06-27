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
    public class ZonaController : ApiController
    {
        #region Metodos

        public IHttpActionResult GetZona([FromUri]byte vistaId, [FromUri]int plazaId, [FromUri] int zonaId)
        {
            ZonaModel zonaModel = new ZonaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaZonas = null
            };

            try
            {
                using (ManejadorZonas manejadorZonas = new ManejadorZonas())
                {
                    zonaModel.ListaZonas = manejadorZonas.ObtenerZonas(vistaId, plazaId, zonaId);
                    zonaModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                zonaModel.Mensaje = ex.Message;
            }
            return Ok(zonaModel);
        }

        public IHttpActionResult PostZona([FromBody]Zona zona)
        {
            ZonaModel zonaModel = new ZonaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaZonas = null
            };

            try
            {
                if (zona != null)
                {
                    using (ManejadorZonas manejadorZonas = new ManejadorZonas())
                    {
                        if (!manejadorZonas.InsertaZona(zona))
                        {
                            zonaModel.Mensaje = "No fue posible dar de alta el zona";
                        }
                        else
                        {
                            zonaModel.Succes = true;
                            zonaModel.Mensaje = "La zona se dio de alta correctamente";
                            zonaModel.Succes = true;
                        }
                    }
                }
                else
                {
                    zonaModel.Mensaje = "El parametro de entrada(zona) es nulo";
                }
            }
            catch (Exception ex)
            {
                zonaModel.Mensaje = ex.Message;
            }
            return Ok(zonaModel);
        }

        public IHttpActionResult PostZona([FromBody]Zona zona, [FromUri] int zonaId)
        {
            ZonaModel zonaModel = new ZonaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaZonas = null
            };

            try
            {
                if (zona != null)
                {
                    using (ManejadorZonas manejadorZonas = new ManejadorZonas())
                    {
                        if (!manejadorZonas.InsertaSubZona(zona, zonaId))
                        {
                            zonaModel.Mensaje = "No fue posible dar de alta la sub zona";
                        }
                        else
                        {
                            zonaModel.Mensaje = "La subzona se dio de alta correctamente";
                            zonaModel.Succes = true;
                        }
                    }
                }
                else
                {
                    zonaModel.Mensaje = "El parametro de entrada(zona) es nulo";
                }
            }
            catch (Exception ex)
            {
                zonaModel.Mensaje = ex.Message;
            }
            return Ok(zonaModel);
        }

        public IHttpActionResult PutZona([FromUri] byte vistaId, [FromBody]Zona zona)
        {
            ZonaModel zonaModel = new ZonaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaZonas = null
            };

            try
            {
                if (zona != null)
                {
                    if (!vistaId.Equals(null))
                    {
                        using (ManejadorZonas manejadorZonas = new ManejadorZonas())
                        {
                            if (vistaId == 1)
                            {
                                if (!manejadorZonas.ActualizaZona(zona))
                                {
                                    zonaModel.Mensaje = "No fue posible realizar la actualización de la zona";
                                }
                                else
                                {
                                    zonaModel.Succes = true;
                                    zonaModel.Mensaje = "La actualización de la zona se realizó correctamente";
                                }
                            }
                            else if (vistaId == 2)
                            {
                                if (!manejadorZonas.ActualizaSubZona(zona))
                                {
                                    zonaModel.Mensaje = "No fue posible realizar la actualización de la subzona";
                                }
                                else
                                {
                                    zonaModel.Succes = true;
                                    zonaModel.Mensaje = "La actualización de la subzona se realizó correctamente";
                                }
                            }
                        }
                    }
                    else
                    {
                        zonaModel.Mensaje = "El parametro de entrada(vista) es nulo";
                    }
                }
                else
                {
                    zonaModel.Mensaje = "El parametro de entrada(zona) es nulo";
                }
            }
            catch (Exception ex)
            {
                zonaModel.Mensaje = ex.Message;
            }
            return Ok(zonaModel);
        }

        public IHttpActionResult DeleteZona([FromUri] byte vistaId, [FromUri]int plazaId,  [FromUri]int zonaId)
        {
            ZonaModel zonaModel = new ZonaModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaZonas = null
            };

            try
            {
                if (!vistaId.Equals(null))
                {
                    using (ManejadorZonas manejadorZonas = new ManejadorZonas())
                    {
                        if (vistaId == 1)
                        {
                            if (!manejadorZonas.EliminarZona(plazaId, zonaId))
                            {
                                zonaModel.Mensaje = "No fue posible eliminar de la zona";
                            }
                            else
                            {
                                zonaModel.Succes = true;
                                zonaModel.Mensaje = "La zona se eliminó correctamente";
                            }
                        }
                        else if (vistaId == 2)
                        {
                            if (!manejadorZonas.EliminarSubZona(plazaId, zonaId))
                            {
                                zonaModel.Mensaje = "No fue posible eliminar de la subzona";
                            }
                            else
                            {
                                zonaModel.Succes = true;
                                zonaModel.Mensaje = "La subzona se eliminó correctamente";
                            }
                        }
                    }
                }
                else
                {
                    zonaModel.Mensaje = "El parametro de entrada(vista) es nulo";
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
