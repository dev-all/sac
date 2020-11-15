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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Proveedor"></param>
        /// <returns></returns>
        public Proveedor CreateProveedor(Proveedor proveedor)
        {
           return  Insertar(proveedor);
        }

        public List<Proveedor> GetProveedor()
        {
            return context.Proveedor.OrderBy(x => x.Nombre ).ToList();
        }

        public Proveedor GetProveedorPorId(int id)
        {           
            return context.Proveedor.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }

      
        public Proveedor ActualizarProveedor(Proveedor ProveedorParaActualizar)
        {
            Proveedor Proveedor = GetProveedorPorId(ProveedorParaActualizar.Id);            
            Proveedor.Nombre = ProveedorParaActualizar.Nombre ?? Proveedor.Nombre;            
            context.SaveChanges();
            return Proveedor;
        }




        public void ActualizarProveedorNotReturn(Proveedor proveedor)
        {
            context.Proveedor.Attach(proveedor);
            context.Entry(proveedor).Property(x => x.Nombre).IsModified = true;
            context.Entry(proveedor).Property(x => x.Activo).IsModified = true;
            context.SaveChanges();
        }

        public void DeleteProveedor(int IdProveedor)
        {
            Proveedor Proveedor = GetProveedorPorId(IdProveedor);
            Proveedor.Activo = false;
            // Proveedor.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
            context.SaveChanges();
        }

     
        


    }
}