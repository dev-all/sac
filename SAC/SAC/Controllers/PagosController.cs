using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//agregadas
using AutoMapper;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.Atributos;
using SAC.Models;
using System.Web.Routing;
using System.Globalization;

namespace SAC.Controllers
{
    public class PagosController : BaseController
    {

        private ServicioCompra oServicioCompra = new ServicioCompra();
        private ServicioProveedor oServicioProveedor = new ServicioProveedor();
        private ServicioCuentaCteProveedor oServicioCuentaCteProveedor = new ServicioCuentaCteProveedor();
        private ServicioPresupuestoActual oServicioPresupuestoActual = new ServicioPresupuestoActual();
        private ServicioChequera oServicioChequera = new ServicioChequera();
        private ServicioCheque oServicioCheque = new ServicioCheque();
        private ServicioBancoCuenta oServicioCuentaBancaria = new ServicioBancoCuenta();
        private ServicioTipoMoneda oServicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioTarjeta oServicioTarjeta = new ServicioTarjeta();
        private ServicioProvincia oServicioProvincia = new ServicioProvincia();
        private ServicioTipoRetencion oServicioTipoRetencion = new ServicioTipoRetencion();
        private ServicioRetencion oServicioRetencion = new ServicioRetencion();
        private ServicioTrackingFacturaPagoCompra oServicioTrackingFacturaPagoCompra = new ServicioTrackingFacturaPagoCompra();
      
        public PagosController()
        {
            oServicioCuentaCteProveedor._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
            oServicioCompra._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            oServicioProveedor._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            oServicioPresupuestoActual._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            oServicioChequera._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            oServicioCuentaBancaria._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            oServicioTipoMoneda._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            oServicioTarjeta._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            oServicioTrackingFacturaPagoCompra._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
        }

        // GET: Pagos
        //[HttpPost, ActionName("SearchCtaCteProveedor")]
        public ActionResult Index()
        {

            Session["Facturas_Pagar"] = null;

            Session["mediosPago"] = null;

            PagosFacturasModelView oPagosFacturasModel = new PagosFacturasModelView();
            ValorCotizacionModel oValorCotizacion = new ValorCotizacionModel();
            oValorCotizacion = oServicioTipoMoneda.GetCotizacionPorIdMoneda(DateTime.Now, 1);

            if (oValorCotizacion == null)
            {
                oPagosFacturasModel.cotizacion_ = 0;
            }
            else
            {
                oPagosFacturasModel.cotizacion_ = oValorCotizacion.Monto;
            }

            // oPagosFacturasModel.FechaOperacion_ = DateTime.Today;
            string anio = DateTime.Now.Year.ToString();
            anio = anio.Substring(anio.Length - 2, 2);
            oPagosFacturasModel.Periodo_ = int.Parse(anio + DateTime.Now.Month.ToString());

            //drop proveedores
            List<ProveedorModelView> ListaProveedores = Mapper.Map<List<ProveedorModel>, List<ProveedorModelView>>(oServicioProveedor.GetAllProveedor());
            oPagosFacturasModel.ListaProveedores_ = (ListaProveedores.Select(x =>
                                        new SelectListItem()
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Nombre
                                        })).ToList();

            //drop tipo moneda
            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(oServicioTipoMoneda.GetAllTipoMonedas());
            oPagosFacturasModel.ListaTipoMonedas_ = (ListaTipoMoneda.Select(x =>
                                       new SelectListItem()
                                       {
                                           Value = x.Id.ToString(),
                                           Text = x.Descripcion
                                       })).ToList();

            oPagosFacturasModel.ListaFacturasAPagar_ = null;

            //drop cuenta bancaria
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());

            oPagosFacturasModel.ListaCuentasBancarias_ = (ListaCuentaBancaria.Select(x =>
                                                                                    new SelectListItem()
                                                                                    {
                                                                                        Value = x.Id.ToString(),
                                                                                        Text = x.Banco.Nombre + ' ' + x.BancoDescripcion
                                                                                    })).ToList();

            ViewBag.listaCuentaBancariaDrop = oPagosFacturasModel.ListaCuentasBancarias_;



            return View(oPagosFacturasModel);
        }

        [HttpPost]
        public ActionResult Index(PagosFacturasModelView pagosFacturasModelView)
        {
            try
            {
                PagosFacturasModelView pagosFacturasModel = new PagosFacturasModelView();

               
                //para seleccionar
                List<ProveedorModelView> ListaProveedores = Mapper.Map<List<ProveedorModel>, List<ProveedorModelView>>(oServicioProveedor.GetAllProveedor());
                pagosFacturasModel.ListaProveedores_ = (ListaProveedores.Select(x =>
                                            new SelectListItem()
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.Nombre
                                            })).ToList();

                List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(oServicioTipoMoneda.GetAllTipoMonedas());
                pagosFacturasModel.ListaTipoMonedas_ = (ListaTipoMoneda.Select(x =>
                                           new SelectListItem()
                                           {
                                               Value = x.Id.ToString(),
                                               Text = x.Descripcion
                                           })).ToList();

                //cuando se busca por proveedor
                pagosFacturasModel.ListaFacturasAPagar_ = Mapper.Map<List<CompraFacturaModel>, List<CompraFacturaViewModel>>(oServicioCompra.ObtenerPorIDProveedor_Moneda(pagosFacturasModelView.ProveedorSelec_, pagosFacturasModelView.idTipoMonedaSelec_));

                pagosFacturasModel.LsitaFacturasSeleccionadasPagar_ = null;
                Session["Facturas_Pagar"] = null;

                Session["mediosPago"] = null;
                //medios de pago
                List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());
                pagosFacturasModel.ListaChequesTerceros = ListaChequesTerceros;
                //--------PartialView cheques propios          
                List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(oServicioChequera.GetAllChequera());
                pagosFacturasModel.ListaChequesPropios = ListaChequesPropios;

                List<TarjetaModelView> ListaTarjetas = Mapper.Map<List<TarjetaModel>, List<TarjetaModelView>>(oServicioTarjeta.GetAllTarjetas());
                pagosFacturasModel.ListaTarjetas_ = (ListaTarjetas.Select(x =>
                                             new SelectListItem()
                                             {
                                                 Value = x.Id.ToString(),
                                                 Text = x.Descripcion
                                             })).ToList();
                pagosFacturasModel.ListaTarjetas_.Insert(0, new SelectListItem() { Value = "0", Text = "Sin Especificar" });

                //drop cuenta bancaria
                List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());

                pagosFacturasModel.ListaCuentasBancarias_ = (ListaCuentaBancaria.Select(x =>
                                                                                        new SelectListItem()
                                                                                        {
                                                                                            Value = x.Id.ToString(),
                                                                                            Text = x.Banco.Nombre + ' ' + x.BancoDescripcion
                                                                                        })).ToList();
                pagosFacturasModel.ListaCuentasBancarias_.Insert(0, new SelectListItem() { Value = "0", Text = "Sin Especificar" });

                ViewBag.listaCuentaBancariaDrop = pagosFacturasModel.ListaCuentasBancarias_;
                //drop presupuesto
                List<PresupuestoActualModelView> ListaPresupuesto = Mapper.Map<List<PresupuestoActualModel>, List<PresupuestoActualModelView>>(oServicioPresupuestoActual.GetAllPresupuestos());
                pagosFacturasModel.ListaPresupuestoActual_ = (ListaPresupuesto.Select(x =>
                                                                    new SelectListItem()
                                                                    {
                                                                        Value = x.Id.ToString(),
                                                                        Text = x.Concepto
                                                                    })).ToList();

                //para la retencion
                RetencionModelView retencionPagoModelView = new RetencionModelView();


                List<TipoRetencionModelView> tipoRetencionModelViews = Mapper.Map<List<TipoRetencionModel>, List<TipoRetencionModelView>>(oServicioTipoRetencion.GetAllTipoRetencion());
                retencionPagoModelView.tipoRetencion = (tipoRetencionModelViews.Select(x =>
                                             new SelectListItem()
                                             {
                                                 Value = x.Id.ToString(),
                                                 Text = x.Descripcion
                                             })).ToList();



                List<ProvinciaModelView> provinciaModelViews = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(oServicioProvincia.GetAllProvincias());
                retencionPagoModelView.ListadoProvincias = (provinciaModelViews.Select(x =>
                                             new SelectListItem()
                                             {
                                                 Value = x.Id.ToString(),
                                                 Text = x.Nombre
                                             })).ToList();



                List<CompraFacturaViewModel> compraFacturaViewModel = Mapper.Map<List<CompraFacturaModel>, List<CompraFacturaViewModel>>(oServicioCompra.ObtenerPorIDProveedor_Moneda(pagosFacturasModelView.ProveedorSelec_, pagosFacturasModelView.idTipoMonedaSelec_));


                if (compraFacturaViewModel.Count > 0)
                {
                    retencionPagoModelView.ListadoFacturas = (compraFacturaViewModel.Select(x =>
                                          new SelectListItem()
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.NumeroFactura.ToString()
                                          })).ToList();
                }

                pagosFacturasModel.Retencion_ = retencionPagoModelView;
                // pagosFacturasModel.ListadoRetenciones_ = null;

                //return PartialView("_FacturasCuentaCte", pagosFacturasModel);
                return View(pagosFacturasModel);
            }
            catch (Exception ex)
            {
                return View(pagosFacturasModelView);
            }

        }


        [HttpPost]
        public ActionResult AgregarRetencion(PagosFacturasModelView pagosFacturasModelView)
        {
            RetencionModelView oRetencion = new RetencionModelView();
            oRetencion = pagosFacturasModelView.Retencion_;
            oRetencion.UltimaModificacion = DateTime.Now;
            oRetencion.Fecha = DateTime.Now;
            oRetencion.Activo = true;

            var a = oServicioRetencion.Agregar(Mapper.Map<RetencionModelView, RetencionModel>(oRetencion));

            List<RetencionModelView> retencionModelView = Mapper.Map<List<RetencionModel>, List<RetencionModelView>>(oServicioRetencion.GetAllRetencion(pagosFacturasModelView.Retencion_.IdCompraFactura));

            PagosFacturasModelView oPagosModelView = new PagosFacturasModelView();
            oPagosModelView.ListadoRetenciones_ = retencionModelView;

            try
            {
                return PartialView("_tablaRetenciones", oPagosModelView);
            }
            catch (Exception ex)
            {
                return null;
            }
            //return PartialView("_TablaRetenciones", retorno);

        }


        [HttpPost]
        public ActionResult EliminarRetencion(int IdRetencionEliminar, int idFacturaEliminar)
        {

            var oRetencionModel = oServicioRetencion.GetRetencionOu(IdRetencionEliminar);
            var a = oServicioRetencion.Eliminar(oRetencionModel);

            List<RetencionModelView> ListaretencionModelView = Mapper.Map<List<RetencionModel>, List<RetencionModelView>>(oServicioRetencion.GetAllRetencion(idFacturaEliminar));

            PagosFacturasModelView oPagosModelView = new PagosFacturasModelView();
            oPagosModelView.ListadoRetenciones_ = ListaretencionModelView;

            try
            {
                return PartialView("_tablaRetenciones", oPagosModelView);
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        //esto es para alimentar la grilla de facturas seleccionadas para pagar
        [HttpPost]
        public ActionResult AgregarFacturaPagar(int idFacturaSeleccionada, int idModoPago, string MontoPago, string CotizacionActual)
        {

            CompraFacturaViewModel oCompraFactura = new CompraFacturaViewModel();
            oCompraFactura = Mapper.Map<CompraFacturaModel, CompraFacturaViewModel>(oServicioCompra.ObtenerPorID(idFacturaSeleccionada));

            List<CompraFacturaViewModel> listaFacturasSeleccionadas = new List<CompraFacturaViewModel>();


            if (idModoPago == 1)
            {
                decimal ValorActualizado = 0;
                if (oCompraFactura.IdMoneda == 2)
                {
                    ValorActualizado = oCompraFactura.Saldo * Convert.ToDecimal(CotizacionActual);
                }
                else
                {
                    ValorActualizado = oCompraFactura.Saldo;
                }

                oCompraFactura.aplicacion = ValorActualizado;

            }

            if (idModoPago == 2)
            {
                if (oCompraFactura.IdMoneda == 2)
                {
                    var MontoNuevo = Convert.ToDecimal(MontoPago) * Convert.ToDecimal(CotizacionActual);
                    oCompraFactura.aplicacion = Convert.ToDecimal(MontoNuevo);
                }
                else
                {
                    oCompraFactura.aplicacion = Convert.ToDecimal(MontoPago);
                }
            }

            //pago con saldo a favor
            if (idModoPago == 3)
            {
                decimal ValorActualizado = 0;
                if (oCompraFactura.IdMoneda == 2)
                {
                    ValorActualizado = oCompraFactura.Saldo * Convert.ToDecimal(CotizacionActual);
                }
                else
                {
                    ValorActualizado = oCompraFactura.Saldo;
                }

                oCompraFactura.aplicacion = -ValorActualizado;

            }



            if (Session["Facturas_Pagar"] != null)
            {
                listaFacturasSeleccionadas = Session["Facturas_Pagar"] as List<CompraFacturaViewModel>;

                bool existe = false;
                foreach (CompraFacturaViewModel a in listaFacturasSeleccionadas)
                {
                    if (a.Id == idFacturaSeleccionada)
                    {
                        existe = true;
                        break;
                    }
                }

                if (existe == false)
                {



                    listaFacturasSeleccionadas.Add(oCompraFactura);
                }
            }
            else
            {
                listaFacturasSeleccionadas.Add(oCompraFactura);
            }

            PagosFacturasModelView oPagosModelView = new PagosFacturasModelView();

            Session["Facturas_Pagar"] = listaFacturasSeleccionadas;
            oPagosModelView.LsitaFacturasSeleccionadasPagar_ = listaFacturasSeleccionadas;

            try
            {
                return PartialView("_TablaFacturasPagar", oPagosModelView);
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        [HttpPost]
        public ActionResult AgregarPago(PagosFacturasModelView oPagosFacturasModelView)
        {
            //PagosFacturasModelView mediosPago = new PagosFacturasModelView();
            Session["mediosPago"] = oPagosFacturasModelView;

            List<CompraFacturaViewModel> listaFacturasSeleccionadas = new List<CompraFacturaViewModel>();

            if (Session["Facturas_Pagar"] != null)
            {
                listaFacturasSeleccionadas = Session["Facturas_Pagar"] as List<CompraFacturaViewModel>;

                decimal totalMontoFacturas = 0;
                decimal totalMontoAplicacionFacturas = 0;

                foreach (CompraFacturaViewModel item in listaFacturasSeleccionadas)
                {
                        decimal valSal = Convert.ToDecimal(item.Saldo);
                        decimal valApli = Convert.ToDecimal(item.aplicacion);
                        totalMontoFacturas += valSal;
                        totalMontoAplicacionFacturas += valApli;
                }

                CompraFacturaViewModel oCompraFactura = new CompraFacturaViewModel();
                oCompraFactura.IdTipoComprobante = 98;
                oCompraFactura.PuntoVenta = 9;
                oCompraFactura.NumeroFactura = 0;
                oCompraFactura.Saldo = 0;
                oCompraFactura.pago = oPagosFacturasModelView.montoTotal_; //suma de medios pago;
                oCompraFactura.aplicacion = -oPagosFacturasModelView.montoTotal_; //suma de fact;

                if( totalMontoAplicacionFacturas >= oPagosFacturasModelView.montoTotal_)
                {
                    oCompraFactura.saldoPagos = 0;
                }
                else
                {
                    oCompraFactura.saldoPagos = oPagosFacturasModelView.montoTotal_ - totalMontoAplicacionFacturas;
                }

                bool existe = false;

                foreach (CompraFacturaViewModel a in listaFacturasSeleccionadas)
                {
                    if (a.IdTipoComprobante == 98 && a.NumeroFactura == 0 ) // es porque ya hay un pago)
                    {
                        existe = true;
                        break;
                    }
                }
                if (existe == false)
                {
                    listaFacturasSeleccionadas.Add(oCompraFactura);
                }
            }

            PagosFacturasModelView oPagosModelView = new PagosFacturasModelView();

            Session["Facturas_Pagar"] = listaFacturasSeleccionadas;
            oPagosModelView.LsitaFacturasSeleccionadasPagar_ = listaFacturasSeleccionadas;


            try
            {
                return PartialView("_TablaFacturasPagar", oPagosModelView);
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        [HttpPost]
        public ActionResult CancelarPago()
        {
            try
            {
                PagosFacturasModelView oPagosModelView = new PagosFacturasModelView();
                Session["Facturas_Pagar"] = null;

                return Json(new { result = true, data = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, data = "Ops!, A ocurriodo un error. Contacte al Administrador" }, JsonRequestBehavior.AllowGet);
            }

            //return PartialView("_TablaFacturasPagar", oPagosModelView);
        }

        [HttpPost]
        public ActionResult Pagar()
        {

            var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

            List<CompraFacturaViewModel> listaFacturasSeleccionadas = new List<CompraFacturaViewModel>();
            listaFacturasSeleccionadas = Session["Facturas_Pagar"] as List<CompraFacturaViewModel>;
            PagosFacturasModelView mediosPago = new PagosFacturasModelView();
            mediosPago = Session["mediosPago"] as PagosFacturasModelView;
            mediosPago.idUsuario_ = datosUsuario.IdUsuario;

            oServicioCompra.RegistrarPagosFacturas(Mapper.Map<List<CompraFacturaViewModel>, List<CompraFacturaModel>>(listaFacturasSeleccionadas), Mapper.Map<PagosFacturasModelView, PagosFacturasModel>(mediosPago));

            var f  = (from i in listaFacturasSeleccionadas
                      where (i.IdTipoComprobante != 98)
                      select new PagosFacturasModelView {
                                idTipoMonedaSelec_ = i.IdMoneda,
                      ProveedorSelec_ = i.IdProveedor}).FirstOrDefault();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult ObtenerDatos(int IdFact)
        {
            CompraFacturaViewModel oCompraFactura = new CompraFacturaViewModel();
            oCompraFactura = Mapper.Map<CompraFacturaModel, CompraFacturaViewModel>(oServicioCompra.ObtenerPorID_paraPagos(IdFact));
            return Json(oCompraFactura, JsonRequestBehavior.AllowGet);
        }


        public int VerificarPago()
        {
            int respuesta = 0;
            try
            {
                List<CompraFacturaViewModel> listaFacturasSeleccionadas = new List<CompraFacturaViewModel>();
                listaFacturasSeleccionadas = Session["Facturas_Pagar"] as List<CompraFacturaViewModel>;
                if (listaFacturasSeleccionadas != null)
                {
                    foreach (CompraFacturaViewModel item in listaFacturasSeleccionadas)
                    {
                        if (item.IdTipoComprobante == 98 && item.NumeroFactura == 0)
                        {
                            respuesta = 1;
                            break;
                        }
                    }
                }
                else
                {
                    respuesta = 0;
                }


                return respuesta;

            }
            catch (Exception ex)
            {
                respuesta = 0;
            }
            return respuesta;
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
//esto modifique ahora 
                return PartialView("~/Views/CuentaCteProveedor/_RDChequesPropios.cshtml", listChequeraModelView);

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

                //return PartialView("_RDChequesPropios", listChequeraModelView);
                return PartialView("~/Views/CuentaCteProveedor/_RDChequesPropios.cshtml", listChequeraModelView);
            }
            catch (Exception ex)
            {
                oServicioChequera._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }

        }


    }
}