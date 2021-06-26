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
            //context.Configuration.LazyLoadingEnabled = false;
            List<FactVenta> listaFacturasVentas= context.FactVenta.Where(p => p.Activo == true ).ToList();
            return listaFacturasVentas;
        }

        public List<FactVenta> GetAllFacturaVentaCliente(int idCliente)
        {
            context.Configuration.LazyLoadingEnabled = false;
            List<FactVenta> listaFacturasVentas = context.FactVenta.Where(p => p.Activo == true && p.IdCliente == idCliente).ToList();
            return listaFacturasVentas;
        }

        public List<FactVenta> GetAllFacturaVentaPorNumero(int nroBuscado, int idCliente)
        {
            //ojo hay q averiguar el id de las facturas
            List<FactVenta> listaFacturasVentas = context.FactVenta.Where(p => p.Activo == true && p.NumeroFactura.Equals(nroBuscado) &&  p.IdCliente == idCliente && (p.IdTipoComprobante == 11 || p.IdTipoComprobante == 19)).ToList();
            return listaFacturasVentas;
        }

        public FactVenta GetFacturaVentaPorNumero(int nroBuscado, int idCliente)
        {
            context.Configuration.LazyLoadingEnabled = false;
            FactVenta FacturasVentas = context.FactVenta.Where(p => p.Activo == true && p.NumeroFactura.Equals(nroBuscado) && p.IdCliente == idCliente).First();
            return FacturasVentas;
        }

        public FactVenta GetFacturaVentaPorNumero(int nroBuscado, string idComprobante)
        {
            context.Configuration.LazyLoadingEnabled = false;
            int idComprobantei = int.Parse(idComprobante);
            FactVenta FacturasVentas = context.FactVenta.Where(p => p.Activo == true && p.NumeroFactura.Equals(nroBuscado) && p.IdTipoComprobante == idComprobantei).First();
            return FacturasVentas;
        }


        public FactVenta Agregar(FactVenta oFactVenta)
        {
            return Insertar(oFactVenta);
        }
        public FactVenta CreateFactura(FactVenta factVenta)
        {
            context.Configuration.LazyLoadingEnabled = false;
            FactVenta nuevaEntidad = DbSet.Add(factVenta);
            Contexto.SaveChanges();
            return nuevaEntidad;
        }
        public List<FactVenta> GetClienteCtaCteCbte(int idCliente)
        {
            //context.Configuration.LazyLoadingEnabled = false;
            //             
            return context.FactVenta
                .Include(c => c.TipoComprobanteVenta)
                .Where(p => p.IdCliente == idCliente && p.Saldo > 0).OrderByDescending(x => x.NumeroFactura).ToList();

        }
        public List<FactVenta> GetClienteCtaCteCbte(int idCliente, int? idTipoMoneda)
        {
            return context.FactVenta
               .Include(c => c.TipoComprobanteVenta)
               .Where(p => p.IdCliente == idCliente && p.IdMoneda == idTipoMoneda && p.Saldo > 0).OrderByDescending(x => x.NumeroFactura).ToList();
        }

        public FactVenta GetFacturaPorId(int idFactura)
        {
          
            context.Configuration.LazyLoadingEnabled = false;
            return context.FactVenta.Where(acc => acc.Id == idFactura && acc.Activo == true).FirstOrDefault();
         
        }

        public FactVenta GetFacturaVentaPorId(int idcliente, int idFactura)
        {

            //context.Configuration.LazyLoadingEnabled = false;
            return context.FactVenta.Where(acc => acc.Id == idFactura && acc.IdCliente == idcliente && acc.Activo == true).FirstOrDefault();

        }
        public List<FactVenta> GetCompraFacturaPorIdProveedor_Moneda(int idCliente, int? idTipoMoneda)
        {
            return context.FactVenta
               .Include(c => c.TipoComprobanteVenta)
               .Where(p => p.IdCliente == idCliente && p.IdMoneda == idTipoMoneda && p.Saldo > 0).OrderByDescending(x => x.NumeroFactura).ToList();
        }

        public FactVenta ActualizaVentaFacturaCobro(FactVenta model)
        {
            FactVenta factura = GetFacturaPorId(model.Id);

            factura.FechaCobro = model.FechaCobro;
            factura.NumeroCobro = model.NumeroCobro;
            factura.Cotiza = model.Cotiza;
            factura.CotizaP = model.CotizaP;
            factura.Saldo = model.Saldo;
            factura.Parcial = model.Parcial;
            context.SaveChanges();

            return factura;
        }
    }
}
