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
   public class ServicioContable : ServicioBase
    {
        private DiarioRepositorio diarioRepositorio;

        public ServicioContable()
        {
            diarioRepositorio = kernel.Get<DiarioRepositorio>();
        }

        public List<DiarioModel> GetAllDiario()
        {
            try
            {
                return Mapper.Map<List<Diario>, List<DiarioModel>>(diarioRepositorio.GetAllDiario());
                 
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }
        
        public void InsertAsientoContable(DiarioModel model)
        {
           // insert en diario

        }

        public object GetNuevoCodigoAsiento()
        {
            throw new NotImplementedException();
        }
    }
}
