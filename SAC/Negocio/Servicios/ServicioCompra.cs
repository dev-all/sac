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
                var compraFactura = Mapper.Map<CompraFacturaModel, CompraFactura>(model);
                compraFactura = repositorio.Insertar(compraFactura);
                //model.IdUsuario = compraFactura.IdUsuario;
                return model;
            }
            catch (Exception)
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
        /// metodo que permite registrar un pago impacta en tbl compra factura, caja, banco, cheques, presupuesto, proveedores
        /// </summary>
        /// <param name="oCompraFacturaModel"></param>
        public void RegistrarPago (FacturaPagoModel oCompraFacturaModel)
        {
            try
            {

                string[] FacturasaPagar = oCompraFacturaModel.idFacturas.Split(';');
                int nroPago = 0;
                foreach (var item in FacturasaPagar)
                {
                    CompraFacturaModel Factura = new CompraFacturaModel();
                    CompraFacturaPagoModel FacturaMedioPago = new CompraFacturaPagoModel();

                    Factura = ObtenerPorID(int.Parse(item));
                   
                    nroPago= oServicioTipoComprobanteVenta.ObtenerNroPago(98);

                    if (nroPago== 0 )
                    {
                        throw new Exception("Ocurrio un Error al intentar obtener el número de pago");
                    }
                    else
                    {
                        //seteo los valores para avutalizar la tabla Compra facturas, punto 1 documento CtaCte
                        Factura.FechaPago = DateTime.Now;
                        Factura.NumeroPago = (nroPago + 1).ToString();
                        Factura.CotizacionDePago = 0;

                        //enviamos los datos a modificar y recupetamos la entidad actualizada
                        Factura = ActualizarCompraFacturaPago(Factura);
                        oServicioTipoComprobanteVenta.ActualizarNroPago(98, nroPago + 1);

                        // creamos el nuevo registro
                        Factura.IdTipoComprobante = 98;
                        Factura.PuntoVenta = 3;
                        Factura.NumeroPago = (nroPago + 1).ToString();
                        Factura.Total = oCompraFacturaModel.TotalAPagar;

                        //condicion si diferencia es 0 graba 0 sino diferencia
                        Factura.Parcial = oCompraFacturaModel.Diferencia;
                        Factura.Saldo = oCompraFacturaModel.Diferencia;
                        Factura.Fecha = DateTime.Now;
                        Factura.Vencimiento = DateTime.Now;
                        //Factura.TotalDolares
                        //Factura.Cotizacion = oServicioTipoMoneda.cotizacion;
                        Factura.FechaPago = DateTime.Now;
                        string anio = DateTime.Now.Year.ToString();
                        anio = anio.Substring(anio.Length - 2, 2);
                        Factura.Periodo = int.Parse(anio + DateTime.Now.Month.ToString());
                        Factura.CotizacionDePago = 0; //esta en la servicio tipoMoneda
                        Factura.Concepto = "pago factura";
                        //Factura.Imputacion = null;
                        //Factura.IdMoneda = oCompraFacturaModel. 
                         //Factura.Recibo = 0;

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

                        if (oCompraFacturaModel.efectivo != null)
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
                                FacturaMedioPago.Monto =decimal.Parse(oChequera.Importes.ToString());
                                FacturaMedioPago.Observaciones = "Cheques propios";
                                FacturaMedioPago.Activo = true;
                                FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                                oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                //actualizo el cheque en la chequera
                                oChequera.NumeroOperacion = nroPago + 1;
                                oChequera.UltimaModificacion = DateTime.Now;
                                oChequera.IdUsuario = oCompraFacturaModel.idUsuario;
                                oChequera.Usado = true;
                                //oChequera.Activo = false; //lo inactivo porque ya se uso
                                oChequera.Destino = Factura.IdProveedor.ToString();//id proveedor
                                oChequera.IdProveedor = Factura.IdProveedor.ToString();
                                oServicioChequera.Actualizar(oChequera);


                                //registrar movimiento cuenta bancaria porque el cheque propio lo toma asi
                                BancoCuentaBancariaModel oBancoCuentaBancariaModel = new BancoCuentaBancariaModel();
                                oBancoCuentaBancariaModel.NumeroOperacion = nroPago + 1;
                                oBancoCuentaBancariaModel.IdBancoCuenta= oChequera.IdBancoCuenta ;
                                oBancoCuentaBancariaModel.CuentaDescripcion = oChequera.BancoCuenta.Descripcion;
                                //oBancoCuentaBancariaModel.Fecha = DateTime.Now; // esta como float
                                oBancoCuentaBancariaModel.FechaEfectiva = DateTime.Now;
                                oBancoCuentaBancariaModel.DiaClearing = "";
                                oBancoCuentaBancariaModel.Importe = decimal.Parse(oChequera.Importes.ToString());
                                oBancoCuentaBancariaModel.IdCliente = Factura.IdProveedor.ToString();
                                oBancoCuentaBancariaModel.Conciliacion = "F";
                                // oBancoCuentaBancariaModel.FechaIngreso = DateTime.Now;//es double
                                oBancoCuentaBancariaModel.IdImputacion = "0";
                                oServicioBancoCuentaBancaria.Agregar(oBancoCuentaBancariaModel);

                            }
                        }

                        if (oCompraFacturaModel.idChequesTerceros != null)
                        {
                            ChequeModel oCheque = new ChequeModel();
                            string[] chequesSeleccionados = oCompraFacturaModel.idChequesPropios.Split(';');
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
                                oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                                //registrar tabla cheques

                                oCheque.FechaEgreso = DateTime.Now.ToString();
                                oCheque.Destino = Factura.IdProveedor.ToString();//id proveedor
                                oCheque.IdMoneda = oCheque.IdMoneda;
                                oCheque.NumeroPago = (nroPago + 1).ToString();
                                oCheque.Proveedor = Factura.IdProveedor.ToString();
                                // oCheque.Activo = false;
                                oCheque.Endosado = true;

                                oServicioCheque.Actualizar(oCheque);

                            }
                        }

                        if (oCompraFacturaModel.idCuentasBancarias != null)
                        {
                                FacturaMedioPago.IdFacturaCompra = int.Parse(item);
                                FacturaMedioPago.IdTipoPago = 4;
                                FacturaMedioPago.IdBancoCuenta = int.Parse(oCompraFacturaModel.idCuentasBancarias);
                                FacturaMedioPago.Monto = oCompraFacturaModel.montoTranferencia;
                                FacturaMedioPago.Observaciones = "Tranferecia Bancaria";
                                FacturaMedioPago.Activo = true;
                                FacturaMedioPago.IdUsuario = oCompraFacturaModel.idUsuario;
                                oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);
                            //registrar movimiento cuenta bancaria

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
                            oServicioCompraFacturaPago.InsertarCompraFacturaPago(FacturaMedioPago);

                            //registra tarjeta



                        }

                        var Retorno = RegistrarPago(Factura);
                        //aca tengo que hacer la parte de la caja pero espero a esteban
                        //sigue caja 




                        //actualizo proveedor
                        var oProveedor = oServicioProveedor.GetProveedor(Factura.IdProveedor);
                        oProveedor.idPresupuesto = oCompraFacturaModel.idPresupuesto;
                        oProveedor.UltimaModificacion = DateTime.Now;
                        oProveedor.IdUsuario = oCompraFacturaModel.idUsuario;
                        oServicioProveedor.ActualizarProveedor(oProveedor);
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
