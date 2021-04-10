using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class FacturaVentasRepositorio : RepositorioBase<FactVenta>
    {
        private SAC_Entities context;

        public FacturaVentasRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<FactVenta> GetAllFacturaVenta()
        {
            List<FactVenta> listaFacturasVentas= context.FactVenta.Where(p => p.Activo == true ).ToList();
            return listaFacturasVentas;
        }

        public FactVenta Agregar(FactVenta oFactVenta)
        {
            return Insertar(oFactVenta);
        }


    }
}
