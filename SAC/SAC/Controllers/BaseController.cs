using System;
using System.Web.Mvc;
using Negocio.Modelos;
using Entidad.Modelos;
using System.Collections.Generic;
using System.Globalization;
using Negocio.Servicios;

namespace SAC.Controllers
{
    public class BaseController : Controller
    {
       
        public BaseController()
        {                      
            UsuarioModel datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            if (datosUsuario != null )
            {
                ViewBag.UserCompleteName = datosUsuario.UserName;
                ViewBag.Metodo = System.Web.HttpContext.Current.Session["metodo"] ?? "Index";
                ViewBag.Controller = System.Web.HttpContext.Current.Session["controller"] ?? "Home";                
                ViewBag.Menu = (ICollection<MenuSideBarModel>)System.Web.HttpContext.Current.Session["menu"];              
            }
            else
            {
                RedirectToAction("Acceder","Cuenta");
            }        
        }
        [NonAction]
        public void CrearTempData(string msg_, string tipo_)
        {
            TempData[tipo_] = msg_;
        }
    }
}