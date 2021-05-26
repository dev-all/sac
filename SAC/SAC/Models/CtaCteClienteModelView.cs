using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Datos.ModeloDeDatos;
using System.Web.Mvc;

namespace SAC.Models
{
    public class CtaCteClienteModelView
    {

        public List<CobroFacturaModelView> CtaCte { get; set; }

        public ClienteModelView cliente { get; set; }

     


    }

  
}