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
    public class CuentaCteProveedorController : BaseController
    {

        private ServicioCompra oServicioCompra = new ServicioCompra();
        private ServicioProveedor servicioProveedor = new ServicioProveedor();
        private ServicioCuentaCteProveedor servicioCuentaCteProveedor = new ServicioCuentaCteProveedor();
        private ServicioPresupuestoActual servicioPresupuestoActual = new ServicioPresupuestoActual();

        public CuentaCteProveedorController()
        {
            servicioCuentaCteProveedor._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
            oServicioCompra._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            servicioProveedor._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            servicioCuentaCteProveedor._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
            servicioPresupuestoActual._mensaje += (msg, tipo_) => CrearTempData(msg, tipo_);
        }

        // GET: CuentaCteProveedor
        public ActionResult Index()
        {
                List<CuentaCteProveedorModelView> model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetAllCuentasCteProveedor());
                return View(model);
        }


        public ActionResult PagarFactura(int idProveedor)
        {
            FacturaPagoViewModel model = new FacturaPagoViewModel();
            model.ListaFacturas =  Mapper.Map<List<CompraFacturaModel>, List<CompraFacturaViewModel>>(oServicioCompra.ObtenerPorIDProveedor(idProveedor));                 
            model.Proveedor = Mapper.Map<ProveedorModel,ProveedorModelView>(servicioProveedor.GetProveedor(idProveedor));
           
            List<PresupuestoActualModelView> ListaPresupuesto = Mapper.Map<List<PresupuestoActualModel>, List<PresupuestoActualModelView>>(servicioPresupuestoActual.GetAllPresupuestos());

            List<SelectListItem> ListaPresupuestoActualDrop =  (ListaPresupuesto.Select(x =>
                                                                 new SelectListItem()
                                                                 {
                                                                     Value = x.Id.ToString(),
                                                                     Text = x.Concepto
                                                                 })).ToList();

            model.ListaPresupuestoActual = ListaPresupuestoActualDrop;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacturaPago(FacturaPagoViewModel model)
       {         
            try
            {
                    //agrego el usuario
                    var datosUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.idUsuario = datosUsuario.IdUsuario;
                    //registramos
                    oServicioCompra.RegistrarPago(Mapper.Map<FacturaPagoViewModel, FacturaPagoModel>(model));

                    return RedirectToAction("Index");
               
            }
            catch (Exception ex)
            {
                servicioProveedor._mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return View(model);
            }
            
        }


        [HttpPost]
        public ActionResult CargarCheques()

        {
            //cheques de terceros
            ServicioCheque oServicioCheque = new ServicioCheque();
            List<ChequeModelView> ListaChequesTerceros = Mapper.Map<List<ChequeModel>, List<ChequeModelView>>(oServicioCheque.GetAllCheque());
            //cheques propios
            ServicioChequera oServicioChequera = new ServicioChequera();
            List<ChequeraModelView> ListaChequesPropios = Mapper.Map<List<ChequeraModel>, List<ChequeraModelView>>(oServicioChequera.GetAllChequera());

            FacturaPagoViewModel oChequesModel = new FacturaPagoViewModel();
            oChequesModel.ListaChequesPropios = ListaChequesPropios;
            oChequesModel.ListaChequesTerceros = ListaChequesTerceros;

            return PartialView("_tablaCheques", oChequesModel);

        }

        
        [HttpPost]
        public ActionResult CargarCuentas()
        {
            //cheques de terceros
            ServicioBancoCuenta oServicioCuentaBancaria = new ServicioBancoCuenta();
            List<BancoCuentaModelView> ListaCuentaBancaria = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(oServicioCuentaBancaria.GetAllCuenta());
           
            FacturaPagoViewModel oCompraFacturaModel = new FacturaPagoViewModel();

            List<SelectListItem> retornoListaCuentaBancaria = new List<SelectListItem>();
            retornoListaCuentaBancaria = (ListaCuentaBancaria.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.Banco + x.Descripcion
                                         })).ToList();

            oCompraFacturaModel.listaCuentaBancariaDrop = retornoListaCuentaBancaria;
            return PartialView("_TablaCuentasBancaria", oCompraFacturaModel);
        }


        [HttpPost]
        public ActionResult CargarTarjetas()
        {

            //cheques de terceros
            ServicioTarjeta oServicioTarjeta = new ServicioTarjeta();
            List<TarjetaModelView> ListaCuentaBancaria = Mapper.Map<List<TarjetaModel>, List<TarjetaModelView>>(oServicioTarjeta.GetAllTarjetas());

            FacturaPagoViewModel oCompraFacturaModel = new FacturaPagoViewModel();

            List<SelectListItem> retornoListaTarjetas = new List<SelectListItem>();
            retornoListaTarjetas = (ListaCuentaBancaria.Select(x =>
                                         new SelectListItem()
                                         {
                                             Value = x.Id.ToString(),
                                             Text =  x.Descripcion
                                         })).ToList();

            oCompraFacturaModel.listaTarjetasDrop = retornoListaTarjetas;
            return PartialView("_TablaTarjetas", oCompraFacturaModel);


        }


        //public int AgregarFacturaAPagar(string idFactura)
        //{
        //    try
        //    {
        //        CompraFacturaPagoViewModel model = new CompraFacturaPagoViewModel();

        //        model.FacturasAPagar = Mapper.Map<List<CompraFacturaModel>, List<CompraFacturaViewModel>>(oServicioCompra.ObtenerListaPorID(int.Parse(idFactura)));
        //        return 1;
        //    }
        //    catch(Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        //public int QuitarFacturaAPagar(string idFactura)
        //{
        //    try
        //    {
        //        CompraFacturaPagoViewModel model = new CompraFacturaPagoViewModel();
        //        model.FacturasAPagar.RemoveAll(p => p.Id.Equals(int.Parse(idFactura)));
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}


        public ActionResult Filtrado(string inicio, string fin)
        {
            List<CuentaCteProveedorModelView> model = new List<CuentaCteProveedorModelView>();
            if (inicio == "" || fin == "")
            {
                model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetAllCuentasCteProveedor());
            }
            else
            {
                var dInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var dFin = DateTime.ParseExact(fin, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                model = Mapper.Map<List<CuentaCteProveedorModel>, List<CuentaCteProveedorModelView>>(servicioCuentaCteProveedor.GetAllCuentasCteProveedor(dInicio, dFin));
            }
            return PartialView("_Tabla", model);
            
        }

    }
}