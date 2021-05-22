using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using Negocio.Modelos;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using System.Net;
using Negocio.Servicios;
using System.Net.Mime;
using System.Text;

namespace Negocio.Servicios
{
    public class ServicioTipoComprobanteVenta : ServicioBase
    {
        private TipoComprovanteVentaRepositorio otipoComprobanteVentaRepositorio;

        public ServicioTipoComprobanteVenta()
        {
            otipoComprobanteVentaRepositorio = kernel.Get<TipoComprovanteVentaRepositorio>();
        }


        public TipoComprobanteVentaModel GetTipoComprobanteVentaPorId(int id)
        {
            try
            {
                return Mapper.Map<TipoComprobanteVenta, TipoComprobanteVentaModel>(otipoComprobanteVentaRepositorio.GetTipoComprobanteVentaPorId(id));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }
     public TipoComprobanteVentaModel GetTipoComprobanteVentaPorNroAfip(int idAfip, int idPuntoVenta)
        {
            try
            {
                return Mapper.Map<TipoComprobanteVenta, TipoComprobanteVentaModel>(otipoComprobanteVentaRepositorio.GetTipoComprobanteVentaPorNroAfip(idAfip, idPuntoVenta));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }
        public int ObtenerNroPago(int idTipoComprobanteVenta)
        {
            try
            {
                int nroPago = 0;
                nroPago = otipoComprobanteVentaRepositorio.ObtenerNroPago(idTipoComprobanteVenta);
                return nroPago;
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return 0;
            }
        }
        public int ActualizarNroPago(int idTipoComprobanteVenta,int nroPago)
        {
            try
            {
               return otipoComprobanteVentaRepositorio.ActualizarNroPago(idTipoComprobanteVenta, nroPago);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return 0;
            }
        }

        
        public int ObtenerNroFactura(int nroComprobante, int puntoVenta)
        {
            try
            {
                return otipoComprobanteVentaRepositorio.ObtenerNroFactura(nroComprobante, puntoVenta);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return 0;
            }
        }

           public TipoComprobanteVentaModel getTipoComprobanteVentaNewNumeroFactura(int nroComprobante, int puntoVenta)
                {
                    try
                    {
                        return Mapper.Map<TipoComprobanteVenta,TipoComprobanteVentaModel>(otipoComprobanteVentaRepositorio.getTipoComprobanteVentaNewNumeroFactura(nroComprobante, puntoVenta));
                    }
                    catch (Exception)
                    {
                        _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                        return null;
                    }
                }
        public int ActualizarNroFactura(int idTipoComprobanteVenta, int nroPago, int nroFactura)
                {
                    try
                    {
                        return otipoComprobanteVentaRepositorio.ActualizarNroFactura(idTipoComprobanteVenta, nroPago, nroFactura);
                    }
                    catch (Exception)
                    {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                        return 0;
                    }
                }

        public TipoComprobanteVentaModel GetNuevoNumeroCobro(int codigoAfip, int puntoVenta)
        {
            try
            {
                return Mapper.Map<TipoComprobanteVenta, TipoComprobanteVentaModel>(otipoComprobanteVentaRepositorio.GetNuevoNumeroCobro(codigoAfip, puntoVenta));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }

        public TipoComprobanteVentaModel GetNuevoNumeroCobro(int IdTipoComprobanteVenta)
        {
            try
            {
                return Mapper.Map<TipoComprobanteVenta, TipoComprobanteVentaModel>(otipoComprobanteVentaRepositorio.GetNuevoNumeroCobro(IdTipoComprobanteVenta));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }
        public List<TipoComprobanteVentaModel> GetAllTipoComprobante()
        {
            try
            {
                return Mapper.Map<List<TipoComprobanteVenta>, List<TipoComprobanteVentaModel>>(otipoComprobanteVentaRepositorio.GetAllTipoComprobante());

            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }

    }
}
