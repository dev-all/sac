using SAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Controllers
{
    public class ComprasController : BaseController
    {
        // GET: Compras
        public ActionResult FacturaCompras()
        {
            CompraFacturaViewModel model = new CompraFacturaViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacturaCompras(CompraFacturaViewModel model)
        {
           // CompraFacturaViewModel model = new CompraFacturaViewModel();
            return View(model);
        }
    }
}