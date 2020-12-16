using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class TipoComprovanteVentaRepositorio : RepositorioBase<TipoComprobante>
    {
        private SAC_Entities context;

        public TipoComprovanteVentaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }


        public TipoComprobanteVenta GetTipoComprobanteVentaPorId(int id)
        {
            var TipoComprobante = context.TipoComprobanteVenta.Where(p => p.Id == id).FirstOrDefault();
            return TipoComprobante;
        }

        
        public int ObtenerNroPago(int id)
        {
            var NroPago = context.TipoComprobanteVenta.Where(p => p.Id == id).Select(p => p.Numero).FirstOrDefault();
            return int.Parse(NroPago);
        }

        public int ActualizarNroPago (int id, int nroPago)
        {
            var TipoComprobante = context.TipoComprobanteVenta.Where(p => p.Id == id).First();
            TipoComprobante.Numero = nroPago.ToString();
            return context.SaveChanges();
        }



    }
}
