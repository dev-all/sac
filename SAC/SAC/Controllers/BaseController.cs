using System;
using System.Web.Mvc;
using Negocio.Modelos;
using Entidad.Modelos;
using System.Collections.Generic;
using System.Globalization;
using Negocio.Servicios;
using System.Threading;

namespace SAC.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

          //  CultureInfo cultureInfo = CultureInfo.GetCultureInfo("ar");
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;


            //set CurrentUICulture
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
            //CultureInfo newUICulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = newUICulture;

            //myCIclone.DateTimeFormat.AMDesignator = "a.m.";
            //myCIclone.DateTimeFormat.DateSeparator = "-";
            //myCIclone.NumberFormat.CurrencySymbol = "USD";
            //myCIclone.NumberFormat.NumberDecimalDigits = 4;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es");
            CultureInfo newUICulture = new CultureInfo("es-AR");
            DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo
            {
                TimeSeparator = "/"
                
            };
            NumberFormatInfo formato = new NumberFormatInfo
                                                {
                                                    NumberDecimalSeparator = ".",
                                                    NumberGroupSeparator = ","
                                                };

            newUICulture.NumberFormat = formato;
            Thread.CurrentThread.CurrentCulture = newUICulture;
            Thread.CurrentThread.CurrentUICulture = newUICulture;
            
         

        }

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

            //CultureInfo current = CultureInfo.CurrentUICulture;
            //CultureInfo newUICulture = new CultureInfo("en-US");
            //NumberFormatInfo formato = newUICulture.NumberFormat;
            //formato.NumberDecimalSeparator = ".";
            //CultureInfo.CurrentUICulture = newUICulture;
        }
        [NonAction]
        public void CrearTempData(string msg_, string tipo_)
        {
            TempData[tipo_] = msg_;
        }
    }
}