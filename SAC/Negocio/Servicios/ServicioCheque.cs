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
   public class ServicioCheque : ServicioBase
    {
        private ChequeRepositorio pChequeRepositorio;
        public Action<string, string> _mensaje;

        public ServicioCheque()
        {
            pChequeRepositorio = kernel.Get<ChequeRepositorio>();
        }


        public List<ChequeModel> GetAllCheque()
        {
            List<ChequeModel> listaCheque = Mapper.Map<List<Cheque>, List<ChequeModel>>(pChequeRepositorio.GetAllCheque());
            return listaCheque;
        }

        public ChequeModel obtenerCheque(int idCheque)
        {
            return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.obtenerCheque(idCheque));
        }


        public ChequeModel Agregar(ChequeModel oChequeModel)
        {
            try
            {
                var oModel = Mapper.Map<ChequeModel, Cheque>(oChequeModel);
                return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.Agregar(oModel));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


        public ChequeModel Actualizar(ChequeModel oChequeModel)
        {
            try
            {
                var oModel = Mapper.Map<ChequeModel, Cheque>(oChequeModel);
                return Mapper.Map<Cheque, ChequeModel>(pChequeRepositorio.Actualizar(oModel));

            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

    }
}
