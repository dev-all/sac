using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        private ServicioConfiguracion servicioConfiguracion = new ServicioConfiguracion();
        static void Main(string[] args)
        {
            var menu = servicioConfiguracion.GetMenuSidebar();
        }
    }
}
