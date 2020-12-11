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
    public class CajaController : BaseController
    {

        private ServicioCajaGrupo serviciocajagrupo = new ServicioCajaGrupo();
        public CajaController()
        {            
            serviciocajagrupo._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }


        // GET: Usuario
        public ActionResult Index()
        {
            List<CajaGrupoModelView> cajaGrupoModelViews = new List<CajaGrupoModelView>();
             cajaGrupoModelViews = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(serviciocajagrupo.GetAllCajaGrupo());
            return View(cajaGrupoModelViews);
        }



        public ActionResult AddOrEdit(int id = 0)
        {
            CajaGrupoModelView model;
            if (id == 0)
            {
                model = new CajaGrupoModelView();
            }
            else
            {
                model = Mapper.Map<CajaGrupoModel, CajaGrupoModelView>(serviciocajagrupo.GetGrupoCajaPorId(id));
                
            }

           // model.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit(CajaGrupoModelView model)
        {
           

            if (ModelState.IsValid)
            {
              
                var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];




                model.IdUsuario= OUsuario.IdUsuario;

                if (model.IdUsuario == null)
                {
                    serviciocajagrupo.GuardarGrupoCaja(Mapper.Map<CajaGrupoModelView, CajaGrupoModel>(model));
                }
                else
                {
                    serviciocajagrupo.ActualizarGrupoCaja(Mapper.Map<CajaGrupoModelView, CajaGrupoModel>(model));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}



