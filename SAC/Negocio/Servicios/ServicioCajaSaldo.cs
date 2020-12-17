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
   public class ServicioCajaSaldo : ServicioBase
    {
        private CajaSaldoRepositorio CajaSaldoRepositorio ;
      
        public ServicioCajaSaldo()
        {
            CajaSaldoRepositorio = kernel.Get<CajaSaldoRepositorio>();
            
        }



      





        #region "Metodos de Lectura de Datos"


        public List<CajaSaldoModel> GetAllCaja()
        {
            try
            {
                var Caja = Mapper.Map<List<CajaSaldo>, List<CajaSaldoModel>>(CajaSaldoRepositorio.GetAllCajaSaldo());
                return Caja;

            }
            catch (Exception e)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }



        public CajaSaldoModel GetCajaPorId(int id)
        {
            try
            {
                return Mapper.Map<CajaSaldo, CajaSaldoModel>(CajaSaldoRepositorio.GetCajaSaldoPorId(id));
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


                var retorno = CajaSaldoRepositorio.DeleteCaja(IdCaja);
                _mensaje("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }

        }



     

        public CajaSaldoModel GuardarCaja(CajaSaldoModel model)
        {

            try
            {
                
                model.Activo = true;               
                model.UltimaModificacion = DateTime.Now;
                var newModel = CajaSaldoRepositorio.Insertar(Mapper.Map< CajaSaldoModel,CajaSaldo>(model));
                _mensaje("Se registro correctamente", "ok");
                return Mapper.Map<CajaSaldo,CajaSaldoModel> (newModel);               
            }
            catch (Exception  ex)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "erro");
                throw new Exception();

            }


        }



        public CajaSaldoModel ActualizarCaja(CajaSaldoModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = CajaSaldoRepositorio.ActualizarCajaSaldo(Mapper.Map<CajaSaldoModel, CajaSaldo>(model));              
                _mensaje("Se actualizo correctamente", "ok");
                
                return Mapper.Map<CajaSaldo, CajaSaldoModel>(newModel);
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


