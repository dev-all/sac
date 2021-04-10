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
  public class ServicioTipoPago : ServicioBase
    {
        private TipoPagoRepositorio oTipoPagoRepositorio;

        public ServicioTipoPago()
        {
            oTipoPagoRepositorio = kernel.Get<TipoPagoRepositorio>();
        }

        public List<TipoPagoModel> GetAllTipoPago()
        {
            try
            {
                var TipoPago = Mapper.Map<List<TipoPago>, List<TipoPagoModel>>(oTipoPagoRepositorio.GetAllTipoPago());
                return TipoPago;
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
        }



    }
}
