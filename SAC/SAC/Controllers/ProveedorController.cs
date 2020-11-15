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
    public class ProveedorController : BaseController
    {

        private ServicioProveedor servicioConfiguracion = new ServicioProveedor();
        // GET: Accion
        public ActionResult Index()
        {
            //ServicioProveedor configAccionModelView = new ServicioProveedor
            //{
            //    Acciones = Mapper.Map<List<>, List<AccionModelView>>(servicioConfiguracion.GetAccion())
            //};
            return View();
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
