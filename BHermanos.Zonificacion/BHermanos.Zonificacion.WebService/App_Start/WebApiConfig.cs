using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            config.Formatters.Clear();

            config.Formatters.Add(new XmlMediaTypeFormatter());            
            config.Formatters.Add(new JsonMediaTypeFormatter());

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
               routeTemplate: "WebService/{controller}/GetZona/{vistaId}/{plazaId}/{zonaId}"
            );

            config.Routes.MapHttpRoute(
              name: "PostZona",
              routeTemplate: "WebService/{controller}/PostZona/{zonaId}",
              defaults: new { zonaId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "PutZona",
               routeTemplate: "WebService/{controller}/PutZona/{vistaId}",
               defaults: null
            );

            config.Routes.MapHttpRoute(
                name: "DeleteZona",
                routeTemplate: "WebService/{controller}/DeleteZona/{vistaId}/{plazaId}/{zonaId}"
            );

            //Mapeo de Colonias
            config.Routes.MapHttpRoute(
               name: "GetColonia",
               routeTemplate: "WebService/{controller}/GetColonia/{vistaId}/{plazaId}/{coloniaId}"
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
               routeTemplate: "WebService/{controller}/GetTab/{tabId}/{plazaId}/{fechaInicio}/{fechaFin}",
               defaults: new { tabId = RouteParameter.Optional, plazaId = RouteParameter.Optional, fechaInicio = RouteParameter.Optional, fechaFin = RouteParameter.Optional }
            );

            //Mapeo de Grupo de Plazas
            config.Routes.MapHttpRoute(
               name: "GetPlaza",
               routeTemplate: "WebService/{controller}/GetPlaza/{vistaId}/{plazaId}",
               defaults: null
            );

            config.Routes.MapHttpRoute(
              name: "PostPlaza",
              routeTemplate: "WebService/{controller}/PostPlaza",
              defaults: null
            );

            config.Routes.MapHttpRoute(
               name: "PutPlaza",
               routeTemplate: "WebService/{controller}/PutPlaza",
               defaults: null
            );

            config.Routes.MapHttpRoute(
                name: "DeletePlaza",
                routeTemplate: "WebService/{controller}/DeletePlaza/{plazaId}"
            );

            //Formato del response
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));
            // Cambiar capitalización a las letras
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Ignorar valores nulos
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            // Cambiar formato de fecha
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            // Cambiar la cultura
            config.Formatters.JsonFormatter.SerializerSettings.Culture = new CultureInfo("es-MX");
            //Serializa 
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;

            //Formato del response
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));
            //Serializa 
            config.Formatters.XmlFormatter.UseXmlSerializer = true;             
            

        }
    }
}
