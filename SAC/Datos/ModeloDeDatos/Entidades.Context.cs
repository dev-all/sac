﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos.ModeloDeDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SAC_Entities : DbContext
    {
        public SAC_Entities()
            : base("name=SAC_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccionPorRol> AccionPorRol { get; set; }
        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<homePorRol> homePorRol { get; set; }
        public virtual DbSet<Notificacion> Notificacion { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<MenuSidebar> MenuSidebar { get; set; }
        public virtual DbSet<Accion> Accion { get; set; }
        public virtual DbSet<Prioridad> Prioridad { get; set; }
        public virtual DbSet<TipoMoneda> TipoMoneda { get; set; }
        public virtual DbSet<TipoProveedor> TipoProveedor { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Localidad> Localidad { get; set; }
        public virtual DbSet<AfipCategorias> AfipCategorias { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<SubRubro> SubRubro { get; set; }
        public virtual DbSet<TipoComprobante> TipoComprobante { get; set; }
        public virtual DbSet<TipoComprobanteTipoIva> TipoComprobanteTipoIva { get; set; }
        public virtual DbSet<TipoIva> TipoIva { get; set; }
        public virtual DbSet<GrupoCuenta> GrupoCuenta { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<BancoCuenta> BancoCuenta { get; set; }
        public virtual DbSet<BancoCuentaBancaria> BancoCuentaBancaria { get; set; }
        public virtual DbSet<Caja> Caja { get; set; }
        public virtual DbSet<CajaTipoMovimiento> CajaTipoMovimiento { get; set; }
        public virtual DbSet<Cheque> Cheque { get; set; }
        public virtual DbSet<Chequera> Chequera { get; set; }
        public virtual DbSet<CompraFacturaPago> CompraFacturaPago { get; set; }
        public virtual DbSet<GrupoCaja> GrupoCaja { get; set; }
        public virtual DbSet<Tarjetas> Tarjetas { get; set; }
        public virtual DbSet<TipoPago> TipoPago { get; set; }
        public virtual DbSet<ValorCotizacion> ValorCotizacion { get; set; }
        public virtual DbSet<CompraFactura> CompraFactura { get; set; }
        public virtual DbSet<CompraIva> CompraIva { get; set; }
        public virtual DbSet<CajaSaldo> CajaSaldo { get; set; }
        public virtual DbSet<Diario> Diario { get; set; }
        public virtual DbSet<Imputacion> Imputacion { get; set; }
        public virtual DbSet<PrespuestoActual> PrespuestoActual { get; set; }
        public virtual DbSet<TipoComprobanteVenta> TipoComprobanteVenta { get; set; }
        public virtual DbSet<CuentaCorriente> CuentaCorriente { get; set; }
    }
}
