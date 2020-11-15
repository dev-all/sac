using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Models;
using Negocio.Servicios;
using Negocio.Modelos;
using AutoMapper;


namespace SAC.Controllers
{
    public class RolController : BaseController
    {

        private ServicioConfiguracion servicioConfiguracion = new ServicioConfiguracion();

        // GET: Rol
        public ActionResult Index()
        {
            ConfigRolModelView model = new ConfigRolModelView
            {
                Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetRol())
            };
           // List<RolModelView> RolModelView = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetRol());
            return View(model);
        }

        public ActionResult MasEvento(int id)
        {
            var rol = Mapper.Map<RolModel, RolModelView>(servicioConfiguracion.GetRolPorId(id));
            List<AccionModelView> ControladorAccion = Mapper.Map<List<AccionModel>, List<AccionModelView>>(servicioConfiguracion.GetAccion());
            rol.Acciones = ControladorAccion ;

            return View("Edit", rol);
        }
     
        [HttpPost, ActionName("AddAccionPorRol")]
        [ValidateAntiForgeryToken]
        public ActionResult AddAccionPorRol(RolModelView rolModelView)
        {
            var accionPorRol = new AccionPorRolView {
                                                    idRol = rolModelView.idRol,
                                                    idAccion = rolModelView.idAccionPorRol
                                                };           
            servicioConfiguracion.InsertarAccionPorRol(Mapper.Map<AccionPorRolView, AccionPorRolModel>(accionPorRol));
            return RedirectToAction("MasEvento", new { id = rolModelView.idRol });
         
        }

        [HttpPost, ActionName("DeleteAccionPorRol")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccionPorRol(int id)
        {
            var rol = (servicioConfiguracion.DeleteAccionPorRol(id));
            return RedirectToAction("MasEvento", new { id= rol.IdRol });
        }

      

        // GET: Rol/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rol/Create
        [HttpPost]
        public ActionResult Create(ConfigRolModelView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Rol.esAdministrador = false;
                    var rol = servicioConfiguracion.CrearRol(Mapper.Map<RolModelView, RolModel>(model.Rol));
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }

        // GET: Rol/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rol/Edit/5
        [HttpPost]
        public ActionResult Edit( RolModelView model )
        {
            try
            {

                //if (ModelState.IsValid)
                //{}
                    servicioConfiguracion.ActualizarRol(Mapper.Map<RolModelView, RolModel>(model));
                    ViewBag.info = "Se Guardo Correctamente";
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        [HttpPost, ActionName("Delete")]
        public JsonResult Delete(int Id)
        {
           // servicioConfiguracion.DeleteMenusidebar(Id);
            return Json(new { status = "Success" });
        }



        private SelectList selectListAccion(List<AccionModelView> acc)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (var item in acc)
            {
                dic.Add(item.IdAccion, item.Controlador + " - " + item.Nombre);
            }
            return new SelectList(dic, "Key", "Value");
        }


    }
}
