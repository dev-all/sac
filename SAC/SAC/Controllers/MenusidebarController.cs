﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Models;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;
using SAC.Atributos;
using Entidad.Models;
using System.Web.Script.Serialization;
namespace SAC.Controllers

    
{
    public class MenusidebarController : BaseController
    {


        /* PRUEBA DE COMENTARIO */

        private ServicioConfiguracion servicioConfiguracion = new ServicioConfiguracion();
        ConfigMenuSidebarModelView configAccionModelView;
        private String JsonTreeView;
        private JavaScriptSerializer jsonString = new JavaScriptSerializer();

        // GET: 
        [AutorizacionDeSistema]
        public ActionResult Index()
        {
            configAccionModelView = new ConfigMenuSidebarModelView
            {
                // listar menu
               // IEmenuSideBar = Mapper.Map<List<MenuSideBarModel>, List<MenuSideBarModelView>>(servicioConfiguracion.GetMenuSidebar()),
                
                // drop de nuevo menu
                ICaccion = Mapper.Map<List<AccionModel>, List<AccionModelView>>(servicioConfiguracion.GetAccion())

            };
            
            ViewBag.JsonMenuSider = TreeView(servicioConfiguracion.GetMenuSidebar());

            return View(configAccionModelView);
        }

        public String TreeView(List<MenuSideBarModel> model)
        {
            List<TreeViewModel> ListTreeView = new List<TreeViewModel>();
            foreach (var i in model)
            {
                TreeViewModel item = new TreeViewModel();
                item.text = i.Titulo;
                item.href = "/Menusidebar/Edit/" + i.IdMenuSidebar.ToString();
                if (i.Group.Count > 0)
                {
                    List<TreeViewModel> ListNode = new List<TreeViewModel>();
                    foreach (var n in i.Group)
                    {
                        TreeViewModel nodo = new TreeViewModel();
                        nodo.text = n.Titulo;
                        nodo.href = "/Menusidebar/Edit/" + n.IdMenuSidebar.ToString();
                        ListNode.Add(nodo);
                    }
                    item.nodes = ListNode;
                }
                ListTreeView.Add(item);
            }
            JsonTreeView += jsonString.Serialize(ListTreeView);
            return JsonTreeView;
        }



        public ActionResult Edit(int id)
        {

            configAccionModelView = new ConfigMenuSidebarModelView
            {
                IEmenuSideBar = Mapper.Map<List<MenuSideBarModel>, List<MenuSideBarModelView>>(servicioConfiguracion.GetMenuSidebar()),
                ICaccion = Mapper.Map<List<AccionModel>, List<AccionModelView>>(servicioConfiguracion.GetAccion()),
                menuSideBar = Mapper.Map<MenuSideBarModel, MenuSideBarModelView>(servicioConfiguracion.GetMenuSidebarPorIdFull(id))
            };
           
            return View(configAccionModelView);

        }
       
        [HttpPost]
        public ActionResult Edit(ConfigMenuSidebarModelView configMenuSidebarModelView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  servicioConfiguracion.ActualizarMenusidebar(Mapper.Map<MenuSideBarModelView, MenuSideBarModel>(configMenuSidebarModelView.menuSideBar));
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }


        //  [AutorizacionDeSistema]
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuSideBarModelView menusidebar)
        {

            MenuSideBarModelView sidebar = new MenuSideBarModelView
            {
                Icono = menusidebar.Icono,
                IdAccion = menusidebar.IdAccion,
                IdParent = menusidebar.IdParent,
                Titulo = menusidebar.Titulo,
                Url = "CreateMenusidebar",
                Activo = true,
                FechaModificacion = Convert.ToDateTime(DateTime.Now.ToString())
            };
            var evento = servicioConfiguracion.CreateMenusidebar(Mapper.Map<MenuSideBarModelView, MenuSideBarModel>(sidebar));
            return RedirectToAction("Index");
        }

        // POST: menusidebar/DeleteMenusidebar        [AutorizacionDeSistema]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    servicioConfiguracion.DeleteMenusidebar(id);
        //    return RedirectToAction("Index");

        //}
        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(int Id)
        {
            servicioConfiguracion.DeleteMenusidebar(Id);
            return Json(new { status = "Success" });
        }

        // GET: Accion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

      


    }
}
