using SAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
using Newtonsoft.Json;
using SAC.Models.Request;

namespace SAC.Controllers
{
    public class ComprasController : BaseController
    {
        private ServicioContable servicioContable = new ServicioContable();
        private ServicioCompra servicioCompra = new ServicioCompra();
        private ServicioTipoComprobante servicioTipoComprobante = new ServicioTipoComprobante();
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioProveedor servicioProveedor = new ServicioProveedor();

        public ComprasController()
        {
            servicioCompra._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }

        // GET: Compras
        public ActionResult FacturaCompras()
        {
            CompraFacturaViewModel model = new CompraFacturaViewModel();
            getTipo(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacturaCompras(CompraFacturaViewModel model)
        {
            //bool hasErrors = ViewData.ModelState.Values.Any(x => x.Errors.Count > 1);
            //foreach (ModelState state in ViewData.ModelState.Values.Where(x => x.Errors.Count > 0))
            //{
            //   servicioCompra._mensaje("noValido", "ok");
            //}


            // validar numero de cactura para el proveedor no debe existir            
            if (servicioCompra.ValidarFacturaPorNroFacturaIdProveedor(model.NumeroFactura, model.IdProveedor))
            {               
                getTipo(model);
                return View("FacturaCompras", model);
            }


            CompraFacturaModel facturaRegistrada = servicioCompra.CreateFactura(Mapper.Map<CompraFacturaViewModel, CompraFacturaModel>(model));
           
           
            //DiarioModel asiento = new DiarioModel();
            //var codigo =servicioContable.GetNuevoCodigoAsiento();
            //asiento.Codigo = codigo;
            //asiento.Fecha = facturaRegistrada.Fecha;
            //asiento.Periodo = DateTime.Now.ToString("yyMM");
            //asiento.Tipo = "CF"; //Compras Facturas
            //asiento.Cotiza = facturaRegistrada.Cotizacion;
            //asiento.Asiento = codigo;
            //asiento.Balance =int.Parse(DateTime.Now.ToString("yyyy"));
            //asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(facturaRegistrada.IdMoneda).Descripcion;
            //asiento.DescripcionMa = "Asiento Ingreso Factura Proveedor";

            //ImputacionModel imputacion = servicioContable.GetImputacionPorAlias("xxx");
            //asiento.IdImputacion = imputacion.Id;           
            //asiento.Importe = (facturaRegistrada.IdMoneda == 1) ? (asiento.Importe = facturaRegistrada.Total ?? 0) :(facturaRegistrada.Total ?? 0 * facturaRegistrada.Cotizacion); 
            //asiento.Descripcion = imputacion.Descripcion;


            //servicioContable.InsertAsientoContable(asiento);


            return RedirectToAction("FacturaCompras");


        }


        private void getTipo(CompraFacturaViewModel model)
        {
            model.TipoMonedas = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
            model.TipoComprobante = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetAllTipoComprobante());        
        }

        [HttpGet()]
        public ActionResult GetListProveedorJson(string term)
        {
            try
            {
                List<ProveedorModel> proveedor = servicioCompra.GetProveedorPorNombre(term);
                var arrayProveedor = (from prov in proveedor
                                      select new AutoCompletarViewModel()
                                      {
                                          id = prov.Id,
                                          label = prov.Nombre
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetProveedorJson(int idProveedor)
        {
            string strJson;
            try
            {
                ProveedorModelView proveedor = Mapper.Map<ProveedorModel, ProveedorModelView>(servicioProveedor.GetProveedor(idProveedor));
                proveedor.ListTipoComprobante = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetTipoComprobantePorTipoIvaProveedor(proveedor.IdTipoIva));
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(proveedor);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
            }
            catch (Exception ex)
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet()]
        public ActionResult GetCotizacionMoneda(int IdMoneda)
        {
            CotizacionAFIP cotizacion = new CotizacionAFIP();
            var f = DateTime.Now;
            string strJson;
            try
            {

                var moneda = servicioTipoMoneda.GetCotizacionPorIdMoneda(f, IdMoneda);
                if (moneda != null)
                {
                    cotizacion.Importe = 100;
                    cotizacion.Fecha = f.ToString("dd/MM/yyyy");
                }
                else
                {
                    // obtener cotizacion de afip y registrar la cotizacion para la fecha
                    cotizacion.Importe = 130;
                    cotizacion.Fecha = f.ToString("dd/MM/yyyy");
                }
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(cotizacion);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
            }
            catch (Exception ex)
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
            }

            return Json(null, JsonRequestBehavior.AllowGet);

        }

        [HttpGet()]
        public ActionResult GetCompraFactura(int idCompraFactura)
        {
            string strJson;
            try
            {
                CompraFacturaModel facturaIva = servicioCompra.GetCompraFacturaIVAPorNro(idCompraFactura);
                if (facturaIva != null)
                {
                    if (facturaIva.CompraIva != null)
                    {
                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(facturaIva.CompraIva);
                        return Json(new { data = strJson, result = true }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { result = false, msj = "No hay Datos de la factura" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { result = false, msj = "La factura NO existe" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, msj = "Ops!, A ocurriodo un error. Contacte al Administrador" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet()]
        public ActionResult GetListCompraFacturaJson(string term)
        {
            try
            {
                List<CompraFacturaModel> factura = servicioCompra.GetAllCompraFacturaPorNro(int.Parse(term));

                var arrayProveedor = (from f in factura
                                      select new AutoCompletarViewModel()
                                      {
                                          id = f.Id,
                                          label = f.NumeroFactura.ToString()
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                servicioCompra._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
        }





    }
}