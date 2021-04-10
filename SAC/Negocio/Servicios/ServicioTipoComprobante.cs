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
   public class ServicioTipoComprobante : ServicioBase
    {
        private TipoComprobanteRepositorio tipoComprobanteRepositorio;

        public ServicioTipoComprobante()
        {
            tipoComprobanteRepositorio = kernel.Get<TipoComprobanteRepositorio>();
        }

        public TipoComprobanteModel GetTipoComprobantePorId(int id)
        {
            try
            {
               return Mapper.Map<TipoComprobante, TipoComprobanteModel>(tipoComprobanteRepositorio.GetTipoComprobantePorId(id));                 
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }


        public List<TipoComprobanteModel> GetTipoComprobantePorTipoIvaProveedor(int IdTipoIva)
        {
            try
            {
                return Mapper.Map<List<TipoComprobante>, List<TipoComprobanteModel>>(tipoComprobanteRepositorio.GetTipoComprobantePorTipoIvaProveedor(IdTipoIva));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }

        public List<TipoComprobanteModel> GetAllTipoComprobante()
        {
            try
            {
               return Mapper.Map<List<TipoComprobante>, List<TipoComprobanteModel>>(tipoComprobanteRepositorio.GetAllTipoComprobante());
             
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }

        public List<TipoComprobanteModel> GetTipoComprobanteLocalesVentaSinFactura()
        {
            try
            {
                return Mapper.Map<List<TipoComprobante>, List<TipoComprobanteModel>>(tipoComprobanteRepositorio.GetTipoComprobanteLocalesVentaSinFactura());

            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }
        public List<TipoComprobanteModel> GetTipoComprobanteExtranjerosVenta()
        {
            try
            {
                return Mapper.Map<List<TipoComprobante>, List<TipoComprobanteModel>>(tipoComprobanteRepositorio.GetTipoComprobanteExtranjerosVenta());

            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese en contacto con el administrador del sistema", "error");
                return null;
            }
        }


    }
}
