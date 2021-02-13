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
        private TarjetaOperacionRepositorio otarjetaoperacionrepositorio ;


        public ServicioCaja()
        {
            CajaRepositorio = kernel.Get<CajaRepositorio>();
            otarjetaoperacionrepositorio = kernel.Get<TarjetaOperacionRepositorio>();
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
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

      
        public List<CajaModel> GetAllCajaPorIdCierre(int v)
        {
            try
            {
                return Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.GetAllCajaPorIdCierre(v));               
            }
            catch (Exception e)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
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
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
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
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
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
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
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

                // Si viene con valor la tarjeta
                if (model.IdTarjeta > 0 && model.ImporteTarjeta > 0)
                {
                    InsertarOperacionTarjeta(model);
                }
                // Si viene con valor el cheque

                if (model.IdCheque > 0 && model.ImporteCheque > 0)
                {
                    //InsertarOperacionTarjeta(model);
                }



                return Mapper.Map<Caja,CajaModel> (newModel);               
            }
            catch (Exception )
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }


        }



        public CajaModel ActualizarCaja(CajaModel model)
        {

            try
            {

                if (model.IdTarjeta > 0)
                {

                    InsertarOperacionTarjeta(model);
                }

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = CajaRepositorio.ActualizarCaja(Mapper.Map<CajaModel, Caja>(model));              
                _mensaje?.Invoke("Se actualizo correctamente", "ok");
                
                return Mapper.Map<Caja, CajaModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }

        }



      




        public TarjetaOperacionModel InsertarOperacionTarjeta(CajaModel Ocaja)



        {
            TarjetaOperacionModel oTarjetaOperacionModel = new TarjetaOperacionModel ();
            oTarjetaOperacionModel.IdTarjeta = Convert.ToInt32( Ocaja.IdTarjeta);
            oTarjetaOperacionModel.IdGrupoCaja = Convert.ToInt32(Ocaja.IdGrupoCaja);
            oTarjetaOperacionModel.Descripcion = Convert.ToString(Ocaja.Concepto);
            oTarjetaOperacionModel.Importe = Convert.ToDecimal(Ocaja.ImporteTarjeta);
            oTarjetaOperacionModel.UltimaModificacion = DateTime.Now;
            oTarjetaOperacionModel.Activo = true;

            oTarjetaOperacionModel.IdUsuario = Convert.ToInt32(Ocaja.IdUsuario);        
          

            var oModel = Mapper.Map<TarjetaOperacionModel, TarjetaOperacion>(oTarjetaOperacionModel);
            //_mensaje("El cheque se ingresó correctamente", "ok");
            return Mapper.Map<TarjetaOperacion, TarjetaOperacionModel>(otarjetaoperacionrepositorio.Agregar(oModel));




        }




        public List<CajaModel> getGrupoCajaFecha(int idgrupocaja, DateTime fechadesde, DateTime fechahasta)
        {

            var Caja = Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.getGrupoCajaFecha(idgrupocaja,fechadesde,fechahasta));
            return Caja;

        }

        public List<CajaModel> GetSaldoInicialCaja(int cIdGrupoCaja, DateTime cfechadesde)

        {

            var Caja = Mapper.Map<List<Caja>, List<CajaModel>>(CajaRepositorio.GetSaldoInicialCaja(cIdGrupoCaja, cfechadesde));
            return Caja;
        }

        public CajaModel GetCajaPorFecha(int idgrupocaja ,DateTime fecha)
        {
            try
            {
                return Mapper.Map<Caja, CajaModel>(CajaRepositorio.GetCajaPorFecha(idgrupocaja, fecha));
            }
            catch (Exception e)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor" + e.Message, "error");
                return null;
            }
        }

        public void ActualizarCierreCaja(CajaModel model)
        {
            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                 CajaRepositorio.ActualizarCierreCaja(Mapper.Map<CajaModel, Caja>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error - ActualizarCierreCaja. contacte al administrador", "erro");
                throw new Exception();

            }
        }



        #endregion
    }

}


