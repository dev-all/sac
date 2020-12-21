using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;

namespace Negocio.Servicios
{
   public class ServicioPresupuestoActual : ServicioBase
    {

        private PresupuestoActualRepositorio oPresupuestoActualRepositorio;
        public Action<string, string> _mensaje;

        public ServicioPresupuestoActual()
        {
            oPresupuestoActualRepositorio = kernel.Get<PresupuestoActualRepositorio>();
        }

        public List<PresupuestoActualModel> GetAllPresupuestos()
        {
            return Mapper.Map<List<PrespuestoActual>, List<PresupuestoActualModel>>(oPresupuestoActualRepositorio.GetAllPresupuestos());
        }

        public PresupuestoActualModel GetAllPresupuestos(int idPresupuesto)
        {
            return Mapper.Map<PrespuestoActual, PresupuestoActualModel>(oPresupuestoActualRepositorio.GetAllPresupuestos(idPresupuesto));
        }


        public PresupuestoActualModel ActualizarPresupuesto(PresupuestoActualModel oPresupuestoActualModel)
        {
            PrespuestoActual oPresupuestoActual = Mapper.Map<PresupuestoActualModel, PrespuestoActual>(oPresupuestoActualModel);
            return Mapper.Map<PrespuestoActual, PresupuestoActualModel>(oPresupuestoActualRepositorio.ActualizarPresupuesto(oPresupuestoActual));
        }


    }
}
