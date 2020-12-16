using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
   public class PresupuestoActualRepositorio : RepositorioBase<PrespuestoActual>
    {

        private SAC_Entities context;

        public PresupuestoActualRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public List<PrespuestoActual> GetAllPresupuestos()
        {
            return context.PrespuestoActual.Where(p => p.Activo == true).ToList();
        }



    }
}
