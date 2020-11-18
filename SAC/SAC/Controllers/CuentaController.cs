using SAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Modelos;
using Negocio.Interfaces;
using Negocio.Servicios;
using Negocio.Helpers;
using System.Configuration;
using AutoMapper;
using Entidad.Modelos;

namespace SAC.Controllers
{
    public class CuentaController : BaseController
    {
       private readonly ServicioUsuarios servicioUsuario;
        public CuentaController()
        {
            servicioUsuario = new ServicioUsuarios();
            servicioUsuario._mensaje += (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }

        [NonAction]
        private void CrearTempData(string msg_, string tipo_)
        {
            TempData[tipo_] = msg_;
        }


        // GET: Cuenta
        public ActionResult Acceder()
        {
            if ((System.Web.HttpContext.Current.Session["currentUser"] != null))
            {
                if ((System.Web.HttpContext.Current.Session["controller"] != null & System.Web.HttpContext.Current.Session["metodo"] != null))
                    return RedirectToAction(System.Web.HttpContext.Current.Session["metodo"].ToString(), System.Web.HttpContext.Current.Session["controller"].ToString());
                else
                    return RedirectToAction("Index", "Evento");
            }

            var viewModel = new LoginViewModel();
            return View(viewModel);
        }


        [HttpPost, ActionName("Acceder")]
        [ValidateAntiForgeryToken]
        public ActionResult Acceder(LoginViewModel loginViewModel)
        {



            if (servicioUsuario.Obtener(loginViewModel.Usuario, loginViewModel.Password, Convert.ToInt32(ConfigurationManager.AppSettings["rolInvitado"])))
            {

                UsuarioModel usuario = servicioUsuario.ObtenerUsuario(loginViewModel.Usuario, 1);
                System.Web.HttpContext.Current.Session["currentUser"] = usuario;

                if(Convert.ToString(loginViewModel.Usuario) == loginViewModel.Password)
                    {
                    System.Web.HttpContext.Current.Session["controller"] = null ;
                    return RedirectToAction("CambiarPassword", "Cuenta");
                }
                else
                {
                    //string ip = Request.UserHostAddress;
                    //servicioUsuario.LogLogin(usuario.IdUsuario, ip);

                     MenuHelper menu = new MenuHelper();
                    //System.Web.HttpContext.Current.Session["menu"] = menu.ObtenerMenu(usuario.IdUsuario);
                    System.Web.HttpContext.Current.Session["menu"] = menu.ObtenerMenuSidebar(usuario.IdUsuario);
                    //falta hacer el mapeo
                    //var sb = Mapper.Map<List<MenuSideBarModel>, List<MenuItemModel>>(menu.ObtenerMenuSidebar(usuario.IdUsuario));
                    //System.Web.HttpContext.Current.Session["menu"] = sb;

                    RolModel rol = servicioUsuario.ObtenerRol(usuario.IdUsuario);

                    if(rol != null)
                    {
                        System.Web.HttpContext.Current.Session["controller"] = rol.Controller;
                        System.Web.HttpContext.Current.Session["metodo"] = rol.Metodo;
                        if (rol.Controller != null && rol.Metodo != null)
                        {
                            return RedirectToAction(rol.Metodo, rol.Controller);
                        }                        
                    }
                    return RedirectToAction("Index", "Home");
                }                
            }
            else
            {
                ViewBag.error = "Acceso invalido";
                return View("Acceder");
            }



        }
     
        [HttpGet]
        public ActionResult Logout()
        {

            Session.Remove("username");
            System.Web.HttpContext.Current.Session["currentUser"] = null ;
            return RedirectToAction("Acceder");
        }
       

    }
}