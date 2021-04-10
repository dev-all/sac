using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Models;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
using SAC.Models.Cobro;

namespace SAC.Controllers
{
    public class CobroController : BaseController
    {
        private ServicioCobro servicioCobro = new ServicioCobro();
        private ServicioCliente servicioCliente = new ServicioCliente();
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioBancoCuenta servicioBancoCuenta = new ServicioBancoCuenta();


        private ServicioPresupuestoActual servicioPresupuestoActual = new ServicioPresupuestoActual();
        private ServicioChequera servicioChequera = new ServicioChequera();
        private ServicioCheque servicioCheque = new ServicioCheque();
        private ServicioTarjeta servicioTarjeta = new ServicioTarjeta();
        private ServicioProvincia servicioProvincia = new ServicioProvincia();
        private ServicioTipoRetencion servicioTipoRetencion = new ServicioTipoRetencion();
        private ServicioRetencion servicioRetencion = new ServicioRetencion();

        public CobroController()
        {
            servicioCobro._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }

        [HttpGet()]
        public ActionResult GetListClienteJson(string term)
        {
            try
            {
                List<ClienteModel> model = servicioCliente.GetClientePorNombre(term);
                var arrayModel = (from prov in model
                                  select new AutoCompletarViewModel()
                                  {
                                      id = prov.Id,
                                      label = prov.Nombre
                                  }).ToArray();
                return Json(arrayModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                servicioCobro._mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult Index(int IdCliente = 0)
        {
            CobroClienteModelView modelView = new CobroClienteModelView();

            try
            {
                if (IdCliente > 0)
                {

                    modelView.Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(IdCliente));
                    modelView.Cotizacion = servicioTipoMoneda.GetCotizacionPorIdMoneda(DateTime.Now, 1);
                    modelView.Periodo = Int32.Parse(DateTime.Now.ToString("yyMM"));
                    modelView.CuentaCorriente = null; // obtener lista de cbte                
                                                      //modelView.ComprobanteDePago = null;
                    modelView.ResumenPago = null;//
                    List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBancoCuenta.GetAllCuenta());
                    modelView.SelectCuentasBancarias = (ListaCuentaBancaria.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Banco.Nombre + ' ' + x.BancoDescripcion
                    })).ToList();
                    modelView.SelectCuentasBancarias.Insert(0, new SelectListItem() { Value = "0", Text = "Cuentas " });

                    List<TipoMonedaModelView> tipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
                    modelView.SelectTipoMoneda = (tipoMoneda.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.Descripcion
                    })).ToList();
                    ///continuar agregando los drop para el cbt de ingreso

                    List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(servicioCheque.GetAllCheque());
                    modelView.ListaChequesTerceros = ListaChequesTerceros;
                    //--------PartialView cheques propios          
                    List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(servicioChequera.GetAllChequera());
                    modelView.ListaChequesPropios = ListaChequesPropios;

                    List<TarjetaModelView> ListaTarjetas = Mapper.Map<List<TarjetaModel>, List<TarjetaModelView>>(servicioTarjeta.GetAllTarjetas());
                    modelView.SelectTarjetas = (ListaTarjetas.Select(x =>
                                                 new SelectListItem()
                                                 {
                                                     Value = x.Id.ToString(),
                                                     Text = x.Descripcion
                                                 })).ToList();
                    modelView.SelectTarjetas.Insert(0, new SelectListItem() { Value = "0", Text = "Tarjetas " });


                    //drop presupuesto
                    List<PresupuestoActualModelView> ListaPresupuesto = Mapper.Map<List<PresupuestoActualModel>, List<PresupuestoActualModelView>>(servicioPresupuestoActual.GetAllPresupuestos());
                    modelView.SelectPresupuestoActual = (ListaPresupuesto.Select(x =>
                                                                        new SelectListItem()
                                                                        {
                                                                            Value = x.Id.ToString(),
                                                                            Text = x.Concepto
                                                                        })).ToList();

                    //para la retencion
                    //RetencionModelView retencionPagoModelView = new RetencionModelView();

                    //List<TipoRetencionModelView> tipoRetencionModelViews = Mapper.Map<List<TipoRetencionModel>, List<TipoRetencionModelView>>(servicioTipoRetencion.GetAllTipoRetencion());
                    //retencionPagoModelView.tipoRetencion = (tipoRetencionModelViews.Select(x =>
                    //                             new SelectListItem()
                    //                             {
                    //                                 Value = x.Id.ToString(),
                    //                                 Text = x.Descripcion
                    //                             })).ToList();

                    //List<ProvinciaModelView> provinciaModelViews = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias());
                    //retencionPagoModelView.ListadoProvincias = (provinciaModelViews.Select(x =>
                    //                             new SelectListItem()
                    //                             {
                    //                                 Value = x.Id.ToString(),
                    //                                 Text = x.Nombre
                    //                             })).ToList();

                    //List<CompraFacturaViewModel> compraFacturaViewModel = Mapper.Map<List<CompraFacturaModel>, List<CompraFacturaViewModel>>(servicioCompra.ObtenerPorIDProveedor_Moneda(pagosFacturasModelView.ProveedorSelec_, pagosFacturasModelView.idTipoMonedaSelec_));

                    //if (compraFacturaViewModel.Count > 0)
                    //{
                    //    retencionPagoModelView.ListadoFacturas = (compraFacturaViewModel.Select(x =>
                    //                          new SelectListItem()
                    //                          {
                    //                              Value = x.Id.ToString(),
                    //                              Text = x.NumeroFactura.ToString()
                    //                          })).ToList();
                    //}

                    //modelView.Retencion_ = retencionPagoModelView;

                    // modelView.idProveedor_ = pagosFacturasModelView.ProveedorSelec_;



                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return View(modelView);
        }

    }
}