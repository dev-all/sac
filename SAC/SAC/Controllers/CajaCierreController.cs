using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Atributos;
using SAC.Models;
using AutoMapper;
using Negocio.Modelos;

namespace SAC.Controllers
{
    public class CajaCierreController : BaseController
    {

        private ServicioCaja servicioCaja = new ServicioCaja();

        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
        private ServicioCajaSaldo servicioCajaSaldo = new ServicioCajaSaldo();


        public CajaCierreController()
        {            
            servicioCaja._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }

        public ActionResult Index(int id = 0)
        {           
            CajaModelView  model = new CajaModelView();
            model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.GetAllCajaPorIdCierre(id));
            model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());
            model.FechaCierre = DateTime.Now; //.ToString("dd/MM/yyyy");
            CargarCierreCaja();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CajaModelView model)
        {

            {
                try
                {


                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario = OUsuario.IdUsuario;
                    model.IdTipoMovimiento = 2;
                    model.IdCajaSaldo = 0;

                    servicioCaja.GuardarCaja(Mapper.Map<CajaModelView, CajaModel>(model));

                    return RedirectToAction(nameof(Index));


                }
                catch (Exception)
                {
                    return View(model);
                }
            }


        }





        public ActionResult AddOrEdit(int id = 0)
        {

          //  CargarCajaGrupo();

            CajaModelView model;
            if (id == 0)
            {
                model = new CajaModelView();
            }
            else
            {
                model = Mapper.Map<CajaModel, CajaModelView>(servicioCaja.GetCajaPorId(id));
                
            }

           // model.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]         
        public ActionResult AddOrEdit(CajaModelView model)
        {
            try
            {        
            if (ModelState.IsValid)
            {

                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario= OUsuario.IdUsuario;
                    //servicioCaja._mensaje("","ok");
               if (model.Id <= 0)
                {
                   servicioCaja.GuardarCaja(Mapper.Map<CajaModelView, CajaModel>(model));
                }
                else
                {
                    servicioCaja.ActualizarCaja(Mapper.Map<CajaModelView, CajaModel>(model));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);

            }
            catch (Exception)
            {
                return View(model);
            }
        }

      
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                servicioCaja.Eliminar(id);
               
            }
            catch (Exception ex)
            {
                servicioCaja._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }

           



        //combo cajagrupo
        //public void CargarCajaGrupo()
        //{
        //    List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
        //    List<SelectListItem> retornoListaCajaGrupo = null;
        //    retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
        //    {
        //        Value = x.Id.ToString(),
        //        Text = x.Codigo
        //    })).ToList();
        //    retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "--Seleccione Codigo de Caja--", Value = "" });
        //    ViewBag.Listapagina = retornoListaCajaGrupo;
        //}




        public void CargarCierreCaja()
        {


            List<CajaSaldoModelView> ListaCajaGrupo = Mapper.Map<List<CajaSaldoModel>, List<CajaSaldoModelView>>(servicioCajaSaldo.GetAllCajaSaldo());
            List<SelectListItem> retornoListaCajaCierre = null;
            retornoListaCajaCierre = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = "Nro: " + x.Id.ToString() + "- Fecha: " +  x.Fecha.ToString("dd/MM/yyyy")
            })).ToList();
            retornoListaCajaCierre.Insert(0, new SelectListItem { Text = "--Cierre Actual --", Value = "0" });
            ViewBag.Listapagina = retornoListaCajaCierre;



          
        }



    }




}



