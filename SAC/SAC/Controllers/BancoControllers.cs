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
        private ServicioCliente oservicioCliente = new ServicioCliente();
        private ServicioCheque  oservicioCheque = new ServicioCheque();
        private ServicioTarjeta oservicioTarjeta = new ServicioTarjeta();     
        private ServicioTarjetaOperacion oservicioTarjetaOperacion = new  ServicioTarjetaOperacion();

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
        public ActionResult Cheques(string Cfechadesde , string Cfechahasta, int Idbanco = 0, int IdCliente = 0)
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
            model.ListaCheque = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oservicioCheque.BuscarCheque(IdCliente,Idbanco, fechaDesde, fechaHasta));

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
        public ActionResult Tarjetas(DateTime Cfechadesde, DateTime Cfechahasta, int IdTipoTarjeta=0)
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
            catch (Exception )
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




    }

  




}



