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

        private ServicioTarjetaOperacion oservicioTarjetaOperacion = new ServicioTarjetaOperacion();

        private ServicioChequera oServicioChequera = new ServicioChequera();
        private ServicioCheque oServicioCheque = new ServicioCheque();
        private ServicioBancoCuenta oServicioCuentaBancaria = new ServicioBancoCuenta();
        private ServicioTipoMoneda oServicioTipoMoneda = new ServicioTipoMoneda();
       
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


            //---------para el PartialView cheques terceros
            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());
            model.ListaChequesTerceros = ListaChequesTerceros;
            //--------PartialView cheques propios  
            model.oChequera = new ChequeraModelView();
            List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(oServicioChequera.GetAllChequera());
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());
            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(oServicioTipoMoneda.GetAllTipoMonedas());
            List<SelectListItem> retornoListaCuentaBancaria = (ListaCuentaBancaria.Select(x =>
                                        new SelectListItem()
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Banco.Nombre + " " + x.BancoDescripcion
                                        })).ToList();

            List<SelectListItem> retornoListaTipoMoneda = (ListaTipoMoneda.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.Descripcion
                                         })).ToList();

            model.ListaChequesPropios = new List<ChequeraModelView>(); //ListaChequesPropios; 

            // segun la cuenta bancaria selecccionada se obtiene el numero de cheque  hacerlo via json
            model.listaCuentaBancariaDrop = retornoListaCuentaBancaria;
            ViewBag.listaCuentaBancariaDrop = retornoListaCuentaBancaria;
            model.ListaTipoMonedaDrop = retornoListaTipoMoneda;

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
                    model.ImporteCheque = model.montoChequesSeleccionados;    
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


        //--------------cheques
        [HttpPost]
        public ActionResult CargarChequesPropios()
        {
            //cheques propios          
            List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(oServicioChequera.GetAllChequera());
            //Listo cuentas bancarias           
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());

            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(oServicioTipoMoneda.GetAllTipoMonedas());

            List<SelectListItem> retornoListaCuentaBancaria = (ListaCuentaBancaria.Select(x =>
                                        new SelectListItem()
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Banco.Nombre + " " + x.BancoDescripcion
                                        })).ToList();

            List<SelectListItem> retornoListaTipoMoneda = (ListaTipoMoneda.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.Descripcion
                                         })).ToList();

            FacturaPagoViewModel oChequesModel = new FacturaPagoViewModel
            {
                ListaChequesPropios = ListaChequesPropios,
                listaCuentaBancariaDrop = retornoListaCuentaBancaria,
                ListaTipoMonedaDrop = retornoListaTipoMoneda
            };

            return PartialView("CuentaCteProveedor/_TablaChequesPropios", oChequesModel);

        }

        [HttpPost]
        public ActionResult CargarChequesTerceros()

        {
            //cheques de terceros

            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());

            FacturaPagoViewModel oChequesModel = new FacturaPagoViewModel();
            oChequesModel.ListaChequesTerceros = ListaChequesTerceros;

            return PartialView("CuentaCteProveedor/_tablaChequesTerceros", oChequesModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IngresarCheque(ChequeraModelView oFacturaPago)

        {
            try
            {

                //buscar el tipo de moneda de la cta
                BancoCuentaModelView bancoCuentaModelView = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(oServicioCuentaBancaria.GetCuentaPorId(oFacturaPago.idCuentaBancariaSeleccionada));

                oFacturaPago.IdBancoCuenta = oFacturaPago.idCuentaBancariaSeleccionada;
                oFacturaPago.Fecha = DateTime.Now;
                oFacturaPago.IdMoneda = bancoCuentaModelView.IdMoneda;
                oFacturaPago.Usado = false;
                oFacturaPago.IdProveedor = null;
                oFacturaPago.NumeroRecibo = null;
                oFacturaPago.Activo = true;
                //oFacturaPago.oChequera.IdUsuario = oFacturaPago.idUsuario;
                oFacturaPago.UltimaModificacion = DateTime.Now;
                ChequeraModel chequePropioGuardado = oServicioChequera.Insertar(Mapper.Map<ChequeraModelView, ChequeraModel>(oFacturaPago));
                if (chequePropioGuardado != null)
                {
                    oServicioChequera.ActualizarNumeroCheque(chequePropioGuardado);
                }


                ChequeraModelView chequeraModelView = Mapper.Map<ChequeraModel, ChequeraModelView>(oServicioChequera.GetChequePropioPorId(chequePropioGuardado.Id));
                List<ChequeraModelView> listChequeraModelView = new List<ChequeraModelView> { chequeraModelView };

                return PartialView("CuentaCteProveedor/_RDChequesPropios", listChequeraModelView);

            }
            catch (Exception ex)
            {
                //throw;
                oServicioChequera._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuitarCheque(int IdCheque = 0)  //PartialViewResult
        {
            try
            {
                oServicioChequera.DeleteChequePropio(IdCheque);
                List<ChequeraModelView> listChequeraModelView = new List<ChequeraModelView>();
                return PartialView("CuentaCteProveedor/_RDChequesPropios", listChequeraModelView);

            }
            catch (Exception ex)
            {
                oServicioChequera._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }

        }


    }

}



