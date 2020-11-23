using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entidad.Modelos;
using Negocio.Modelos;
namespace SAC.Models
{
    public class UsuarioModelView
    {
        public int idUsuario { get; set; }

        [Display(Name = "Usuario: ")]
        [Required(ErrorMessage = "Ops!, complete el campo Usuario.")]      
        public string userName { get; set; }
        [Required(ErrorMessage = "Ops!, complete el campo clave.")]
        public string password { get; set; }
        public bool activo { get; set; }
        public Nullable<System.DateTime> Creado { get; set; }
        public Nullable<System.DateTime> Actualizado { get; set; }
        public Nullable<int> idPersona { get; set; }
        public Nullable<int> idRol { get; set; }
        public Nullable<int> Usuario1 { get; set; }      
        public PersonaModel Persona { get; set; }
        public RolModel Rol { get; set; }

     
    }
}