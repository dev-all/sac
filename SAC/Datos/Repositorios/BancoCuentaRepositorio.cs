using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class BancoCuentaRepositorio : RepositorioBase<BancoCuenta>
    {

        private SAC_Entities context;

        public BancoCuentaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public List<BancoCuenta> GetAllCuenta()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.BancoCuenta.ToList();

        }

        public BancoCuenta GetCuentaPorId(int id)
        {
            return context.BancoCuenta.Where(p => p.Id == id).First();           
        }


        public List<BancoCuenta> GetBancoPorNombre(string strBanco)
        {
            List<BancoCuenta> p = (from c in context.BancoCuenta
                                   where c.Activo == true && c.BancoDescripcion.Contains(strBanco)
                                   select c).ToList();
            return p;
        }
    }
}
