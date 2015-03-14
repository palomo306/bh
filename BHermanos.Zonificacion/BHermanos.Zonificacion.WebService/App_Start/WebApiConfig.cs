using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BHermanos.Zonificacion.WebService
{
    public static class WebServiceConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //Mapeo de Acceso
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetAcceso",
                routeTemplate: "WebService/{controller}/GetAcceso/{usuario}/{contrasena}/{aplicacionId}",
                defaults: new { usuario = RouteParameter.Optional, contrasena = RouteParameter.Optional, aplicacionId = RouteParameter.Optional }
            );

            //Mapeo de Usuarios
            config.Routes.MapHttpRoute(
                name: "PostUsuario",
                routeTemplate: "WebService/{controller}/PostUsuario"
            );

            config.Routes.MapHttpRoute(
                name: "PutUsuario",
                routeTemplate: "WebService/{controller}/PutUsuario/{vistaId}"
            );

            config.Routes.MapHttpRoute(
               name: "GetUsuario",
               routeTemplate: "WebService/{controller}/GetUsuario/{usuario}",
               defaults: new { usuario = RouteParameter.Optional }
            );

            //Mapeo de Roles
            config.Routes.MapHttpRoute(
                name: "PostRol",
                routeTemplate: "WebService/{controller}/PostRol"
            );

            config.Routes.MapHttpRoute(
                name: "PutRol",
                routeTemplate: "WebService/{controller}/PutRol/{vistaId}"
            );

            config.Routes.MapHttpRoute(
               name: "GetRol",
               routeTemplate: "WebService/{controller}/GetRol/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            //Mapeo de Estados            
            config.Routes.MapHttpRoute(
               name: "GetEstado",
               routeTemplate: "WebService/{controller}/GetEstado/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            //Mapeo de Municipios
            config.Routes.MapHttpRoute(
               name: "GetMunicipio",
               routeTemplate: "WebService/{controller}/GetMunicipio/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            //Mapeo de Zonas
            config.Routes.MapHttpRoute(
               name: "GetZona",
               routeTemplate: "WebService/{controller}/GetZona/{vistaId}/{estadoId}/{municipioId}/{zonaId}"
            );

            config.Routes.MapHttpRoute(
              name: "PostZona",
              routeTemplate: "WebService/{controller}/PostZona/{zonaId}",
              defaults: new { zonaId = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "PutZona",
               routeTemplate: "WebService/{controller}/PutZona/{vistaId}",
               defaults: new { vistaId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DeleteZona",
                routeTemplate: "WebService/{controller}/DeleteZona/{vistaId}/{estadoId}/{municipioId}/{zonaId}"
            );

            //Mapeo de Colonias
            config.Routes.MapHttpRoute(
               name: "GetColonia",
               routeTemplate: "WebService/{controller}/GetColonia/{vistaId}/{estadoId}/{municipioId}/{coloniaId}"
            );

            //Mapeo de Grupo de Rubros
            config.Routes.MapHttpRoute(
               name: "GetGrupoRubros",
               routeTemplate: "WebService/{controller}/GetGrupoRubros/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "PutGrupoRubros",
               routeTemplate: "WebService/{controller}/PutGrupoRubros"
               );

            //Mapeo de Grupo de Tabs
            config.Routes.MapHttpRoute(
               name: "GetTab",
               routeTemplate: "WebService/{controller}/GetTab/{tabId}/{estadoId}/{municipioId}",
               defaults: new { tabId = RouteParameter.Optional, estadoId = RouteParameter.Optional, municipioId = RouteParameter.Optional }
            );

            //Mapeo de Grupo de Plazas
            config.Routes.MapHttpRoute(
               name: "GetPlaza",
               routeTemplate: "WebService/{controller}/GetPlaza/{vistaId}/{plazaId}",
               defaults: new { vistaId = RouteParameter.Optional, plazaId = RouteParameter.Optional }
            );
            
            //Formato del response
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));
            //config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
