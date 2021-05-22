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
 public class ServicioBuque : ServicioBase
    {
        private BuqueRespositorio oBuqueRespositorio;
        public Action<string, string> _mensaje;

        public ServicioBuque()
        {
            oBuqueRespositorio = kernel.Get<BuqueRespositorio>();
        }

        public List<BuqueModel> GetAllBuque()
        {
            List<BuqueModel> listaBuque = Mapper.Map<List<Buque>, List<BuqueModel>>(oBuqueRespositorio.GetAllBuque());
            return listaBuque;
        }

        public BuqueModel Agregar(BuqueModel oBuqueModel)
        {
            try
            {
                var oModel = Mapper.Map<BuqueModel, Buque>(oBuqueModel);
                return Mapper.Map<Buque, BuqueModel>(oBuqueRespositorio.Agregar(oModel));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

    }
}
