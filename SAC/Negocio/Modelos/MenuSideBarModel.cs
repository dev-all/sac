﻿using Datos.ModeloDeDatos;
using System.Collections.Generic;

namespace Negocio.Modelos
{
    public class MenuSideBarModel
    {
        public int IdMenuSidebar { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public string Titulo { get; set; }

        public int? IdParent { get; set; }
        public int? IdAccion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaModificacion { get; set; }
        public virtual Accion Accion { get; set; }

        public virtual ICollection<MenuSideBarModel> Group { get; set; }
    }
}