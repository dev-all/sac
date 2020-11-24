using AutoMapper;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.Models;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SAC.Controllers
{
    public class ProveedorController : BaseController
    {

        private ServicioProveedor servicioProveedor = new ServicioProveedor();

        public ProveedorController()
        {
            servicioProveedor._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }

        // GET: Accion
        public ActionResult Index()
        {

            List<ProveedorModelView> model = Mapper.Map<List<ProveedorModel>, List<ProveedorModelView>>(servicioProveedor.GetAllProveedor());
            return View(model);
        }

        // GET: Accion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Accion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accion/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(AccionModelView accionModelView)
        {
            try
            {
               // var evento = servicioConfiguracion.CreateAccion(Mapper.Map<AccionModelView, AccionModel>(accionModelView));
               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConfiguracionModelView configuracionModelView)
        {
            try
            {
               // AccionModelView accionModelView = configuracionModelView.accion;
                //var evento = servicioConfiguracion.CreateAccion(Mapper.Map<AccionModelView, AccionModel>(accionModelView));
                //var evento = servicioConfiguracion.CreateAccion(Mapper.Map<AccionModelView, AccionModel>(accionModelView));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }

   
        // GET: Accion/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
               // AccionModel accionModel = servicioConfiguracion.GetAccionPorId(id);
               // var accion = Mapper.Map<AccionModel, AccionModelView>(accionModel);
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }

        // POST: Accion/Edit/5
        [HttpPost]
        public ActionResult Edit(AccionModelView accionModelView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                 //   var accionModel = servicioConfiguracion.ActualizarAccion(Mapper.Map<AccionModelView, AccionModel>(accionModelView));
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.info = ex.InnerException;
                return View();
            }
        }


        // POST: Accion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
              //  servicioConfiguracion.DeleteAccion(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





    }
}
