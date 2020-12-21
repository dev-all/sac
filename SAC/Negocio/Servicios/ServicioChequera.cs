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
   public class ServicioChequera : ServicioBase
    {
        private ChequeraRepositorio pChequeraRepositorio;
        public Action<string, string> _mensaje;

        public ServicioChequera()
        {
            pChequeraRepositorio = kernel.Get<ChequeraRepositorio>();
        }


        public List<ChequeraModel> GetAllChequera()
        {
            List<ChequeraModel> listaChequera =Mapper.Map<List<Chequera>, List<ChequeraModel>>(pChequeraRepositorio.GetAllChequera());
            return listaChequera;
        }

        public ChequeraModel obtenerCheque(int idCheque)
        {
            return Mapper.Map<Chequera,ChequeraModel>(pChequeraRepositorio.obtenerCheque(idCheque));
        }


        public ChequeraModel Actualizar(ChequeraModel oChequeModel)
        {
            try
            {
                var oModel = Mapper.Map<ChequeraModel, Chequera>(oChequeModel);
                return Mapper.Map<Chequera, ChequeraModel>(pChequeraRepositorio.Actualizar(oModel));

            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }

    }
}

   