using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class IngresoBancoModelView
    {


        public int Id {get;set;}


        public BancoCuentaBancariaModelView Ingresos { get; set; }

    }
}