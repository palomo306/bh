using BHermanos.Zonificacion.BusinessEntities.Cast;
using BHermanos.Zonificacion.WebService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE = BHermanos.Zonificacion.BusinessEntities;


namespace BHermanos.Zonificacion.Web
{
    public partial class BHermanosSite : System.Web.UI.MasterPage
    {
        #region WebMethods
        [WebMethod]
        public static void SignOff()
        {
            try
            {
                HttpContext.Current.Session.Clear();
            }
            catch
            {

            }
        }
        #endregion        

        #region Propiedades
        public BE.Usuario CurrentUser
        {
            get
            {
                if (Session["User"] != null)
                    return (BE.Usuario)Session["User"];
                return null;
            }
            set
            {
                Session["User"] = value;
            }
        }
        #endregion

        #region Carga de Datos
        private BE.Menu LoadTabsMenu()
        {
            try
            {
                //Lista de regreso
                BE.Menu tabsMenu = new BE.Menu() { Id = 0, Nombre = "Vistas", Orden = 100, Aplicacion = "", ListMenus = new List<BE.Menu>() };
                //Se cargan los tabs desde el servicio
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                url += "Tab/GetTab?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 20000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                TabModel objResponse = JsonSerializer.Parse<TabModel>(streamReader.ReadToEnd());
                if (objResponse.Succes)
                {
                    int i = 1;
                    foreach (BE.Tab tb in objResponse.ListaTabs.OrderBy(t => t.Nombre))
                    {
                        BE.Menu newTab = new BE.Menu();
                        newTab.Id = i;
                        newTab.Nombre = tb.Nombre;
                        newTab.Orden = i;
                        newTab.Aplicacion = "/Modules/ZoneInfoChild.aspx?tabId=" + tb.Id.ToString();
                        tabsMenu.ListMenus.Add(newTab);
                    }
                }
                return tabsMenu;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Pintado del Menú
        private void PrintSubMenu(BE.Menu menu, ref string outHtml)
        {
            outHtml += @"<section class=""toggle"">" + Environment.NewLine;
            outHtml += @"<label>" + menu.Nombre + "</label>" + Environment.NewLine;
            if (menu.ListMenus.Count > 0)
            {                
                outHtml += @"<div class=""toggle-content"">" + Environment.NewLine;
                foreach (BE.Menu subMenu in menu.ListMenus)
                {
                    outHtml += @"<p class=""MenuOption"" onclick=""javascript:OpenMenu('" + subMenu.Aplicacion + @"');"">" + subMenu.Nombre + "</p>" + Environment.NewLine;
                }
                outHtml += "</div>" + Environment.NewLine;
            }                
            outHtml += "</section>" + Environment.NewLine;
        }

        private void PrintMenu()
        {
            List<BE.Menu> lstAllMenus = new List<BE.Menu>();
            foreach (BE.Rol rol in CurrentUser.UserRoles)
            {
                lstAllMenus = lstAllMenus.Union(rol.ListMenus, new BE.Comparer.MenuComparer()).ToList();
            }
            //Se agrega el menu de los Tabs
            lstAllMenus.Add(LoadTabsMenu());
            string menuHtml = string.Empty;
            foreach (BE.Menu menu in lstAllMenus)
            {
                PrintSubMenu(menu, ref menuHtml);
            }
            mainMenu.InnerHtml = menuHtml;
        }
        #endregion

        #region Carga de la Página
        private void ValidaUser()
        {
            if (this.CurrentUser == null)
                Response.Redirect("~/Login/Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidaUser();            
            UpdatePanel1.Attributes.CssStyle.Add("height", "100%");
            if (!Page.IsPostBack)
            {
                PrintMenu();
            }
        }
        #endregion
    }
}