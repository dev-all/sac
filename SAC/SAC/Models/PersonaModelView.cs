using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.ModeloDeDatos;
namespace SAC.Models
{
    public class PersonaModelView
    {
        public int? id { get; set; }
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string apellidoNombre { get; set; }

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Ops!, complete el campo Usuario.")]
        public string email { get; set; }
        public string sexo { get; set; }
        public string cuil { get; set; }
        public string telefono { get; set; }
        public string telefonoFijo { get; set; }
        public string telefonoAlternativo { get; set; }
        public string codigoPostal { get; set; }
        public string domicilio { get; set; }
    }
}