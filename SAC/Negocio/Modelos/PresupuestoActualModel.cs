﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Modelos
{
   public class PresupuestoActualModel
    {

        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Concepto { get; set; }

        public decimal Actual { get; set; }

        public decimal Ejecutado { get; set; }

        public decimal Historico { get; set; }

        public int IdImputacion { get; set; }

        public int IdGrupoCaja { get; set; }

        public string CodigoCaja { get; set; }

        public string Visible { get; set; }

        public string Costos { get; set; }

        public int Periodo { get; set; }

        public Nullable<bool> Activo { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        public Nullable<System.DateTime> UltimaModificacion { get; set; }
       
    }
}
