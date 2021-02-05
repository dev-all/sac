﻿using Datos.Interfaces;
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

        private ServicioTipoComprobanteVenta oServicioTipoComprobanteVenta = new ServicioTipoComprobanteVenta();
        private ServicioTipoComprobante oServicionTipoComprobante = new ServicioTipoComprobante();
        private ServicioTipoMoneda oServicioTipoMoneda = new ServicioTipoMoneda();
        private ServicioChequera oServicioChequera = new ServicioChequera();
        private ServicioCheque oServicioCheque = new ServicioCheque();
        private ServicioCompraFacturaPago oServicioCompraFacturaPago = new ServicioCompraFacturaPago();
        private ServicioBancoCuentaBancaria oServicioBancoCuentaBancaria = new ServicioBancoCuentaBancaria();
        private ServicioProveedor oServicioProveedor = new ServicioProveedor();
        private ServicioPresupuestoActual oServicioPresupuestoActual = new ServicioPresupuestoActual();
        private ServicioPresupuestoCosto oServicioPresupuestoCosto = new ServicioPresupuestoCosto();
        private ServicioPresupuestoItem oServicioPresupuestoItem = new ServicioPresupuestoItem();
        private ServicioCaja oServicioCaja = new ServicioCaja();
        private ServicioTarjetaOperacion oServicioTarjetaOperacion = new ServicioTarjetaOperacion();
        private ServicioImputacion oServicioImputacion = new ServicioImputacion();
        private ServicioContable oServicioContable = new ServicioContable();
        private ServicioTrackingFacturaPagoCompra oServicioTrackingFacturaPagoCompra = new ServicioTrackingFacturaPagoCompra();

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
                model.Total = model.CompraIva.Total;
                model.Saldo = model.CompraIva.Total;
                model.NumeroPago = 0; //agregue 28/01/2021 bre
                model.FechaPago = null;// DateTime.Now;
                model.UltimaModificacion = DateTime.Now;
                model.CompraIva.UltimaModificacion = DateTime.Now;
                model.Activo = true;

                if (model.IdMoneda != 1)
                {
                    model.TotalDolares = model.CompraIva.Total;
                }

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
                var compraFactura = repositorio.CreateFactura(Mapper.Map<CompraFacturaModel, CompraFactura>(model));
                return Mapper.Map<CompraFactura, CompraFacturaModel>(compraFactura);
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
                return Mapper.Map<List<CompraFactura>, List<CompraFacturaModel>>(repositorio.GetAllCompraFactura());
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
                var factura = Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.GetCompraFacturaPorNroFacturaIdProveedor(numeroFactura, idProveedor));
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
                return Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.GetCompraFacturaPorId(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }

        public CompraFacturaModel ObtenerPorID_paraPagos(int id)
        {
            try
            {
                return Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.ObtenerPorID_paraPagos(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }

        }

        //
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
        public List<CompraFacturaModel> ObtenerPorIDProveedor_Moneda(int id, int idMoneda)
        {

            try
            {
                return Mapper.Map<List<CompraFactura>, List<CompraFacturaModel>>(repositorio.GetCompraFacturaPorIdProveedor_Moneda(id, idMoneda));
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
                return Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.ActualizarCompraFacturaPago(Mapper.Map<CompraFacturaModel, CompraFactura>(oCompraFacturaModel)));
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
                            Factura.NumeroPago = nroPago + 1;
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
                            Factura.NumeroPago = nroPago + 1;
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

                            var ComprobantePago = RegistrarPago(Factura);

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
                                FacturaMedioPago.IdFacturaCompra = ComprobantePago.Id;
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
                                    FacturaMedioPago.IdFacturaCompra = ComprobantePago.Id;
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

                                    FacturaMedioPago.IdFacturaCompra = ComprobantePago.Id;
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
                                FacturaMedioPago.IdFacturaCompra = ComprobantePago.Id;
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
                                FacturaMedioPago.IdFacturaCompra = ComprobantePago.Id;
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


        /// <summary>
        /// Este metodo se usa para registrar el pago del modulo PAGOS
        /// </summary>
        public void RegistrarPagosFacturas(List<CompraFacturaModel> oListaFacturas, PagosFacturasModel oMediosPago)
        {
            int nroPago = 0;
            //obtengo el nro de pago
            nroPago = oServicioTipoComprobanteVenta.ObtenerNroPago(98);
            nroPago += 1;

            ProveedorModel facturaProveedor = oServicioProveedor.GetProveedor(oListaFacturas[0].IdProveedor);
            decimal saldoPagoTotal = oMediosPago.montoTotal_;
            foreach (var Factura in oListaFacturas)
            {
                CompraFacturaPagoModel FacturaMedioPago = new CompraFacturaPagoModel();
                if (nroPago == 0)
                {
                    throw new Exception("Ocurrio un Error al intentar obtener el número de pago");
                }
                else
                {
                    //estos seteos son para grabar en caja
                    decimal ValorFactura = oMediosPago.montoTotal_;
                    decimal ImporteCheque = oMediosPago.montoChequesSeleccionados;
                    decimal ImporteTarjeta = oMediosPago.montoTarjetaSelec_;
                    decimal ImporteTranferencia = oMediosPago.montoCuentaBancaria_;
                    int cuentaBanco = 0;

                    //instancio una factura para obtener los valores de retorno de la capa de datos
                    CompraFacturaModel FacturaN = new CompraFacturaModel();

                    //--------- Registro de Facturas a Pagar -----------
                    if (Factura.IdTipoComprobante != 98)
                    {

                        //seteo los valores para actualizar la tabla Compra facturas, punto 1 documento CtaCte
                        Factura.FechaPago = DateTime.Now;
                        Factura.NumeroPago = nroPago;
                        Factura.CotizacionDePago = 0; // esto deberia ser la cotizacion puesta en el txtcotizacion

                        //esto se hace para saber si el pago es mayos a todas las facturas pagadas
                        saldoPagoTotal = saldoPagoTotal - Factura.Saldo;

                        if (oMediosPago.montoTotal_ >= Factura.Saldo)
                        {
                            Factura.Saldo = 0;
                            Factura.Parcial = 0;
                        }
                        else
                        {
                            Factura.Parcial = oMediosPago.montoTotal_;
                            var nuevoSaldo = Factura.Saldo - Factura.Parcial;
                            Factura.Saldo = nuevoSaldo;
                        }

                        //enviamos los datos a modificar y recupetamos la entidad actualizada
                        FacturaN = ActualizarCompraFacturaPago(Factura);

                        // seteo el tipo de moneda para grabar en caja
                        int TipoDeMoneda = Factura.IdMoneda;
                    }
                    else
                    {
                        
                        ///------- Registro el Comprobante de Pago --------- CompraFacturaModel
                        /// 1 - Ctes de Pago con saldo positivo a favor de la empresa
                        /// 2 - New Cte de Pago 

                        // 1
                        if (Factura.NumeroFactura > 0 && Factura.IdTipoComprobante == 98)
                        {
                            Factura.FechaPago = DateTime.Now;
                            Factura.NumeroPago = nroPago;
                            Factura.CotizacionDePago = 0; // esto deberia ser la cotizacion puesta en el txtcotizacion
                            //esto se hace para saber si el pago es mayos a todas las facturas pagadas
                            saldoPagoTotal -= Factura.Saldo;
                            if (oMediosPago.montoTotal_ >= Factura.Saldo)
                            {
                                Factura.Saldo = 0;
                                Factura.Parcial = 0;
                            }
                            else
                            {
                                Factura.Parcial = oMediosPago.montoTotal_;
                                var nuevoSaldo = Factura.Saldo - Factura.Parcial;
                                Factura.Saldo = nuevoSaldo;
                            }

                            //enviamos los datos a modificar y recupetamos la entidad actualizada
                            FacturaN = ActualizarCompraFacturaPago(Factura);

                            // seteo el tipo de moneda para grabar en caja
                            int TipoDeMoneda = Factura.IdMoneda;
                        }
                        else
                        {
                            //2

                            // creamos el nuevo registro CompraFacturaModel 

                            //tengo q agregar al modelo los datos que necesito para insertar aca, ej idmoneda, proveedor, cotizacion
                            //graba en la sesion y puedo recuperar aca ya que es un pago para todas las facturas cargadas

                            Factura.NumeroFactura = nroPago;
                            Factura.IdProveedor = facturaProveedor.Id;   //traer 
                            Factura.IdMoneda = 1;
                            // Factura.IdMoneda = 0; //oMediosPago.id agregar al modelo para traer aca

                            Factura.IdTipoComprobante = 98;
                            Factura.Total = oMediosPago.montoTotal_;
                            if (saldoPagoTotal < 0)
                            {
                                saldoPagoTotal = 0;
                            }
                            Factura.Saldo = saldoPagoTotal;
                            Factura.Fecha = DateTime.Now;
                            Factura.FechaPago = DateTime.Now;
                            Factura.CotizacionDePago = 0; //traer 
                            Factura.Concepto = oMediosPago.ConceptoPago_;
                            Factura.NumeroPago = nroPago;
                            Factura.Activo = true;
                            Factura.IdUsuario = oMediosPago.idUsuario_;
                            Factura.UltimaModificacion = DateTime.Now;
                            Factura.Vencimiento = DateTime.Now;
                            Factura.Proveedor = null;
                            Factura.CompraIva = null;
                            Factura.IdCompraIva = null;
                            Factura.Imputacion = null;
                            Factura.TipoMoneda = null;
                            Factura.TipoComprobante = null;
                            //------ Periodo ---------
                            string anio = DateTime.Now.Year.ToString();
                            string mes = DateTime.Now.Month.ToString();
                            if (mes.Length < 2)
                            {
                                mes = "0" + mes;
                            }
                            anio = anio.Substring(anio.Length - 2, 2);
                            Factura.Periodo = int.Parse(anio + mes);
                            
                            // add codigo al cbte del pago  y utilizar el mismo para todos los asientos de pago
                            var CodigoAsiento = oServicioContable.GetNuevoCodigoAsiento() + 1;

                            //----------- Registro la "Factura del Pago" --------------
                            var Retorno = RegistrarPago(Factura);

                            SaveTrackingCompras(oListaFacturas, Retorno);

                          

                            if (Retorno != null)
                            {

                                //    //------aca insertamos los medios de pago
                                if (oMediosPago.idCuentaBancariaSelec_ > 0)
                                {
                                    FacturaMedioPago.IdFacturaCompra = Retorno.Id;
                                    FacturaMedioPago.IdTipoPago = 4;
                                    FacturaMedioPago.IdBancoCuenta = oMediosPago.idCuentaBancariaSelec_;
                                    FacturaMedioPago.Monto = oMediosPago.montoCuentaBancaria_;
                                    FacturaMedioPago.Observaciones = "Tranferecia Bancaria";
                                    FacturaMedioPago.Activo = true;
                                    FacturaMedioPago.IdUsuario = oMediosPago.idUsuario_;
                                    FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                    oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                    //-------Registrar movimiento Cuenta Bancaria
                                    //seteo para grabar en caja
                                    cuentaBanco = oMediosPago.idCuentaBancariaSelec_;
                                    //--------------------------
                                    //registrar movimiento cuenta bancaria porque el cheque propio lo toma asi
                                    BancoCuentaBancariaModel oBancoCuentaBancariaModel = new BancoCuentaBancariaModel();
                                    oBancoCuentaBancariaModel.NumeroOperacion = nroPago;
                                    oBancoCuentaBancariaModel.IdBancoCuenta = cuentaBanco;
                                    oBancoCuentaBancariaModel.CuentaDescripcion = FacturaMedioPago.Observaciones;
                                    //oBancoCuentaBancariaModel.Fecha = DateTime.Now; // esta como float
                                    oBancoCuentaBancariaModel.FechaEfectiva = DateTime.Now;
                                    oBancoCuentaBancariaModel.DiaClearing = "";
                                    oBancoCuentaBancariaModel.Importe = decimal.Parse(oMediosPago.montoCuentaBancaria_.ToString());
                                    oBancoCuentaBancariaModel.IdCliente = Factura.IdProveedor.ToString();
                                    oBancoCuentaBancariaModel.Conciliacion = "F";
                                    // oBancoCuentaBancariaModel.FechaIngreso = DateTime.Now;//es double
                                    oBancoCuentaBancariaModel.IdImputacion = "0";
                                   var pagoBancoCuentaBancaria= oServicioBancoCuentaBancaria.Agregar(oBancoCuentaBancariaModel);


                                    // ------------- Inicio registro de asientos Por Transferencia Bancaria ----------dev-a
                                    DiarioModel asiento = new DiarioModel();
                                    asiento.Codigo = oMediosPago.idCuentaBancariaSelec_;
                                    asiento.Importe = oMediosPago.montoTarjetaSelec_;
                                    asiento.Fecha = oMediosPago.FechaOperacion_;
                                    asiento.Periodo = DateTime.Now.ToString("yyMM");
                                    asiento.Tipo = "CP"; //Compras Pago
                                    asiento.Cotiza = 0 ; //dev-a se debe obtener la Cotizacion de dia
                                    asiento.Asiento = CodigoAsiento;
                                    asiento.Balance = int.Parse(DateTime.Now.ToString("yyyy"));
                                    asiento.Moneda = oServicioTipoMoneda.GetTipoMoneda(oMediosPago.idTipoMonedaSelec_).Descripcion;
                                    asiento.DescripcionMa = "Transferencia Pago Proveedor" + facturaProveedor.Nombre ;
                                    asiento.Titulo = "Asiento de Pagos Proveedores";

                                    /// asiento Inputacion Proveedor en valor - negativo
                                    var asientoDiario = oServicioContable.InsertAsientoContable(null
                                                                              , (oMediosPago.montoTarjetaSelec_) * (-1)
                                                                              , asiento
                                                                              , Retorno
                                                                              , facturaProveedor.IdImputacionProveedor ?? -1);
                                    /// Actualizar Cuenta Contable General (Libro Mayor)CTACBLE                
                                    /// oServicioImputacion.AsintoContableGeneral(asientoDiario);

                                }

                                if (oMediosPago.montoEfectivo_ != 0)
                                {
                                    FacturaMedioPago.IdFacturaCompra = Retorno.Id;
                                    FacturaMedioPago.IdTipoPago = 2;
                                    FacturaMedioPago.Monto = oMediosPago.montoEfectivo_;
                                    FacturaMedioPago.Observaciones = "Efectivo";
                                    FacturaMedioPago.Activo = true;
                                    FacturaMedioPago.IdUsuario = oMediosPago.idUsuario_;
                                    FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                    oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);
                                }

                                if (oMediosPago.idTarjetaSelec_ != 0)
                                {
                                    FacturaMedioPago.IdFacturaCompra = Retorno.Id;
                                    FacturaMedioPago.IdTipoPago = 5;
                                    FacturaMedioPago.IdTarjeta = oMediosPago.idTarjetaSelec_;
                                    FacturaMedioPago.Monto = oMediosPago.montoTarjetaSelec_;
                                    FacturaMedioPago.Observaciones = "Tarjeta";
                                    FacturaMedioPago.Activo = true;
                                    FacturaMedioPago.IdUsuario = oMediosPago.idUsuario_;
                                    FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                    oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                    //obtengo idGrupo caja de presupuesto
                                    PresupuestoActualModel oPresupuestoActualModel = new PresupuestoActualModel();
                                    oPresupuestoActualModel = oServicioPresupuestoActual.GetAllPresupuestos(oMediosPago.idPresupuesto_);
                                    int idGrupoCaja = oPresupuestoActualModel.IdGrupoCaja;
                                    //registra tarjeta

                                    TarjetaOperacionModel oTarjetaOperacionModel = new TarjetaOperacionModel();
                                    oTarjetaOperacionModel.IdTarjeta = oMediosPago.idTarjetaSelec_;
                                    oTarjetaOperacionModel.Descripcion = "Pago Factura: " + Retorno.NumeroFactura;
                                    oTarjetaOperacionModel.IdGrupoCaja = idGrupoCaja;
                                    oTarjetaOperacionModel.Conciliacion = false;
                                    oTarjetaOperacionModel.NumeroPago = nroPago.ToString();
                                    oTarjetaOperacionModel.Activo = true;
                                    oTarjetaOperacionModel.IdUsuario = oMediosPago.idUsuario_;
                                    oTarjetaOperacionModel.UltimaModificacion = DateTime.Now;
                                    oServicioTarjetaOperacion.Insertar(oTarjetaOperacionModel);
                                }

                                if ((oMediosPago.idChequesPropios != null) && (oMediosPago.idChequesPropios != ""))
                                {
                                    ChequeraModel oChequera = new ChequeraModel();
                                    string[] chequesSeleccionados = oMediosPago.idChequesPropios.Split(';');
                                    foreach (var itemCheque in chequesSeleccionados)
                                    {
                                        oChequera = oServicioChequera.obtenerCheque(int.Parse(itemCheque));
                                        //inserto medio de pago
                                        FacturaMedioPago.IdFacturaCompra = Retorno.Id;
                                        FacturaMedioPago.IdTipoPago = 3;
                                        FacturaMedioPago.IdChequera = int.Parse(itemCheque);
                                        FacturaMedioPago.Monto = decimal.Parse(oChequera.Importes.ToString());
                                        FacturaMedioPago.Observaciones = "Cheques propios";
                                        FacturaMedioPago.Activo = true;
                                        FacturaMedioPago.IdUsuario = oMediosPago.idUsuario_;
                                        FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                        oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                        //actualizo el cheque en la chequera
                                        oChequera.NumeroOperacion = nroPago;
                                        oChequera.UltimaModificacion = DateTime.Now;
                                        oChequera.IdUsuario = oMediosPago.idUsuario_;
                                        oChequera.Usado = true;//lo inactivo porque ya se uso                                 
                                        oChequera.Destino = "Pago a proveedor" + facturaProveedor.Nombre;
                                        oChequera.IdProveedor = facturaProveedor.Id;
                                        oChequera.IdUsuario = oMediosPago.idUsuario_;
                                        oChequera.UltimaModificacion = DateTime.Now;
                                        oServicioChequera.Actualizar(oChequera);


                                        //registrar movimiento cuenta bancaria porque el cheque propio lo toma asi
                                        BancoCuentaBancariaModel oBancoCuentaBancariaModel = new BancoCuentaBancariaModel();
                                        oBancoCuentaBancariaModel.NumeroOperacion = nroPago;
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
                                        oBancoCuentaBancariaModel.IdUsuario = oMediosPago.idUsuario_;
                                        oBancoCuentaBancariaModel.UltimaModificacion = DateTime.Now;

                                        oServicioBancoCuentaBancaria.Agregar(oBancoCuentaBancariaModel);

                                    }
                                }


                                if ((oMediosPago.idChequesTerceros != null) && (oMediosPago.idChequesTerceros != ""))
                                {
                                    ChequeModel oCheque = new ChequeModel();
                                    string[] chequesSeleccionados = oMediosPago.idChequesTerceros.Split(';');
                                    foreach (var itemCheque in chequesSeleccionados)
                                    {
                                        oCheque = oServicioCheque.obtenerCheque(int.Parse(itemCheque));

                                        FacturaMedioPago.IdFacturaCompra = Retorno.Id;
                                        FacturaMedioPago.IdTipoPago = 3;
                                        FacturaMedioPago.IdCheque = int.Parse(itemCheque);
                                        FacturaMedioPago.Monto = decimal.Parse(oCheque.Importe.ToString());
                                        FacturaMedioPago.Observaciones = "Cheques propios";
                                        FacturaMedioPago.Activo = true;
                                        FacturaMedioPago.IdUsuario = oMediosPago.idUsuario_;
                                        FacturaMedioPago.UltimaModificacion = DateTime.Now;
                                        oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                        //registrar tabla cheques
                                        oCheque.FechaEgreso = DateTime.Now;
                                        oCheque.Destino = Factura.IdProveedor.ToString();//id proveedor
                                        oCheque.IdMoneda = oCheque.IdMoneda;
                                        oCheque.NumeroPago = nroPago.ToString();
                                        oCheque.Proveedor = Factura.IdProveedor.ToString();
                                        // oCheque.Activo = false;
                                        oCheque.Endosado = true;

                                        oServicioCheque.Actualizar(oCheque);

                                    }
                                }
                                
                                ///------------Registro Caja----------
                                CajaModel oCajaModel = new CajaModel();
                                oCajaModel.IdTipoMovimiento = 1;
                                oCajaModel.Concepto =Factura.Concepto + ", Nro. factura: " + Factura.NumeroFactura ;
                                oCajaModel.Fecha = oMediosPago.FechaOperacion_;
                                oCajaModel.Tipo = "";
                                oCajaModel.Saldo = "";
                                oCajaModel.Recibo = Retorno.NumeroPago.ToString();

                                //segun la regla plasmada en .doc siempre se paga en pesos
                                oCajaModel.ImportePesos = ValorFactura;
                                //datos instanciados al inicio
                                oCajaModel.ImporteCheque = ImporteCheque;
                                oCajaModel.ImporteDeposito = ImporteTranferencia;
                                oCajaModel.ImporteTarjeta = ImporteTarjeta;
                                oCajaModel.IdCuentaBanco = oMediosPago.idCuentaBancariaSelec_;

                                //validar y actualizar presupuesto 

                                PresupuestoActualModel oPrespuestoActual = oServicioPresupuestoActual.GetAllPresupuestos(oMediosPago.idPresupuesto_);
                                oPrespuestoActual.Ejecutado += Factura.Total;
                                oPrespuestoActual.IdUsuario = oMediosPago.idUsuario_;
                                oPrespuestoActual.UltimaModificacion = DateTime.Now;
                                oServicioPresupuestoActual.ActualizarPresupuesto(oPrespuestoActual);

                                if (oPrespuestoActual.Id != 0)
                                {
                                    PresupuestoCostoModel oPresupuestoCosto = oServicioPresupuestoCosto.GetAllPresupuestoCosto(oPrespuestoActual.Codigo);
                                    if (oPresupuestoCosto != null)
                                    {
                                        oPresupuestoCosto.Ejecutado += Factura.Total;
                                        oPresupuestoCosto.IdUsuario = oMediosPago.idUsuario_;
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


                                facturaProveedor.idPresupuesto = oMediosPago.idPresupuesto_;
                                facturaProveedor.UltimaModificacion = DateTime.Now;
                                facturaProveedor.IdUsuario = oMediosPago.idUsuario_;
                                oServicioProveedor.ActualizarPresupuestoProveedor(facturaProveedor);


                                //acturalizo el nro de pago
                                oServicioTipoComprobanteVenta.ActualizarNroPago(98, nroPago);


                                _mensaje?.Invoke("Se pagó correctamente", "ok");

                            }

                        }
                    }
                }

            }

        }

        private void SaveTrackingCompras(List<CompraFacturaModel> oListaFacturas, CompraFacturaModel pago)
        {
            foreach (var item in oListaFacturas)
            {
                if (item.Id > 0)
                {
                    TrackingFacturaPagoCompraModel tracking = new TrackingFacturaPagoCompraModel();
                    tracking.IdFactura = item.Id;
                    tracking.IdPago = pago.Id;
                    oServicioTrackingFacturaPagoCompra.Insert(tracking);
                }

            }
        }
    }
}
