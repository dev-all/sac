using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
  public class PresupuestoItemRepositorio : RepositorioBase<PresupuestoItem>
    {
        private SAC_Entities context;

        public PresupuestoItemRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public PresupuestoItem InsertarProveedor(PresupuestoItem presupuestoItem)
        {
            PresupuestoItem a = new PresupuestoItem();
            try
            {
                return Insertar(presupuestoItem);
            }
            catch (Exception ex)
            {
                return a;
            }

        }



    }
}
