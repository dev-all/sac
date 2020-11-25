using System.Linq;
using System.Web;
using System.Web.Mvc;
//agregadas
using AutoMapper;
using Negocio.Modelos;
using Negocio.Servicios;
using SAC.Atributos;
using SAC.Models;


namespace SAC.Controllers
{
    public class LocalidadController : Controller
    {
        // GET: Localidad
        public ActionResult Index()
        {
            return View();
        }
    }
}