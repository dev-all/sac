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
using SAC.QR;

using System.Drawing;


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

            long cuitOriginador = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());

            long nroFactura = 0;

            switch (model.idTipoComprobanteSeleccionado)
            {
               

                case 1: //factura 
                    #region Factura
                    string cae = "";
                    long idWsAfip = 0 ;
                    string mensajeErrorAfip = "";
                    //string mensajeObservacionesAfip = "";
                    string ResultadoAfip = "";

                    //pregunto si la carga de la factura es manual, si es asi no inserta la factura electronica
                    if (model.FacturaManual == false)
                    {
                        //insertar la factura
                        //agrego mi cuit porque da error


                        if (cliente.TipoCliente.Id == 2) // extranjero
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
                                if (a != null)
                                {
                                    if (a.FEXErr.ErrMsg == "OK")
                                    {
                                        ResultadoAfip = "A";
                                        idWsAfip = a.FEXResultAuth.Id;
                                        cae = a.FEXResultAuth.Cae;
                                        nroFactura = a.FEXResultAuth.Cbte_nro;
                                    }
                                    else
                                    {
                                        ResultadoAfip = a.FEXErr.ErrCode.ToString();
                                        mensajeErrorAfip = a.FEXErr.ErrMsg;
                                        servicioFacturaVenta._mensaje?.Invoke("Ops!, Notificacion de Afip: " + mensajeErrorAfip + "(" + ResultadoAfip + ")", "error");
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

                            RetornoAfip = InsertarFacturaAfipLocal(ClaseLogin, model, cbt, cuitOriginador);

                            if (RetornoAfip != null)
                            {
                                if (RetornoAfip.Errors != null)
                                {
                                    //mostrar mensaje error;
                                    foreach (var er in RetornoAfip.Errors)
                                    {
                                        mensajeErrorAfip += string.Format("Er: {0}: {1}", er.Code, er.Msg);
                                    }
                                    cae = "0";
                                    servicioFacturaVenta._mensaje?.Invoke(mensajeErrorAfip, "error");
                                }
                                else //son observaciones pero puede rechazar la factura
                                {
                                    if (RetornoAfip.FeDetResp[0].Observaciones != null && RetornoAfip.FeDetResp[0].Resultado != "A")
                                    {
                                        foreach (var obs in RetornoAfip.FeDetResp[0].Observaciones)
                                        {
                                            mensajeErrorAfip += string.Format("Er: {0}: {1}", obs.Msg, obs.Code);
                                        }
                                        cae = "0";
                                        servicioFacturaVenta._mensaje?.Invoke(mensajeErrorAfip, "error");
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
                    FacturaElectronica.NROCBTE_AFIP = comprobanteActualizado;
                    FacturaElectronica.PUNTOVTA = model.IdPuntoVenta;
                    FacturaElectronica.ID_TIPOCBTE = cbt.Id; //traigo el tipo cbte
                    FacturaElectronica.ID_CBTE_WSAFIP = idWsAfip;

                    if (ResultadoAfip != null)
                    {
                        FacturaElectronica.RESULTADO = ResultadoAfip;
                    }
                    else
                    {
                        FacturaElectronica.RESULTADO = null;
                    }
                    FacturaElectronica.OBS = mensajeErrorAfip;
                    FacturaElectronica.IVA = 0;
                    FacturaElectronica.IDIVA = "3";
                    FacturaElectronica.TIPODOC = 80;
                    FacturaElectronica.IVA10 = 0;
                    FacturaElectronica.NETO10 = 0;
                    FacturaElectronica.IDMONEDA = moneda;


                    DatosQrAfip qrAfip = new DatosQrAfip();
                    qrAfip.ver = 1;
                    qrAfip.fecha = model.Fecha;
                    qrAfip.cuit = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
                    qrAfip.ptoVenta = model.IdPuntoVenta;
                    qrAfip.tipoCmp = cbt.Id;
                    qrAfip.nroCmp = comprobanteActualizado;
                    qrAfip.Importe = model.TotalFactura; ;
                    qrAfip.moneda = moneda;
                    qrAfip.ctz = 1;
                    qrAfip.tipoDocRec = 80;
                    qrAfip.nroDocRec = long.Parse(model.Cuit);
                    qrAfip.tipoCodAut = "E";
                    qrAfip.codAut = long.Parse(cae);

                    string jsonQrAfip = Newtonsoft.Json.JsonConvert.SerializeObject(qrAfip);

                    GeneradorQR Qr = new GeneradorQR();
                    Bitmap bm = Qr.GetQRBitmap(jsonQrAfip);
                    string Qr64b = Qr.convertirBase64(bm);

                    FacturaElectronica.QR = Qr64b;

                    FacturaElectronicaModel FacturaElectronicaInsertada = servicioFacturaElectronica.Agregar(FacturaElectronica);


                    //actualiza tabla cotiza ?? nose
                    //List<FacturaVentaModel> ListaFactura = servicioFacturaVenta.GetAllFacturaVentaCliente(model.IdCliente);

                    // agrega en tbl FactVenta
                    FacturaVentaModel facturaVentaModel = new FacturaVentaModel();
                    facturaVentaModel.IdTipoComprobante = cbt.Id; //model.idTipoComprobanteSeleccionado;  // facturaVentaModel. = model.IdPuntoVenta.ToString();
                    facturaVentaModel.IdFacturaElectronica = FacturaElectronicaInsertada.ID;
                    facturaVentaModel.NumeroFactura = cbt.Numero;//int.Parse(nroFactura.ToString()); //;
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
                    facturaVentaModel.Baja = "*";
                    facturaVentaModel.IdImputacion = 0;
                    facturaVentaModel.NumeroCobro = 0;
                    facturaVentaModel.IdMoneda = model.idTipoMoneda;
                    facturaVentaModel.Descuento = "1"; 
                    facturaVentaModel.Recibo = "0";
                    //aca grabo el cae
                    facturaVentaModel.NumeroTra = cae;
                    //-------------
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

                    servicioFacturaVenta._mensaje?.Invoke("Ops!, El servicio de AFIP no responde...", "ok");

                    break;
                #endregion
                //------------------------------------------------------------------
                case 2: //nota debito
                    #region Nota Debito

                    string caeNd = "";
                    long idWsAfipNd = 0;
                    string ResultadoAfipNd = "";
                    string mensajeErrorAfipNd = "";

                    //factura nota debito ws afip
                    if (model.FacturaManual == false)
                    {

                        Afip_TicketAccesoModel loginDebito;
                        AfipHelper afipHelperDebito = new AfipHelper();
                        ClaseLoginAfip ClaseLoginDebito = null;

                        if (cliente.TipoCliente.Id == 2) // extranjero
                        {

                            if (cbt.CodigoAfip == 20)
                            {
                                loginDebito = afipHelperDebito.VerificarTicketAcceso("wsfex");


                                if (loginDebito == null)
                                {
                                    //busca el token nuevo y graba en la bd
                                    ClaseLoginDebito = afipHelperDebito.ObtenerTicketAccesoWS("wsfex", OUsuario.IdUsuario);
                                }
                                else
                                {
                                    //usa el token de la base
                                    ClaseLoginDebito = afipHelperDebito.ObtenerTicketAccesoSinWS("wsfex", OUsuario.IdUsuario);
                                    ClaseLoginDebito.Token = loginDebito.token;
                                    ClaseLoginDebito.Sign = loginDebito.sing;
                                }

                                var a = InsertarNotaDebitoAfipExterior(ClaseLoginDebito, model, cbt, cuitOriginador);
                                if (a != null)
                                {
                                    if (a.FEXErr.ErrMsg == "OK")
                                    {
                                        ResultadoAfipNd = "A";
                                        idWsAfipNd = a.FEXResultAuth.Id;
                                        caeNd = a.FEXResultAuth.Cae;
                                    }
                                    else
                                    {
                                        ResultadoAfipNd = a.FEXErr.ErrCode.ToString();
                                        mensajeErrorAfipNd = a.FEXErr.ErrMsg;
                                        servicioFacturaVenta._mensaje?.Invoke("Ops!, Notificacion de Afip: " + mensajeErrorAfipNd + "(" + ResultadoAfipNd + ")", "error");
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
                            // Afip_TicketAccesoModel login;

                            loginDebito = afipHelper.VerificarTicketAcceso("wsfe");


                            if (loginDebito == null)
                            {
                                //busca el token nuevo y graba en la bd
                                ClaseLoginDebito = afipHelper.ObtenerTicketAccesoWS("wsfe", OUsuario.IdUsuario);
                            }
                            else
                            {
                                //usa el token de la base
                                ClaseLoginDebito = afipHelper.ObtenerTicketAccesoSinWS("wsfe", OUsuario.IdUsuario);
                                ClaseLoginDebito.Token = loginDebito.token;
                                ClaseLoginDebito.Sign = loginDebito.sing;
                            }

                            RetornoAfip = InsertarNotaDebitoAfipLocal(ClaseLoginDebito, model, cbt, cuitOriginador);

                            // string mensajeErrorAfipNd = "";
                            // string mensajeObservacionesAfipNd = "";

                            //string ResultadoAfipNd = "";

                            if (RetornoAfip != null)
                            {
                                if (RetornoAfip.Errors != null)
                                {
                                    //mostrar mensaje error;
                                    foreach (var er in RetornoAfip.Errors)
                                    {
                                        mensajeErrorAfipNd += string.Format("Er: {0}: {1}", er.Code, er.Msg);
                                    }
                                    caeNd = "0";
                                    ResultadoAfipNd = RetornoAfip.FeDetResp[0].Resultado;
                                    servicioFacturaVenta._mensaje?.Invoke(mensajeErrorAfipNd, "error");
                                }
                                else //son observaciones pero puede rechazar la factura
                                {
                                    if (RetornoAfip.FeDetResp[0].Observaciones != null && RetornoAfip.FeDetResp[0].Resultado != "A")
                                    {
                                        foreach (var obs in RetornoAfip.FeDetResp[0].Observaciones)
                                        {
                                            mensajeErrorAfipNd += string.Format("Er: {0}: {1}", obs.Msg, obs.Code);
                                        }
                                        caeNd = "0";
                                        ResultadoAfipNd = RetornoAfip.FeDetResp[0].Resultado;
                                        servicioFacturaVenta._mensaje?.Invoke(mensajeErrorAfipNd, "error");
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
                            }
                            else
                            {
                                servicioFacturaVenta._mensaje?.Invoke("Ops!, El servicio de AFIP no responde...", "error");
                                return RedirectToAction("Index");
                            }
                        }

                    }
                    //afip 
                    FacturaElectronicaModel FacturaElectronicaND = new FacturaElectronicaModel();
                    FacturaElectronicaND.ALICUOTA = "0";
                    FacturaElectronicaND.CAE = caeNd;
                    FacturaElectronicaND.CATEGORIA = "Exento";
                    FacturaElectronicaND.CODBARRA = "";
                    FacturaElectronicaND.NRODOC = model.Cuit;
                    FacturaElectronicaND.TIPOSERV = 2;
                    FacturaElectronicaND.NOMBRE = model.NombreComp;
                    FacturaElectronicaND.CODCLI = model.CodigoCliente;
                    FacturaElectronicaND.CODPAIS = model.CodPaisAfip.ToString();
                    FacturaElectronicaND.COTIZA = 0;//obtener cotizacion
                    FacturaElectronicaND.DOMICILIO = model.DireccionCompuesta;
                    FacturaElectronicaND.ESTADO = "2";
                    FacturaElectronicaND.FDESDE = model.Fecha;
                    FacturaElectronicaND.FECHACBTE = model.Fecha;
                    FacturaElectronicaND.FECHAVEN = model.Fecha;
                    FacturaElectronicaND.FECHAVTO = model.Fecha.AddDays(180);
                    FacturaElectronicaND.FHASTA = null;
                    FacturaElectronicaND.FORMAPAGO = model.IdTipoPago.ToString();
                    FacturaElectronicaND.TOTAL = model.MontoAjuste;
                    FacturaElectronicaND.NETO = model.MontoAjuste;
                    FacturaElectronicaND.NROCBTE_AFIP = comprobanteActualizado;
                    FacturaElectronicaND.PUNTOVTA = model.IdPuntoVenta;
                    FacturaElectronicaND.ID_TIPOCBTE = cbt.Id; //traigo el tipo cbte
                    FacturaElectronicaND.ID_CBTE_WSAFIP = idWsAfipNd;

                    if (ResultadoAfipNd != null)
                    {
                        FacturaElectronicaND.RESULTADO = ResultadoAfipNd;
                    }
                    else
                    {
                        FacturaElectronicaND.RESULTADO = null;
                    }
                    FacturaElectronicaND.OBS = mensajeErrorAfipNd;
                    FacturaElectronicaND.IVA = 0;
                    FacturaElectronicaND.IDIVA = "3";
                    FacturaElectronicaND.TIPODOC = 80;
                    FacturaElectronicaND.IVA10 = 0;
                    FacturaElectronicaND.NETO10 = 0;

                    String monedaNd = "";
                    if (model.idTipoMoneda == 1)
                    {
                        moneda = "PES";
                    }
                    if (model.idTipoMoneda == 2)
                    {
                        moneda = "DOL";
                    }


                    FacturaElectronicaND.IDMONEDA = monedaNd;


                    DatosQrAfip qrAfipNd = new DatosQrAfip();
                    qrAfipNd.ver = 1;
                    qrAfipNd.fecha = model.Fecha;
                    qrAfipNd.cuit = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
                    qrAfipNd.ptoVenta = model.IdPuntoVenta;
                    qrAfipNd.tipoCmp = cbt.Id;
                    qrAfipNd.nroCmp = comprobanteActualizado;
                    qrAfipNd.Importe = model.MontoAjuste;
                    qrAfipNd.moneda = monedaNd;
                    qrAfipNd.ctz = 1;
                    qrAfipNd.tipoDocRec = 80;
                    qrAfipNd.nroDocRec = long.Parse(model.Cuit);
                    qrAfipNd.tipoCodAut = "E";
                    qrAfipNd.codAut = long.Parse(caeNd);

                    string jsonQrAfipNd = Newtonsoft.Json.JsonConvert.SerializeObject(qrAfipNd);

                    GeneradorQR QrNd = new GeneradorQR();
                    Bitmap bmNd = QrNd.GetQRBitmap(jsonQrAfipNd);
                    string Qr64bNd = QrNd.convertirBase64(bmNd);

                    FacturaElectronicaND.QR = Qr64bNd;

                    FacturaElectronicaModel FacturaElectronicaInsertadaNd = servicioFacturaElectronica.Agregar(FacturaElectronicaND);

                    // agrega en tbl FactVenta
                    FacturaVentaModel facturaModelNotaDebito = new FacturaVentaModel();
                    FacturaVentaModel facturaVentaOriginal = servicioFacturaVenta.GetFacturaVentaPorNumero(int.Parse(model.AplicaNC), model.IdCliente);
                    facturaModelNotaDebito.IdTipoComprobante = cbt.Id; //model.idTipoComprobanteSeleccionado;  // facturaVentaModel. = model.IdPuntoVenta.ToString();
                    facturaModelNotaDebito.NumeroFactura = cbt.Numero;
                    facturaModelNotaDebito.IdFacturaElectronica = FacturaElectronicaInsertadaNd.ID;
                    //aca grabo el cae
                    facturaModelNotaDebito.NumeroTra = caeNd;
                    //-------------
                    facturaModelNotaDebito.Codigo = facturaVentaOriginal.Codigo;
                    facturaModelNotaDebito.IdCliente = facturaVentaOriginal.IdCliente;
                    facturaModelNotaDebito.IdMoneda = model.idTipoMoneda;
                    facturaModelNotaDebito.Fecha = DateTime.Now; //model.Fecha;
                    facturaModelNotaDebito.Impre = "false";
                    facturaModelNotaDebito.Vencimiento = DateTime.Now.AddDays(1);
                    facturaModelNotaDebito.Concepto = model.EncabezadoFact;
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
                   // facturaModelNotaDebito.NumeroTra = "0"; //no se de donde sale
                    facturaModelNotaDebito.Anula = facturaVentaOriginal.NumeroFactura.ToString();
                    facturaModelNotaDebito.Activo = true;
                    facturaModelNotaDebito.IdUsuario = OUsuario.IdUsuario;
                    facturaModelNotaDebito.UltimaModificacion = DateTime.Now;

                    FacturaVentaModel FacNotaDebitoInsertada = servicioFacturaVenta.Agregar(facturaModelNotaDebito);

                    break;
                #endregion
                case 3: //nota credito
                        // agrega en tbl FactVenta
                    #region notaCredito

                    string caeNC = "";
                    long idWsAfipNC = 0;
                    string mensajeErrorAfipNC = "";
                    string ResultadoAfipNC = "";

                    //factura nota debito ws afip
                    if (model.FacturaManual == false)
                    {

                        Afip_TicketAccesoModel loginCredito;
                        AfipHelper afipHelperCredito = new AfipHelper();
                        ClaseLoginAfip ClaseLoginCredito = null;

                        if (cliente.TipoCliente.Id == 2) // extranjero
                        {

                            if (cbt.CodigoAfip == 21)
                            {
                                loginCredito = afipHelperCredito.VerificarTicketAcceso("wsfex");


                                if (loginCredito == null)
                                {
                                    //busca el token nuevo y graba en la bd
                                    ClaseLoginCredito = afipHelperCredito.ObtenerTicketAccesoWS("wsfex", OUsuario.IdUsuario);
                                }
                                else
                                {
                                    //usa el token de la base
                                    ClaseLoginCredito = afipHelperCredito.ObtenerTicketAccesoSinWS("wsfex", OUsuario.IdUsuario);
                                    ClaseLoginCredito.Token = loginCredito.token;
                                    ClaseLoginCredito.Sign = loginCredito.sing;
                                }

                                var a = InsertarNotaCreditoAfipExterior(ClaseLoginCredito, model, cbt, cuitOriginador);
                                if (a != null)
                                {
                                    if (a.FEXErr.ErrMsg == "OK")
                                    {
                                        ResultadoAfipNC = "A";
                                        idWsAfipNC = a.FEXResultAuth.Id;
                                        caeNC = a.FEXResultAuth.Cae;
                                    }
                                    else
                                    {
                                        ResultadoAfipNC = a.FEXErr.ErrCode.ToString();
                                        mensajeErrorAfip = a.FEXErr.ErrMsg;
                                        servicioFacturaVenta._mensaje?.Invoke("Ops!, Notificacion de Afip: " + mensajeErrorAfipNC + "(" + ResultadoAfipNC + ")", "error");
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
                            // Afip_TicketAccesoModel login;

                            loginCredito = afipHelper.VerificarTicketAcceso("wsfe");


                            if (loginCredito == null)
                            {
                                //busca el token nuevo y graba en la bd
                                ClaseLoginCredito = afipHelper.ObtenerTicketAccesoWS("wsfe", OUsuario.IdUsuario);
                            }
                            else
                            {
                                //usa el token de la base
                                ClaseLoginCredito = afipHelper.ObtenerTicketAccesoSinWS("wsfe", OUsuario.IdUsuario);
                                ClaseLoginCredito.Token = loginCredito.token;
                                ClaseLoginCredito.Sign = loginCredito.sing;
                            }

                            RetornoAfip = InsertarNotaCreditoAfipLocal(ClaseLoginCredito, model, cbt, cuitOriginador);

                            if (RetornoAfip != null)
                            {
                                if (RetornoAfip.Errors != null)
                                {
                                    //mostrar mensaje error;
                                    foreach (var er in RetornoAfip.Errors)
                                    {
                                        mensajeErrorAfipNC += string.Format("Er: {0}: {1}", er.Code, er.Msg);
                                    }
                                    caeNC = "0";
                                    ResultadoAfipNC = RetornoAfip.FeDetResp[0].Resultado;
                                    servicioFacturaVenta._mensaje?.Invoke(mensajeErrorAfipNC, "error");

                                }
                                else //son observaciones pero puede rechazar la factura
                                {
                                    if (RetornoAfip.FeDetResp[0].Observaciones != null && RetornoAfip.FeDetResp[0].Resultado != "A")
                                    {
                                        foreach (var obs in RetornoAfip.FeDetResp[0].Observaciones)
                                        {
                                            mensajeErrorAfipNC += string.Format("Er: {0}: {1}", obs.Msg, obs.Code);
                                        }
                                        caeNC = "0";
                                        ResultadoAfipNd = RetornoAfip.FeDetResp[0].Resultado;
                                        servicioFacturaVenta._mensaje?.Invoke(mensajeErrorAfipNC, "error");
                                    }
                                    else
                                    {
                                        ResultadoAfipNC = RetornoAfip.FeDetResp[0].Resultado;
                                        if (RetornoAfip.FeDetResp[0].Resultado == "A")
                                        {
                                            caeNC = RetornoAfip.FeDetResp[0].CAE;
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


                    FacturaElectronicaModel FacturaElectronicaNC = new FacturaElectronicaModel();
                    FacturaElectronicaNC.ALICUOTA = "0";
                    FacturaElectronicaNC.CAE = caeNC;
                    FacturaElectronicaNC.CATEGORIA = "Exento";
                    FacturaElectronicaNC.CODBARRA = "";
                    FacturaElectronicaNC.NRODOC = model.Cuit;
                    FacturaElectronicaNC.TIPOSERV = 2;
                    FacturaElectronicaNC.NOMBRE = model.NombreComp;
                    FacturaElectronicaNC.CODCLI = model.CodigoCliente;
                    FacturaElectronicaNC.CODPAIS = model.CodPaisAfip.ToString();
                    FacturaElectronicaNC.COTIZA = 0;//obtener cotizacion
                    FacturaElectronicaNC.DOMICILIO = model.DireccionCompuesta;
                    FacturaElectronicaNC.ESTADO = "2";
                    FacturaElectronicaNC.FDESDE = model.Fecha;
                    FacturaElectronicaNC.FECHACBTE = model.Fecha;
                    FacturaElectronicaNC.FECHAVEN = model.Fecha;
                    FacturaElectronicaNC.FECHAVTO = model.Fecha.AddDays(180);
                    FacturaElectronicaNC.FHASTA = null;
                    FacturaElectronicaNC.FORMAPAGO = model.IdTipoPago.ToString();
                    FacturaElectronicaNC.TOTAL = model.MontoAjuste; 
                    FacturaElectronicaNC.NETO = model.MontoAjuste; 
                    FacturaElectronicaNC.NROCBTE_AFIP = comprobanteActualizado;
                    FacturaElectronicaNC.PUNTOVTA = model.IdPuntoVenta;
                    FacturaElectronicaNC.ID_TIPOCBTE = cbt.Id; //traigo el tipo cbte
                    FacturaElectronicaNC.ID_CBTE_WSAFIP = idWsAfipNC;

                    if (ResultadoAfipNC != null)
                    {
                        FacturaElectronicaNC.RESULTADO = ResultadoAfipNC;
                    }
                    else
                    {
                        FacturaElectronicaNC.RESULTADO = null;
                    }
                    FacturaElectronicaNC.OBS = ResultadoAfipNC;
                    FacturaElectronicaNC.IVA = 0;
                    FacturaElectronicaNC.IDIVA = "3";
                    FacturaElectronicaNC.TIPODOC = 80;
                    FacturaElectronicaNC.IVA10 = 0;
                    FacturaElectronicaNC.NETO10 = 0;

                    String monedaNc = "";
                    if (model.idTipoMoneda == 1)
                    {
                        moneda = "PES";
                    }
                    if (model.idTipoMoneda == 2)
                    {
                        moneda = "DOL";
                    }


                    FacturaElectronicaNC.IDMONEDA = monedaNc;


                    DatosQrAfip qrAfipNc = new DatosQrAfip();
                    qrAfipNc.ver = 1;
                    qrAfipNc.fecha = model.Fecha;
                    qrAfipNc.cuit = long.Parse(System.Configuration.ConfigurationManager.AppSettings["cuitUserAfip"].ToString());
                    qrAfipNc.ptoVenta = model.IdPuntoVenta;
                    qrAfipNc.tipoCmp = cbt.Id;
                    qrAfipNc.nroCmp = comprobanteActualizado;
                    qrAfipNc.Importe = model.MontoAjuste; 
                    qrAfipNc.moneda = monedaNc;
                    qrAfipNc.ctz = 1;
                    qrAfipNc.tipoDocRec = 80;
                    qrAfipNc.nroDocRec = long.Parse(model.Cuit);
                    qrAfipNc.tipoCodAut = "E";
                    qrAfipNc.codAut = long.Parse(caeNC);

                    string jsonQrAfipNc = Newtonsoft.Json.JsonConvert.SerializeObject(qrAfipNc);

                    GeneradorQR QrNc = new GeneradorQR();
                    Bitmap bmNc = QrNc.GetQRBitmap(jsonQrAfipNc);
                    string Qr64bNc = QrNc.convertirBase64(bmNc);

                    FacturaElectronicaNC.QR = Qr64bNc;

                    FacturaElectronicaModel FacturaElectronicaInsertadaNC = servicioFacturaElectronica.Agregar(FacturaElectronicaNC);

                    FacturaVentaModel facturaModelNotaCredito = new FacturaVentaModel();
                    FacturaVentaModel facturaVentaOriginal1 = servicioFacturaVenta.GetFacturaVentaPorNumero(int.Parse(model.AplicaNC), model.IdCliente);
                    facturaModelNotaCredito.IdTipoComprobante = cbt.Id; //model.idTipoComprobanteSeleccionado;
                                                                        // facturaVentaModel. = model.IdPuntoVenta.ToString();
                    facturaModelNotaCredito.IdFacturaElectronica = FacturaElectronicaInsertadaNC.ID;
                    facturaModelNotaCredito.NumeroFactura = cbt.Numero;
                    facturaModelNotaCredito.Codigo = facturaVentaOriginal1.Codigo;
                    facturaModelNotaCredito.IdCliente = facturaVentaOriginal1.IdCliente;
                    facturaModelNotaCredito.NumeroTra = caeNC;
                    facturaModelNotaCredito.IdMoneda = model.idTipoMoneda;
                    facturaModelNotaCredito.Fecha = DateTime.Now; //model.Fecha;
                    facturaModelNotaCredito.Impre = "false";
                    facturaModelNotaCredito.Vencimiento = DateTime.Now.AddDays(1);
                    facturaModelNotaCredito.Concepto = model.EncabezadoFact;
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
                    //facturaModelNotaCredito.NumeroTra = "0"; //no se de donde sale
                    //aca grabo el cae
                    facturaModelNotaCredito.NumeroTra = caeNC;
                    facturaModelNotaCredito.Anula = facturaVentaOriginal1.NumeroFactura.ToString();
                    facturaModelNotaCredito.Activo = true;
                    facturaModelNotaCredito.IdUsuario = OUsuario.IdUsuario;
                    facturaModelNotaCredito.UltimaModificacion = DateTime.Now;

                    FacturaVentaModel FacNotaCreditoInsertada = servicioFacturaVenta.Agregar(facturaModelNotaCredito);

                    break;


                    #endregion
            }

            //   var FacturaActuralizada = servicioTipoComprobanteVenta.ActualizarNroFactura(comprobanteActualizado, model.IdPuntoVenta, nroFactura);

            return RedirectToAction("Index");


        }


        public FECAEResponse InsertarFacturaAfipLocal(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
            {
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
                int tipoComprobante = Comprobante.CodigoAfip;

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

                FECAEResponse Respuesta = new FECAEResponse();

                //FEParamGetCotizacion 
                //if (Comprobante.Numero == ultimoNroComprobante)
                //{
                    CuerpoSolicitud.CbteDesde = ultimoNroComprobante;
                    CuerpoSolicitud.CbteHasta = ultimoNroComprobante;

                    CuerpoSolicitud.CbteFch = model.Fecha.ToString("yyyyMMdd");// model.Fecha.ToString("yyyyMMdd") ;//DateTime.Today.ToString("yyyyMMdd");

                    CuerpoSolicitud.ImpTotal = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                    CuerpoSolicitud.ImpNeto = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                   // CuerpoSolicitud.ImpIVA = 0;
                    CuerpoSolicitud.ImpTotConc = 0;
                    CuerpoSolicitud.ImpOpEx = 0;
                    CuerpoSolicitud.ImpTrib = 0;
                    CuerpoSolicitud.FchServDesde = model.Fecha.ToString("yyyyMMdd"); //model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                    CuerpoSolicitud.FchServHasta = model.Fecha.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                    CuerpoSolicitud.FchVtoPago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");//(model.Fecha.AddDays(180)).ToString("yyyyMMdd"); //DateTime.Today.ToString("yyyyMMdd");

                   

                    switch (model.idTipoMoneda)
                    {
                        case 1:
                            CuerpoSolicitud.MonId = "PES";
                            CuerpoSolicitud.MonCotiz = 1;
                            break;
                        case 2:
                            FECotizacionResponse paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, "DOL");
                            CuerpoSolicitud.MonId = "DOL";
                            CuerpoSolicitud.MonCotiz = paramCoti.ResultGet.MonCotiz;
                            break;
                    }

                    //AlicIva Alicuota = new AlicIva();
                    ////el id de alicuota es el tipo de iva
                    //Alicuota.Id = 3;
                    //Alicuota.BaseImp = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                    //Alicuota.Importe = 0;

                    //CuerpoSolicitud.Iva = new[] { Alicuota };

                    Solicitud.FeDetReq = new[] { CuerpoSolicitud };

                    //se envia el servicio al WS
                    Respuesta = ServicioWebFactura.FECAESolicitar(Autenticacion, Solicitud);
                    return Respuesta;
                //}
                //else
                //{
                //    Err error = new Err();
                //    error.Code = 0;
                //    error.Msg = "El nro de comprobante local no coincide con el de AFIP";
                //    Respuesta.Errors = new[] { error };
                //    return Respuesta;
                //}

            }
            catch (Exception e)
            {

                return null;
            }

        }

        public FECAEResponse InsertarNotaDebitoAfipLocal(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
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
                int tipoComprobante = Comprobante.CodigoAfip;

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

                //autorizarse
                FERecuperaLastCbteResponse UltimoRes = ServicioWebFactura.FECompUltimoAutorizado(Autenticacion, puntoVenta, 12);
                int ultimoNroComprobante = UltimoRes.CbteNro + 1;

                FECAEResponse Respuesta = new FECAEResponse();

                //if (ultimoNroComprobante == Comprobante.Numero)
                //{
                    CuerpoSolicitud.CbteDesde = ultimoNroComprobante;
                    CuerpoSolicitud.CbteHasta = ultimoNroComprobante;

                    DateTime fechaHard = DateTime.Now;

                    CuerpoSolicitud.CbteFch = fechaHard.ToString("yyyyMMdd");// model.Fecha.ToString("yyyyMMdd") ;//DateTime.Today.ToString("yyyyMMdd");

                    CuerpoSolicitud.ImpTotal = decimal.ToDouble(Math.Round(model.MontoAjuste, 2));
                    CuerpoSolicitud.ImpNeto = decimal.ToDouble(Math.Round(model.MontoAjuste, 2));
                    CuerpoSolicitud.ImpIVA = 0;
                    CuerpoSolicitud.ImpTotConc = 0;
                    CuerpoSolicitud.ImpOpEx = 0;
                    CuerpoSolicitud.ImpTrib = 0;
                    CuerpoSolicitud.FchServDesde = fechaHard.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                    CuerpoSolicitud.FchServHasta = fechaHard.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                    CuerpoSolicitud.FchVtoPago = (fechaHard.AddDays(180)).ToString("yyyyMMdd");//(model.Fecha.AddDays(180)).ToString("yyyyMMdd"); //DateTime.Today.ToString("yyyyMMdd");

                   

                    switch (model.idTipoMoneda)
                    {
                        case 1:
                            CuerpoSolicitud.MonId = "PES";
                            CuerpoSolicitud.MonCotiz = 1;
                            break;
                        case 2:
                            FECotizacionResponse paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, "DOL");
                            CuerpoSolicitud.MonId = "DOL";
                            CuerpoSolicitud.MonCotiz = paramCoti.ResultGet.MonCotiz;
                            break;
                    }

                    //AlicIva Alicuota = new AlicIva();
                    ////el id de alicuota es el tipo de iva
                    //Alicuota.Id = 3;
                    //Alicuota.BaseImp = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                    //Alicuota.Importe = 0;

                    //CuerpoSolicitud.Iva = new[] { Alicuota };

                    CbteAsoc cbteAsoc = new CbteAsoc();
                    cbteAsoc.Nro = long.Parse(model.AplicaNC); //nro factura;
                    cbteAsoc.Tipo = 11;
                    cbteAsoc.PtoVta = model.IdPuntoVenta;
                    cbteAsoc.Cuit = model.Cuit;

                    CuerpoSolicitud.CbtesAsoc = new[] { cbteAsoc };

                    Solicitud.FeDetReq = new[] { CuerpoSolicitud };

                    Respuesta = ServicioWebFactura.FECAESolicitar(Autenticacion, Solicitud);

                    return Respuesta;
                //}
                //else
                //{
                //    Err error = new Err();
                //    error.Code = 0;
                //    error.Msg = "El nro de comprobante local no coincide con el de AFIP";
                //    Respuesta.Errors = new[] { error };
                //    return Respuesta;
                //}
            }
            catch (Exception e )
            {
                return null;
            }
           
        }

        public FECAEResponse InsertarNotaCreditoAfipLocal(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
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
                int tipoComprobante = Comprobante.CodigoAfip;

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

                //autorizarse
                FERecuperaLastCbteResponse UltimoRes = ServicioWebFactura.FECompUltimoAutorizado(Autenticacion, puntoVenta, 13);
                int ultimoNroComprobante = UltimoRes.CbteNro + 1;

                FECAEResponse Respuesta = new FECAEResponse();

                //if (ultimoNroComprobante == Comprobante.Numero)
                //{
                    CuerpoSolicitud.CbteDesde = ultimoNroComprobante;
                    CuerpoSolicitud.CbteHasta = ultimoNroComprobante;

                    DateTime fechaHard = DateTime.Now;

                    CuerpoSolicitud.CbteFch = fechaHard.ToString("yyyyMMdd");// model.Fecha.ToString("yyyyMMdd") ;//DateTime.Today.ToString("yyyyMMdd");

                    //pongo el monto ajuste para indicar el monto de la nota de credito
                    CuerpoSolicitud.ImpTotal = decimal.ToDouble(Math.Round(model.MontoAjuste, 2));
                    CuerpoSolicitud.ImpNeto = decimal.ToDouble(Math.Round(model.MontoAjuste, 2));
                    CuerpoSolicitud.ImpIVA = 0;
                    CuerpoSolicitud.ImpTotConc = 0;
                    CuerpoSolicitud.ImpOpEx = 0;
                    CuerpoSolicitud.ImpTrib = 0;
                    CuerpoSolicitud.FchServDesde = fechaHard.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                    CuerpoSolicitud.FchServHasta = fechaHard.ToString("yyyyMMdd");//model.Fecha.ToString("yyyyMMdd");//DateTime.Today.ToString("yyyyMMdd");
                    CuerpoSolicitud.FchVtoPago = (fechaHard.AddDays(180)).ToString("yyyyMMdd");
                    //(model.Fecha.AddDays(180)).ToString("yyyyMMdd"); //DateTime.Today.ToString("yyyyMMdd");


                    switch (model.idTipoMoneda)
                    {
                        case 1:
                            CuerpoSolicitud.MonId = "PES";
                            CuerpoSolicitud.MonCotiz = 1;
                            break;
                        case 2:
                            FECotizacionResponse paramCoti = ServicioWebFactura.FEParamGetCotizacion(Autenticacion, "DOL");
                            CuerpoSolicitud.MonId = "DOL";
                            CuerpoSolicitud.MonCotiz = paramCoti.ResultGet.MonCotiz;
                            break;
                    }

                    //AlicIva Alicuota = new AlicIva();
                    ////el id de alicuota es el tipo de iva
                    //Alicuota.Id = 3;
                    //Alicuota.BaseImp = decimal.ToDouble(Math.Round(model.TotalFactura, 2));
                    //Alicuota.Importe = 0;

                    //CuerpoSolicitud.Iva = new[] { Alicuota };

                    CbteAsoc cbteAsoc = new CbteAsoc();
                    cbteAsoc.Nro = long.Parse(model.AplicaNC); //nro factura;
                    cbteAsoc.Tipo = 11;
                    cbteAsoc.PtoVta = model.IdPuntoVenta;
                    cbteAsoc.Cuit = model.Cuit;

                    CuerpoSolicitud.CbtesAsoc = new[] { cbteAsoc };

                    Solicitud.FeDetReq = new[] { CuerpoSolicitud };

                    Respuesta = ServicioWebFactura.FECAESolicitar(Autenticacion, Solicitud);

                    return Respuesta;
                //}
                //else
                //{
                //    Err error = new Err();
                //    error.Code = 0;
                //    error.Msg = "El nro de comprobante local no coincide con el de AFIP";
                //    Respuesta.Errors = new[] { error };
                //    return Respuesta;
                //}
            }
            catch (Exception e)
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
                
                //verificaciona ws 
                var EstadoWs = ServicioWebFacturaExterior.FEXDummy();
             
                //FERecuperaLastCbteResponse UltimoRes = ServicioWebFacturaExterior.FECompUltimoAutorizado(Autenticacion, puntoVenta, tipoComprobante);
                //int ultimoNroComprobante = UltimoRes.CbteNro + 1;

                ClsFEX_LastCMP ultimoComprobante = new ClsFEX_LastCMP();
                ultimoComprobante.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                ultimoComprobante.Pto_venta = model.IdPuntoVenta;
                ultimoComprobante.Cuit = cuitPropietario;
                ultimoComprobante.Sign = TicketAcceso.Sign;
                ultimoComprobante.Token = TicketAcceso.Token;

                var cbte_nroObternido = ServicioWebFacturaExterior.FEXGetLast_CMP(ultimoComprobante);

                ClsFEXGetCMP clsFEXGetCMP = new ClsFEXGetCMP();
                clsFEXGetCMP.Cbte_nro = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro;
                clsFEXGetCMP.Cbte_tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                clsFEXGetCMP.Punto_vta = model.IdPuntoVenta;

                var UltimoCbte_Existente = ServicioWebFacturaExterior.FEXGetCMP(Autenticacion, clsFEXGetCMP);


                var ultimo_id = ServicioWebFacturaExterior.FEXGetLast_ID(Autenticacion);

                FEXResponseAuthorize Respuesta = new FEXResponseAuthorize();

                //if (Comprobante.Numero == cbte_nroObternido.FEXResult_LastCMP.Cbte_nro)
                //{
                    ClsFEXRequest solicitud = new ClsFEXRequest();

                //ClsFEXResponse_LastID UltimoId = new ClsFEXResponse_LastID();
                //var idU = UltimoId.Id + 1;

                    solicitud.Id = ultimo_id.FEXResultGet.Id + 1;
                    solicitud.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                    solicitud.Fecha_cbte = model.Fecha.ToString("yyyyMMdd");
                    solicitud.Punto_vta = model.IdPuntoVenta;
                    solicitud.Cbte_nro = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro + 1;
                    solicitud.Tipo_expo = 2; //servicios
                    solicitud.Permiso_existente = "";
                    solicitud.Dst_cmp = short.Parse(model.CodPaisAfip.ToString()); //pais destino
                    solicitud.Cliente = model.Cliente.Nombre;
                    solicitud.Cuit_pais_cliente = long.Parse(model.Cuit);
                    solicitud.Domicilio_cliente = model.DireccionCompuesta;
                    solicitud.Id_impositivo = null; //averiguar

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
                    solicitud.Obs_comerciales = "Observaciones comerciales";
                    solicitud.Imp_total = model.TotalFactura;
                    solicitud.Obs = "Sin observaciones";
                    solicitud.Forma_pago = "tktk";
                    solicitud.Fecha_pago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");

                    var icoterm = ServicioWebFacturaExterior.FEXGetPARAM_Incoterms(Autenticacion);

                    solicitud.Incoterms = null;
                    solicitud.Incoterms_Ds = null;
                    solicitud.Idioma_cbte = short.Parse(model.IdTipoIdioma.ToString());
                    solicitud.Permisos = null; // ver este item

                    ///*defino el comprobante asociado ej Factura E*/
                    //Cmp_asoc ComprobanteAsociado = new Cmp_asoc();
                    //ComprobanteAsociado.Cbte_tipo = 19;
                    //ComprobanteAsociado.Cbte_punto_vta = model.IdPuntoVenta;
                    //ComprobanteAsociado.Cbte_nro = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro + 1;
                    //ComprobanteAsociado.Cbte_cuit = long.Parse(model.Cuit);
                    ////asigno el comprobante a la nota debito que estoy generando
                    //solicitud.Cmps_asoc = new[] { ComprobanteAsociado };


                    /*agrego el item*/
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
                    Respuesta = ServicioWebFacturaExterior.FEXAuthorize(Autenticacion, solicitud);


                    if (Respuesta.FEXErr != null)
                    {
                        if (Respuesta.FEXErr.ErrMsg == "OK")
                        {
                            return Respuesta;
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
                //}
                //else
                //{
                //    ClsFEXErr error = new ClsFEXErr();
                //    error.ErrCode = 0;
                //    error.ErrMsg = "El nro de comprobante local no coincide con el de AFIP";
                //    Respuesta.FEXErr = error;
                //    return Respuesta;
                //}

            }
            catch
            {
                return null;
            }

        }

        public FEXResponseAuthorize InsertarNotaDebitoAfipExterior(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
            {
                //var totalImporte = model.TotalFactura;

                //instancio objeto autenticacion
                ClsFEXAuthRequest Autenticacion = new ClsFEXAuthRequest();
                Autenticacion.Cuit = cuitPropietario;//long.Parse(model.Cuit);
                Autenticacion.Sign = TicketAcceso.Sign;
                Autenticacion.Token = TicketAcceso.Token;

                //se prepara el servicio para enviar
                afip.wswhomo_Exportacion.Service ServicioWebFacturaExterior = new afip.wswhomo_Exportacion.Service();
                ServicioWebFacturaExterior.Url = @"https://wswhomo.afip.gov.ar/wsfexv1/service.asmx?WSDL";

                ServicioWebFacturaExterior.ClientCertificates.Add(TicketAcceso.certificado);
                
                //verificaciona ws 
                var EstadoWs = ServicioWebFacturaExterior.FEXDummy();

                //FERecuperaLastCbteResponse UltimoRes = ServicioWebFacturaExterior.FECompUltimoAutorizado(Autenticacion, puntoVenta, tipoComprobante);
                //int ultimoNroComprobante = UltimoRes.CbteNro + 1;

                ClsFEX_LastCMP ultimoComprobante = new ClsFEX_LastCMP();
                ultimoComprobante.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                ultimoComprobante.Pto_venta = model.IdPuntoVenta;
                ultimoComprobante.Cuit = cuitPropietario;
                ultimoComprobante.Sign = TicketAcceso.Sign;
                ultimoComprobante.Token = TicketAcceso.Token;

                var cbte_nroObternido = ServicioWebFacturaExterior.FEXGetLast_CMP(ultimoComprobante);
                //var NroCbte = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro + 1;

                //obtengo el ultimo comprobante cagado en afip
                ClsFEXGetCMP clsFEXGetCMP = new ClsFEXGetCMP();
                clsFEXGetCMP.Cbte_nro = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro+1;
                clsFEXGetCMP.Cbte_tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                clsFEXGetCMP.Punto_vta = model.IdPuntoVenta;

                var UltimoCbte_Existente = ServicioWebFacturaExterior.FEXGetCMP(Autenticacion, clsFEXGetCMP);

                var ultimo_id = ServicioWebFacturaExterior.FEXGetLast_ID(Autenticacion);


                FEXResponseAuthorize Respuesta = new FEXResponseAuthorize();

                //if (Comprobante.Numero == cbte_nroObternido.FEXResult_LastCMP.Cbte_nro + 1)
                //{
                ClsFEXRequest solicitud = new ClsFEXRequest();
                solicitud.Id = ultimo_id.FEXResultGet.Id + 1;
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

                /*defino el comprobante asociado ej Factura E*/
                Cmp_asoc ComprobanteAsociado = new Cmp_asoc();
                ComprobanteAsociado.Cbte_tipo = 19;
                ComprobanteAsociado.Cbte_punto_vta = model.IdPuntoVenta;
                ComprobanteAsociado.Cbte_nro = long.Parse(model.AplicaNC);
                /*cambio esto por mi cuil
                 error:Campo Cmps_asoc.Cbte_tipo: Para comprobantes tipo 20 o 21 si informa el cuit del emisor del comprobante asociado, debe ser igual al emisor del comprobante que esta autorizando
                 */
                ComprobanteAsociado.Cbte_cuit = cuitPropietario;//long.Parse(model.Cuit);
                //asigno el comprobante a la nota debito que estoy generando
                solicitud.Cmps_asoc = new[] { ComprobanteAsociado };

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
                
                solicitud.Obs = null;
                solicitud.Forma_pago = null;
               // solicitud.Fecha_pago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");
                solicitud.Incoterms = null;
                solicitud.Incoterms_Ds = null;
                solicitud.Idioma_cbte = short.Parse(model.IdTipoIdioma.ToString());
                solicitud.Permisos = null;

                Item item = new Item();
                item.Pro_codigo = null;
                item.Pro_ds = null;

                Item itemGrabar = new Item();
                itemGrabar.Pro_codigo = "1";
                itemGrabar.Pro_ds = "Nd item de Factura";
                itemGrabar.Pro_qty = 1;
                itemGrabar.Pro_umed = 1;
                itemGrabar.Pro_precio_uni = decimal.Parse(model.MontoAjuste.ToString());
                itemGrabar.Pro_bonificacion = 0;
                itemGrabar.Pro_total_item = itemGrabar.Pro_qty * itemGrabar.Pro_precio_uni - itemGrabar.Pro_bonificacion;

                solicitud.Imp_total = itemGrabar.Pro_total_item;

                //inserto los items
                solicitud.Items = new[] { itemGrabar };

                ////se envia el servicio al WS
               Respuesta = ServicioWebFacturaExterior.FEXAuthorize(Autenticacion, solicitud);

                if (Respuesta.FEXErr != null)
                {
                    if (Respuesta.FEXErr.ErrMsg == "OK")
                    {
                        return Respuesta;
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
                //}
                //else
                //{
                //    ClsFEXErr error = new ClsFEXErr();
                //    error.ErrCode = 0;
                //    error.ErrMsg = "El nro de comprobante local no coincide con el de AFIP";
                //    Respuesta.FEXErr = error;
                //    return Respuesta;
                //}
                return null;
            }
            catch
            {
                return null;
            }

        }

        public FEXResponseAuthorize InsertarNotaCreditoAfipExterior(ClaseLoginAfip TicketAcceso, FacturaModelView model, TipoComprobanteVentaModel Comprobante, long cuitPropietario)
        {
            try
            {
               // var totalImporte = model.TotalFactura;

                //instancio objeto autenticacion
                ClsFEXAuthRequest Autenticacion = new ClsFEXAuthRequest();
                Autenticacion.Cuit = cuitPropietario;//long.Parse(model.Cuit);
                Autenticacion.Sign = TicketAcceso.Sign;
                Autenticacion.Token = TicketAcceso.Token;

                //se prepara el servicio para enviar
                afip.wswhomo_Exportacion.Service ServicioWebFacturaExterior = new afip.wswhomo_Exportacion.Service();
                ServicioWebFacturaExterior.Url = @"https://wswhomo.afip.gov.ar/wsfexv1/service.asmx?WSDL";

                ServicioWebFacturaExterior.ClientCertificates.Add(TicketAcceso.certificado);

                //verificaciona ws 
                var EstadoWs = ServicioWebFacturaExterior.FEXDummy();


                ////cargo los datos de la factura
                //int puntoVenta = model.IdPuntoVenta;
                //int tipoComprobante = model.idTipoComprobanteSeleccionado;


                //FERecuperaLastCbteResponse UltimoRes = ServicioWebFacturaExterior.FECompUltimoAutorizado(Autenticacion, puntoVenta, tipoComprobante);
                //int ultimoNroComprobante = UltimoRes.CbteNro + 1;

                ClsFEX_LastCMP ultimoComprobante = new ClsFEX_LastCMP();
                ultimoComprobante.Cbte_Tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                ultimoComprobante.Pto_venta = model.IdPuntoVenta;
                ultimoComprobante.Cuit = cuitPropietario;
                ultimoComprobante.Sign = TicketAcceso.Sign;
                ultimoComprobante.Token = TicketAcceso.Token;

                var cbte_nroObternido = ServicioWebFacturaExterior.FEXGetLast_CMP(ultimoComprobante);

                //obtengo el ultimo comprobante cagado en afip
                ClsFEXGetCMP clsFEXGetCMP = new ClsFEXGetCMP();
                clsFEXGetCMP.Cbte_nro = cbte_nroObternido.FEXResult_LastCMP.Cbte_nro + 1;
                clsFEXGetCMP.Cbte_tipo = short.Parse(Comprobante.CodigoAfip.ToString());
                clsFEXGetCMP.Punto_vta = model.IdPuntoVenta;

                var UltimoCbte_Existente = ServicioWebFacturaExterior.FEXGetCMP(Autenticacion, clsFEXGetCMP);

                var ultimo_id = ServicioWebFacturaExterior.FEXGetLast_ID(Autenticacion);


                FEXResponseAuthorize Respuesta = new FEXResponseAuthorize();

                //if (Comprobante.Numero == cbte_nroObternido.FEXResult_LastCMP.Cbte_nro)
                //{
                    ClsFEXRequest solicitud = new ClsFEXRequest();
                    solicitud.Id = ultimo_id.FEXResultGet.Id + 1;
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

                    /*defino el comprobante asociado ej Factura E*/
                    Cmp_asoc ComprobanteAsociado = new Cmp_asoc();
                    ComprobanteAsociado.Cbte_tipo = 19;
                    ComprobanteAsociado.Cbte_punto_vta = model.IdPuntoVenta;
                    ComprobanteAsociado.Cbte_nro = long.Parse(model.AplicaNC);
                    /*cambio esto por mi cuil
                  error:Campo Cmps_asoc.Cbte_tipo: Para comprobantes tipo 20 o 21 si informa el cuit del emisor del comprobante asociado, debe ser igual al emisor del comprobante que esta autorizando
                  */
                    ComprobanteAsociado.Cbte_cuit = cuitPropietario;//long.Parse(model.Cuit);

                    //asigno el comprobante a la nota debito que estoy generando
                    solicitud.Cmps_asoc = new[] { ComprobanteAsociado };

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
                   
                    solicitud.Obs = null;
                    solicitud.Forma_pago = null;
                   //solicitud.Fecha_pago = (model.Fecha.AddDays(180)).ToString("yyyyMMdd");
                    solicitud.Incoterms = null;
                    solicitud.Incoterms_Ds = null;
                    solicitud.Idioma_cbte = short.Parse(model.IdTipoIdioma.ToString());
                    solicitud.Permisos = null;

                    Item item = new Item();
                    item.Pro_codigo = null;
                    item.Pro_ds = null;

                    Item itemGrabar = new Item();
                    itemGrabar.Pro_codigo = "1";
                    itemGrabar.Pro_ds = "Nc item de Factura";
                    itemGrabar.Pro_qty = 1;
                    itemGrabar.Pro_umed = 1;
                    itemGrabar.Pro_precio_uni = decimal.Parse(model.MontoAjuste.ToString());
                    itemGrabar.Pro_bonificacion = 0;
                    itemGrabar.Pro_total_item = itemGrabar.Pro_qty * itemGrabar.Pro_precio_uni - itemGrabar.Pro_bonificacion;

                    solicitud.Imp_total = itemGrabar.Pro_total_item;
                    //inserto los items
                    solicitud.Items = new[] { itemGrabar };

                    //se envia el servicio al WS
                    Respuesta = ServicioWebFacturaExterior.FEXAuthorize(Autenticacion, solicitud);

                    if (Respuesta.FEXErr != null)
                    {
                        if (Respuesta.FEXErr.ErrMsg == "OK")
                        {
                            return Respuesta;
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
                //}
                //else
                //{
                //    ClsFEXErr error = new ClsFEXErr();
                //    error.ErrCode = 0;
                //    error.ErrMsg = "El nro de comprobante local no coincide con el de AFIP";
                //    Respuesta.FEXErr = error;
                //    return Respuesta;
                //}

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