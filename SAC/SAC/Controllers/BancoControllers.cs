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
    public class BancoController : BaseController
    {

        private ServicioCaja servicioCaja = new ServicioCaja();

        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
        private ServicioCajaSaldo servicioCajaSaldo = new ServicioCajaSaldo();
        private ServicioBancoCuenta servicioBanco = new ServicioBancoCuenta();
        private ServicioBancoCuentaBancaria servicioBancoCuentaBancaria = new ServicioBancoCuentaBancaria();
        private ServicioCliente oservicioCliente = new ServicioCliente();
        private ServicioCheque oservicioCheque = new ServicioCheque();
        private ServicioTarjeta oservicioTarjeta = new ServicioTarjeta();
        private ServicioTarjetaOperacion oservicioTarjetaOperacion = new ServicioTarjetaOperacion();

        public ServicioPresupuestoActual servicioPresupuestoActual = new ServicioPresupuestoActual();
        public ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        public ServicioImputacion servicioImputacion = new ServicioImputacion();
        public ServicioContable servicioContable = new ServicioContable();

        private ServicioCheque oServicioCheque = new ServicioCheque();

        public BancoController()
        {
            servicioCaja._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }


        //public ActionResult Index()
        //{
        //    BancoCuentaModelView model = new BancoCuentaModelView();
        //    CargarBanco();
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Index(int IdBanco , DateTime Fecha)
        //{
        //    BancoCuentaModelView model = new BancoCuentaModelView();
        //    model.ListaBancoCuenta = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBanco.GetBancoPorFecha(IdBanco, Fecha));
        //    CargarBanco();
        //    return View(model);
        //}


        // cargar bancos 

        private void CargarBanco()
        {
            List<BancoCuentaModel> ListBancoCuentaModels = servicioBanco.GetAllCuenta();

            List<SelectListItem> ListBancoCuenta = null;
            ListBancoCuenta = (ListBancoCuentaModels.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.BancoDescripcion
            })).ToList();
            ListBancoCuenta.Insert(0, new SelectListItem { Text = "Seleccionar", Value = "" });
            ViewBag.CargarBanco = ListBancoCuenta;
        }

        // PRIMERA CARGA DE LA PAGINA  DE LA VISTA CHEQUES

        public ActionResult Cheques()
        {
            ChequeModelView model = new ChequeModelView();
            model.ListaCheque = new List<ChequeModelView>();
            model.cFechaDesde = DateTime.Now;
            CargarListaOpcion();
            return View(model);
        }

        [HttpPost]
        public ActionResult Cheques(string Cfechadesde, string Cfechahasta, int Idbanco = 0, int IdCliente = 0)
        {

            ChequeModelView model = new ChequeModelView();
            model.ListaCheque = new List<ChequeModelView>();

            DateTime fechaDesde = DateTime.Now;
            if (!string.IsNullOrEmpty(Cfechadesde))
            {
                fechaDesde = DateTime.ParseExact(Cfechadesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DateTime fechaHasta = DateTime.Now;
            if (!string.IsNullOrEmpty(Cfechahasta))
            {
                fechaHasta = DateTime.ParseExact(Cfechahasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            //if (Idbanco > 0)              
            //{
            //    model.ListaCheque = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oservicioCheque.GetAllChequePorBanco(Idbanco, fechaDesde, fechaHasta));
            //}

            //if (IdCliente > 0)

            //{
            //   model.ListaCheque = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oservicioCheque.GetAllChequePorCliente(IdCliente, fechaDesde, fechaHasta));
            //}
            model.ListaCheque = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oservicioCheque.BuscarCheque(IdCliente, Idbanco, fechaDesde, fechaHasta));

            model.cFechaDesde = fechaDesde;
            model.cFechaHasta = fechaHasta;

            CargarListaOpcion();

            return View(model);

        }


        // PRIMERA CARGA DE LA PAGINA  DE LA VISTA TARJETA

        public ActionResult Tarjetas()
        {

            TarjetaOperacionModelView model = new TarjetaOperacionModelView();
            CargarTarjetas();
            model.ListaTarjetaOperacion = new List<TarjetaOperacionModelView>();
            model.cFechaDesde = DateTime.Today;
            model.cFechaHasta = DateTime.Today;
            return View(model);
        }

        [HttpPost]
        public ActionResult Tarjetas(DateTime Cfechadesde, DateTime Cfechahasta, int IdTipoTarjeta = 0)
        {
            TarjetaOperacionModelView model = new TarjetaOperacionModelView();
            model.ListaTarjetaOperacion = new List<TarjetaOperacionModelView>();
            if (IdTipoTarjeta != 0)
            {


                if (Cfechadesde == Cfechahasta)  // si la fecha es igual trae todos los movimientos
                {
                    model.ListaTarjetaOperacion = Mapper.Map<List<TarjetaOperacionModel>, List<TarjetaOperacionModelView>>(oservicioTarjetaOperacion.GetTarjetaOperacionGastos(IdTipoTarjeta));
                }
                else  // si la fecha es distintas filtra por las fecha
                {
                    model.ListaTarjetaOperacion = Mapper.Map<List<TarjetaOperacionModel>, List<TarjetaOperacionModelView>>(oservicioTarjetaOperacion.GetTarjetaOperacionGastos(IdTipoTarjeta, Cfechadesde, Cfechahasta));
                }
            }

            model.cFechaDesde = Cfechadesde;
            model.cFechaHasta = Cfechahasta;

            CargarTarjetas();

            return View(model);


        }

        [HttpPost]
        public ActionResult ConciliarTarjeta(TarjetaOperacionModelView model)
        {
            string[] movimientosSeleccionados = model.IdTarjetaConciliar.Split(';');
            foreach (var item in movimientosSeleccionados)
            {
                oservicioTarjetaOperacion.ConciliarMovimiento(int.Parse(item));
            }

            return RedirectToAction("Tarjetas");
        }


        //[HttpPost]
        //public ActionResult ConciliarMovimiento(IngresoBancoModelView model)
        //{

        //    return RedirectToAction(model);
        //}


        public void CargarListaOpcion()
        {

            ViewBag.Opcion1 = GetOpcion1();
            ViewBag.Opcion2 = GetOpcion2();

        }

        private List<SelectListItem> GetOpcion1()
        {
            return Opcion1;
        }

        private static readonly List<SelectListItem> Opcion1 = new List<SelectListItem>
        {
            new SelectListItem() {Value = "0",Text = "Elija una opcion"},
            new SelectListItem() {Value = "1",Text = "Origen"},
            new SelectListItem() {Value = "2",Text = "Destino"},
            new SelectListItem() {Value = "3",Text = "Cheques en Cartera"}
        };

        private List<SelectListItem> GetOpcion2()
        {
            return Opcion2;
        }

        private static readonly List<SelectListItem> Opcion2 = new List<SelectListItem>
        {
            new SelectListItem() {Value = "0",Text = "Elija una opcion"},
            new SelectListItem() {Value = "1",Text = "Fecha de Ingreso"},
            new SelectListItem() {Value = "2",Text = "Clientes"},
            new SelectListItem() {Value = "3",Text = "Banco"}
        };
        // cargar  Clientes
        private void CargarClientes()
        {

            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Codigo
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar Grupo", Value = "" });
            ViewBag.CargarClientes = retornoListaCajaGrupo;


        }

        // metodos de autocompletar de Banco y de clientes

        [HttpGet()]
        public ActionResult GetListBancoJson(string term)
        {
            try
            {
                IList<BancoCuentaModel> proveedor = servicioBanco.GetBancoPorNombre(term);
                var arrayProveedor = (from prov in proveedor
                                      select new AutoCompletarViewModel()
                                      {
                                          id = prov.Id,
                                          label = prov.Banco.Nombre
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                servicioBanco._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetListClienteJson(string term)
        {
            try
            {
                List<ClienteModel> proveedor = oservicioCliente.GetClientePorNombre(term);
                var arrayProveedor = (from prov in proveedor
                                      select new AutoCompletarViewModel()
                                      {
                                          id = prov.Id,
                                          label = prov.Nombre
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                servicioBanco._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

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
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar una Tarjeta", Value = "" });
            ViewBag.ListaTarjeta = retornoListaCajaGrupo;


        }


        [HttpGet]
        public ActionResult IngresoCuentaBancaria(int IdBancoCuenta = 0, String searchFecha = null)
        {
            IngresoBancoModelView modelView = new IngresoBancoModelView();
            modelView.BancoCuenta = new BancoCuentaModelView();
            modelView.BancoCuenta.Fecha = DateTime.Now;
            modelView.BancoCuenta.IdMoneda = 1;
            modelView.Cotizacion = 5;
            if (IdBancoCuenta != 0)
            {
                DateTime fecha = DateTime.Now;
                if (!string.IsNullOrEmpty(searchFecha))
                {
                    fecha = DateTime.ParseExact(searchFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                modelView.BancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(IdBancoCuenta));
                modelView.ListaBancoCuenta = Mapper.Map<List<BancoCuentaBancariaModel>, List<BancoCuentaBancariaModelView>>(servicioBanco.GetmMovimientosPendientesCuentaBancaria(IdBancoCuenta, fecha));
            }

            CargarBanco();

            BancoCuentaBancariaModelView ingresos = new BancoCuentaBancariaModelView();
            ingresos.ListItemsGrupoCaja = CargarCajaGrupo();
            ingresos.ListItemsBancoCuenta = CargarBancoCuenta();
            ingresos.IdTipoMoneda = modelView.BancoCuenta.IdMoneda;

            //debe ser segun la cuenta banco seleccionada
            //---------para el PartialView cheques terceros

            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());

            // validacion de fecha de efectivo <= a fecha actual
            ingresos.ListaChequesTerceros = (from c in ListaChequesTerceros
                                             where c.FechaEgreso >= DateTime.Now
                                             select c).ToList();

            ingresos.ListaChequesTerceros = ListaChequesTerceros;
            modelView.Ingresos = ingresos;



            return View(modelView);
        }

        [HttpPost]
        public ActionResult Ingreso(BancoCuentaBancariaModelView modelView)
        {
            //IngresoBancoModelView model
            //BancoCuentaBancariaModelView modelView = model.Ingresos;
            //modelView.Cotizacion = model.Cotizacion;
            //modelView.IdBancoCuenta = model.IdBancoCuenta;
            // el tipo de moneda esta determinado por la cta

            BancoCuentaModelView modelBancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(modelView.IdBancoCuenta));
            switch (modelView.TipoMovimiento)
            {
                case "cv":

                    RegistroIngresoPorCargosVarios(modelView, modelBancoCuenta);

                    break;

                case "de":

                    RegistroIngresoPorDespositoEfectivo(modelView, modelBancoCuenta);
                    break;

                case "tc":

                    RegistroIngresoPorTrasnferenciaCaja(modelView, modelBancoCuenta);
                    break;

                case "tt":

                    RegistroIngresoPorTrasnferenciaEntreCuentas(modelView, modelBancoCuenta);
                    break;

                case "ch":

                    RegistroIngresoDepositoDeCheque(modelView, modelBancoCuenta);
                    break;
                default:
                    //ingreso por cheque

                    break;
            }


            return RedirectToAction("IngresoCuentaBancaria");
        }


        [HttpPost]
        public ActionResult ConfirmarConciliacion(IngresoBancoModelView modelView)
        {
            //BancoCuentaModelView modelBancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(modelView.IdBancoCuenta));
            //modelView.ListaBancoCuenta = Mapper.Map<List<BancoCuentaBancariaModel>, List<BancoCuentaBancariaModelView>>(servicioBanco.GetmMovimientosPendientesCuentaBancaria(IdBancoCuenta, fecha));
            string[] movimientosSeleccionados = modelView.IdConciliacionMovimiento.Split(';');
            foreach (var item in movimientosSeleccionados)
            {
                servicioBancoCuentaBancaria.ConciliarMovimiento(int.Parse(item));                               
            }

            return RedirectToAction("IngresoCuentaBancaria");
        }
        [HttpPost]
        public ActionResult CierreCuenta(IngresoBancoModelView modelView)
        {
            BancoCuentaModelView modelBancoCuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBanco.GetBancoCuentaPorId(modelView.IdBancoCuenta));

            List<BancoCuentaBancariaModel> bancoCuentaBancariaModels = servicioBancoCuentaBancaria.GetAllMovimientosConciliados(modelBancoCuenta.Id);

            decimal SaldoCierre = modelBancoCuenta.Saldo + bancoCuentaBancariaModels.Sum(s => s.Importe);

            BancoCuentaModel cierre = servicioBanco.CierreDeCuentaBancaria(modelBancoCuenta.Id, SaldoCierre);

            foreach (var item in bancoCuentaBancariaModels)
            {
                //actualizar nro cierre en BancoCuentaBancariaModel
                item.NumeroCierre = cierre.NumeroCierre;
                servicioBancoCuentaBancaria.UpdateNumeroCierreMovimiento(item);
            }

            return RedirectToAction("IngresoCuentaBancaria");
        }


        private void RegistroIngresoDepositoDeCheque(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            try
            {
                if (modelView.Importe <= 0)
                {
                    throw new Exception("Complete los Campos requeridos");
                }

                if ((modelView.idChequesTerceros != null) && (modelView.idChequesTerceros != ""))
                {

                    ChequeModel oCheque = new ChequeModel();
                    BancoModel bancoModel = new BancoModel();

                    var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
                    fecha = fecha.AddDays(2);

                    string[] chequesSeleccionados = modelView.idChequesTerceros.Split(';');
                    foreach (var itemCheque in chequesSeleccionados)
                    {

                        oCheque = oServicioCheque.obtenerCheque(int.Parse(itemCheque));
                        if (oCheque != null)
                        {


                            // obtener banco seleccionado para deposito de cheque
                            bancoModel = servicioBanco.GetBancoPorId(modelBancoCuenta.IdBanco);//obtener banco

                            //--------------------------- BancoCuentaBancaria ------
                            modelView.NumeroOperacion = oCheque.NumeroCheque;
                            modelView.IdBancoCuenta = modelBancoCuenta.Id; //obtener el id de la cuta seleccionada
                            modelView.Fecha = Convert.ToDateTime(DateTime.Now);
                            modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now);
                            modelView.FechaEfectiva = fecha;
                            modelView.DiaClearing = "2";
                            modelView.Conciliacion = false;
                            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
                            if (cajaGrupoModel != null)
                            {
                                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"TRANS"
                            }
                            else
                            {
                                modelView.IdGrupoCaja = 0;
                            }
                            modelView.IdCliente = "BANCO";
                            modelView.IdUsuario = usuario.IdUsuario;
                            modelView.Importe *= -1; /// valor en negativo 
                            modelView.CuentaDescripcion = "Deposito de Cheque Nº" + oCheque.NumeroCheque.ToString();//nro cheque

                            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));


                            //------------------- Actualizar cheques                    
                            oCheque.FechaEgreso = DateTime.Now;
                            oCheque.Destino = modelView.CuentaDescripcion + bancoModel.Nombre;
                            //oCheque.NumeroPago = null;
                            oCheque.Proveedor = "BANCO";
                            oCheque.Activo = false;
                            oCheque.Endosado = true;
                            oServicioCheque.Actualizar(oCheque);

                            ///------------------- Caja--------------------

                            cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("BANCH");
                            if (cajaGrupoModel != null)
                            {
                                modelView.IdGrupoCaja = cajaGrupoModel.Id;
                            }
                            else
                            {
                                modelView.IdGrupoCaja = 0;
                            }

                            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView,
                                                                  BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);


                        }
                    }

                }

            }
            catch (Exception ex)
            {
                //podria guardar el log
                servicioCaja._mensaje?.Invoke("Ops!!. " + ex.Message.ToString(), "error");
                //return RedirectToAction(nameof(IngresoCuentaBancaria));
            }

        }

        private void RegistroIngresoPorTrasnferenciaEntreCuentas(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            modelView.IdBancoCuenta = modelBancoCuenta.Id; // el que es seleccionado por el cliente en la vntana consulta
            modelView.Fecha = Convert.ToDateTime(DateTime.Now);
            modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now);
            modelView.FechaEfectiva = fecha;
            modelView.DiaClearing = "2";
            modelView.Importe *= -1; // paso a negativo
            modelView.Conciliacion = false;

            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
            if (cajaGrupoModel != null)
            { modelView.IdGrupoCaja = cajaGrupoModel.Id; }
            else { modelView.IdGrupoCaja = 0; }

            modelView.IdCliente = "BANCO";
            modelView.IdUsuario = usuario.IdUsuario;
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));


            modelView.IdBancoCuenta = modelView.IdBancoCuentaDestino;
            modelView.Importe *= -1; // paso a positivo       
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));


        }

        private void RegistroIngresoPorTrasnferenciaCaja(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            modelView.IdBancoCuenta = modelBancoCuenta.Id;
            modelView.Fecha = Convert.ToDateTime(DateTime.Now);
            modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now);
            modelView.FechaEfectiva = fecha;
            modelView.DiaClearing = "2";
            modelView.Importe *= -1; // paso a negativo
            modelView.Conciliacion = false;
            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
            if (cajaGrupoModel != null)
            {
                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"TRANS"
            }
            else { modelView.IdGrupoCaja = 0; }

            modelView.IdCliente = "BANCO";
            modelView.IdUsuario = usuario.IdUsuario;
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));

            modelView.Importe *= -1; //paso a positivo    
            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);


            // inicio registro de asientos
            DiarioModel asiento = new DiarioModel();
            asiento.Codigo = 0;
            asiento.Fecha = Convert.ToDateTime(DateTime.Now); ;
            asiento.Periodo = DateTime.Now.ToString("yyMM");
            asiento.Tipo = "DE"; //Compras Facturas
            asiento.Cotiza = modelView.Cotizacion;
            asiento.Asiento = 0;
            asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
            asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(modelView.IdTipoMoneda).Descripcion;
            asiento.DescripcionMa = "Ingreso Cuenta Bancaria";
            asiento.Descripcion = "Ingreso Cuenta Bancaria";
            asiento.Importe = modelView.Importe;  //(modelView.IdTipoMoneda == 1) ? (modelView.Importe) : (modelView.Importe * modelView.Cotizacion);
            asiento.Titulo = "Ingreso Cuenta Bancaria";

            string alias = "";
            if (modelView.IdTipoMoneda == 1)
            {
                alias = "PESOS";
            }
            else
            {
                alias = "DOLAR";
            }


            var asientoDiario = servicioContable.InsertAsientoContable(alias, asiento, 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


            asiento.Importe *= -1; // importe negativo
            asientoDiario = servicioContable.InsertAsientoContable("", asiento, modelBancoCuenta.IdImputacion);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


        }

        private void RegistroIngresoPorDespositoEfectivo(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            modelView.IdBancoCuenta = modelBancoCuenta.Id;
            modelView.Fecha = Convert.ToDateTime(DateTime.Now);
            modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now);
            modelView.FechaEfectiva = fecha;
            modelView.DiaClearing = "2";
            modelView.Conciliacion = false;

            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("TRANS");
            if (cajaGrupoModel != null)
            {
                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"TRANS"
            }
            else
            {
                modelView.IdGrupoCaja = 0;
            }
            modelView.IdCliente = "BANCO";
            modelView.IdUsuario = usuario.IdUsuario;
            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));

            modelView.Importe *= -1; /// valor en negativo 

            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);


            // inicio registro de asientos
            DiarioModel asiento = new DiarioModel();
            asiento.Codigo = 0;
            asiento.Fecha = Convert.ToDateTime(DateTime.Now); ;
            asiento.Periodo = DateTime.Now.ToString("yyMM");
            asiento.Tipo = "DE"; //Compras Facturas
            asiento.Cotiza = modelView.Cotizacion;
            asiento.Asiento = 0;
            asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
            asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(modelView.IdTipoMoneda).Descripcion;
            asiento.Descripcion = "Ingreso Cuenta Bancaria";
            asiento.DescripcionMa = "Ingreso Cuenta Bancaria";
            // asiento.Importe = (modelView.IdTipoMoneda == 1) ? (modelView.Importe) : (modelView.Importe * modelView.Cotizacion);
            asiento.Titulo = "Ingreso Cuenta Bancaria";


            asiento.Importe *= -1;
            string alias = "";
            if (modelView.IdTipoMoneda == 1)
            {
                alias = "PESOS";
            }
            else
            {
                alias = "DOLAR";
            }


            var asientoDiario = servicioContable.InsertAsientoContable(alias, asiento, 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);

            modelView.Importe *= -1; /// valor en +  
            asientoDiario = servicioContable.InsertAsientoContable("", asiento, modelBancoCuenta.IdImputacion);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


        }

        private void RegistroIngresoPorCargosVarios(BancoCuentaBancariaModelView modelView, BancoCuentaModelView modelBancoCuenta)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            // CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorId(modelView.IdGrupoCaja);

            modelView.IdBancoCuenta = modelBancoCuenta.Id;
            modelView.Fecha = Convert.ToDateTime(DateTime.Now);
            modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now);
            modelView.FechaEfectiva = fecha;
            modelView.DiaClearing = "2";
            modelView.Importe *= -1; // importe negativo
            modelView.Conciliacion = false;
            modelView.IdUsuario = usuario.IdUsuario;

            servicioBancoCuentaBancaria.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView));


            servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);

            CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorCodigo("BANCH");
            if (cajaGrupoModel != null)
            {
                modelView.IdGrupoCaja = cajaGrupoModel.Id; //"BANCH"                       
                servicioCaja.IngresoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), modelView.IdGrupoCaja);
                servicioPresupuestoActual.UpdatePorIngreoCuentaBancaria(Mapper.Map<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>(modelView), cajaGrupoModel);
            }


            // Agregar asientos Contables

            // inicio registro de asientos
            DiarioModel asiento = new DiarioModel();
            asiento.Codigo = 0;
            asiento.Fecha = Convert.ToDateTime(DateTime.Now); ;
            asiento.Periodo = DateTime.Now.ToString("yyMM");
            asiento.Tipo = "CV"; //Compras Facturas
            asiento.Cotiza = modelView.Cotizacion;
            asiento.Asiento = 0;
            asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
            asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(modelView.IdTipoMoneda).Descripcion;
            asiento.Descripcion = "Ingreso Cuenta Bancaria";
            asiento.DescripcionMa = "Ingreso Cuenta Bancaria";
            // asiento.Importe = (modelView.IdTipoMoneda == 1) ? (modelView.Importe) : (modelView.Importe * modelView.Cotizacion);
            asiento.Titulo = "Ingreso Cuenta Bancaria";

            //imputacion por grupo caja
            var asientoDiario = servicioContable.InsertAsientoContable("", asiento, modelBancoCuenta.IdImputacion);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


            asiento.Importe *= -1;
            asientoDiario = servicioContable.InsertAsientoContable("", asiento, cajaGrupoModel.IdImputacion ?? 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);

        }




        public List<SelectListItem> CargarCajaGrupo()
        {
            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Codigo
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar Grupo", Value = "" });
            return retornoListaCajaGrupo;
        }
        public List<SelectListItem> CargarBancoCuenta()
        {
            List<BancoCuentaModel> ListBancoCuentaModels = servicioBanco.GetAllCuenta();

            List<SelectListItem> ListBancoCuenta = null;
            ListBancoCuenta = (ListBancoCuentaModels.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.BancoDescripcion
            })).ToList();
            ListBancoCuenta.Insert(0, new SelectListItem { Text = "Seleccionar", Value = "" });
            return ListBancoCuenta;
        }
    }






}



