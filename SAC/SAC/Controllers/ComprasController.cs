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
namespace SAC.Controllers
{
    public class ComprasController : BaseController
    {
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

            model.TipoMonedas = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
            model.TipoComprobante =Mapper.Map<List<TipoComprobanteModel> ,List<TipoComprobanteModelView>>(servicioTipoComprobante.GetAllTipoComprobante());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacturaCompras(CompraFacturaViewModel model)
        {
            // CompraFacturaViewModel model = new CompraFacturaViewModel();
            return View(model);
        }

        [HttpGet()]
        public ActionResult GetListProveedorJson(string term)
        {
            string strJson;
            try
            {                
                List<ProveedorModel> proveedor = servicioCompra.GetProveedorPorNombre(term);
                var arrayProveedor = (from prov in proveedor
                                 select new AutoCompletarViewModel()
                                 {
                                     id = prov.Id,
                                     label = prov.Nombre
                                 }).ToArray();


                //List<AutoCompletarViewModel> auto = new List<AutoCompletarViewModel> { new AutoCompletarViewModel { id = 1, label = "Leon " } };
                //var l = (from prov in auto
                //         select new AutoCompletarViewModel()
                //         {
                //             id = prov.id,
                //             label = prov.label
                //         }).ToArray();

                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                var rJson = Json(new { result = false, tipoError = "Error try " }, JsonRequestBehavior.AllowGet);
                return rJson;
            }

        }
       
        [HttpGet()]
        public ActionResult GetProveedorJson(int idProveedor)
        {
            string strJson;
            try {
                ProveedorModelView proveedor = Mapper.Map<ProveedorModel, ProveedorModelView>(servicioProveedor.GetProveedorPorID(idProveedor));
             
                //ProveedorModelView proveedor = new ProveedorModelView { Id = 1, Nombre = "Responsable Inscripto " };

                proveedor.ListTipoComprobante = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>( servicioTipoComprobante.GetTipoComprobantePorTipoIvaProveedor(proveedor.IdTipoIva));
                //proveedor.ListTipoComprobante = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetTipoComprobantePorTipoIvaProveedor(9));

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(proveedor);
               if ((strJson != null))
                    {
                        var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                        return rJson;
                    }                   
            }
            catch (Exception ex)
            {                
               
            }
           
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        public decimal GetCotizacionMoneda()
        {
            return 100;
        }

        public CompraFacturaViewModel GetFacturaCompra()
        {
            return null;
        }
        

    }
}