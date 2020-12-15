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

        private ServicioCaja servicioCaja = new ServicioCaja();

        private ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();


        public CajaController()
        {            
            servicioCaja._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }


        // GET: Usuario
        public ActionResult Index()
        {
            List<CajaModelView> cajaGrupoModelViews = new List<CajaModelView>();
             cajaGrupoModelViews = Mapper.Map<List<CajaModel>, List<CajaModelView>>(servicioCaja.GetAllCaja());
            return View(cajaGrupoModelViews);



        }





     
        public ActionResult AddOrEdit(int id = 0)
        {

            CargarCajaGrupo();

            CajaModelView model;
            if (id == 0)
            {
                model = new CajaModelView();
            }
            else
            {
                model = Mapper.Map<CajaModel, CajaModelView>(servicioCaja.GetCajaPorId(id));
                
            }

           // model.Roles = Mapper.Map<List<RolModel>, List<RolModelView>>(servicioConfiguracion.GetAllRoles());
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken] 
        
        public ActionResult AddOrEdit(CajaModelView model)
        {
            try
            {        
            if (ModelState.IsValid)
            {

                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario= OUsuario.IdUsuario;
                    //servicioCaja._mensaje("","ok");
               if (model.Id <= 0)
                {
                   servicioCaja.GuardarCaja(Mapper.Map<CajaModelView, CajaModel>(model));
                }
                else
                {
                    servicioCaja.ActualizarCaja(Mapper.Map<CajaModelView, CajaModel>(model));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);

            }
            catch (Exception)
            {
                return View(model);
            }
        }



        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                servicioCaja.Eliminar(id);
               
            }
            catch (Exception ex)
            {
                servicioCaja._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }


      



        //combo pais
        public void CargarCajaGrupo()
        {

            ServicioCajaGrupo servicioCajaGrupo = new ServicioCajaGrupo();
            List<CajaGrupoModelView> ListaCajaGrupo = Mapper.Map<List<CajaGrupoModel>, List<CajaGrupoModelView>>(servicioCajaGrupo.GetAllCajaGrupo());
            //ListaPais = ListaPais.OrderBy(p => p.Nombre).ToList();
            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaCajaGrupo = null;
            retornoListaCajaGrupo = (ListaCajaGrupo.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Codigo
                                  })).ToList();
            retornoListaCajaGrupo.Insert(0, new SelectListItem { Text = "--Seleccione Codigo de Caja--", Value = "" });


            ViewBag.Listapagina = retornoListaCajaGrupo;
        }




        //public ActionResult DateSales(string value)



        //{
        //    var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
        //    var view = db.SaleDetails.Include(s => s.Sale).Where(s => s.Sale.Warehouse.CompanyId == user.CompanyId && s.Sale.Date == DateTime.Today);
        //    return View(view.ToList());

        //    ViewBag.TotalPrice = view.Sum(m => m.Price);
        //    ViewBag.TotalQuantity = view.Sum(m => m.Quantity);
        //}


    }




}



