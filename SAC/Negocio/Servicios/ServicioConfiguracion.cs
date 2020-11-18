using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using AutoMapper;
using Negocio.Modelos;

namespace Negocio.Servicios
{   
    public class ServicioConfiguracion : ServicioBase
    {
        private RolRepositorio rolRepositorio;
        private AccionRepositorio accionRepositorio;
        private MenuSidebarRepositorio menuSidebarRepositorio;
        public Action<string, string> _mensaje;
        public ServicioConfiguracion()
        {
            rolRepositorio = kernel.Get<RolRepositorio>();
            accionRepositorio = kernel.Get<AccionRepositorio>();
            menuSidebarRepositorio = kernel.Get<MenuSidebarRepositorio>();
        }


       
        /// <summary>
        ///  meodos para gestion de acciones
        /// </summary>
        /// <returns></returns>
        public List<Modelos.AccionModel> GetAccion()
        { 
            return Mapper.Map < List<Accion>,  List<Modelos.AccionModel>>(accionRepositorio.GetAccion());            
        }    
        public AccionModel CreateAccion(Modelos.AccionModel Accion)
        {
            Accion acc = Mapper.Map< Modelos.AccionModel, Accion>(Accion);
            acc.Activo = true;
            acc.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
            return Mapper.Map< Accion, AccionModel>(accionRepositorio.CreateAccion(acc));
        }
        public AccionModel GetAccionPorId(int idAccion)
        {
            return Mapper.Map < Accion, Modelos.AccionModel > (accionRepositorio.GetAccionPorId(idAccion));
        }
        public AccionModel ActualizarAccion(AccionModel Accion)
        {
            Accion AccionParaActualizar = Mapper.Map<Modelos.AccionModel, Accion>(Accion);
            return Mapper.Map<Accion, Modelos.AccionModel>(accionRepositorio.ActualizarAccion(AccionParaActualizar));

        }

      
        public void DeleteAccion(int idAccion)
        {
            accionRepositorio.DeleteAccion(idAccion);
        }

       
       
        /// <summary>
        ///  meodos para gestion de roles
        /// </summary>
        /// <returns></returns>
        public List<Modelos.RolModel> GetRol()
        { 
            return Mapper.Map<List<Rol>, List<Modelos.RolModel>>(rolRepositorio.GetRol());            
        }    
        public Modelos.RolModel CrearRol(Modelos.RolModel rol)
        {
            Rol p = Mapper.Map<Modelos.RolModel, Rol>(rol);
            return Mapper.Map<Rol, Modelos.RolModel>(rolRepositorio.Insertar(p));
        }
        public RolModel GetRolPorId(int idRol)
        {
            return Mapper.Map<Rol, Modelos.RolModel>(rolRepositorio.GetRolPorId(idRol));
        }
        public void ActualizarRol(RolModel rol)
        {             
            rolRepositorio.ActualizarRol(Mapper.Map<Modelos.RolModel, Rol>(rol));
        }
        public void InsertarAccionPorRol(AccionPorRolModel accionPorRol)
        {
            rolRepositorio.InsertarAccionPorRol(Mapper.Map< AccionPorRolModel, AccionPorRol>(accionPorRol));
        }

        public List<AccionModel> GetAccionNoMenu(int idRol)
        {
            var a = accionRepositorio.GetAccionNoMenu(idRol);
            return Mapper.Map<List<Accion>, List<AccionModel>>(a);
        }

        public List<AccionPorRolModel> GetAllAccionPorRol(int idRol)
        {
            return Mapper.Map<List<AccionPorRol>, List<AccionPorRolModel>>(accionRepositorio.GetAllAccionPorRol(idRol));
        }

        public AccionPorRolModel GetAccionPorRol(int idRol,int idAccion)
        {
            return Mapper.Map<AccionPorRol, AccionPorRolModel>(accionRepositorio.GetAccionPorRol(idRol, idAccion));
        }

        public List<MenuSideBarModel> GetMenuSidebarPorRol(int idRol)
        {
            return Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(menuSidebarRepositorio.GetMenuSidebarPorRol(idRol));
        }

        
        public List<AccionModel> GetAccionesEnMenuSidebarPorRol(int idRol)
        {
            return Mapper.Map<List<Accion>, List<AccionModel>>(menuSidebarRepositorio.GetAccionesEnMenuSidebarPorRol(idRol));
        }

        public RolModel DeleteAccionPorRol(int idRolPorAccion)
        {
            return Mapper.Map<Rol, Modelos.RolModel>(rolRepositorio.DeleteAccionPorRol(idRolPorAccion));           
        }
       
        /// <summary>
        ///  meodos para gestion del MenuSideBar
        ///  treeview
        /// </summary>
        /// <returns></returns>
        public List<MenuSideBarModel> GetMenuSidebar()
        {
          return  Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(menuSidebarRepositorio.GetMenuSidebar());          
        }


        public MenuSideBarModel GetMenuSidebarPorId(int id)
        {
            
            try
            {
                var menuSideBarModel = Mapper.Map<MenuSidebar, MenuSideBarModel>(menuSidebarRepositorio.GetMenuSidebarPorId(id));
                return menuSideBarModel;
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
        }
        public MenuSideBarModel GetMenuSidebarPorIdFull(int id)
        {
            try
            {
                var menuSideBarModel = Mapper.Map<MenuSidebar, MenuSideBarModel>(menuSidebarRepositorio.GetMenuSidebarPorIdFull(id));
                return menuSideBarModel;
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
        }
      
        public List<AccionModel> GetAccionEnMenuSide(int idmenusider)
        {
            try
            {               
                var AccionesEnMenu = Mapper.Map<List<Accion>, List<AccionModel>>(menuSidebarRepositorio.GetAccionEnMenuSide(idmenusider));
                return AccionesEnMenu;
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
        }

        public List<MenuSideBarModel> GetMenuSidebar(int IdUsuario)
        {         
            try
            {
                var lista = Mapper.Map<List<MenuSidebar>, List<MenuSideBarModel>>(menuSidebarRepositorio.GetMenuSidebar());
                return lista;
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }

        }

        public MenuSideBarModel CreateMenusidebar(MenuSideBarModel sidebar)
        {
           

                MenuSidebar menu = Mapper.Map<MenuSideBarModel, MenuSidebar>(sidebar);
            return Mapper.Map<MenuSidebar, MenuSideBarModel>(menuSidebarRepositorio.Insertar(menu));

          

        }
        public void ActualizarMenusidebar(MenuSideBarModel menuSideBarModel)
        {
           

            MenuSidebar menu = Mapper.Map<MenuSideBarModel, MenuSidebar>(menuSideBarModel);
            //return Mapper.Map<MenuSidebar, MenuSideBarModel>(menuSidebarRepositorio.ActualizarMenusidebar(menu));
            menuSidebarRepositorio.ActualizarMenusidebar(menu);
        }

        public void DeleteMenusidebar(int id)
        {
                menuSidebarRepositorio.DeleteMenusidebar(id);          
        }
      
       
    }

}

