using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;

namespace Negocio.Servicios
{

    public class ServicioCompra : ServicioBase
    {

        ServicioTipoComprobanteVenta oServicioTipoComprobanteVenta = new ServicioTipoComprobanteVenta();
        ServicioTipoMoneda oServicioTipoMoneda = new ServicioTipoMoneda();
        ServicioChequera oServicioChequera = new ServicioChequera();
        ServicioCheque oServicioCheque = new ServicioCheque();
        ServicioCompraFacturaPago oServicioCompraFacturaPago = new ServicioCompraFacturaPago();
        ServicioBancoCuentaBancaria oServicioBancoCuentaBancaria = new ServicioBancoCuentaBancaria();
        ServicioProveedor oServicioProveedor = new ServicioProveedor();
        ServicioPresupuestoActual oServicioPresupuestoActual = new ServicioPresupuestoActual();
        ServicioPresupuestoCosto oServicioPresupuestoCosto = new ServicioPresupuestoCosto();
        ServicioPresupuestoItem oServicioPresupuestoItem = new ServicioPresupuestoItem();
        ServicioCaja oServicioCaja = new ServicioCaja();
        ServicioTarjetaOperacion oServicioTarjetaOperacion = new ServicioTarjetaOperacion();
        ServicioImputacion oServicioImputacion = new ServicioImputacion();

        private CompraRepositorio repositorio { get; set; }

        //public Action<string, string> _mensaje;
        public ServicioCompra()
        {
            repositorio = kernel.Get<CompraRepositorio>();
        }
        public CompraFacturaModel CreateFactura(CompraFacturaModel model)
        {
            try
            {
                model.Proveedor = null;
                model.CompraFacturaPago = null;
                model.FechaPago = DateTime.Now;
                model.UltimaModificacion = DateTime.Now;
                model.CompraIva.UltimaModificacion = DateTime.Now;
                model.Activo = true;
                CompraFactura compraFactura = Mapper.Map<CompraFacturaModel, CompraFactura>(model);
                compraFactura = repositorio.Insertar(compraFactura);
                _mensaje("Se guardo la factura Correctamente", "ok");
                return Mapper.Map<CompraFactura, CompraFacturaModel>(compraFactura);
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                throw new Exception();
            }

        }

        public CompraFacturaModel RegistrarPago(CompraFacturaModel model)
        {
            try
            {                
                var  compraFactura = repositorio.CreateFactura(Mapper.Map<CompraFacturaModel, CompraFactura>(model));
                return Mapper.Map<CompraFactura,CompraFacturaModel>(compraFactura);
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }

        public List<CompraFacturaModel> GetAllCompraFactura()
        {
            try
            {
               return  Mapper.Map <List<CompraFactura>, List<CompraFacturaModel> > (repositorio.GetAllCompraFactura());
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
         
        }
        public bool ValidarFacturaPorNroFacturaIdProveedor(int numeroFactura, int idProveedor)
        {
            try
            {
                // validar numero de factura para el proveedor no debe existir
                var factura = Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.GetCompraFacturaPorNroFacturaIdProveedor(numeroFactura,idProveedor));
                if (factura != null)
                {
                    _mensaje("Ya existe el número de Factura para el Proveedor", "error");
                    return true;
                }                
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                throw new Exception();
            }
            return false;
        }



        public List<ProveedorModel> GetProveedorPorNombre(string strProveedor)
        {
            try
            {
                return Mapper.Map<List<Proveedor>, List<ProveedorModel>>(repositorio.GetProveedorPorNombre(strProveedor));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
        }

        public CompraFacturaModel ObtenerPorID(int id)
        {
            try
            {              
                return  Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.GetCompraFacturaPorId(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
            
        }

  public List<CompraFacturaModel> GetAllCompraFacturaPorNro(int NroFactura)
        {
            try
            {
                return Mapper.Map<List<CompraFactura>, List<CompraFacturaModel>>(repositorio.GetAllCompraFacturaPorNro(NroFactura));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                throw new Exception();
            }
        }

        public CompraFacturaModel GetCompraFacturaIVAPorNro(int NroFactura)
        {
            try
            {
                return Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.GetCompraFacturaIVAPorNro(NroFactura));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                throw new Exception();
            }
        }
      

        public List<CompraFacturaModel> ObtenerListaPorID(int id)
        {
            try
            {
                return Mapper.Map<List<CompraFactura>, List<CompraFacturaModel>>(repositorio.GetCompraFacturaListaPorId(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }
        public List<CompraFacturaModel> ObtenerPorIDProveedor(int id)
        {
            try
            {
                return Mapper.Map<List<CompraFactura>, List<CompraFacturaModel>>(repositorio.GetCompraFacturaPorIdProveedor(id));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "error");
                return null;
            }
        }

        public CompraFacturaModel ActualizarCompraFacturaPago(CompraFacturaModel oCompraFacturaModel)
        {
            try
            {
               return Mapper.Map<CompraFactura, CompraFacturaModel>( repositorio.ActualizarCompraFacturaPago(Mapper.Map<CompraFacturaModel, CompraFactura>(oCompraFacturaModel)));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();
            }
        }




        /// <summary>
        /// metodo que permite registrar un pago impacta en tbl comprafactura, caja, banco, cheques, presupuesto, proveedores
        /// </summary>
        /// <param name="oCompraFacturaModel"></param>
        public void RegistrarPago(FacturaPagoModel oCompraFacturaModel)
        {
            try
            {

                //controlo ingreso de los datos
                if (oCompraFacturaModel.efectivo == 0 && oCompraFacturaModel.idChequesPropios == null
                    && oCompraFacturaModel.idChequesTerceros == null && oCompraFacturaModel.idCuentasBancarias == "0"
                    && oCompraFacturaModel.idTarjeta == 0)
                {
                    _mensaje("Ops!, Faltan datos!!!", "war");
                }
                else
                {
                    string[] FacturasaPagar = oCompraFacturaModel.idFacturas.Split(';');
                    int nroPago = 0;
                    foreach (var item in FacturasaPagar)
                    {
                       
                        CompraFacturaPagoModel FacturaMedioPago = new CompraFacturaPagoModel();

                        CompraFacturaModel Factura = ObtenerPorID(int.Parse(item));
                        ProveedorModel facturaProveedor = oServicioProveedor.GetProveedor(Factura.IdProveedor);
                        nroPago = oServicioTipoComprobanteVenta.ObtenerNroPago(98);

                        if (nroPago == 0)
                        {           
                            _mensaje("Ops!, Error al intentar obtener el número de pago. contacte al administrador", "erro");
                        }
                        else
                        {

                            //estos seteos son para grabar en caja
                            decimal ValorFactura = oCompraFacturaModel.TotalAPagar;
                            decimal ImporteCheque = oCompraFacturaModel.montoChequesSeleccionados;
                            decimal ImporteTarjeta = oCompraFacturaModel.montoTarjetaSeleccionados;
                            decimal ImporteTranferencia = oCompraFacturaModel.montoTranferencia;
                            int cuentaBanco = 0;
                            //------------------------------------

                            //seteo los valores para avutalizar la tabla Compra facturas, punto 1 documento CtaCte
                            Factura.FechaPago = DateTime.Now;
                            Factura.NumeroPago = (nroPago + 1).ToString();
                            Factura.CotizacionDePago = 0;

                            //enviamos los datos a modificar y recupetamos la entidad actualizada
                            Factura = ActualizarCompraFacturaPago(Factura);

                            // seteo el tipo de moneda para grabar en caja
                            int TipoDeMoneda = Factura.IdMoneda;
                            //--------------------------------------

                            oServicioTipoComprobanteVenta.ActualizarNroPago(98, nroPago + 1);

                            // creamos el nuevo registro CompraFacturaModel 
                            Factura.Id = 0;
                            Factura.IdTipoComprobante = 98;
                            Factura.PuntoVenta = 3;
                            Factura.NumeroPago = (nroPago + 1).ToString();
                            Factura.Total = oCompraFacturaModel.TotalAPagar;

                            Factura.Parcial = oCompraFacturaModel.Diferencia;
                            Factura.Saldo = oCompraFacturaModel.Diferencia;
                            Factura.Fecha = DateTime.Now;
                            Factura.Vencimiento = DateTime.Now;
                            //Factura.TotalDolares

                            //obtengo la cotizacion 
                            ValorCotizacionModel valorCotizacion = oServicioTipoMoneda.GetCotizacionPorIdMoneda(Factura.Fecha, Factura.IdMoneda);
                            //-----
                            if (valorCotizacion != null)
                            {
                                Factura.Cotizacion = valorCotizacion.Monto;
                            }
                            else
                            {
                                Factura.Cotizacion = 0;
                            }

                            Factura.FechaPago = DateTime.Now;
                            string anio = DateTime.Now.Year.ToString();
                            anio = anio.Substring(anio.Length - 2, 2);
                            Factura.Periodo = int.Parse(anio + DateTime.Now.Month.ToString());

                            Factura.TipoMoneda = null;
                            Factura.Proveedor = null;
                            Factura.CompraIva = null;


                            Factura.CotizacionDePago = 0; //esta en la servicio tipoMoneda
                            Factura.Concepto = "Pago factura";
                            Factura.IdImputacion = 0;
                            Factura.IdCompraIva = null;
                            Factura.Imputacion = null;
                            Factura.IdUsuario = oCompraFacturaModel.idUsuario;
                            Factura.UltimaModificacion = DateTime.Now;

                            var Retorno = RegistrarPago(Factura);

                            //------aca insertamos los medios de pago
                            //if (oCompraFacturaModel.idCuentasBancarias != null)
                            //{
                            //    FacturaMedioPago.IdFacturaCompra =int.Parse(item);
                            //    FacturaMedioPago.IdTipoPago = 1;
                            //    FacturaMedioPago.Id_pago = int.Parse(oCompraFacturaModel.idCuentasBancarias);
                            //    FacturaMedioPago.Monto = oCompraFacturaModel.montoTranferencia;
                            //    FacturaMedioPago.Observaciones = "tranferencia bancaria";
                            //    FacturaMedioPago.Activo = true;
                            //    FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                            //    oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);
                            //}

                            if (oCompraFacturaModel.efectivo != 0)
                            {
                                FacturaMedioPago.IdFacturaCompra = int.Parse(item);
                                FacturaMedioPago.IdTipoPago = 2;
                                FacturaMedioPago.Monto = oCompraFacturaModel.efectivo;
                                FacturaMedioPago.Observaciones = "Efectivo";
                                FacturaMedioPago.Activo = true;
                                FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                                FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                            }

                            if (oCompraFacturaModel.idChequesPropios != null)
                            {
                                ChequeraModel oChequera = new ChequeraModel();
                                string[] chequesSeleccionados = oCompraFacturaModel.idChequesPropios.Split(';');
                                foreach (var itemCheque in chequesSeleccionados)
                                {
                                    oChequera = oServicioChequera.obtenerCheque(int.Parse(itemCheque));
                                    //inserto medio de pago
                                    FacturaMedioPago.IdFacturaCompra = int.Parse(item);
                                    FacturaMedioPago.IdTipoPago = 3;
                                    FacturaMedioPago.IdChequera = int.Parse(itemCheque);
                                    FacturaMedioPago.Monto = decimal.Parse(oChequera.Importes.ToString());
                                    FacturaMedioPago.Observaciones = "Cheques propios";
                                    FacturaMedioPago.Activo = true;
                                    FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                                    FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                    oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                    //actualizo el cheque en la chequera
                                    oChequera.NumeroOperacion = nroPago + 1;
                                    oChequera.UltimaModificacion = DateTime.Now;
                                    oChequera.IdUsuario = oCompraFacturaModel.idUsuario;
                                    oChequera.Usado = true;//lo inactivo porque ya se uso                                 
                                    oChequera.Destino = "Pago a proveedor" + facturaProveedor.Nombre;
                                    oChequera.IdProveedor = facturaProveedor.Id;
                                    oChequera.IdUsuario = oCompraFacturaModel.idUsuario;
                                    oChequera.UltimaModificacion = DateTime.Now;
                                    oServicioChequera.Actualizar(oChequera);


                                    //registrar movimiento cuenta bancaria porque el cheque propio lo toma asi
                                    BancoCuentaBancariaModel oBancoCuentaBancariaModel = new BancoCuentaBancariaModel();
                                    oBancoCuentaBancariaModel.NumeroOperacion = nroPago + 1;
                                    oBancoCuentaBancariaModel.IdBancoCuenta = oChequera.IdBancoCuenta ?? 0;
                                    oBancoCuentaBancariaModel.CuentaDescripcion = oChequera.BancoCuenta.BancoDescripcion;
                                    oBancoCuentaBancariaModel.Fecha = DateTime.Now; 
                                    oBancoCuentaBancariaModel.FechaEfectiva = DateTime.Now;
                                    oBancoCuentaBancariaModel.DiaClearing = "";
                                    oBancoCuentaBancariaModel.Importe = decimal.Parse(oChequera.Importes.ToString());
                                    oBancoCuentaBancariaModel.IdCliente = Factura.IdProveedor.ToString();
                                    oBancoCuentaBancariaModel.Conciliacion = "F";

                                    // oBancoCuentaBancariaModel.FechaIngreso = DateTime.Now;//es double
                                    oBancoCuentaBancariaModel.Activo = true;
                                    oBancoCuentaBancariaModel.IdImputacion = "0";
                                    oBancoCuentaBancariaModel.IdUsuario = oCompraFacturaModel.idUsuario;
                                    oBancoCuentaBancariaModel.UltimaModificacion = DateTime.Now;

                                    oServicioBancoCuentaBancaria.Agregar(oBancoCuentaBancariaModel);

                                }
                            }

                            if (oCompraFacturaModel.idChequesTerceros != null)
                            {
                                ChequeModel oCheque = new ChequeModel();
                                string[] chequesSeleccionados = oCompraFacturaModel.idChequesTerceros.Split(';');
                                foreach (var itemCheque in chequesSeleccionados)
                                {
                                    oCheque = oServicioCheque.obtenerCheque(int.Parse(itemCheque));

                                    FacturaMedioPago.IdFacturaCompra = int.Parse(item);
                                    FacturaMedioPago.IdTipoPago = 3;
                                    FacturaMedioPago.IdCheque = int.Parse(itemCheque);
                                    FacturaMedioPago.Monto = decimal.Parse(oCheque.Importe.ToString());
                                    FacturaMedioPago.Observaciones = "Cheques propios";
                                    FacturaMedioPago.Activo = true;
                                    FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                                    FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                    oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                    //registrar tabla cheques
                                    oCheque.FechaEgreso = DateTime.Now;
                                    oCheque.Destino = Factura.IdProveedor.ToString();//id proveedor
                                    oCheque.IdMoneda = oCheque.IdMoneda;
                                    oCheque.NumeroPago = (nroPago + 1).ToString();
                                    oCheque.Proveedor = Factura.IdProveedor.ToString();
                                    // oCheque.Activo = false;
                                    oCheque.Endosado = true;

                                    oServicioCheque.Actualizar(oCheque);

                                }
                            }

                            if (oCompraFacturaModel.idCuentasBancarias != "0")
                            {
                                FacturaMedioPago.IdFacturaCompra = int.Parse(item);
                                FacturaMedioPago.IdTipoPago = 4;
                                FacturaMedioPago.IdBancoCuenta = int.Parse(oCompraFacturaModel.idCuentasBancarias);
                                FacturaMedioPago.Monto = oCompraFacturaModel.montoTranferencia;
                                FacturaMedioPago.Observaciones = "Tranferecia Bancaria";
                                FacturaMedioPago.Activo = true;
                                FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                                FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);
                                //registrar movimiento cuenta bancaria

                                //seteo para grabar en caja
                                cuentaBanco = int.Parse(oCompraFacturaModel.idCuentasBancarias);
                                //--------------------------

                                //registrar movimiento cuenta bancaria porque el cheque propio lo toma asi
                                BancoCuentaBancariaModel oBancoCuentaBancariaModel = new BancoCuentaBancariaModel();
                                oBancoCuentaBancariaModel.NumeroOperacion = nroPago + 1;
                                oBancoCuentaBancariaModel.IdBancoCuenta = int.Parse(oCompraFacturaModel.idCuentasBancarias);
                                // oBancoCuentaBancariaModel.CuentaDescripcion = oCompraFacturaModel. BancoCuenta.Descripcion;
                                //oBancoCuentaBancariaModel.Fecha = DateTime.Now; // esta como float
                                oBancoCuentaBancariaModel.FechaEfectiva = DateTime.Now;
                                oBancoCuentaBancariaModel.DiaClearing = "";
                                oBancoCuentaBancariaModel.Importe = decimal.Parse(oCompraFacturaModel.montoTranferencia.ToString());
                                oBancoCuentaBancariaModel.IdCliente = Factura.IdProveedor.ToString();
                                oBancoCuentaBancariaModel.Conciliacion = "F";
                                // oBancoCuentaBancariaModel.FechaIngreso = DateTime.Now;//es double
                                oBancoCuentaBancariaModel.IdImputacion = "0";
                                oServicioBancoCuentaBancaria.Agregar(oBancoCuentaBancariaModel);

                            }

                            if (oCompraFacturaModel.idTarjeta != 0)
                            {
                                FacturaMedioPago.IdFacturaCompra = int.Parse(item);
                                FacturaMedioPago.IdTipoPago = 5;
                                FacturaMedioPago.IdTarjeta = oCompraFacturaModel.idTarjeta;
                                FacturaMedioPago.Monto = oCompraFacturaModel.montoTarjetaSeleccionados;
                                FacturaMedioPago.Observaciones = "Tarjeta";
                                FacturaMedioPago.Activo = true;
                                FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                                FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);


                                //obtengo idGrupo caja de presupuesto
                                PresupuestoActualModel oPresupuestoActualModel = new PresupuestoActualModel();
                                oPresupuestoActualModel = oServicioPresupuestoActual.GetAllPresupuestos(oCompraFacturaModel.idPresupuesto);
                                int idGrupoCaja = oPresupuestoActualModel.IdGrupoCaja;
                                //registra tarjeta

                                TarjetaOperacionModel oTarjetaOperacionModel = new TarjetaOperacionModel();
                                oTarjetaOperacionModel.IdTarjeta = oCompraFacturaModel.idTarjeta;
                                oTarjetaOperacionModel.Descripcion = "Pago Factura: " + Factura.NumeroFactura;
                                oTarjetaOperacionModel.IdGrupoCaja = idGrupoCaja;
                                oTarjetaOperacionModel.Conciliacion = false;
                                oTarjetaOperacionModel.NumeroPago = (nroPago + 1).ToString();
                                oTarjetaOperacionModel.Activo = true;
                                oTarjetaOperacionModel.IdUsuario = oCompraFacturaModel.idUsuario;
                                oTarjetaOperacionModel.UltimaModificacion = DateTime.Now;
                                oServicioTarjetaOperacion.Insertar(oTarjetaOperacionModel);

                            }


                            //grabo en caja
                            CajaModel oCajaModel = new CajaModel();

                            oCajaModel.IdTipoMovimiento = 1;
                            oCajaModel.Concepto = "nro factura: " + Factura.NumeroFactura;
                            oCajaModel.Fecha = DateTime.Now;
                            oCajaModel.Tipo = "";
                            oCajaModel.Saldo = "";
                            oCajaModel.Recibo = "";

                            if (TipoDeMoneda == 1)
                            {
                                oCajaModel.ImportePesos = ValorFactura;
                            }
                            else if (TipoDeMoneda == 2)
                            {
                                oCajaModel.ImporteDolar = ValorFactura;
                            }

                            oCajaModel.IdCuentaBanco = cuentaBanco;
                            oCajaModel.ImporteCheque = ImporteCheque;
                            oCajaModel.ImporteDeposito = ImporteTranferencia;
                            oCajaModel.ImporteTarjeta = ImporteTarjeta;

                            oCajaModel.IdCuentaBanco = int.Parse(oCompraFacturaModel.idCuentasBancarias);


                         
                            //validar y actualizar presupuesto 

                            PresupuestoActualModel oPrespuestoActual = oServicioPresupuestoActual.GetAllPresupuestos(oCompraFacturaModel.idPresupuesto);
                            oPrespuestoActual.Ejecutado += Factura.Total;
                            oPrespuestoActual.IdUsuario = oCompraFacturaModel.idUsuario;
                            oPrespuestoActual.UltimaModificacion = DateTime.Now;
                            oServicioPresupuestoActual.ActualizarPresupuesto(oPrespuestoActual);

                            if (oPrespuestoActual.Id != 0)
                            {
                                PresupuestoCostoModel oPresupuestoCosto = oServicioPresupuestoCosto.GetAllPresupuestoCosto(oPrespuestoActual.Codigo);
                                if (oPresupuestoCosto != null)
                                {
                                    oPresupuestoCosto.Ejecutado += Factura.Total;
                                    oPresupuestoCosto.IdUsuario = oCompraFacturaModel.idUsuario;
                                    oPresupuestoCosto.UltimaModificacion = DateTime.Now;
                                    oServicioPresupuestoCosto.ActualizarPresupuesto(oPresupuestoCosto);
                                }

                            }

                            //formo el periodo
                            string anioPeriodo = DateTime.Now.Year.ToString();
                            anioPeriodo = anioPeriodo.Substring(anioPeriodo.Length - 2, 2);
                            PresupuestoItemModel presupuestoItem = new PresupuestoItemModel();
                            presupuestoItem.Codigo = oPrespuestoActual.Codigo;
                            presupuestoItem.Concepto = facturaProveedor.Nombre + "Nro Factura: " + Factura.NumeroFactura;
                            presupuestoItem.Pagado = Factura.Total;
                            presupuestoItem.Ejecutado = "true";
                            presupuestoItem.Periodo = int.Parse(anioPeriodo + DateTime.Now.Month.ToString());

                            PresupuestoItemModel oPresupuestoItem = oServicioPresupuestoItem.Insertar(presupuestoItem);



                            //aca tengo que hacer la parte de la caja pero espero a esteban
                            //sigue caja 

                            
                            /// ERROR AL ACTUALZIZAR PROVEEDOR
                          //  var oProveedor = oServicioProveedor.GetProveedor(Factura.IdProveedor);
                            facturaProveedor.idPresupuesto = oCompraFacturaModel.idPresupuesto;
                            facturaProveedor.UltimaModificacion = DateTime.Now;
                            facturaProveedor.IdUsuario = oCompraFacturaModel.idUsuario;
                            oServicioProveedor.ActualizarProveedor(facturaProveedor);

                            _mensaje("Se registro el pago correctamente", "ok");
                        }

                    }
                }



            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");

            }
        }



    }

}
