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

//tema afip
using SAC.Models.Afip;
using SAC.afip.wswhomo; //ws facturas locales
using SAC.afip.wswhomo_Exportacion; //ws facturas externas

using System.Security.Cryptography.X509Certificates;
using System.Security;
using SAC.Helpers;
using System.Configuration;

namespace SAC.Controllers
{
    public class FacturaController : BaseController
    {
        private ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioCliente servicioCliente = new ServicioCliente();
        private ServicioClienteDireccion servicioClienteDireccion = new ServicioClienteDireccion();
        private ServicioDepartamento servicioDepartamento = new ServicioDepartamento();
        private ServicioTipoComprobanteVenta servicioTipoComprobanteVenta = new ServicioTipoComprobanteVenta();
        // private ServicioTipoPago servicioTipoPago = new ServicioTipoPago();
        // private ServicioBancoCuenta servicioCuentaBancaria = new ServicioBancoCuenta();
        private ServicioTipoComprobante servicioTipoComprobante = new ServicioTipoComprobante();
        private ServicioArticulo servicioArticulo = new ServicioArticulo();

        private ServicioPieNota servicioPieNota = new ServicioPieNota();
        private ServicioBancoCuenta servicioBancoCuenta = new ServicioBancoCuenta();
        private ServicioItemImpr servicioItemImpr = new ServicioItemImpr();
        private ServicioDto servicioDto = new ServicioDto();
        private ServicioFacturaVenta servicioFacturaVenta = new ServicioFacturaVenta();
        private ServicioIvaVenta servicioIvaVenta = new ServicioIvaVenta();
        private ServicioBuque servicioBuque = new ServicioBuque();
        private ServicioCotiza servicioCotiza = new ServicioCotiza();
        private ServicioContable servicioContable = new ServicioContable();
        private ServicioImputacion servicioImputacion = new ServicioImputacion();

        private ServicioAfip_TicketAcceso servicioAfip_TicketAcceso = new ServicioAfip_TicketAcceso();

        private ServicioFacturaVentaItems servicioFacturaVentaItems = new ServicioFacturaVentaItems();
        private ServicioFacturaElectronica servicioFacturaElectronica = new ServicioFacturaElectronica();

       private AfipHelper afipHelper = new AfipHelper();
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
            List<SelectListItem> lstTipoComprobante = (ListaComprobantes.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Denominacion
                                  })).ToList();

            List<DepartamentoModelView> ListaDepartamentos = Mapper.Map<List<DepartamentoModel>, List<DepartamentoModelView>>(servicioDepartamento.GetAllDepartamento());
            List<SelectListItem> lstDepartamentos = (ListaDepartamentos.Select(x =>
                                 new SelectListItem()
                                 {
                                     Value = x.Id.ToString(),
                                     Text = x.Descripcion
                                 })).ToList();
            lstDepartamentos.Insert(0, new SelectListItem { Value = "0", Text = "Sin Especificar" });

            List<BancoCuentaModelView> ListaCuentasBancarias = Mapper.Map<List<BancoCuentaModel>, List<BancoCuentaModelView>>(servicioBancoCuenta.GetAllCuenta());
            List<SelectListItem> lstCuentasBancarias = null;
            lstCuentasBancarias = (ListaCuentasBancarias.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.BancoDescripcion
                                  })).ToList();
            lstCuentasBancarias.Insert(0, new SelectListItem { Value = "0", Text = "Sin Especificar" });



            model.TipoComprobante = lstTipoComprobante;
            model.Departamentos = lstDepartamentos;
            model.TipoMonedas = lstTipoMoneda;
            model.CuentaBancaria = lstCuentasBancarias;
            model.FormaPago = listFormaPago;
            model.ClienteDirecciones = null;
            model.Fecha = DateTime.Now;

            ValorCotizacionModel valorCotizacion = servicioTipoMoneda.GetCotizacionPorIdMoneda(DateTime.Now, 1);
            if (valorCotizacion != null)
            {
                model.Cotizacion = valorCotizacion.Monto;
            }
            else
            {
                model.Cotizacion = 1;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GrabarFactura(FacturaModelView model)
        {
            var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

            ClienteModel cliente = servicioCliente.GetClientePorId(model.IdCliente);

            //se verifica que tipo de comprobante se selecciono
            string tipoComprobante = "";
            string tipoComprobanteAbreviado = "";

            switch (model.idTipoComprobanteSeleccionado)
            {
                case 1:
                    tipoComprobante = "Factura";
                    tipoComprobanteAbreviado = "F";

                    break;
                case 2:
                    tipoComprobante = "Debito";
                    tipoComprobanteAbreviado = "D";
                    break;
                case 3:
                    tipoComprobante = "Credito";
                    tipoComprobanteAbreviado = "C";
                    break;
            }

            //seteo la factura en cero sino es como sabana corta!!!
            int comprobanteActualizado = DeterminarNroComprovante(model.idTipoIva, model.mipyme, model.TotalFactura, tipoComprobante);

            var cbt = servicioTipoComprobanteVenta.getTipoComprobanteVentaNewNumeroFactura(comprobanteActualizado, model.IdPuntoVenta);



            int nFactor = 1;
            if (tipoComprobante != "Credito")
            {
                nFactor = 1;
            }
            else
            {
                if (tipoComprobante == "Credito")
                {
                    nFactor = -1;
                }
            }

            decimal totalGastosPesos = 0; // ver esto porque deberia ser el acumulado de gastos
            decimal totalGastosDolares = 0;
            //tengo que controlar el tipo de comprobante que viene solo para la factura son los items


            //preparo e inserto la factura electronica en base datos + ws
            FECAEResponse RetornoAfip = new FECAEResponse();

            switch (model.idTipoComprobanteSeleccionado)
            {
                case 1: //factura 

                    string cae = "";
                    string mensajeErrorAfip = "";
                    string mensajeObservacionesAfip = "";
                    string ResultadoAfip = "";

                    //pregunto si la carga de la factura es manual, si es asi no inserta la factura electronica
                    if (model.FacturaManual == false)
                    {
                        //insertar la factura
                        //agrego mi cuit porque da error
                        long cuitOriginador = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());

                        if (cliente.TipoCliente.Id == 2 ) // extranjero
                        {
                            if (cbt.CodigoAfip == 19)//factura E
                            {
                                //verificar en la base si el token esta vencido 
                                Afip_TicketAccesoModel login;
                                AfipHelper afipHelper = new AfipHelper();
                                login = afipHelper.VerificarTicketAcceso("wsfex");

                                ClaseLoginAfip ClaseLogin = null;
                                if (login == null)
                                {
                                    //busca el token nuevo y graba en la bd
                                    ClaseLogin = afipHelper.ObtenerTicketAccesoWS("wsfex", OUsuario.IdUsuario);
                                }
                                else
                                {
                                    //usa el token de la base
                                    ClaseLogin = afipHelper.ObtenerTicketAccesoSinWS("wsfex", OUsuario.IdUsuario);
                                    ClaseLogin.Token = login.token;
                                    ClaseLogin.Sign = login.sing;
                                }

                               
                                var a = InsertarFacturaAfipExterior(ClaseLogin, model, cbt, cuitOriginador);
                                if (a!= null)
                                {
                                    if (a.FEXErr.ErrMsg == "OK")
                                    {
                                        ResultadoAfip = "A";
                                        cae = a.FEXResultAuth.Cae;
                                    }
                                    else
                                    {
                                        ResultadoAfip = a.FEXErr.ErrCode.ToString();
                                        mensajeErrorAfip = a.FEXErr.ErrMsg;
                                        servicioFacturaVenta._mensaje?.Invoke("Ops!, Notificacion de Afip: " + mensajeErrorAfip + "("+ ResultadoAfip + ")", "error");
                                        return RedirectToAction("Index");
                                    }
                                }
                                else
                                {
                                    //mensaje que no genero la factura electronica
                                    servicioFacturaVenta._mensaje?.Invoke("Ops!, El servicio de AFIP no responde...", "error");
                                    return RedirectToAction("Index");
                                }
                            }
                        }
                        else //local
                        {
                            //verificar en la base si el token esta vencido 
                            Afip_TicketAccesoModel login;
                           
                            login = afipHelper.VerificarTicketAcceso("wsfe");

                            ClaseLoginAfip ClaseLogin = null;
                            if (login == null)
                            {
                                //busca el token nuevo y graba en la bd
                                ClaseLogin = afipHelper.ObtenerTicketAccesoWS("wsfe", OUsuario.IdUsuario);
                            }
                            else
                            {
                                //usa el token de la base
                                ClaseLogin = afipHelper.ObtenerTicketAccesoSinWS("wsfe", OUsuario.IdUsuario);
                                ClaseLogin.Token = login.token;
                                ClaseLogin.Sign = login.sing;
                            }

                            RetornoAfip = InsertarFacturaAfipLocal(ClaseLogin, model, cbt.CodigoAfip, cuitOriginador);

                            if (RetornoAfip != null)
                            {
                                if (RetornoAfip.Errors != null)
                                {
                                    //mostrar mensaje error;
                                    foreach (var er in RetornoAfip.Errors)
                                    {
                                        mensajeErrorAfip += string.Format("Er: {0}: {1}", er.Code, er.Msg);
                                    }
                                }
                                else
                                {
                                    if (RetornoAfip.FeDetResp[0].Observaciones != null && RetornoAfip.FeDetResp[0].Resultado != "A")
                                    {
                                        foreach (var obs in RetornoAfip.FeDetResp[0].Observaciones)
                                        {
                                            mensajeErrorAfip += string.Format("Er: {0}: {1}", obs.Msg, obs.Code);
                                        }
                                    }
                                    else
                                    {
                                        ResultadoAfip = RetornoAfip.FeDetResp[0].Resultado;
                                        if (RetornoAfip.FeDetResp[0].Resultado == "A")
                                        {
                                            cae = RetornoAfip.FeDetResp[0].CAE;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                servicioFacturaVenta._mensaje?.Invoke("Ops!, El servicio de AFIP no responde...", "error");
                                return RedirectToAction("Index");
                            }
                        }
                       
                    }
                                        
                    String moneda = "";
                    if (model.idTipoMoneda == 1)
                    {
                        moneda = "PES";
                    }
                    if (model.idTipoMoneda == 2)
                    {
                        moneda = "DOL";
                    }

                    FacturaElectronicaModel FacturaElectronica = new FacturaElectronicaModel();
                    FacturaElectronica.ALICUOTA = "0";
                    FacturaElectronica.CAE = cae;
                    FacturaElectronica.CATEGORIA = "Exento";
                    FacturaElectronica.CODBARRA = "";
                    FacturaElectronica.NRODOC = model.Cuit;
                    FacturaElectronica.TIPOSERV = 2;
                    FacturaElectronica.NOMBRE = model.NombreComp;
                    FacturaElectronica.CODCLI = model.CodigoCliente;
                    FacturaElectronica.CODPAIS = model.CodPaisAfip.ToString();
                    FacturaElectronica.COTIZA = 0;//obtener cotizacion
                    FacturaElectronica.DOMICILIO = model.DireccionCompuesta;
                    FacturaElectronica.ESTADO = "2";
                    FacturaElectronica.FDESDE = model.Fecha;
                    FacturaElectronica.FECHACBTE = model.Fecha;
                    FacturaElectronica.FECHAVEN = model.Fecha;
                    FacturaElectronica.FECHAVTO = model.Fecha.AddDays(180);
                    FacturaElectronica.FHASTA = null;
                    FacturaElectronica.FORMAPAGO = model.IdTipoPago.ToString();
                    FacturaElectronica.TOTAL = model.TotalFactura;
                    FacturaElectronica.NETO = model.TotalFactura;
                    FacturaElectronica.NROCBTE = comprobanteActualizado;
                    FacturaElectronica.PUNTOVTA = model.IdPuntoVenta;
                    FacturaElectronica.TIPOCBTE = cbt.Id; //traigo el tipo cbte
                    FacturaElectronica.RESULTADO = ResultadoAfip;
                    FacturaElectronica.OBS = mensajeErrorAfip;
                    FacturaElectronica.IVA = 0;
                    FacturaElectronica.IDIVA = "3";
                    FacturaElectronica.TIPODOC = 80;
                    FacturaElectronica.IVA10 = 0;
                    FacturaElectronica.NETO10 = 0;
                    FacturaElectronica.IDMONEDA = moneda;

                    FacturaElectronicaModel FacturaElectronicaInsertada = servicioFacturaElectronica.Agregar(FacturaElectronica);


                    //actualiza tabla cotiza ?? nose
                    //List<FacturaVentaModel> ListaFactura = servicioFacturaVenta.GetAllFacturaVentaCliente(model.IdCliente);

                    // agrega en tbl FactVenta
                    FacturaVentaModel facturaVentaModel = new FacturaVentaModel();
                    facturaVentaModel.IdTipoComprobante = cbt.Id; //model.idTipoComprobanteSeleccionado;  // facturaVentaModel. = model.IdPuntoVenta.ToString();
                    facturaVentaModel.IdFacturaElectronica = FacturaElectronicaInsertada.ID;
                    facturaVentaModel.NumeroFactura = cbt.Numero;
                    facturaVentaModel.Codigo = model.CodigoCliente;
                    facturaVentaModel.IdCliente = model.IdCliente;
                    facturaVentaModel.Fecha = DateTime.Now; //model.Fecha;
                    facturaVentaModel.Impre = "true";
                    facturaVentaModel.Vencimiento = DateTime.Now.AddDays(1);
                    facturaVentaModel.Concepto = model.EncabezadoFact;
                    facturaVentaModel.Condicion = "1";
                    facturaVentaModel.IdProvincia = model.idProvincia;
                    facturaVentaModel.IdPais = model.idPais;

                    //Monto de factura
                    decimal TotalFactura = model.TotalFactura * nFactor;
                    if (model.idTipoMoneda == 2)
                    {
                        facturaVentaModel.TotalDolares = TotalFactura;
                    }
                    facturaVentaModel.Total = model.TotalFactura * nFactor;
                    facturaVentaModel.Saldo = model.TotalFactura * nFactor;
                    facturaVentaModel.TipoIva = model.idTipoIva.ToString();
                    facturaVentaModel.Cotiza = model.Cotizacion;
                    facturaVentaModel.YRef = model.YREf;
                    facturaVentaModel.ORef = model.ORef;
                    //facturaVentaModel.Dto // no existe campo                
                    // facturaVentaModel.IdTipoComprobante = model.idTipoComprobanteSeleccionado;

                    //lo agrego asi por que el documento no explica a q tipo de cbante le da esas letras
                    //seteo arriba
                    facturaVentaModel.Tipo = tipoComprobanteAbreviado;

                    if (model.IdPuntoVenta == 2)
                    {
                        facturaVentaModel.TipoFac = "E";
                    }
                    else
                    {
                        facturaVentaModel.TipoFac = "L";
                    }
                    facturaVentaModel.Periodo = int.Parse(DateTime.Now.ToString("yyMM"));
                    //datos obligatorios
                    facturaVentaModel.Baja = "*";
                    facturaVentaModel.IdImputacion = 0;
                    facturaVentaModel.NumeroCobro = 0;
                    facturaVentaModel.IdMoneda = model.idTipoMoneda;
                    facturaVentaModel.Descuento = "1"; //no se de donde sale
                    facturaVentaModel.Recibo = "0";
                    facturaVentaModel.NumeroTra = "0"; //no se de donde sale
                    facturaVentaModel.Anula = "0";
                    facturaVentaModel.Activo = true;
                    facturaVentaModel.IdUsuario = OUsuario.IdUsuario;
                    facturaVentaModel.UltimaModificacion = DateTime.Now;
                    facturaVentaModel.TipoMoneda = null;
                    facturaVentaModel.TipoComprobanteVenta = null;
                    facturaVentaModel.FactVentaCobro = null;
                    facturaVentaModel.ItemImpre = null;
                    facturaVentaModel.Retencion = null;

                    // add codigo al cbte del pago  y utilizar el mismo para todos los asientos de pago                  
                    var CodigoAsiento = servicioContable.GetNuevoCodigoAsiento() + 1;
                    facturaVentaModel.CodigoDiario = CodigoAsiento;

                    FacturaVentaModel FacturaInsertada = servicioFacturaVenta.Agregar(facturaVentaModel);


                    //inserto items de la factura
                    var ListadoItemsFactura = JsonConvert.DeserializeObject<List<ItemImprFacturaModelView>>(model.hdnArticulos);
                    foreach (ItemImprFacturaModelView item in ListadoItemsFactura)
                    {
                        ItemImprModelView itemImpr = new ItemImprModelView();
                        itemImpr.IdTipoComprobante = cbt.Id;
                        itemImpr.PuntoVenta = model.IdPuntoVenta.ToString();
                        itemImpr.IdFactVenta = FacturaInsertada.Id;
                        itemImpr.Factura = cbt.Numero;
                        itemImpr.Codigo = item.codigo;
                        itemImpr.Descripcion = "descripcion";//item.descripcion;
                        itemImpr.Precio = item.valor;
                        itemImpr.Activo = true;
                        itemImpr.Cantidad = 1;
                        itemImpr.Factura = cbt.Numero;
                        itemImpr.IdUsuario = OUsuario.IdUsuario;
                        itemImpr.UltimaModificacion = DateTime.Now;
                        //agrego item
                        ItemImprModel itemInsertado = servicioItemImpr.Agregar(Mapper.Map<ItemImprModelView, ItemImprModel>(itemImpr));

                        ArticuloModel artModel = servicioArticulo.GetArticuloOuCodigo(item.codigo);
                        //agrego, actualizo dto
                        DtoModel dtoModel = servicioDto.ActualizarDatosDto(DateTime.Now, itemInsertado, artModel.Codigo, model.idTipoIva, model.idDepartamento, nFactor, model.idTipoMoneda, model.Cotizacion, artModel, OUsuario);

                        //asiento contable
                        if (artModel.Tipo.Contains("Gastos"))
                        {
                            if (FacturaInsertada.IdMoneda == 2)
                            {
                                totalGastosPesos += (item.valor * model.Cotizacion) * nFactor;
                            }
                            else {
                                totalGastosPesos += item.valor * nFactor;
                            }
                        }
                    }


                    var ImportePesos = (FacturaInsertada.IdMoneda == 1) ? (FacturaInsertada.Total * nFactor) : (FacturaInsertada.TotalDolares * FacturaInsertada.Cotiza * nFactor);
                    /// asientos de ventas
                    if (FacturaInsertada != null)
                    {
                        DiarioModel asiento = new DiarioModel();
                        asiento.Codigo = FacturaInsertada.CodigoDiario;
                        asiento.Fecha = FacturaInsertada.Fecha;
                        asiento.Periodo = DateTime.Now.ToString("yyMM");
                        asiento.Tipo = "VF";
                        asiento.Cotiza = FacturaInsertada.Cotiza;
                        asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
                        asiento.Moneda = servicioTipoMoneda.GetTipoMoneda(FacturaInsertada.IdMoneda).Descripcion;
                        asiento.Descripcion = "Deudores por Ventas Cliente " + FacturaInsertada.NumeroFactura;
                        asiento.DescripcionMa = "Asiento de Factura Venta " + FacturaInsertada.NumeroFactura;
                        asiento.Titulo = "Asiento de Venta";
                        if (model.idTipoIva == 4) // exterior
                        {
                            // 1 
                            asiento.Importe = (FacturaInsertada.IdMoneda == 1) ? (FacturaInsertada.Total) : ((FacturaInsertada.TotalDolares * FacturaInsertada.Cotiza));
                            var asientoVEXT = servicioContable.InsertAsientoContable("VEXT", asiento, 0);
                            if (asientoVEXT != null) { servicioImputacion.AsintoContableGeneral(asientoVEXT); }

                            //2
                            asiento.Descripcion = "Servicios";
                            asiento.Importe = -(ImportePesos - totalGastosPesos);
                            if (asiento.Importe != 0)
                            {
                                var asientoSEXT = servicioContable.InsertAsientoContable("SEXT", asiento, 0);
                                if (asientoSEXT != null) { servicioImputacion.AsintoContableGeneral(asientoSEXT); }
                            }

                            //3 totalGastosPesos
                            asiento.Importe = -totalGastosPesos;
                            if (asiento.Importe != 0)
                            {
                                asiento.Descripcion = "Recupero de Gastos";
                                var asientoGastos = servicioContable.InsertAsientoContable("VGAS", asiento, 0);
                                if (asientoGastos != null) { servicioImputacion.AsintoContableGeneral(asientoGastos); }
                            }

                        }
                        else //local
                        {
                            // 1 
                            asiento.Importe = (FacturaInsertada.IdMoneda == 1) ? (FacturaInsertada.Total) : ((FacturaInsertada.TotalDolares * FacturaInsertada.Cotiza));
                            var asientoVEXT = servicioContable.InsertAsientoContable("VLOC", asiento, 0);
                            if (asientoVEXT != null) { servicioImputacion.AsintoContableGeneral(asientoVEXT); }
                            //2
                            asiento.Descripcion = "Servicios";
                            asiento.Importe = -(ImportePesos - totalGastosPesos);
                            var asientoSEXT = servicioContable.InsertAsientoContable("SLOC", asiento, 0);
                            if (asientoSEXT != null) { servicioImputacion.AsintoContableGeneral(asientoSEXT); }

                            //3 totalGastosPesos
                            asiento.Descripcion = "Recupero de Gastos";
                            asiento.Importe = -totalGastosPesos;
                            var asientoGastos = servicioContable.InsertAsientoContable("VGAS", asiento, 0);
                            if (asientoGastos != null) { servicioImputacion.AsintoContableGeneral(asientoSEXT); }

                            //GrabaDiario(0, 'VLOC', nImporte, Rlmisce: NroDia, oItem, 'VF',, Moneda, Cotiza, cDescripMa)
                            //        GrabaDiario(0, 'SLOC', -(nImporte - nTotGasto), Rlmisce: NroDia, oItem, 'VF',, Moneda, Cotiza, cDescripMa)

                        }

                    }

                    //agrega en tbl IvaVenta
                    IvaVentaModel ivaVenta = new IvaVentaModel();
                    ivaVenta.IdTipoComprobantes = cbt.Id;
                    ivaVenta.PuntoVenta = model.IdPuntoVenta.ToString();
                    ivaVenta.NumeroFactura = cbt.Numero;
                    ivaVenta.NroEmp = model.IdCliente;
                    ivaVenta.NomEmp = model.CodigoCliente;
                    ivaVenta.IdImputacion = model.idImputacion.ToString();
                    ivaVenta.Fecha = DateTime.Now;
                    ivaVenta.Periodo = DateTime.Now.ToString("yyMM");
                    ivaVenta.Neto = ImportePesos - totalGastosPesos;
                    ivaVenta.Total = ImportePesos;
                    ivaVenta.Gasto = totalGastosPesos;
                    ivaVenta.Isib = 0; // no se q es
                    ivaVenta.Moneda = model.idTipoMoneda.ToString();
                    ivaVenta.TipoIva = model.idTipoIva.ToString();
                    ivaVenta.Dolar = model.Cotizacion;
                    ivaVenta.Activo = true;
                    ivaVenta.IdUsuario = OUsuario.IdUsuario;
                    ivaVenta.UltimaModificacion = DateTime.Now;
                    ivaVenta.AuxiliarNumero = "0";
                    ivaVenta.Diario = FacturaInsertada.CodigoDiario.ToString();

                    if (model.IdPuntoVenta == 2)
                    {
                        ivaVenta.TipoFac = "E";
                    }
                    else
                    {
                        ivaVenta.TipoFac = "L";
                    }
                    ivaVenta.Cuit = model.Cuit.ToString();
                    ivaVenta.Clase = tipoComprobanteAbreviado;

                    IvaVentaModel ivaModelInsertado = servicioIvaVenta.Agregar(ivaVenta);

                    //agrega registro tbl Buque si no es nota credito!!!

                    BuqueModel buqueModel = new BuqueModel();
                    buqueModel.NumeroFactura = cbt.Numero;
                    buqueModel.Cliente = model.IdCliente.ToString();
                    buqueModel.Fecha = DateTime.Now;
                    buqueModel.Buque1 = model.EncabezadoFact;
                    buqueModel.Monto = model.TotalFactura;
                    buqueModel.Descripcion = model.EncabezadoFact;
                    buqueModel.YRef = model.YREf;
                    buqueModel.ORef = model.ORef;
                    buqueModel.Carpeta = model.nroCarpera.ToString();
                    buqueModel.Legajo = model.nroCarperaFinal.ToString();
                    buqueModel.Activo = true;
                    buqueModel.IdUsuario = OUsuario.IdUsuario;
                    buqueModel.UltimaModificacion = DateTime.Now;
                    BuqueModel buqueModelInsertado = servicioBuque.Agregar(buqueModel);

                    break;

                //------------------------------------------------------------------
                case 2: //nota debito

                    //factura nota debito ws afip
                    if (model.FacturaManual == false)
                    {
                        //verificar en la base si el token esta vencido 
                        Afip_TicketAccesoModel login;
                        AfipHelper afipHelper = new AfipHelper();

                        login = afipHelper.VerificarTicketAcceso("wsfe");

                        ClaseLoginAfip ClaseLogin = null;
                        if (login == null)
                        {
                            //busca el token nuevo y graba en la bd
                            ClaseLogin = afipHelper.ObtenerTicketAccesoWS("wsfe", OUsuario.IdUsuario);
                        }
                        else
                        {
                            //usa el token de la base
                            ClaseLogin = afipHelper.ObtenerTicketAccesoSinWS("wsfe", OUsuario.IdUsuario);
                            ClaseLogin.Token = login.token;
                            ClaseLogin.Sign = login.sing;
                        }
                        //insertar la factura
                        //agrego mi cuit porque da error
                        long CuitP = long.Parse(ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
                        if (cbt.CodigoAfip == 20)//factura E
                        {
                            //RetornoAfip = InsertarNotaDebitoAfipExterior(ClaseLogin, model, cbt.CodigoAfip, CuitP);
                        }
                        else
                        {
                            RetornoAfip = InsertarNotaDebitoAfipLocal(ClaseLogin, model, cbt.CodigoAfip, CuitP);
                        }
                       

                    }
                    //afip 

                    string mensajeErrorAfipNd = "";
                    string mensajeObservacionesAfipNd = "";
                    string caeNd = "";
                    string ResultadoAfipNd = "";
                    if (RetornoAfip != null)
                    {
                        if (RetornoAfip.Errors != null)
                        {
                            //mostrar mensaje error;
                            foreach (var er in RetornoAfip.Errors)
                            {
                                mensajeErrorAfipNd += string.Format("Er: {0}: {1}", er.Code, er.Msg);
                            }

                            if (RetornoAfip.FeDetResp[0].Observaciones != null)
                            {
                                foreach (var obs in RetornoAfip.FeDetResp[0].Observaciones)
                                {
                                    mensajeObservacionesAfipNd += string.Format("Er: {0}: {1}", obs.Msg, obs.Code);
                                }
                            }
                        }
                        else
                        {
                            ResultadoAfipNd = RetornoAfip.FeDetResp[0].Resultado;
                            if (RetornoAfip.FeDetResp[0].Resultado == "A")
                            {
                                caeNd = RetornoAfip.FeDetResp[0].CAE;
                            }
                        }
                    }
                    else
                    {
                        //mensaje que no grabo en afip por error
                    }


                    // agrega en tbl FactVenta
                    FacturaVentaModel facturaModelNotaDebito = new FacturaVentaModel();
                    FacturaVentaModel facturaVentaOriginal = servicioFacturaVenta.GetFacturaVentaPorNumero(int.Parse(model.AplicaNC), model.IdCliente);
                    facturaModelNotaDebito.IdTipoComprobante = cbt.Id; //model.idTipoComprobanteSeleccionado;  // facturaVentaModel. = model.IdPuntoVenta.ToString();
                    facturaModelNotaDebito.NumeroFactura = cbt.Numero;
                    facturaModelNotaDebito.Codigo = facturaVentaOriginal.Codigo;
                    facturaModelNotaDebito.IdCliente = facturaVentaOriginal.IdCliente;
                    facturaModelNotaDebito.IdMoneda = model.idTipoMoneda;
                    facturaModelNotaDebito.Fecha = DateTime.Now; //model.Fecha;
                    facturaModelNotaDebito.Impre = "false";
                    facturaModelNotaDebito.Vencimiento = DateTime.Now.AddDays(1);
                    facturaModelNotaDebito.Concepto = "Nota Debito";
                    facturaModelNotaDebito.Condicion = "1";
                    facturaModelNotaDebito.IdProvincia = facturaVentaOriginal.IdProvincia;
                    facturaModelNotaDebito.IdPais = facturaVentaOriginal.IdPais;
                    if (model.idTipoMoneda == 2) //faltan campos ej totalDolar
                    {
                        facturaModelNotaDebito.TotalDolares = model.MontoAjuste * nFactor;
                        facturaModelNotaDebito.Saldo = (facturaVentaOriginal.TotalDolares - model.MontoAjuste) * nFactor;
                    }
                    else
                    {
                        facturaModelNotaDebito.Total = model.MontoAjuste * nFactor;
                        facturaModelNotaDebito.Saldo = (facturaVentaOriginal.Total - model.MontoAjuste) * nFactor;
                    }

                    facturaModelNotaDebito.TipoIva = facturaVentaOriginal.TipoIva.ToString();
                    facturaModelNotaDebito.Cotiza = facturaVentaOriginal.CotizaP;
                    facturaModelNotaDebito.YRef = facturaVentaOriginal.YRef;
                    facturaModelNotaDebito.ORef = facturaVentaOriginal.ORef;
                    facturaModelNotaDebito.Tipo = tipoComprobanteAbreviado;

                    if (model.IdPuntoVenta == 2)
                    {
                        facturaModelNotaDebito.TipoFac = "E";
                    }
                    else
                    {
                        facturaModelNotaDebito.TipoFac = "L";
                    }
                    facturaModelNotaDebito.Periodo = int.Parse(DateTime.Now.ToString("yyMM"));
                    //datos obligatorios
                    facturaModelNotaDebito.Baja = "*";
                    facturaModelNotaDebito.IdImputacion = 0;
                    facturaModelNotaDebito.NumeroCobro = 0;
                    //facturaVentaModel.Moneda = model.idTipoMoneda.ToString();
                    facturaModelNotaDebito.Descuento = "1"; //no se de donde sale
                    facturaModelNotaDebito.Recibo = "0";
                    facturaModelNotaDebito.NumeroTra = "0"; //no se de donde sale
                    facturaModelNotaDebito.Anula = facturaVentaOriginal.NumeroFactura.ToString();
                    facturaModelNotaDebito.Activo = true;
                    facturaModelNotaDebito.IdUsuario = OUsuario.IdUsuario;
                    facturaModelNotaDebito.UltimaModificacion = DateTime.Now;

                    FacturaVentaModel FacNotaDebitoInsertada = servicioFacturaVenta.Agregar(facturaModelNotaDebito);

                    break;
                case 3: //nota credito
                        // agrega en tbl FactVenta
                    FacturaVentaModel facturaModelNotaCredito = new FacturaVentaModel();
                    FacturaVentaModel facturaVentaOriginal1 = servicioFacturaVenta.GetFacturaVentaPorNumero(int.Parse(model.AplicaNC), model.IdCliente);
                    facturaModelNotaCredito.IdTipoComprobante = cbt.Id; //model.idTipoComprobanteSeleccionado;
                                                                        // facturaVentaModel. = model.IdPuntoVenta.ToString();
                    facturaModelNotaCredito.NumeroFactura = cbt.Numero;
                    facturaModelNotaCredito.Codigo = facturaVentaOriginal1.Codigo;
                    facturaModelNotaCredito.IdCliente = facturaVentaOriginal1.IdCliente;
                    facturaModelNotaCredito.IdMoneda = model.idTipoMoneda;
                    facturaModelNotaCredito.Fecha = DateTime.Now; //model.Fecha;
                    facturaModelNotaCredito.Impre = "false";
                    facturaModelNotaCredito.Vencimiento = DateTime.Now.AddDays(1);
                    facturaModelNotaCredito.Concepto = "Nota Credito";
                    facturaModelNotaCredito.Condicion = "1";
                    facturaModelNotaCredito.IdProvincia = facturaVentaOriginal1.IdProvincia;
                    facturaModelNotaCredito.IdPais = facturaVentaOriginal1.IdPais;
                    if (model.idTipoMoneda == 2) //faltan campos ej totalDolar
                    {
                        facturaModelNotaCredito.TotalDolares = model.MontoAjuste * nFactor;
                        facturaModelNotaCredito.Saldo = (facturaVentaOriginal1.TotalDolares - model.MontoAjuste) * nFactor;
                    }
                    else
                    {
                        facturaModelNotaCredito.Total = model.MontoAjuste * nFactor;
                        facturaModelNotaCredito.Saldo = (facturaVentaOriginal1.Total - model.MontoAjuste) * nFactor;
                    }

                    facturaModelNotaCredito.TipoIva = facturaVentaOriginal1.TipoIva.ToString();
                    facturaModelNotaCredito.Cotiza = facturaVentaOriginal1.CotizaP;
                    facturaModelNotaCredito.YRef = facturaVentaOriginal1.YRef;
                    facturaModelNotaCredito.ORef = facturaVentaOriginal1.ORef;
                    facturaModelNotaCredito.Tipo = tipoComprobanteAbreviado;

                    if (model.IdPuntoVenta == 2)
                    {
                        facturaModelNotaCredito.TipoFac = "E";
                    }
                    else
                    {
                        facturaModelNotaCredito.TipoFac = "L";
                    }
                    facturaModelNotaCredito.Periodo = int.Parse(DateTime.Now.ToString("yyMM"));
                    //datos obligatorios
                    facturaModelNotaCredito.Baja = "*";
                    facturaModelNotaCredito.IdImputacion = 0;
                    facturaModelNotaCredito.NumeroCobro = 0;
                    //facturaVentaModel.Moneda = model.idTipoMoneda.ToString();
                    facturaModelNotaCredito.Descuento = "1"; //no se de donde sale
                    facturaModelNotaCredito.Recibo = "0";
                    facturaModelNotaCredito.NumeroTra = "0"; //no se de donde sale
                    facturaModelNotaCredito.Anula = facturaVentaOriginal1.NumeroFactura.ToString();
                    facturaModelNotaCredito.Activo = true;
                    facturaModelNotaCredito.IdUsuario = OUsuario.IdUsuario;
                    facturaModelNotaCredito.UltimaModificacion = DateTime.Now;

                    FacturaVentaModel FacNotaCreditoInsertada = servicioFacturaVenta.Agregar(facturaModelNotaCredito);

                    break;
            }

            //   var FacturaActuralizada = servicioTipoComprobanteVenta.ActualizarNroFactura(comprobanteActualizado, model.IdPuntoVenta, nroFactura);

            return RedirectToAction("Index");


        }


        public FECAEResponse InsertarNotaDebitoAfipLocal(ClaseLoginAfip TicketAcceso, FacturaModelView model, int NroComprobante, long cuitPropietario)
        {

            FEAuthRequest Autenticacion = new FEAuthRequest();
            Autenticacion.Cuit = cuitPropietario;//long.Parse(model.Cuit);
            Autenticacion.Sign = TicketAcceso.Sign;
            Autenticacion.Token = TicketAcceso.Token;

            //se prepara el servicio para enviar
            afip.wswhomo.Service ServicioWebFactura = new afip.wswhomo.Service();
            ServicioWebFactura.Url = @"https://wswhomo.afip.gov.ar/wsfev1/service.asmx?WSDL";

            ServicioWebFactura.ClientCertificates.Add(TicketAcceso.certificado);
            //cargo los datos de la factura
            int puntoVenta = model.IdPuntoVenta;
            int tipoComprobante = NroComprobante;

            //inicio solicitud
            FECAERequest Solicitud = new FECAERequest();
            //encabezado solicitud
            FECAECabRequest EncabezadoSolicitud = new FECAECabRequest();
            //cuerpo solicitud 
            FECAEDetRequest CuerpoSolicitud = new FECAEDetRequest();

            EncabezadoSolicitud.CantReg = 1;
            EncabezadoSolicitud.PtoVta = puntoVenta;
            EncabezadoSolicitud.CbteTipo = tipoComprobante;
            Solicitud.FeCabReq = EncabezadoSolicitud;

            //cargamos el cuerpo
            CuerpoSolicitud.Concepto = 2;//servicios
            CuerpoSolicitud.DocTipo = 80; //model.IdTipoPago;
            CuerpoSolicitud.DocNro = Convert.ToInt64(model.Cuit);//model.NumeroFactura;

            var a = ServicioWebFactura.FEParamGetTiposCbte(Autenticacion);
            string mensaje = "";
            foreach (var re in a.ResultGet)
            {
                mensaje += string.Format("Resultado: {0}: {1}", re.Id, re.Desc);
            }

            //autorizarse
            FERecuperaLastCbteResponse UltimoRes = ServicioWebFactura.FECompUltimoAutorizado(Autenticacion, puntoVenta, 2);
            int ultimoNroComprobante = UltimoRes.CbteNro + 1;


            CuerpoSolicitud.CbteDesde = ultimoNroComprobante;
            CuerpoSolicitud.CbteHasta = ultimoNroComprobante;

            DateTime fechaHard = DateTime.Now;

            CuerpoSolicitud.CbteFch = fechaHard.ToString("yyyyMMdd");// model.Fecha.ToString("yyyyMMdd") ;//DateTime.Today.ToString("yyyyMMdd");

            CuerpoSolicitud.ImpTotal = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
            CuerpoSolicitud.ImpNeto = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
            CuerpoSolicitud.ImpIVA = 0;
            CuerpoSolicitud.ImpTotConc = 0;
            CuerpoSolicitud.ImpOpEx = 0;
            CuerpoSolicitud.ImpTrib = 0;
            CuerpoSolicitud.FchServDesde = fechaHard.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
            CuerpoSolicitud.FchServHasta = fechaHard.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
            CuerpoSolicitud.FchVtoPago = (fechaHard.AddDays(180)).ToString("yyyyMMdd");//(model.Fecha.AddDays(180)).ToString("yyyyMMdd"); //DateTime.Today.ToString("yyyyMMdd");

            FECotizacionResponse paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, "DOL");

            switch (model.idTipoMoneda)
            {
                case 1:
                    CuerpoSolicitud.MonId = "PES";
                    CuerpoSolicitud.MonCotiz = 1;
                    break;
                case 2:
                    CuerpoSolicitud.MonId = "DOL";
                    CuerpoSolicitud.MonCotiz = paramCoti.ResultGet.MonCotiz;
                    break;
            }


            AlicIva Alicuota = new AlicIva();
            //el id de alicuota es el tipo de iva
            Alicuota.Id = 3;
            Alicuota.BaseImp = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
            Alicuota.Importe = 0;

            CuerpoSolicitud.Iva = new[] { Alicuota };

            CbteAsoc cbteAsoc = new CbteAsoc();
            cbteAsoc.Nro = long.Parse(model.AplicaNC); //nro factura;
            cbteAsoc.Tipo = 19;
            cbteAsoc.PtoVta = model.IdPuntoVenta;
            cbteAsoc.Cuit = model.Cuit;



            CuerpoSolicitud.CbtesAsoc = new[] { cbteAsoc };

            Solicitud.FeDetReq = new[] { CuerpoSolicitud };
            //Solicitud.FeDetReq[0] = CuerpoSolicitud;

            //se envia el servicio al WS
            var r = ServicioWebFactura.FECAESolicitar(Autenticacion, Solicitud);

            if (r.Errors != null)
            {
                string error = "";
                string Observaciones = "";

                foreach (var er in r.Errors)
                {
                    error += string.Format("Er: {0}: {1}", er.Code, er.Msg);
                }

                if (r.FeDetResp[0].Observaciones != null)
                {

                    foreach (var obs in r.FeDetResp[0].Observaciones)
                    {
                        Observaciones += string.Format("Er: {0}: {1}", obs.Msg, obs.Code);
                    }
                }

                //return error;
            }
            else
            {
               // return r.FeCabResp.Resultado;
            }


            return r;
        }

        public FECAEResponse InsertarFacturaAfipLocal(ClaseLoginAfip TicketAcceso, FacturaModelView model, int NroComprobante, long cuitPropietario )
        {
            try
            {
                decimal totalGastos = 0; // ver esto porque deberia ser el acumulado de gastos       
                var ListadoItemsFactura = JsonConvert.DeserializeObject<List<ItemImprFacturaModelView>>(model.hdnArticulos);
                foreach (ItemImprFacturaModelView item in ListadoItemsFactura)
                {
                    ArticuloModel artModel = servicioArticulo.GetArticuloOuCodigo(item.codigo);
                    if (artModel.Tipo.Contains("Gastos"))
                    {
                        totalGastos += item.valor;
                    }
                }

                var totalImporte = model.TotalFactura;

                //instancio objeto autenticacion
                FEAuthRequest Autenticacion = new FEAuthRequest();
                Autenticacion.Cuit = cuitPropietario;//long.Parse(model.Cuit);
                Autenticacion.Sign = TicketAcceso.Sign;
                Autenticacion.Token = TicketAcceso.Token;

                //se prepara el servicio para enviar
                afip.wswhomo.Service ServicioWebFactura = new afip.wswhomo.Service();
                ServicioWebFactura.Url = @"https://wswhomo.afip.gov.ar/wsfev1/service.asmx?WSDL";

                ServicioWebFactura.ClientCertificates.Add(TicketAcceso.certificado);
                //cargo los datos de la factura
                int puntoVenta = model.IdPuntoVenta;
                int tipoComprobante = model.idTipoComprobanteSeleccionado;

                //inicio solicitud
                FECAERequest Solicitud = new FECAERequest();
                //encabezado solicitud
                FECAECabRequest EncabezadoSolicitud = new FECAECabRequest();
                //cuerpo solicitud 
                FECAEDetRequest CuerpoSolicitud = new FECAEDetRequest();

                EncabezadoSolicitud.CantReg = 1;
                EncabezadoSolicitud.PtoVta = puntoVenta;
                EncabezadoSolicitud.CbteTipo = tipoComprobante;
                Solicitud.FeCabReq = EncabezadoSolicitud;

                //cargamos el cuerpo

                CuerpoSolicitud.Concepto = 2;//servicios
                CuerpoSolicitud.DocTipo = 80; //model.IdTipoPago;
                CuerpoSolicitud.DocNro = 23000000000;//Convert.ToInt64(model.Cuit);//model.NumeroFactura;


                //autorizarse
                FERecuperaLastCbteResponse UltimoRes = ServicioWebFactura.FECompUltimoAutorizado(Autenticacion, puntoVenta, tipoComprobante);
                int ultimoNroComprobante = UltimoRes.CbteNro + 1;
                //int ultimoNroComprobante = NroComprobante;

                //FEParamGetCotizacion 

                //FECotizacionResponse paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, "DOL");

                CuerpoSolicitud.CbteDesde = ultimoNroComprobante;
                CuerpoSolicitud.CbteHasta = ultimoNroComprobante;

                /* tengo q dejar tomar el del ws sw afip, si mando mi nro factura no coicide y da error
                CuerpoSolicitud.CbteDesde = NroComprobante;
                CuerpoSolicitud.CbteHasta = NroComprobante;
                */

                CuerpoSolicitud.CbteFch = model.Fecha.ToString("yyyyMMdd");// model.Fecha.ToString("yyyyMMdd") ;//DateTime.Today.ToString("yyyyMMdd");

                CuerpoSolicitud.ImpTotal = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                CuerpoSolicitud.ImpNeto = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                CuerpoSolicitud.ImpIVA = 0;
                CuerpoSolicitud.ImpTotConc = 0;
                CuerpoSolicitud.ImpOpEx = 0;
                CuerpoSolicitud.ImpTrib = 0;
                CuerpoSolicitud.FchServDesde = model.Fecha.ToString("yyyyMMdd"); //model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                CuerpoSolicitud.FchServHasta = model.Fecha.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                CuerpoSolicitud.FchVtoPago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");//(model.Fecha.AddDays(180)).ToString("yyyyMMdd"); //DateTime.Today.ToString("yyyyMMdd");

                FECotizacionResponse paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, "DOL");


                switch (model.idTipoMoneda)
                {
                    case 1:
                        CuerpoSolicitud.MonId = "PES";
                        CuerpoSolicitud.MonCotiz = 1;
                        break;
                    case 2:
                        CuerpoSolicitud.MonId = "DOL";
                        CuerpoSolicitud.MonCotiz = paramCoti.ResultGet.MonCotiz;
                        break;
                }

                AlicIva Alicuota = new AlicIva();
                //el id de alicuota es el tipo de iva
                Alicuota.Id = 3;
                Alicuota.BaseImp = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                Alicuota.Importe = 0;

                CuerpoSolicitud.Iva = new[] { Alicuota };

                //CuerpoSolicitud.Iva[] = Alicuota;
                Solicitud.FeDetReq = new[] { CuerpoSolicitud };
                //Solicitud.FeDetReq[0] = CuerpoSolicitud;

                //se envia el servicio al WS
                var r = ServicioWebFactura.FECAESolicitar(Autenticacion, Solicitud);

                return r;
               

            }
            catch
            {
                return null;
            }
           
        }

        public FEXResponseAuthorize InsertarFacturaAfipExterior(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
            {
                var totalImporte = model.TotalFactura;

                //instancio objeto autenticacion
                ClsFEXAuthRequest Autenticacion = new ClsFEXAuthRequest();
                Autenticacion.Cuit = cuitPropietario;//long.Parse(model.Cuit);
                Autenticacion.Sign = TicketAcceso.Sign;
                Autenticacion.Token = TicketAcceso.Token;

                //se prepara el servicio para enviar
                afip.wswhomo_Exportacion.Service ServicioWebFacturaExterior = new afip.wswhomo_Exportacion.Service();
                ServicioWebFacturaExterior.Url = @"https://wswhomo.afip.gov.ar/wsfexv1/service.asmx?WSDL";

                ServicioWebFacturaExterior.ClientCertificates.Add(TicketAcceso.certificado);
                //cargo los datos de la factura
                int puntoVenta = model.IdPuntoVenta;
                int tipoComprobante = model.idTipoComprobanteSeleccionado;


                //FERecuperaLastCbteResponse UltimoRes = ServicioWebFacturaExterior.FECompUltimoAutorizado(Autenticacion, puntoVenta, tipoComprobante);
                //int ultimoNroComprobante = UltimoRes.CbteNro + 1;

                ClsFEX_LastCMP ultimoComprobante = new ClsFEX_LastCMP();
                ultimoComprobante.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                ultimoComprobante.Pto_venta = model.IdPuntoVenta;
                ultimoComprobante.Cuit = cuitPropietario;
                ultimoComprobante.Sign = TicketAcceso.Sign;
                ultimoComprobante.Token = TicketAcceso.Token;


                var cbte_nroObternido = ServicioWebFacturaExterior.FEXGetLast_CMP(ultimoComprobante);

                if (Comprobante.Numero == cbte_nroObternido.FEXResult_LastCMP.Cbte_nro)
                {
                    ClsFEXRequest solicitud = new ClsFEXRequest();
                    solicitud.Id = 1;
                    solicitud.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                    solicitud.Fecha_cbte = model.Fecha.ToString("yyyyMMdd");
                    solicitud.Punto_vta = model.IdPuntoVenta;
                    solicitud.Cbte_nro = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro + 1;
                    solicitud.Tipo_expo = 2; //servicios
                    solicitud.Permiso_existente = "";
                    solicitud.Dst_cmp = short.Parse(model.CodPaisAfip.ToString());
                    solicitud.Cliente = model.Cliente.Nombre;
                    solicitud.Cuit_pais_cliente = long.Parse(model.Cuit);
                    solicitud.Domicilio_cliente = model.DireccionCompuesta;
                    solicitud.Id_impositivo = null;

                    AfipHelper afipHelper = new AfipHelper();
                    switch (model.idTipoMoneda)
                    {
                        case 1:
                            solicitud.Moneda_Id = "PES";
                            solicitud.Moneda_ctz = 1;
                            break;
                        case 2:
                            solicitud.Moneda_Id = "DOL";
                            solicitud.Moneda_ctz = decimal.Parse(afipHelper.GetCotizacion("DOL").ResultGet.MonCotiz.ToString());
                            break;
                    }
                    solicitud.Obs_comerciales = null;
                    solicitud.Imp_total = model.TotalFactura;
                    solicitud.Obs = null;
                    solicitud.Forma_pago = null;
                    solicitud.Fecha_pago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");
                    solicitud.Incoterms = null;
                    solicitud.Incoterms_Ds = null;
                    solicitud.Idioma_cbte = short.Parse(model.IdTipoIdioma.ToString());
                    solicitud.Permisos = null;
                    solicitud.Cmps_asoc = null;

                    Item item = new Item();
                    item.Pro_codigo = null;
                    item.Pro_ds = null;

                    var ListadoItemsFactura = JsonConvert.DeserializeObject<List<ItemImprFacturaModelView>>(model.hdnArticulos);
                    foreach (ItemImprFacturaModelView itemIterar in ListadoItemsFactura)
                    {
                        Item itemGrabar = new Item();
                        itemGrabar.Pro_codigo = itemIterar.codigo;
                        itemGrabar.Pro_ds = itemIterar.descripcion;
                        itemGrabar.Pro_qty = 1;
                        itemGrabar.Pro_umed = 1;
                        itemGrabar.Pro_precio_uni = itemIterar.valor;
                        itemGrabar.Pro_bonificacion = 0;
                        itemGrabar.Pro_total_item = itemGrabar.Pro_qty * itemGrabar.Pro_precio_uni - itemGrabar.Pro_bonificacion;
                        //inserto los items
                        solicitud.Items = new[] { itemGrabar };
                    }

                    //se envia el servicio al WS
                    var r = ServicioWebFacturaExterior.FEXAuthorize(Autenticacion, solicitud);


                    if (r.FEXErr != null)
                    {
                        if (r.FEXErr.ErrMsg == "OK")
                        {
                            return r;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    
                    return null;
                }

            }
            catch
            {
                return null;
            }

        }


        //metodo para obtener el tipo de comprobante
        public int DeterminarNroComprovante (int tipoIva, bool miPyme, decimal TotalFactura, string tipoComprobante)
        {
            int retorno = 0 ;
            if (tipoIva != 4)
            {
                if (miPyme == true && TotalFactura > 100000)
                {
                    //facturacion electronica
                    switch (tipoComprobante)
                    {
                        case "Factura":
                            retorno = 211;
                            break;
                        case "Debito":
                            retorno = 212;
                            break;
                        case "Credito":
                            retorno = 213;
                            break;
                    }
                }

                if (miPyme == false)
                {//documentacion C
                    switch (tipoComprobante)
                    {
                        case "Factura":
                            retorno = 11;
                            break;
                        case "Debito":
                            retorno = 12;
                            break;
                        case "Credito":
                            retorno = 13;
                            break;
                    }
                }
            }
            else
            {//documentacion exterior
                switch (tipoComprobante)
                {
                    case "Factura":
                        retorno = 19;
                        break;
                    case "Debito":
                        retorno = 20;
                        break;
                    case "Credito":
                        retorno = 21;
                        break;
                }
            }
            return retorno;
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
        public ActionResult GetExisteFacturaJson(string term, string idCliente)
        {
            try
            {
                //if (idCliente == null)
                //{
                //    idCliente = "0";
                //}
                List<FacturaVentaModel> Listadofactura = servicioFacturaVenta.GetAllFacturaVentaPorNumero(int.Parse(term), int.Parse(idCliente));
                var arrayProveedor = (from fact in Listadofactura
                                      select new AutoCompletarViewModel()
                                      {
                                          //id = cli.Id,
                                          label = fact.NumeroFactura.ToString()
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
        public ActionResult GetObtenerFacturaJson(string idCliente, string nroFactura, string idComprobante)
        {
            try
            {
                string strJson;
                int idPuntoVenta=0;
                ClienteModelView Cliente = Mapper.Map<ClienteModel, ClienteModelView>(servicioCliente.GetClientePorId(int.Parse(idCliente)));

                if (Cliente.IdTipoiva != 4)
                {
                    idPuntoVenta = 3;
                }
                else
                {
                    idPuntoVenta = 2;
                }

                int comprobanteActualizado = DeterminarNroComprovante(Cliente.IdTipoiva, Cliente.MiPyme, 0, "Factura");
                TipoComprobanteVentaModelView tipoComprobante = Mapper.Map<TipoComprobanteVentaModel, TipoComprobanteVentaModelView>(servicioTipoComprobanteVenta.GetTipoComprobanteVentaPorNroAfip(comprobanteActualizado, idPuntoVenta));

                FacturaVentaItemsModel FacturaItems = servicioFacturaVentaItems.ObtenerDatosFacturaItems(int.Parse(idCliente), int.Parse(nroFactura), tipoComprobante.Id);

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(FacturaItems);

                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }

                return Json(strJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }


        [HttpGet()]
        public ActionResult GetDireccionJson(string idDireccion)
        {
            try
            {
                string strJson;
                ClienteDireccionModel direccion = servicioClienteDireccion.ObtenerPorID(int.Parse(idDireccion));

                strJson = Newtonsoft.Json.JsonConvert.SerializeObject(direccion);
                if ((strJson != null))
                {
                    var rJson = Json(strJson, JsonRequestBehavior.AllowGet);
                    return rJson;
                }

                return Json(strJson, JsonRequestBehavior.AllowGet);
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

                List<SelectListItem> tipoComprobantes = new List<SelectListItem>();
                tipoComprobantes.Add(new SelectListItem() { Text = "Factura", Value = "1" });
                tipoComprobantes.Add(new SelectListItem() { Text = "Nota Debito", Value = "2" });
                tipoComprobantes.Add(new SelectListItem() { Text = "Nota Credito", Value = "3" });
                // model.TipoIdioma = lstIdioma;
                Cliente.ListaComprobantesDrop = tipoComprobantes;

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


        //[HttpGet()]
        //public ActionResult GetCotizacionJson(string term)
        //{
        //    try
        //    {
        //        List<ArticuloModel> articulos = servicioArticulo.GetArticulosPorCodigo(term);
        //        var arrayArticulos = (from cli in articulos
        //                              select new AutoCompletarViewModel()
        //                              {
        //                                  id = cli.Id,
        //                                  label = cli.DescripcionCastellano
        //                              }).ToArray();
        //        return Json(arrayArticulos, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
        //        return null;
        //    }

        //}


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
        public ActionResult GetCotizacionJson(string IdMoneda)
        {
      
            CotizacionAFIP cotizacion = new CotizacionAFIP();
            var f = DateTime.Now;
            string strJson;
            try
            {

                var moneda = servicioTipoMoneda.GetCotizacionPorIdMoneda(int.Parse(IdMoneda));
                if (moneda == null)
                {
                    cotizacion.Importe = 1;
                    cotizacion.Fecha = f.ToString("dd/MM/yyyy");
                }
                else
                {
                    cotizacion.Importe = moneda.Monto;
                    cotizacion.IdMoneda = moneda.Id.ToString();
                    cotizacion.Fecha = moneda.Fecha.ToString();
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
                servicioCliente._mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
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


        

    }
}