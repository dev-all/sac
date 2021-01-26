﻿using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
  public class ClienteModel
    {


        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdTipoiva { get; set; }
        public string Cuit { get; set; }
        public int DiasFactura { get; set; }
        public int IdImputacion { get; set; }
        public string Observaciones { get; set; }
        public string Email { get; set; }
        public int IdXml { get; set; }
        public int IdPieNota { get; set; }
        public int IdIdioma { get; set; }
        public int IdTipoCliente { get; set; }
        public bool Visible { get; set; }
        public int IdNotaPieB { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdGrupoPresupuesto { get; set; }
        public bool MiPyme { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime UltimaModificacion { get; set; }

        public virtual GrupoPresupuesto GrupoPresupuesto { get; set; }
      
        public virtual PieNotaModel PieNota { get; set; }
        public virtual TipoClienteModel TipoCliente { get; set; }
        public virtual TipoMonedaModel TipoMoneda { get; set; }
               public virtual ICollection<ClienteDireccionModel> ClienteDireccion { get; set; }



    }
}
