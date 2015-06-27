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
    public class GrupoRubrosController : ApiController
    {

        #region Metodos

        public IHttpActionResult GetGrupoRubros()
        {
            GrupoRubrosModel grupoRubrosModel = new GrupoRubrosModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaGrupoRubros = null
            };

            try
            {
                using (ManejadorGrupoRubros manejoGrupoRubros = new ManejadorGrupoRubros())
                {
                    grupoRubrosModel.ListaGrupoRubros = manejoGrupoRubros.ObtenerGrupoDeRubros();
                    grupoRubrosModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                grupoRubrosModel.Mensaje = ex.Message;
            }
            return Ok(grupoRubrosModel);
        }

        public IHttpActionResult GetGrupoRubros([FromUri] int id)
        {
            GrupoRubrosModel grupoRubrosModel = new GrupoRubrosModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaGrupoRubros = null
            };

            try
            {
                using (ManejadorGrupoRubros manejoGrupoRubros = new ManejadorGrupoRubros())
                {
                    grupoRubrosModel.ListaGrupoRubros = manejoGrupoRubros.ObtenerGrupoDeRubros(id);
                    grupoRubrosModel.Succes = true;
                }
            }
            catch (Exception ex)
            {
                grupoRubrosModel.Mensaje = ex.Message;
            }
            return Ok(grupoRubrosModel);
        }

        public IHttpActionResult PutGrupoRubros([FromBody]GrupoRubros grupo)
        {
            GrupoRubrosModel grupoRubrosModel = new GrupoRubrosModel()
            {
                Succes = false,
                Mensaje = string.Empty,
                ListaGrupoRubros = null
            };

            try
            {
                if (!grupo.Equals(null))
                {
                    using (ManejadorGrupoRubros manejadorGrupoRubros = new ManejadorGrupoRubros())
                    {
                        if (!manejadorGrupoRubros.ActualizaGrupoRubros(grupo))
                        {
                            grupoRubrosModel.Mensaje = "No fue posible actualizar el grupo de rubros";
                        }
                        else
                        {
                            grupoRubrosModel.Succes = true;
                            grupoRubrosModel.Mensaje = "La actualización del grupo de rubro se realizó de forma correcta";
                        }
                    }
                }
                else
                {
                    grupoRubrosModel.Mensaje = "El parametro de entrada(grupo) es nulo";
                }
            }
            catch (Exception ex)
            {
                grupoRubrosModel.Mensaje = ex.Message;
            }
            return Ok(grupoRubrosModel);
        }

        #endregion

    }
}
