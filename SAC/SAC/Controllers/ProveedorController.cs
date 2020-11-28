using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//agregadas
using AutoMapper;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.Atributos;
using SAC.Models;
using System.Web.Routing;


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

       
        // GET: Accion/Create
        public ActionResult Agregar()
        {
            CargarCombos();
            return View();
        }


        public void CargarCombos()
        {
            CargarPais();
            CargarTipoIva();
            CargarSubRubro();
            CargarTipoMoneda();
        }


        public void CargarPais()
        {
            ServicioPais servicioPais = new ServicioPais();
            List<PaisModelView> ListaPais = Mapper.Map<List<PaisModel>, List<PaisModelView>>(servicioPais.GetAllPais());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaPais = null;
            retornoListaPais = (ListaPais.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaPais.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });
            ViewBag.ListaPais = retornoListaPais;
        }

        public void CargarTipoIva()
        {
            ServicioTipoIva servicioTipoIva = new ServicioTipoIva();
            List<TipoIvaViewModel> ListaPais = Mapper.Map<List<TipoIvaModel>, List<TipoIvaViewModel>>(servicioTipoIva.GetAllTipoIva());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaTipoIva = null;
            retornoListaTipoIva = (ListaPais.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaTipoIva.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });
            ViewBag.ListaTipoIva = retornoListaTipoIva;
        }

        public void CargarSubRubro()
        {
            ServicioSubRubro servicioSubRubro = new ServicioSubRubro();
            List<SubRubroModelView> ListaSubRubro = Mapper.Map<List<SubRubroModel>, List<SubRubroModelView>>(servicioSubRubro.GetAllSubRubro());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaSubRubro = null;
            retornoListaSubRubro = (ListaSubRubro.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaSubRubro.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });
            ViewBag.ListaSubRubro = retornoListaSubRubro;
        }

        //public void CargarAfipRegimen()
        //{
        //    ServicioAfipRegimen servicioSubRubro = new ServicioAfipRegimen();
        //    List<AfipRegimenModelView> ListaSubRubro = Mapper.Map<List<AfipRegimenModel>, List<AfipRegimenModelView>>(servicioSubRubro.GetAllAfipRegimen());

        //    //esto es para pasarlo a select list (drop down list)
        //    List<SelectListItem> retornoListaAfipRegimen = null;
        //    retornoListaAfipRegimen = (ListaSubRubro.Select(x =>
        //                          new SelectListItem()
        //                          {
        //                              Value = x.Id.ToString(),
        //                              Text = x.Descripcion
        //                          })).ToList();
        //    retornoListaAfipRegimen.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });
        //    ViewBag.ListaAfipRegimen = retornoListaAfipRegimen;
        //}

        public void CargarTipoMoneda()
        {
            ServicioTipoMoneda servicioTipoMoneda = new ServicioTipoMoneda();
            List<TipoMonedaModelView> ListaTipoMoneda = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(servicioTipoMoneda.GetAllTipoMonedas());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaAfipRegimen = null;
            retornoListaAfipRegimen = (ListaTipoMoneda.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaAfipRegimen.Insert(0, new SelectListItem { Text = "--Seleccione País--", Value = "" });
            ViewBag.ListaTipoMoneda = retornoListaAfipRegimen;
        }


    }
}
