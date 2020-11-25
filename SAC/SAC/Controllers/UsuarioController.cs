using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Atributos;
using SAC.Models;
using AutoMapper;
using Negocio.Modelos;

namespace SAC.Controllers
{
    public class UsuarioController : BaseController
    {

        private readonly ServicioUsuarios servicioUsuario;

        private ServicioPersona servicioPersona;
        private ServicioConfiguracion servicioConfiguracion = new ServicioConfiguracion();

        public UsuarioModelView usuario = new UsuarioModelView();

        public UsuarioController()
        {
            servicioUsuario = new ServicioUsuarios();
            servicioUsuario._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }


        // GET: Usuario
        public ActionResult Index()
        {
            List<UsuarioModelView> usuarioModelView = Mapper.Map<List<UsuarioModel>, List<UsuarioModelView>>(servicioUsuario.GetAllUsuario());
            return View(usuarioModelView);
        }


        private SelectList selectListRoles()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var roles = servicioConfiguracion.GetAllRoles();
            foreach (var item in roles)
            {
                dic.Add(item.IdRol, item.descripcion);
            }
            return new SelectList(dic, "Key", "Value");
        }
       
     
        public ActionResult AddOrEdit(int id = 0)
        {
            UsuarioModelView model;
            if (id == 0)
            {
                model = new UsuarioModelView();               
            }
            else
            {
                model = Mapper.Map<UsuarioModel, UsuarioModelView>(servicioUsuario.ObtenerPorID(id));
                model.password = "";               
            }      
            model.Roles = Mapper.Map<List<RolModel>,List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);                 
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(UsuarioModelView model)
        {
            //bool hasErrors = ViewData.ModelState.Values.Any(x => x.Errors.Count > 1);
            //foreach (ModelState state in ViewData.ModelState.Values.Where(x => x.Errors.Count > 0))
            //{
            //    servicioConfiguracion._mensaje(state.Value.ToString(), "ok");
            //}
            if (ModelState.IsValid)
            {
                UsuarioModel usuario = (UsuarioModel) System.Web.HttpContext.Current.Session["currentUser"];
                model.idUsuarioLogin = usuario.IdUsuario;
                if (model.idUsuario == null) {                   
                    servicioUsuario.CreateUsuario(Mapper.Map< UsuarioModelView, UsuarioModel>(model));
                }
                else {
                    servicioUsuario.UpdateUsuario(Mapper.Map<UsuarioModelView, UsuarioModel>(model));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // POST: Usuario/Create
        //[HttpPost, ActionName("Guardar")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Guardar(UsuarioModelView usuarioGuardar)
        //{



        //    var personaModel = usuarioGuardar.Persona;
        //    personaModel.FechaCreacion = Convert.ToDateTime(DateTime.Now.ToString());
        //    personaModel.FechaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
        //    personaModel.Activo = true;
        //    var persona = servicioPersona.CrearPersona(personaModel);

        //    var usuario = Mapper.Map<UsuarioModelView, UsuarioModel>(usuarioGuardar);
        //    usuario.IdPersona = persona.Id;
        //    usuario.IdRol = usuarioGuardar.idRol;
        //    usuario.Password = Negocio.Helpers.StringHelper.ObtenerMD5(persona.Documento);
        //    usuario.Actualizado = Convert.ToDateTime(DateTime.Now.ToString());
        //    usuario.Creado = Convert.ToDateTime(DateTime.Now.ToString());
        //    usuario.Activo = true;
        //    var evento = servicioUsuario.Agregar(usuario);

        //    return RedirectToAction("Index");

        //}


        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    
    
    }
}
