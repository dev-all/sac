using System;
using System.Web.Mvc;
using Negocio.Modelos;
using Entidad.Modelos;
using System.Collections.Generic;
using System.Globalization;

namespace SAC.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            UsuarioModel datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

            if (datosUsuario != null)
            {
                ViewBag.UserCompleteName = datosUsuario.Nombre + " " + datosUsuario.Apellido;
                ViewBag.Metodo = System.Web.HttpContext.Current.Session["metodo"] ?? "Index";
                ViewBag.Controller = System.Web.HttpContext.Current.Session["controller"] ?? "Home";
                // ViewBag.Menu = (List<MenuItemModel>)System.Web.HttpContext.Current.Session["menu"];
                ViewBag.Menu = (ICollection<MenuSideBarModel>)System.Web.HttpContext.Current.Session["menu"];

            }
            else
            {
                RedirectToAction("Acceder","Cuenta");
            }

        }
    }
}