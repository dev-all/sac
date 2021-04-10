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
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Text;

namespace SAC.Controllers
{
    public class FacturaController : BaseController
    {
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioCliente servicioCliente= new ServicioCliente();
        private ServicioClienteDireccion servicioClienteDireccion = new ServicioClienteDireccion();
        private ServicioDepartamento servicioDepartamento = new ServicioDepartamento();
        private ServicioTipoComprobanteVenta servicioTipoComprobanteVenta = new ServicioTipoComprobanteVenta();
       // private ServicioTipoPago servicioTipoPago = new ServicioTipoPago();
        private ServicioBancoCuenta servicioCuentaBancaria = new ServicioBancoCuenta();
        private ServicioTipoComprobante servicioTipoComprobante = new ServicioTipoComprobante();
        private ServicioArticulo servicioArticulo = new ServicioArticulo();

        private ServicioPieNota servicioPieNota = new ServicioPieNota();
        ServicioBancoCuenta servicioBancoCuenta = new ServicioBancoCuenta();
        private ServicioItemImpr servicioItemImpr = new ServicioItemImpr();
        private ServicioDto servicioDto = new ServicioDto();
        private ServicioFacturaVenta servicioFacturaVenta = new ServicioFacturaVenta();
        private ServicioIvaVenta servicioIvaVenta = new ServicioIvaVenta();


        // GET: Factura
        public ActionResult Index()
        {
            FacturaModelView model = new FacturaModelView();

            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());
            List<SelectListItem> lstTipoMoneda = null;
            lstTipoMoneda = (ListaTipoMoneda.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();


        List<SelectListItem> listFormaPago = new List<SelectListItem>();
            listFormaPago.Add(new SelectListItem() { Text = "Contado", Value = "1" });
            listFormaPago.Add(new SelectListItem() { Text = "Tarjeta de Crédito", Value = "68" });
            listFormaPago.Add(new SelectListItem() { Text = "Tarjeta de Débito", Value = "69" });
            listFormaPago.Add(new SelectListItem() { Text = "Cheque", Value = "97" });
            listFormaPago.Add(new SelectListItem() { Text = "Ticket", Value = "98" });
            listFormaPago.Add(new SelectListItem() { Text = "Otra", Value = "99" });
            listFormaPago.Add(new SelectListItem() { Text = "Cuenta Corriente", Value = "96" });
            listFormaPago.Add(new SelectListItem() { Text = "30 días", Value = "93" });
            listFormaPago.Add(new SelectListItem() { Text = "60 días", Value = "94" });
            listFormaPago.Add(new SelectListItem() { Text = "90 días", Value = "95" });


            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Exterior", Value = "2" });
            lst.Add(new SelectListItem() { Text = "Local", Value = "3" });
            model.SelectPuntoVenta = lst;

            List<SelectListItem> lstIdioma = new List<SelectListItem>();
            lstIdioma.Add(new SelectListItem() { Text = "Español", Value = "1" });
            lstIdioma.Add(new SelectListItem() { Text = "Ingles", Value = "2" });
            model.TipoIdioma = lstIdioma;

            List<TipoComprobanteVentaModelView> ListaComprobantes = Mapper.Map<List<TipoComprobanteVentaModel>, List<TipoComprobanteVentaModelView>>(servicioTipoComprobanteVenta.GetAllTipoComprobante());
            List<SelectListItem> lstTipoComprobante=  (ListaComprobantes.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Denominacion
                                  })).ToList();

            List<DepartamentoModelView> ListaDepartamentos = Mapper.Map<List<DepartamentoModel>, List<DepartamentoModelView>>(servicioDepartamento.GetAllDepartamento());
            List<SelectListItem> lstDepartamentos =  (ListaDepartamentos.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            lstDepartamentos.Insert(0, new SelectListItem { Value = "0", Text = "Sin Especificar" });

            List<BancoCuentaModelView> ListaCuentasBancarias = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioCuentaBancaria.GetAllCuenta());
            List<SelectListItem> lstCuentasBancarias = null;
            lstCuentasBancarias = (ListaCuentasBancarias.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.BancoDescripcion
                                  })).ToList();
            lstCuentasBancarias.Insert(0, new SelectListItem { Value = "0", Text = "Sin Especificar" });

            //List<BancoCuentaModelView> ListaCuentasBancarias = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioCuentaBancaria.GetAllCuenta());
            //List<SelectListItem> lstCuentasBancarias = null;
            //lstCuentasBancarias = (ListaCuentasBancarias.Select(x =>
            //                      new SelectListItem()
            //                      {
            //                          Value = x.Id.ToString(),
            //                          Text = x.BancoDescripcion
            //                      })).ToList();

            model.TipoComprobante = lstTipoComprobante;
            model.Departamentos = lstDepartamentos;
            model.TipoMonedas = lstTipoMoneda;
            model.CuentaBancaria = lstCuentasBancarias;
            model.FormaPago = listFormaPago;


            model.ClienteDirecciones = null;
           
            ValorCotizacionModel valorCotizacion = servicioTipoMoneda.GetCotizacionPorIdMoneda(DateTime.Now, 1);
            if (valorCotizacion != null)
            {
                model.Cotizacion = valorCotizacion.Monto;
            }
            else
            {
                model.Cotizacion = 0;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult GrabarFactura(FacturaModelView model)
        {

            var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

            // ClienteModel clienteSeleccionado = servicioCliente.GetClientePorId(model.IdCliente);

            var nroFactura = servicioTipoComprobanteVenta.ObtenerNroFactura(model.idTipoComprobanteSeleccionado, model.IdPuntoVenta);
            int nFactor = 1;
            //11 es factura C
            if (model.IdTipoComprobante != 13)
            {
                nFactor = 1;
            }
            else
            {
                if (model.IdTipoComprobante == 13)
                {
                    nFactor = -1;
                }
            }

            var ListadoItemsFactura = JsonConvert.DeserializeObject<List<ItemImprFacturaModelView>>(model.hdnArticulos);
            foreach (ItemImprFacturaModelView item in ListadoItemsFactura)
            {
                ItemImprModelView itemImpr = new ItemImprModelView();
                itemImpr.IdTipoComprobante = model.idTipoComprobanteSeleccionado;
                itemImpr.PuntoVenta = model.IdPuntoVenta.ToString();
                itemImpr.Factura = model.NumeroFactura;
                itemImpr.Codigo = item.codigo;
                itemImpr.Descripcion = "descripcion";//item.descripcion;
                itemImpr.Precio = item.valor;
                itemImpr.Activo = true;
                itemImpr.Cantidad = 1;
                itemImpr.Factura = nroFactura;
                itemImpr.IdUsuario = OUsuario.IdUsuario;
                itemImpr.UltimaModificacion = DateTime.Now;
                //agrego item
                ItemImprModel itemInsertado = servicioItemImpr.Agregar(Mapper.Map<ItemImprModelView, ItemImprModel>(itemImpr));

                ArticuloModel artModel = servicioArticulo.GetArticuloOuCodigo(item.codigo);
                //agrego, actualizo dto
                DtoModel dtoModel = servicioDto.ActualizarDatosDto(DateTime.Now, itemInsertado, artModel.Id, model.idTipoIva, model.idDepartamento, nFactor, model.idTipoMoneda, model.Cotizacion, artModel);

                //asiento contable
                if (artModel.Tipo.Contains("Gastos"))
                {
                    //generar asiento contable
                }

            }

            //actualiza tabla cotiza

            //genera asiento ventas

            // agrega en tbl FactVenta

            FacturaVentaModel facturaVentaModel = new FacturaVentaModel();
            facturaVentaModel.IdTipoComprobante = model.idTipoComprobanteSeleccionado;
            facturaVentaModel.PuntoVenta = model.IdPuntoVenta.ToString();
            facturaVentaModel.NumeroFactura = model.NumeroFactura;
            facturaVentaModel.Codigo = model.IdCliente.ToString();
            facturaVentaModel.Fecha = model.Fecha;
            facturaVentaModel.Impre = "false";
            facturaVentaModel.Vencimiento = model.Fecha.AddDays(1);
            facturaVentaModel.Concepto = model.EncabezadoFact;
            facturaVentaModel.Condic = "1";
            //facturaVentaModel.IdProvincia =
            //facturaVentaModel.IdPais = model.PaisComp;
            //if (model.idTipoMoneda == 2 ) //faltan campos ej totalDolar
            //{
            //    facturaVentaModel.
            //}
            //facturaVentaModel.Saldo = model.TotalFactura * nfactor;
            facturaVentaModel.TipoIva = model.idTipoIva.ToString();
            facturaVentaModel.Cotiza = model.Cotizacion;
            facturaVentaModel.YRef = model.YREf;
            facturaVentaModel.ORef = model.ORef;
            //facturaVentaModel.Dto // no existe campo
            facturaVentaModel.Diario = "nro dia";
            facturaVentaModel.IdTipoComprobante = model.idTipoComprobanteSeleccionado;
            if (model.IdPuntoVenta == 2)
            {
                facturaVentaModel.TipoFac = "E";
            }
            else
            {
                facturaVentaModel.TipoFac = "L";
            }
            //facturaVentaModel.Periodo = buscar

            FacturaVentaModel FacturaInsertada = servicioFacturaVenta.Agregar(facturaVentaModel);


            //agrega en tbl IvaVenta

            //agrega registro tbl Buque si no es nota credito OJO VER ESTO!!!

            return RedirectToAction("Index");



            //return View(model);
        }




        [HttpGet()]
        public ActionResult GetListClienteJson(string term)
        {
            try
            {
                List<ClienteModel> cliente = servicioCliente.GetClientePorCodigo(term);
                var arrayProveedor = (from cli in cliente
                                      select new AutoCompletarViewModel()
                                      {
                                          id = cli.Id,
                                          label = cli.Nombre
                                         
                                      }).ToArray();
                return Json(arrayProveedor, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }

        [HttpGet()]
        public ActionResult GetClienteJson(int IdCliente)
        {
            string strJson;
            try
            {

                ClienteModelView Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(IdCliente));
                Cliente.ClienteDireccion = Mapper.Map<List<ClienteDireccionModel>, List<ClienteDireccionModelView>>(servicioClienteDireccion.GetDireccionPorcliente(Cliente.Id));


                List<TipoComprobanteModelView> tipoComprobantes = null;
                //ClienteModelView Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(IdCliente));
                //Cliente.ClienteDireccion = Mapper.Map<List<ClienteDireccionModel>, List<ClienteDireccionModelView>>(servicioClienteDireccion.GetDireccionPorcliente(Cliente.Id));
                if (Cliente.IdTipoiva == 4)
                {
                    tipoComprobantes = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetTipoComprobanteExtranjerosVenta());
                }
                if (Cliente.IdTipoiva != 4)
                {
                    if (Cliente.MiPyme == false)
                    {
                        tipoComprobantes = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetTipoComprobanteLocalesVentaSinFactura());
                    }
                    //else
                    //{
                    //    if (factura > 100000)
                    //    {
                    //        //aca busca los tipo cbte 211,212,213
                    //    }
                    //}
                }


                Cliente.ListaComprobantes = tipoComprobantes;

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(Cliente);
                    if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        [HttpGet()]
        public ActionResult GetListCodigoJson(string term)
        {
            try
            {
                List<ArticuloModel> articulos = servicioArticulo.GetArticulosPorCodigo(term);
                var arrayArticulos = (from cli in articulos
                                      select new AutoCompletarViewModel()
                                      {
                                          id = cli.Id,
                                          label = cli.DescripcionCastellano
                                      }).ToArray();
                return Json(arrayArticulos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }


        [HttpGet()]
        public ActionResult GetCodigoJson(int IdArticulo)
        {
            string strJson;
            try
            {

                ArticuloModelView Codigo = Mapper.Map<ArticuloModel, ArticuloModelView>(servicioArticulo.GetArticulo(IdArticulo));

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(Codigo);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        [HttpGet()]
        public ActionResult GetDireccionJson(string idDireccion)
        {
            try
            {
                string strJson;
                ClienteDireccionModelView direccion = Mapper.Map<ClienteDireccionModel, ClienteDireccionModelView>(servicioClienteDireccion.ObtenerPorID(int.Parse(idDireccion)));
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(direccion);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }
              
            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }


        [HttpGet()]
        public ActionResult GetPieNotaJson(string idCodigoCuentaBancaria)
        {
            try
            {
                BancoCuentaModelView cuenta = Mapper.Map<BancoCuentaModel, BancoCuentaModelView>(servicioBancoCuenta.GetCuentaPorId(int.Parse(idCodigoCuentaBancaria)));

                string strJson;
                PieNotaModelView nota = Mapper.Map<PieNotaModel, PieNotaModelView>(servicioPieNota.GetPieNotaPorCodigo(cuenta.Codigo));
                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(nota);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }

            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
            return Json(null, JsonRequestBehavior.AllowGet);

        }


        //[HttpGet()]
        //public ActionResult GetComprobantes(int idTipoIva, bool mipyme, int factura)
        //{
        //    string strJson;
        //    try
        //    {
        //        List<TipoComprobanteModelView> tipoComprobantes = null;
        //        //ClienteModelView Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(IdCliente));
        //        //Cliente.ClienteDireccion = Mapper.Map<List<ClienteDireccionModel>, List<ClienteDireccionModelView>>(servicioClienteDireccion.GetDireccionPorcliente(Cliente.Id));
        //        if(idTipoIva == 4 )
        //        {
        //            tipoComprobantes = Mapper.Map<List<TipoComprobanteModel>,List<TipoComprobanteModelView>>(servicioTipoComprobante.GetTipoComprobanteExtranjerosVenta());
        //        }
        //        if (idTipoIva != 4 )
        //        {
        //            if (mipyme == false)
        //            {
        //                tipoComprobantes = Mapper.Map<List<TipoComprobanteModel>, List<TipoComprobanteModelView>>(servicioTipoComprobante.GetTipoComprobanteLocalesVentaSinFactura());
        //            }
        //            else
        //            {
        //                if (factura > 100000)
        //                {
        //                    //aca busca los tipo cbte 211,212,213
        //                }
        //            }
        //        }

        //        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(tipoComprobantes);
        //        if ((strJson != null))
        //        {
        //            var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
        //            return rJson;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
        //    }
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}


    }
}