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

        public BancoController()
        {
            servicioCaja._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
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
            return RedirectToAction("Tarjetas");
        }


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


        // cargar bancos 

        private void CargarBanco()
        {

            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Codigo
            })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "Seleccionar Grupo", Value = "" });
            ViewBag.CargarBanco = retornoListaCajaGrupo;



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
        public ActionResult IngresoCuentaBancaria()
        {
            IngresoBancoModelView modelView = new IngresoBancoModelView();
            BancoCuentaBancariaModelView ingresos = new BancoCuentaBancariaModelView();
            ingresos.ListItemsGrupoCaja = CargarCajaGrupo();
            ingresos.IdTipoMoneda = 1;
            modelView.Ingresos = ingresos;
            return View(modelView);
        }


        [HttpPost]
        public ActionResult Ingreso(BancoCuentaBancariaModelView modelView)
        {

            // el tipo de moneda esta determinado por la cta

            switch (modelView.TipoMovimiento)
            {
                case "cv":

                    RegistroIngresoPorCargosVarios(modelView);

                    break;

                case "de":

                    RegistroIngresoPorDespositoEfectivo(modelView);
                    break;

                case "tc":

                    RegistroIngresoPorTrasnferenciaCaja(modelView);
                    break;

                case "tt":

                    RegistroIngresoPorTrasnferenciaEntreCuentas(modelView);
                    break;

                default:
                    //ingreso por cheque

                    break;
            }


            return RedirectToAction("IngresoCuentaBancaria");
        }

        private void RegistroIngresoPorTrasnferenciaEntreCuentas(BancoCuentaBancariaModelView modelView)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            //modelView.IdBancoCuenta = 1; // el que es seleccionado por el cliente en la vntana consulta
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

        private void RegistroIngresoPorTrasnferenciaCaja(BancoCuentaBancariaModelView modelView)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            //modelView.IdBancoCuenta = 1;
            modelView.Fecha = Convert.ToDateTime(DateTime.Now);
            //modelView.FechaIngreso = Convert.ToDateTime(DateTime.Now); cambiar a datetime                
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
            asientoDiario = servicioContable.InsertAsientoContable("Cuentas", asiento, 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


        }

        private void RegistroIngresoPorDespositoEfectivo(BancoCuentaBancariaModelView modelView)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            modelView.IdBancoCuenta = 1;
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
            asientoDiario = servicioContable.InsertAsientoContable("Cuentas", asiento, 0);
            /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
            servicioImputacion.AsintoContableGeneral(asientoDiario);


        }

        private void RegistroIngresoPorCargosVarios(BancoCuentaBancariaModelView modelView)
        {
            var usuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
            DateTime fecha = Convert.ToDateTime(DateTime.Now, new CultureInfo("es-ES"));
            fecha = fecha.AddDays(2);

            // CajaGrupoModel cajaGrupoModel = servicioCajaGrupo.GetGrupoCajaPorId(modelView.IdGrupoCaja);

            modelView.IdBancoCuenta = 1;
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
            asiento.DescripcionMa = "Ingreso Cuenta Bancaria";
            // asiento.Importe = (modelView.IdTipoMoneda == 1) ? (modelView.Importe) : (modelView.Importe * modelView.Cotizacion);
            asiento.Titulo = "Ingreso Cuenta Bancaria";

            //imputacion por grupo caja
            var asientoDiario = servicioContable.InsertAsientoContable("", asiento, cajaGrupoModel.IdImputacion ?? 0);
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

    }






}



