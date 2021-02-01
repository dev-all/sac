using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class ProveedorRepositorio : RepositorioBase<Proveedor>
    {
       private SAC_Entities context;
    
        public ProveedorRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

      
        public Proveedor InsertarProveedor(Proveedor proveedor)
        {
            Proveedor a = new Proveedor();
            try {
                return Insertar(proveedor);
            }
            catch (Exception ex)
            {
                return a;
            }
          
        }

        public List<Proveedor> GetAllProveedor()
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor.Where( p=> p.Activo == true).ToList();

            //anda
            //context.Configuration.LazyLoadingEnabled = false;
            //var items = context.Proveedor
            //              .Include(x => x.Pais)
            //              .Include(x => x.Provincia)
            //              .Include(x => x.TipoIva)                        
            //              .Include(x => x.TipoProveedor)
            //              .Include(x => x.TipoMoneda).Where(x => x.Activo == true).ToList();
            //return items;


        }


        public List<CuentaCorrienteProveedorDetalles> CtaCteDetalle(int idProveedor, DateTime fechaDesde)
        {
            context.Configuration.LazyLoadingEnabled = false;

            return context.CompraFactura
                            .Include("Proveedor")
                            .Include("CompraFacturaPago")
                            .Include("TipoComprobante")
                            .Where(p => p.IdProveedor == idProveedor  && p.Activo == true  && p.Fecha >= fechaDesde)
                            .Select(p => new CuentaCorrienteProveedorDetalles(){
                                        IdComprobante = p.TipoComprobante.Id,
                                        Id= p.Proveedor.Id,
                                        RazonSocial = p.Proveedor.Nombre,
                                        Cbte =p.TipoComprobante.Denominacion,
                                        PuntoVenta=    p.PuntoVenta,
                                        NumeroFactura=      p.NumeroFactura,
                                        Fecha=   p.Fecha,
                                        Total = p.Total,
                                        compraFacturaPagos = p.CompraFacturaPago,
                                        TotalDolares=p.TotalDolares,
                                        Parcial=p.Parcial,
                                        Saldo= p.Saldo,
                                        Cotizacion=p.Cotizacion,
                                        NumeroPago=p.NumeroPago,
                                        Recibo=p.Recibo

                            }).ToList();
           
        }

        public List<CuentaCorriente> GetAllCuentaCorriente()
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.CuentaCorriente.ToList();
        }
        //busqueda por fecha
        public List<CuentaCorriente> GetAllCuentaCorriente(DateTime inicio,DateTime fin)
        {
            //var dInicio = DateTime.Parse(inicio);
            //var dFin    = DateTime.Parse(fin);
            context.Configuration.LazyLoadingEnabled = false;
            return context.CuentaCorriente.Where(p=> p.UltimoMovimiento >= inicio && p.UltimoMovimiento <= fin ).ToList();
        }

        public Proveedor GetProveedorPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }
        public Proveedor GetProveedorPorIdCompleto(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor
                .Include("TipoIva")
                .Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault();
        }
        public Proveedor ObtenerProveedorPorNombre(string nombre)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor.Where(p => p.Nombre == nombre).FirstOrDefault();
        }
        public Proveedor ObtenerProveedorPorNombre(string oNombre, string oCuit)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor.Where(p => p.Nombre == oNombre && p.Cuit == oCuit).FirstOrDefault();
        }

        public Proveedor ObtenerProveedorPorNombre(string oNombre, string oCuit, int oId)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor.Where(p => p.Nombre == oNombre && p.Cuit == oCuit && p.Id != oId).FirstOrDefault();
        }

        public Proveedor ActualizarProveedor(Proveedor ProveedorParaActualizar)
        {
            Proveedor Proveedor = GetProveedorPorId(ProveedorParaActualizar.Id);

            Proveedor.Id = ProveedorParaActualizar.Id;   
            Proveedor.Nombre = ProveedorParaActualizar.Nombre;
            Proveedor.Direccion = ProveedorParaActualizar.Direccion;
            Proveedor.Telefono = ProveedorParaActualizar.Telefono;
            Proveedor.IdPais = ProveedorParaActualizar.IdPais;
            Proveedor.IdProvincia = ProveedorParaActualizar.IdProvincia;
            Proveedor.IdLocalidad = ProveedorParaActualizar.IdLocalidad;
            Proveedor.IdCodigoPostal = ProveedorParaActualizar.IdCodigoPostal;
            Proveedor.IdImputacionProveedor = ProveedorParaActualizar.IdImputacionProveedor;
            Proveedor.Email = ProveedorParaActualizar.Email;
            Proveedor.IdTipoIva = ProveedorParaActualizar.IdTipoIva;
            Proveedor.Cuit = ProveedorParaActualizar.Cuit;
            Proveedor.IdImputacionFactura = ProveedorParaActualizar.IdImputacionFactura;
            Proveedor.IdTipoProveedor = ProveedorParaActualizar.IdTipoProveedor;
            Proveedor.IdTipoMoneda = ProveedorParaActualizar.IdTipoMoneda;
            Proveedor.Observaciones = ProveedorParaActualizar.Observaciones;
            Proveedor.UltimoPuntoVenta = ProveedorParaActualizar.UltimoPuntoVenta;
            Proveedor.Activo = true;
            Proveedor.IdUsuario = ProveedorParaActualizar.IdUsuario;//hay que poner el id del usuario logueado
            Proveedor.UltimaModificacion = ProveedorParaActualizar.UltimaModificacion;                   
            context.SaveChanges();
            return Proveedor;
        }

        public void ActualizarPresupuestoProveedor(Proveedor model)
        {
            Proveedor Proveedor = GetProveedorPorId(model.Id);            
            Proveedor.IdPresupuesto = model.IdPresupuesto;         
            Proveedor.Activo = true;
            Proveedor.IdUsuario = model.IdUsuario;
            Proveedor.UltimaModificacion = model.UltimaModificacion;
            context.SaveChanges();
        }

        public int EliminarProveedor(int IdProveedor)
        {
            Proveedor Proveedor = GetProveedorPorId(IdProveedor);
            Proveedor.Activo = false;
            // Proveedor.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
            return context.SaveChanges();
        }

      
    }
}