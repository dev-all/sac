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
             
    }
}