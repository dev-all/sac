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
   public class ServicioBancoCuentaBancaria: ServicioBase
    {
        private BancoCuentaBancariaRepositorio oBancoCuentaBancariaRepositorio;
        public Action<string, string> _mensaje;

        public ServicioBancoCuentaBancaria()
        {
            oBancoCuentaBancariaRepositorio = kernel.Get<BancoCuentaBancariaRepositorio>();
        }

        public List<BancoCuentaBancariaModel> GetAllBancoCuentaBancaria()
        {
            return Mapper.Map<List<BancoCuentaBancaria>, List<BancoCuentaBancariaModel>>(oBancoCuentaBancariaRepositorio.GetAllBancoCuentaBancaria());
        }

        public BancoCuentaBancariaModel Agregar(BancoCuentaBancariaModel oBancoCuentaBancariaModel)
        {
            try
            {
                var oModel = Mapper.Map<BancoCuentaBancariaModel, BancoCuentaBancaria>(oBancoCuentaBancariaModel);
                return Mapper.Map<BancoCuentaBancaria, BancoCuentaBancariaModel>(oBancoCuentaBancariaRepositorio.Agregar(oModel));
            }
            catch(Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
         
        }


    }
}
