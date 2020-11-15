using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using Z.EntityFramework.Plus;

namespace Datos.Repositorios
{
    public class MenuSidebarRepositorio : RepositorioBase<MenuSidebar>
    {
       private SAC_Entities context;
    
        public MenuSidebarRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        #region
        /// <summary>
        /// MenuSidebar
        /// </summary>
        public List<MenuSidebar> GetMenuSidebar()
        {
            var side = context.MenuSidebar.Where(m => m.Activo == true && m.IdParent == null).
                        IncludeFilter(m => m.MenuSidebar1.Where(p => p.Activo==true))
                        .OrderBy(acc => acc.Titulo).ToList();           
            return side;
        }

        /// <summary>
        /// MenuSidebar
        /// </summary>
        public List<MenuSidebar> GetMenuSidebar(int IdUsuario)
        {
            var side = context.MenuSidebar.
                IncludeFilter(m => m.MenuSidebar1.Where(p => p.Activo == true))
                .Where(menu => menu.Activo == true && menu.IdParent == null)
                .OrderBy(acc => acc.Titulo).ToList();
            return side;
        }


        public MenuSidebar CrearMenusidebar(MenuSidebar menusidebar)
        {
          return Insertar(menusidebar);         
        }

        public void DeleteMenusidebar(int id)
        {
            MenuSidebar menusidebar = GetMenuSidebarPorId(id);
            if (menusidebar !=  null) { 
                menusidebar.Activo = false;
                menusidebar.FechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                context.SaveChanges();
            }
        }

        public MenuSidebar GetMenuSidebarPorId(int id)
        {            
                return context.MenuSidebar
                        .Include(m => m.Accion)
                        .Include(m => m.MenuSidebar1)
                        .Where(menu => menu.IdMenuSidebar == id && menu.Activo == true).FirstOrDefault();
        }

        public void ActualizarMenusidebar(MenuSidebar menuSideBarModel)
        {

         //1 update completo
            //context.Entry(menuSideBarModel).State = EntityState.Modified;
            //context.SaveChanges();

         //2 casero jaja
            //MenuSidebar menu = GetMenuSidebarPorId(menuSideBarModel.IdMenuSidebar);
            // menu.Controlador = AccionParaActualizar.Controlador ?? menu.Controlador;          
            //context.SaveChanges();

            // modifica pero no retornaria datos //si mencionas al diablo se te puede aparecer
            context.MenuSidebar.Attach(menuSideBarModel);
            context.Entry(menuSideBarModel).Property(x => x.Icono).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.Titulo).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.Url).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.IdParent).IsModified = true;
            context.Entry(menuSideBarModel).Property(x => x.IdAccion).IsModified = true;
            context.SaveChanges();

            //MenuSidebar menu = GetMenuSidebarPorId(menuSideBarModel.IdMenuSidebar);
            //return menu;
        }


        #endregion
    }
}