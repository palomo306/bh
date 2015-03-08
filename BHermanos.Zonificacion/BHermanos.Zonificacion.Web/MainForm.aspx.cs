using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE = BHermanos.Zonificacion.BusinessEntities;


namespace BHermanos.Zonificacion.Web
{
    public partial class MainForm : System.Web.UI.Page
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

        #region Pintado del Menú
        private void PrintSubMenu(BE.Menu menu, ref string outHtml)
        {
            outHtml += "<li>" + Environment.NewLine;
            if (menu.ListMenus.Count > 0)
            {
                outHtml += @"<a href=""#"">" + menu.Nombre + "</a>" + Environment.NewLine;
                outHtml += "<ul>" + Environment.NewLine;
                foreach (BE.Menu subMenu in menu.ListMenus)
                {
                    PrintSubMenu(subMenu, ref outHtml);
                }
                outHtml += "</ul>" + Environment.NewLine;
            }
            else
                outHtml += @"<a href=""javascript:OpenMenu('" + menu.Aplicacion + @"');"">" + menu.Nombre + "</a>" + Environment.NewLine;
            outHtml += "</li>" + Environment.NewLine;
        }

        private void PrintMenu()
        {
            List<BE.Menu> lstAllMenus = new List<BE.Menu>();
            foreach (BE.Rol rol in CurrentUser.UserRoles)
            {
                lstAllMenus = lstAllMenus.Union(rol.ListMenus, new BE.Comparer.MenuComparer()).ToList();
            }
            string menuHtml = string.Empty;
            foreach (BE.Menu menu in lstAllMenus)
            {
                PrintSubMenu(menu, ref menuHtml);
            }
            MainMenu.InnerHtml = menuHtml;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "PrepareMenu();", true);
            ValidaUser();
            if (!Page.IsPostBack)
            {
                PrintMenu();
            }
        }
        #endregion
    }
}