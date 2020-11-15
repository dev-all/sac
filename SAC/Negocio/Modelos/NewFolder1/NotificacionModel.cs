using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace Negocio.Modelos
{
 public class NotificacionModel
    {
        public int id { get; set; }
        public int idPersona { get; set; }
        public Nullable<bool> envioEmail { get; set; }
        public string obs { get; set; }
        public bool activo { get; set; }
        public System.DateTime fechaCreacion { get; set; }

    }
}
