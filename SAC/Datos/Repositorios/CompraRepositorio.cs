using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class CompraRepositorio : RepositorioBase<CompraFactura>
    {
       private SAC_Entities context;
    
        public CompraRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
            
        public CompraFactura CreateAccion(CompraFactura compraFactura)
        {
           return  Insertar(compraFactura);
        }

        public List<CompraFactura> GetAllCompraFactura()
        {
            return context.CompraFactura.Where(acc => acc.Activo == true).OrderBy(acc => acc.Fecha).ToList();
        }

        public CompraFactura GetCompraFacturaPorId(int id)
        {           
            return context.CompraFactura.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }

        public List<CompraFactura> GetCompraFacturaListaPorId(int id)
        {
            List<CompraFactura> oListaFacturas =context.CompraFactura.Where(acc => acc.Id == id && acc.Activo == true).ToList();
            return oListaFacturas;
        }

        public List<CompraFactura> GetCompraFacturaPorIdProveedor(int idProveedor)
        {
            return context.CompraFactura.Where(p => p.IdProveedor == idProveedor && p.IdTipoComprobante == 11 &&  p.NumeroPago == "0"  ).ToList();
            
        }

        //esta actualizacion es solo para el pago de facturas
        public CompraFactura ActualizarCompraFacturaPago(CompraFactura model)
        {

            CompraFactura CompraFactura = GetCompraFacturaPorId(model.Id);

            CompraFactura.FechaPago= model.FechaPago;
            CompraFactura.NumeroPago = model.NumeroPago;
            CompraFactura.CotizacionDePago= model.CotizacionDePago;
            context.SaveChanges();

            return CompraFactura;
        }
        
        public CompraFactura DeleteCompraFactura(int id)
        {

            CompraFactura CompraFactura = GetCompraFacturaPorId(id);
            CompraFactura.Activo = false;
            CompraFactura.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            context.SaveChanges();

            return CompraFactura;
        }

        public List<Proveedor> GetProveedorPorNombre(string strProveedor)
        {
            List<Proveedor> p = (from c in context.Proveedor
                                 where c.Activo == true && c.Nombre.Contains(strProveedor)
                                 select c).ToList();
            return p;                
        }
    }
}