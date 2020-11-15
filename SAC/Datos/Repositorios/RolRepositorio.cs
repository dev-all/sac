using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class RolRepositorio : RepositorioBase<Rol>
    {
       private SAC_Entities context;
    
        public RolRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Rol CrearRol(Rol rol)
        {
            return Insertar(rol);
        }

        public List<Rol> GetRol()
        {
            return context.Rol
                .Include(r => r.AccionPorRol)                
                .OrderBy(acc => acc.Descripcion).ToList();
        }

        public Rol GetRolPorId(int id)
        {           
            return context.Rol.Where(r => r.IdRol == id).FirstOrDefault(); 
        }
      
        public void ActualizarRol(Rol RolParaActualizar)
        {

            context.Rol.Attach(RolParaActualizar);
            context.Entry(RolParaActualizar).Property(x => x.Descripcion).IsModified = true;
            context.Entry(RolParaActualizar).Property(x => x.EsAdministrador).IsModified = true;
            //context.Entry(RolParaActualizar).Property(x => x.IdHome).IsModified = true;
            //context.Entry(RolParaActualizar).Property(x => x.AccionPorRol).IsModified = true;
            context.SaveChanges();

            //Rol rol = GetRolPorId(RolParaActualizar.IdRol);
            //rol.Descripcion = RolParaActualizar.Descripcion ?? rol.Descripcion;
            //rol.IdHome = RolParaActualizar.IdHome ?? rol.IdHome;
            //rol.AccionPorRol = RolParaActualizar.AccionPorRol ?? rol.AccionPorRol;
            //context.SaveChanges();

          // return rol;
        }

        public Rol DeleteAccionPorRol(int idRolPorAccion)
        {                       
            var apr = context.AccionPorRol.Where(r => r.idRolPorAccion == idRolPorAccion).FirstOrDefault();
            var idRol = apr.idRol;
            context.AccionPorRol.Remove(apr);
            context.SaveChanges();
            Rol rol = GetRolPorId(idRol);
            return rol;
        }

        public void InsertarAccionPorRol(AccionPorRol accionPorRolView)
        {
            context.AccionPorRol.Add(accionPorRolView);
            context.SaveChanges();          
        }
    }
}