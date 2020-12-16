using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
  public class BancoCuentaBancariaRepositorio: RepositorioBase<BancoCuentaBancaria>
    {
        private SAC_Entities context;

        public BancoCuentaBancariaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<BancoCuentaBancaria> GetAllBancoCuentaBancaria()
        {
            return context.BancoCuentaBancaria.ToList();
        }


        public BancoCuentaBancaria Agregar(BancoCuentaBancaria oBancoCuentaBancaria)
        {
            return Insertar(oBancoCuentaBancaria);
        }

    }
}
