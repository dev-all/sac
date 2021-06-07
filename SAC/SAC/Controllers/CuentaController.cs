﻿using SAC.Models;
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
using SAC.Helpers;

namespace SAC.Controllers
{
    public class CuentaController : BaseController
    {
        private readonly ServicioUsuarios servicioUsuario;
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private AfipHelper afipHelper = new AfipHelper();
        public CuentaController()
        {
            servicioUsuario = new ServicioUsuarios();
            servicioUsuario._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }




        // GET: Cuenta
        public ActionResult Acceder()
        {



            if ((System.Web.HttpContext.Current.Session["currentUser"] != null))
            {
                if ((System.Web.HttpContext.Current.Session["controller"] != null & System.Web.HttpContext.Current.Session["metodo"] != null))
                    return RedirectToAction(System.Web.HttpContext.Current.Session["metodo"].ToString(), System.Web.HttpContext.Current.Session["controller"].ToString());
                else
                    return RedirectToAction("Acceder", "Cuenta");
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

                var moneda = servicioTipoMoneda.GetCotizacionPorIdMoneda(DateTime.Today, 2);
                if (moneda == null)
                {
                    var x = afipHelper.GetCotizacion("DOL");
                    if (x.ResultGet != null)
                    {
                        ValorCotizacionModel valorCotizacionModel = new ValorCotizacionModel();

                        valorCotizacionModel.IdTipoMoneda = 2;
                        valorCotizacionModel.Monto = decimal.Parse(x.ResultGet.MonCotiz.ToString());

                        string str = x.ResultGet.FchCotiz.ToString();
                        int y = int.Parse(str.Substring(0, 4));
                        int m = int.Parse(str.Substring(4, 2));
                        int d = int.Parse(str.Substring(6, 2));
                        valorCotizacionModel.Fecha = new DateTime(y, m, d);
                        valorCotizacionModel.UltimaModificacion = DateTime.Now;
                        valorCotizacionModel.IdUsuario = usuario.IdUsuario;
                        servicioTipoMoneda.updateCotizacionPorIdMoneda(valorCotizacionModel);
                    }



                }

                if (Convert.ToString(loginViewModel.Usuario) == loginViewModel.Password)
                {
                    System.Web.HttpContext.Current.Session["controller"] = null;
                    return RedirectToAction("CambiarPassword", "Cuenta");
                }
                else
                {

                    MenuHelper menu = new MenuHelper();

                    System.Web.HttpContext.Current.Session["menu"] = menu.ObtenerMenuSidebar(usuario.IdUsuario);

                    RolModel rol = servicioUsuario.ObtenerRol(usuario.IdUsuario);

                    if (rol != null)
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
                ViewData.ModelState.AddModelError("Usuario", "Acceso invalido");
                return View("Acceder");
            }



        }

        [HttpGet]
        public ActionResult Logout()
        {

            Session.Remove("username");
            System.Web.HttpContext.Current.Session["currentUser"] = null;
            return RedirectToAction("Acceder");
        }



    }
}