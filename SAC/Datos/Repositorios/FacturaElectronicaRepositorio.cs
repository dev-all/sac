using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class FacturaElectronicaRepositorio : RepositorioBase<FacturaElectronica>
    {
        private SAC_Entities context;

        public FacturaElectronicaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<FacturaElectronica> GetAllFacturasElectonicas()
        {
            // context.Configuration.LazyLoadingEnabled = false;
            return context.FacturaElectronica.ToList();

        }

        public FacturaElectronica GetFacturasElectonicasPorId(int tipoComprobante, int idPuntoVenta, int nroCbte)
        {
            return context.FacturaElectronica.Where(p => p.ID_TIPOCBTE == tipoComprobante  && p.PUNTOVTA == idPuntoVenta && p.NROCBTE_AFIP == nroCbte).First();
        }

        public FacturaElectronica Agregar(FacturaElectronica oFacturaElectronica)
        {
            return Insertar(oFacturaElectronica);
        }

    }
}
