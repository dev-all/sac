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
    class ServicioTarjetaOperacion : ServicioBase
    {

        private TarjetaOperacionRepositorio oTarjetaRepositorio;


        public ServicioTarjetaOperacion()
        {
            oTarjetaRepositorio = kernel.Get<TarjetaOperacionRepositorio>();
        }


        public TarjetaOperacionModel Insertar(TarjetaOperacionModel oTarjetaOperacionModel)
        {
            var oModel = Mapper.Map<TarjetaOperacionModel, TarjetaOperacion>(oTarjetaOperacionModel);
            //_mensaje("El cheque se ingresó correctamente", "ok");
            return Mapper.Map<TarjetaOperacion, TarjetaOperacionModel>(oTarjetaRepositorio.Insertar(oModel));
        }

    }
}
