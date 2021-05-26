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
using System.Globalization;
using System.Text;

namespace SAC.Controllers
{
    public class ClienteController : BaseController
    {

         private ServicioCliente oServicioCliente = new ServicioCliente();

         private ServicioClienteDireccion oServicioClienteDireccion = new ServicioClienteDireccion();

         private ServicioTipoCliente OservicioTipoCliente = new ServicioTipoCliente();

        private ServicioPieNota OservicioPieNota = new ServicioPieNota();

        private ServicioTipoMoneda OservicioTipoMoneda= new ServicioTipoMoneda();

        private ServicioGrupoPresupuesto OservicioGrupoPresupuesto = new ServicioGrupoPresupuesto();

        private ServicioPais OservicioPais = new ServicioPais();



        public ClienteController()
        {
            oServicioCliente._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
            oServicioClienteDireccion._mensaje = (msg_, tipo_) => CrearTempData(msg_, tipo_);
        }


        // GET: Usuario
        public ActionResult Index()
        {
            ClienteModelView model = new ClienteModelView();
            model.CVisible = false;
             CargarTipoCliente();
             return View(model);          
        }


        [HttpPost]
        public ActionResult Index(int idTipoCliente, string CNombreCliente)
        {

            ClienteModelView model = new ClienteModelView();

            if (idTipoCliente == 0)

            {
                

                  model.ListaCliente = Mapper.Map<List<ClienteModel>, List<ClienteModelView>>(oServicioCliente.GetClientePorNombre(CNombreCliente));
            }

            
            if (idTipoCliente > 0)

            {
                  model.ListaCliente = Mapper.Map<List<ClienteModel>, List<ClienteModelView>>(oServicioCliente.GetClientePorTipoCliente(idTipoCliente));
            }

            model.CVisible = true;
            CargarTipoCliente ();

            return View(model);

        }

        public ActionResult AddOrEdit(int id = 0)
        {

          
            CargarTipoCliente();
            CargarTipoIva();
            CargarPieNota();
            CargarIdioma();
            CargarGrupoPresupuesto();
            CargarTipoMoneda();

            ClienteModelView model;

            if (id == 0)
            {
                model = new ClienteModelView();
             
            }
            else
            {
                model = Mapper.Map<ClienteModel, ClienteModelView>(oServicioCliente.GetClientePorId(id));

            }           
            return View(model);
        }

        [HttpPost]       
        public ActionResult AddOrEdit(ClienteModelView model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];
                    model.IdUsuario = OUsuario.IdUsuario;


                    System.Web.HttpContext.Current.Session["idCliente"] = model.Id;

                    //serviciocajagrupo._mensaje("","ok");
                    if (model.Id <= 0)
                    {
                        oServicioCliente.GuardarCliente(Mapper.Map<ClienteModelView, ClienteModel>(model));
                    }
                    else
                    {
                        oServicioCliente.ActualizarCliente(Mapper.Map<ClienteModelView, ClienteModel>(model));
                    }

                  //  return RedirectToAction(nameof(Index));

                    return RedirectToAction("AddOrEdit", new { Id = model.Id });
                   // return RedirectToAction(nameof(AddOrEdit

                }


              

                return View(model);

            }

            catch (Exception)
            {
                return View(model);
            }
        }



        #region "Servicios Reportes"

        // 1 Cuenta Corriente Detalle


        public ActionResult CtaCteDetalle(int IdCliente = 0, string searchFecha= null)
        {
            CtaCteClienteModelView model = new CtaCteClienteModelView();
            DateTime fechaHasta = DateTime.Now;
            
            if (IdCliente != 0)
            {
                if (!string.IsNullOrEmpty(searchFecha))
                {
                    fechaHasta = DateTime.ParseExact(searchFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);                  
                }

                model.CtaCte = Mapper.Map<List<CobroFacturaModel>, List<CobroFacturaModelView>>(oServicioCliente.GetCtaCteDetalle(IdCliente, fechaHasta));
                model.cliente = Mapper.Map<ClienteModel, ClienteModelView>(oServicioCliente.GetClientePorId(IdCliente));

            }

            return View(model);

        }



        // 2 Cuenta Corriente Resumen
        public ActionResult CtaCteResumen(string searchFecha)
        {

            




                List<CteCteClienteResumenModelView> model = new List<CteCteClienteResumenModelView> ();
try
            {
            DateTime fechaHasta = DateTime.Now;

            if (!string.IsNullOrEmpty(searchFecha))

            {
                fechaHasta = DateTime.ParseExact(searchFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                model = Mapper.Map<List<CteCteClienteResumenModel>, List<CteCteClienteResumenModelView>>(oServicioCliente.GetCtaCteResumen(fechaHasta));

           }

            }

            catch (Exception ex)
            {
                oServicioCliente._mensaje(ex.Message, "error");
            }


            return View(model);
        }


        //3  Registro de Ventas Mensuales

        public ActionResult ConsultaIvaVentas(int anio=0, int mes=0)
        {


            ConsultaIvaVentaModelView model = new ConsultaIvaVentaModelView();
            model.ListaConsultaIva = null;

            int Anio = DateTime.Now.Year;
            int Mes = DateTime.Now.Month;

            if (anio != 0)

            {

                if (mes != 0)
                {
                    Anio = anio;
                    Mes = mes;
               }

                string Periodo = Convert.ToString(Anio) + Convert.ToString(Mes);

                model.ListaConsultaIva = Mapper.Map<List<ConsultaIvaVentaModel>, List<ConsultaIvaVentaModelView>>(oServicioCliente.GetIvaVentas(Periodo));

               // model.ListaConsultaIva= Mapper.Map<List<ConsultaIvaVentaModel>, List<ConsultaIvaVentaModel>>(oServicioCliente.GetIvaVentas(Periodo, Mes));

            }


            CargarAnio();
            CargarMes();

            return View(model);
        }

        private void CargarAnio()
        {
            List<Anios> ListaAnio = new List<Anios>()
            {
                new Anios(){ Id = "0", Descripcion = "Selecionar" },
                new Anios(){ Id = "19", Descripcion = "2019" },
                new Anios(){ Id = "20", Descripcion = "2020" },
                new Anios(){ Id = "21", Descripcion = "2021" },
                new Anios(){ Id = "22", Descripcion = "2022" },
                new Anios(){ Id = "23", Descripcion = "2023" },
                new Anios(){ Id = "24", Descripcion = "2024" },
                new Anios(){ Id = "25", Descripcion = "2025" }

            };

            StringBuilder sb = new StringBuilder();
            foreach (var type in ListaAnio)
            {
                sb.Append("<option value='" + type.Id + "'>" + type.Descripcion + "</option>");
            }
            ViewBag.ListaAnio = sb.ToString();
        }




        private void CargarMes()
        {
            List<Meses> ListaMes = new List<Meses>()
            {
                new Meses(){ Id = "0", Descripcion = "Selecionar" },
                new Meses(){ Id = "01", Descripcion = "Enero" },
                new Meses(){ Id = "02", Descripcion = "Febrero" },
                new Meses(){ Id = "03", Descripcion = "Marzo" },
                new Meses(){ Id = "04", Descripcion = "Abril" },
                new Meses(){ Id = "05", Descripcion = "Mayo" },
                new Meses(){ Id = "06", Descripcion = "Junio" },
                new Meses(){ Id = "07", Descripcion = "Julio" },
                new Meses(){ Id = "08", Descripcion = "Agosto" },
                new Meses(){ Id = "09", Descripcion = "Septiembre" },
                new Meses(){ Id = "10", Descripcion = "Octubre" },
                new Meses(){ Id = "11", Descripcion = "Noviembre" },
                new Meses(){ Id = "12", Descripcion = "Diciembre" }};
            StringBuilder sb = new StringBuilder();
            foreach (var type in ListaMes)
            {
                sb.Append("<option value='" + type.Id + "'>" + type.Descripcion + "</option>");
            }
            ViewBag.ListaMes = sb.ToString();
        }

        #endregion



        #region "Direccion de los Clientes"

        public ActionResult AddOrEditDireccion( int Id = 0)
        {

            // Combo Pais
            // Combo Provincia
            // Combo Codigo Postal
            // Combo PieNota
            // Combo Tipo Idioma
            

            CargarPieNota();
            CargarIdioma();
            CargarTipoMoneda();
            CargarPais();
           
           ClienteDireccionModelView model;

            

            if (Id == 0)
            {
                model = new ClienteDireccionModelView();
                model.IdCliente = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCliente"]); 
                
            }
            else
            {
                model = Mapper.Map<ClienteDireccionModel, ClienteDireccionModelView>(oServicioClienteDireccion.ObtenerPorID(Id));

            }



            CargarProvincia(model.IdPais ?? 0);
            CargarLocalidad(model.IdProvincia ?? 0);

          

            return View(model);
        }


             


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddOrEditDireccion(ClienteDireccionModelView model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var OUsuario = (UsuarioModel)System.Web.HttpContext.Current.Session["currentUser"];

                    model.IdUsuario = OUsuario.IdUsuario;
                    //serviciocajagrupo._mensaje("","ok");
                    if (model.Id <= 0)
                    {
                        oServicioClienteDireccion.GuardarDireccion(Mapper.Map<ClienteDireccionModelView, ClienteDireccionModel>(model));
                    }
                    else
                    {
                        oServicioClienteDireccion.ActualizarDireccion(Mapper.Map<ClienteDireccionModelView, ClienteDireccionModel>(model));
                    }

                    // return RedirectToAction(nameof(Index));
                    return RedirectToAction("DireccionCliente", new { IdCliente = model.IdCliente });
                }



                return RedirectToAction("DireccionCliente", new { IdCliente = model.IdCliente });

              




                return RedirectToAction("DireccionCliente", new { IdCliente = model.IdCliente });

                // return View(model);
              //  return RedirectToAction("DireccionCliente", new { I = model.Id });

            }

            catch (Exception)
            {
                return View(model);
            }
        }

        #endregion

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                oServicioCliente.Eliminar(id);

            }
            catch (Exception ex)
            {
                oServicioCliente._mensaje(ex.Message, "error");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BloquearCliente(int id)
        {
            try
            {


                oServicioCliente.BloquearCliente(id);

                //return Json("'Success':'true'");

            }
            catch (Exception ex)
            {
                //oServicioCliente._mensaje(ex.Message, "error");

               // return Json(String.Format("'Success':'false','Error':'Ha habido un error al insertar el registro.'" + ex.Message));
            }



            return RedirectToAction("Index");


        }


        [HttpPost]
        public ActionResult HabilitarCliente(int id)
        {
            try
            {


                oServicioCliente.HabilitarCliente(id);
              

            }
            catch (Exception ex)
            {
                oServicioCliente._mensaje(ex.Message, "error");

               
            }


            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public ActionResult EliminarDireccion(int Id, int IdCliente=0)
        {
            try
            {
                oServicioClienteDireccion.Eliminar(Id);

            }
            catch (Exception ex)
            {
                oServicioCliente._mensaje(ex.Message, "error");
            }

            //return RedirectToAction("DireccionCliente??IdCliente=" + id);

            return RedirectToAction("DireccionCliente", new { IdCliente = IdCliente });
        }


        public ActionResult DireccionCliente(int IdCliente)
        {

            ClienteDireccionModelView model = new ClienteDireccionModelView();

            model.ListaDireccion = Mapper.Map<List<ClienteDireccionModel>, List<ClienteDireccionModelView>>(oServicioClienteDireccion.GetDireccionPorcliente(IdCliente));

            model.IdCliente = IdCliente;
            return View(model);

        }

      

        #region "Cargar Combos"



        //combo CargarTipoCliente
        public void CargarTipoCliente()
        {
            List<TipoClienteModelView> ListaTipoCliente = Mapper.Map<List<TipoClienteModel>, List<TipoClienteModelView>>(OservicioTipoCliente.GetAllTipoCliente());
            List<SelectListItem> retornoListaTipoCliente = null;
            retornoListaTipoCliente = (ListaTipoCliente.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descripcion
            })).ToList();
            retornoListaTipoCliente.Insert(0, new SelectListItem { Text = "Consultar Por Nombre", Value = "" });
            ViewBag.ListaTipoCliente = retornoListaTipoCliente;
        }


        //combo Tipo Iva
        public void CargarTipoIva()
        {
            ServicioTipoIva ServicioIva = new ServicioTipoIva();
            List<TipoIvaViewModel> ListaTipoIva = Mapper.Map<List<TipoIvaModel>, List<TipoIvaViewModel>>(ServicioIva.GetAllTipoIva());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaTipoIva = null;
            retornoListaTipoIva = (ListaTipoIva.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaTipoIva.Insert(0, new SelectListItem { Text = "--Seleccione el Tipo de Iva--", Value = "" });

            ViewBag.ListaTipoIva = retornoListaTipoIva;
        }


        //combo PieNota
        public void CargarPieNota()
        {
            ServicioPieNota Servicio = new ServicioPieNota();
            List<PieNotaModelView> ListaTipoIva = Mapper.Map<List<PieNotaModel>, List<PieNotaModelView>>(Servicio.GetAllPieNota());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaPieNota = null;
            retornoListaPieNota = (ListaTipoIva.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            //retornoListaPieNota.Insert(0, new SelectListItem { Text = "--Seleccione el Pie de Nota--", Value = "" });

            ViewBag.ListaPieNota = retornoListaPieNota;
        }



        //combo Idioma
        public void CargarIdioma()
        {


            ServicioTipoIdioma Servicio = new ServicioTipoIdioma();
            List<TipoIdiomaModelView> Lista = Mapper.Map<List<TipoIdiomaModel>, List<TipoIdiomaModelView>>(Servicio.GetAllTipoIdioma());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoLista = null;
            retornoLista = (Lista.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoLista.Insert(0, new SelectListItem { Text = "--Seleccione el Idioma--", Value = "" });

           



            ViewBag.ListaTipoIdioma = retornoLista;
        }



        //combo Tipo Iva
        public void CargarTipoMoneda()
        {
            ServicioTipoMoneda Servicio = new ServicioTipoMoneda();
            List<TipoMonedaModelView> ListaTipoIva = Mapper.Map<List<TipoMonedaModel>, List<TipoMonedaModelView>>(Servicio.GetAllTipoMonedas());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaTipoMoneda = null;
            retornoListaTipoMoneda = (ListaTipoIva.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoListaTipoMoneda.Insert(0, new SelectListItem { Text = "--Seleccione la Moneda--", Value = "" });

            ViewBag.ListaTipoMoneda = retornoListaTipoMoneda;
        }



        //combo Tipo Iva
        public void CargarGrupoPresupuesto()
        {
            ServicioGrupoPresupuesto Servicio = new ServicioGrupoPresupuesto();
            List<GrupoPresupuestoModelView> Lista = Mapper.Map<List<GrupoPresupuestoModel>, List<GrupoPresupuestoModelView>>(Servicio.GetAllGrupoPresupuesto());

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoLista = null;
            retornoLista = (Lista.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Descripcion
                                  })).ToList();
            retornoLista.Insert(0, new SelectListItem { Text = "--Seleccione el Grupo de Presupuesto--", Value = "" });

            ViewBag.ListaGrupoPresupuesto = retornoLista;
        }


        //combo Pais
        public void CargarPais()
        {
            ServicioPais servicioPais = new ServicioPais();
            List<PaisModelView> ListaPais = Mapper.Map<List<PaisModel>, List<PaisModelView>>(servicioPais.GetAllPais());
            ListaPais = ListaPais.OrderBy(p => p.Nombre).ToList();

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaPais = null;
            retornoListaPais = (ListaPais.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaPais.Insert(0, new SelectListItem { Text = "-- Seleccione País --", Value = "" });
            ViewBag.ListaPais = retornoListaPais;
        }

        public void CargarProvincia(int oPais)
        {
            ServicioProvincia servicioProvincia = new ServicioProvincia();
            List<ProvinciaModelView> ListaProvincia = Mapper.Map<List<ProvinciaModel>, List<ProvinciaModelView>>(servicioProvincia.GetAllProvincias(oPais));
            ListaProvincia = ListaProvincia.OrderBy(p => p.Nombre).ToList();

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaProvincia = null;
            retornoListaProvincia = (ListaProvincia.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaProvincia.Insert(0, new SelectListItem { Text = "-- Seleccione Provincia --", Value = "" });
            ViewBag.ListaProvincia = retornoListaProvincia;
        }

        public void CargarLocalidad(int oProvincia)
        {
            ServicioLocalidad servicioLocalidad = new ServicioLocalidad();
            List<LocalidadModelView> ListaLocalidad = Mapper.Map<List<LocalidadModel>, List<LocalidadModelView>>(servicioLocalidad.GetAllLocalidads(oProvincia));
            ListaLocalidad = ListaLocalidad.OrderBy(p => p.Nombre).ToList();

            //esto es para pasarlo a select list (drop down list)
            List<SelectListItem> retornoListaLocalidad = null;
            retornoListaLocalidad = (ListaLocalidad.Select(x =>
                                  new SelectListItem()
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Nombre
                                  })).ToList();
            retornoListaLocalidad.Insert(0, new SelectListItem { Text = "-- Seleccione Localidad --", Value = "" });
            ViewBag.ListaLocalidad = retornoListaLocalidad;
        }


        #endregion


    }






}



