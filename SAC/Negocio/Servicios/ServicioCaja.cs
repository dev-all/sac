using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using Negocio.Modelos;
using AutoMapper;
using System.Net.Mail;
using System.IO;
using System.Net;
using Negocio.Servicios;
using System.Net.Mime;
using System.Text;

namespace Negocio.Servicios
{
   public class ServicioCaja : ServicioBase
    {
        private CajaRepositorio CajaRepositorio ;
      
        public ServicioCaja()
        {
            CajaRepositorio = kernel.Get<CajaRepositorio>();
            
        }

        #region "Metodos de Lectura de Datos"


        public List<CajaModel> GetAllCaja()
        {
            try
            {
                var Caja = Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.GetAllCaja());
                return Caja;

            }
            catch (Exception e)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

   

        public CajaModel GetCajaPorId(int id)
        {
            try
            {
                return Mapper.Map<Caja, CajaModel>(CajaRepositorio.GetCajaPorId(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }


        public CajaModel GetCajaPorCodigo(int idcodigocaja )
        {
            try
            {
                return Mapper.Map<Caja, CajaModel>(CajaRepositorio.GetCajaPorGrupo(idcodigocaja));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }

        #endregion

        #region "Metodos de Actualizacion de Datos"

        public void Eliminar(int IdCaja)
        {
            try
            {
                var retorno = CajaRepositorio.DeleteCaja(IdCaja);
                _mensaje("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }
        }     

        public CajaModel GuardarCaja(CajaModel model)
        {
            try
            {                
                model.Activo = true;               
                model.UltimaModificacion = DateTime.Now;
                var newModel = CajaRepositorio.Insertar(Mapper.Map< CajaModel,Caja>(model));
                _mensaje("Se registro correctamente", "ok");
                return Mapper.Map<Caja,CajaModel> (newModel);               
            }
            catch (Exception  ex)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "erro");
                throw new Exception();
            }
        }

        public CajaModel ActualizarCaja(CajaModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = CajaRepositorio.ActualizarCaja(Mapper.Map<CajaModel, Caja>(model));              
                _mensaje("Se actualizo correctamente", "ok");
                
                return Mapper.Map<Caja, CajaModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }

        }
   

        #endregion
 }

}


