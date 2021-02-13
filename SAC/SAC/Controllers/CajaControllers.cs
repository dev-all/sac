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
using System.Globalization;

namespace SAC.Controllers
{
    public class CajaController : BaseController
    {

        private ServicioCaja servicioCaja = new ServicioCaja();

        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
        private ServicioCajaSaldo servicioCajaSaldo = new ServicioCajaSaldo();

        private ServicioTarjeta oservicioTarjeta = new ServicioTarjeta();

        private ServicioCheque oservicioCheque = new ServicioCheque();

        private ServicioTarjetaOperacion oservicioTarjetaOperacion = new ServicioTarjetaOperacion();



        public CajaController()
        {
            servicioCaja._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }


        public ActionResult Index()
        {


            CajaModelView model = new CajaModelView();
            model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.GetAllCaja());
            model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());
            CargarCajaGrupo();
            CargarTarjetas();
            CargarCheques();

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

            CargarCajaGrupo();

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
                    model.IdUsuario = OUsuario.IdUsuario;
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
        public void CargarCajaGrupo()
        {
            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Codigo
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar Grupo", Value = "" });
            ViewBag.Listapagina = retornoListaCajaGrupo;
        }



        public ActionResult ConsultaCaja(int CIdGrupoCaja = 0, String searchFechaDesde = null, String searchFechaHasta = null)
        {
            CajaModelView model = new CajaModelView();
            DateTime fechaDesde = DateTime.Now;
            DateTime fechaHasta = DateTime.Now;
            if (CIdGrupoCaja != 0)
            {

                if (!string.IsNullOrEmpty(searchFechaDesde))
                {
                    fechaDesde = DateTime.ParseExact(searchFechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                if (!string.IsNullOrEmpty(searchFechaHasta))
                {
                    fechaHasta = DateTime.ParseExact(searchFechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                model.GrupoCaja = Mapper.Map<CajaGrupoModel, CajaGrupoModelView>(servicioCajaGrupo.GetGrupoCajaPorId(CIdGrupoCaja));
                model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.getGrupoCajaFecha(CIdGrupoCaja, fechaDesde, fechaHasta));

                model.CajaDesde = Mapper.Map<CajaModel, CajaModelView>(servicioCaja.GetCajaPorFecha(CIdGrupoCaja,fechaDesde)) ?? new CajaModelView();
              
                model.CajaHasta = Mapper.Map<CajaModel, CajaModelView>(servicioCaja.GetCajaPorFecha(CIdGrupoCaja,fechaHasta)) ?? new CajaModelView();
                model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());

            }

            model.cFechaDesde = fechaDesde;
            model.cFechaHasta = fechaHasta;


            CargarCajaGrupo();

            return View(model);
        }



        public ActionResult ConsultaPorGrupo()
        {


            CajaModelView model = new CajaModelView();
            model.ListaCaja = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.GetAllCaja());
            model.CajaSaldoInicial = Mapper.Map<CajaSaldoModel, CajaSaldoModelView>(servicioCajaSaldo.GetUltimoCierre());           
            model.cFechaDesde = DateTime.Now;
            model.cFechaHasta = DateTime.Now;
            CargarCajaGrupo();

            // return View();
            return View(model);



        }

        private void CargarTarjetas()
        {

            List<TarjetaModelView> ListaCajaGrupo = Mapper.Map<List<TarjetaModel>, List<TarjetaModelView>>(oservicioTarjeta.GetAllTarjetas());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descripcion
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Tarjeta", Value = "" });
            ViewBag.ListaTarjeta = retornoListaCajaGrupo;



        }


        private void CargarCheques()
            {

            List<ChequeModelView> ListaCajaGrupo = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oservicioCheque.GetAllCheque());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.NumeroCheque.ToString ()
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Cheque", Value = "" });
            ViewBag.ListaCheque = retornoListaCajaGrupo;

        }

    }






}



