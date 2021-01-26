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
   public class ServicioClienteDireccion : ServicioBase
    {
        private ClienteDireccionRepositorio oClienteDireccionRepositorio;

      
     

        public ServicioClienteDireccion()
        {
            oClienteDireccionRepositorio = kernel.Get<ClienteDireccionRepositorio>();
        }




        #region "Metodos de Actualizacion de Datos"







        public ClienteDireccionModel ActualizarDireccion(ClienteDireccionModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = oClienteDireccionRepositorio.ActualizarDireccion(Mapper.Map<ClienteDireccionModel, ClienteDireccion>(model));
                _mensaje("Se actualizo correctamente", "ok");

                return Mapper.Map<ClienteDireccion, ClienteDireccionModel>(newModel);
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "erro");
                throw new Exception();

            }

        }



        public void Eliminar(int IdDireccion)
        {
            try
            {


                var retorno = oClienteDireccionRepositorio.DeleteDireccion(IdDireccion);
                _mensaje("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }

        }



        public ClienteDireccionModel GuardarDireccion(ClienteDireccionModel model)
        {

            try
            {

                model.Activo = true;
                model.UltimaModificacion = DateTime.Now;
                var newModel = oClienteDireccionRepositorio.Agregar(Mapper.Map<ClienteDireccionModel, ClienteDireccion>(model));
                _mensaje("Se registro correctamente", "ok");
                return Mapper.Map<ClienteDireccion, ClienteDireccionModel>(newModel);
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "erro");
                throw new Exception();

            }


        }



        #endregion






        public List<ClienteDireccionModel> GetDireccionPorcliente(int Idcliente)
        {

            try
            {
                return Mapper.Map<List<ClienteDireccion>, List<ClienteDireccionModel>>(oClienteDireccionRepositorio.GetDireccionPorcliente(Idcliente));
            }
            catch (Exception e)
            {
                
               ServicioElog.Log(this, e);
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }


        }


        public ClienteDireccionModel ObtenerPorID(int id)
        {
            try
            {
                return Mapper.Map<ClienteDireccion, ClienteDireccionModel>(oClienteDireccionRepositorio.GetObtenerDireccion(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");

                
                
                return null;
            }

        }

      
    }
}
