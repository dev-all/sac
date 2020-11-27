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
 public class ServicioTipoIva: ServicioBase
    {
        private TipoIvaRepositorio oTipoIvaRepositorio;
        public Action<string, string> _mensaje;

        public ServicioTipoIva()
        {
            oTipoIvaRepositorio = kernel.Get<TipoIvaRepositorio>();
        }

        public List<TipoIvaModel> GetAllTipoIva()
        {
            return Mapper.Map<List<TipoIva>, List<TipoIvaModel>>(oTipoIvaRepositorio.GetAllTipoIva());
        }


    }
}
