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
   public class ServicioBancoCuenta : ServicioBase
    {
        private BancoCuentaRepositorio oBancoCuentaRepositorio;
        public Action<string, string> _mensaje;

        public ServicioBancoCuenta()
        {
            oBancoCuentaRepositorio = kernel.Get<BancoCuentaRepositorio>();
        }

        public List<BancoCuentaModel> GetAllCuenta()
        {
            return Mapper.Map<List<BancoCuenta>, List<BancoCuentaModel>>(oBancoCuentaRepositorio.GetAllCuenta());
        }




    }
}
