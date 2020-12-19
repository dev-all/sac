using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
  public class CompraFacturaPagoRepositorio : RepositorioBase<CompraFacturaPago>

    {

        private SAC_Entities context;

        public CompraFacturaPagoRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public CompraFacturaPago InsertarCompraFacturaPago(CompraFacturaPago oCompraFacturaPago)
        {
            try
            {
                return Insertar(oCompraFacturaPago);
            }
            catch (Exception ex)
            {
                return null;
            }

        }



    }
}
