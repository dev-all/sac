﻿using System.Data.Entity;
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
    
        public CompraFactura ActualizarGetCompraFactura(CompraFactura model)
        {

            //CompraFactura CompraFactura = GetAccionPorId(AccionParaActualizar.IdAccion);
            //CompraFactura.Controlador = AccionParaActualizar.Controlador ?? CompraFactura.Controlador;
            //CompraFactura.Nombre = AccionParaActualizar.Nombre ?? CompraFactura.Nombre;
            //CompraFactura.Descripcion = AccionParaActualizar.Descripcion ?? CompraFactura.Descripcion;
            //context.SaveChanges();

            return null;
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

        public CompraFactura GetCompraFacturaPorNroFacturaIdProveedor(int numeroFactura, int idProveedor)
        {
            context.Configuration.LazyLoadingEnabled = false;
            var fact = context.CompraFactura
                            .Include("CompraIva")
                            .Where(f => f.Activo == true
                            && f.NumeroFactura==numeroFactura
                            && f.IdProveedor == idProveedor
                            ).FirstOrDefault();
            return fact;
        }

        public List<CompraFactura> GetAllCompraFacturaPorNro(int nroFactura)
        {
          var fact =   context.CompraFactura.Where(f => f.Activo == true && f.NumeroFactura.ToString().Contains(nroFactura.ToString())).OrderBy(acc => acc.NumeroFactura).ToList();
            return fact;

        }
        public CompraFactura GetCompraFacturaIVAPorNro(int nroFactura)
        {
            var fact = context.CompraFactura.Where(f => f.Activo == true && f.NumeroFactura == nroFactura).OrderBy(acc => acc.NumeroFactura).FirstOrDefault();            
            return fact;

        }
        //public CompraIva GetCompraFacturaIVAPorNro(int nroFactura)
        //{
        //    var fact = context.CompraFactura.Where(f => f.Activo == true && f.NumeroFactura == nroFactura).OrderBy(acc => acc.NumeroFactura).FirstOrDefault();
        //    var cfi = (from f in context.CompraFactura
        //               where f.NumeroFactura == nroFactura
        //               select f.CompraIva).FirstOrDefault();
        //    return cfi;

        //}
    }
}