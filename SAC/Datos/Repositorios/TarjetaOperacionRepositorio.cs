using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
 public class TarjetaOperacionRepositorio : RepositorioBase<TarjetaOperacion>
    {
        private SAC_Entities context;

        public TarjetaOperacionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public TarjetaOperacion InsertarPais(TarjetaOperacion tarjetaOperacion)
        {
            return Insertar(tarjetaOperacion);
        }
       


    }
}
