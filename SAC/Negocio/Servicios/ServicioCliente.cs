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
   public class ServicioCliente : ServicioBase
    {
        private ClienteRepositorio oClienteRepositorio;
       

        public ServicioCliente()
        {
            oClienteRepositorio = kernel.Get<ClienteRepositorio>();
        }


        public List<ClienteModel> GetAllCliente()
        {

            try
            {
                List<ClienteModel> listaCheque = Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetAllCliente());

           

            return listaCheque;
            }
            catch (Exception )
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }


        }



        public List<ClienteModel> GetClientePorNombre(string strCliente)
        {
            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorNombre(strCliente));
            }
            catch (Exception )
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
        }
     public List<ClienteModel> GetClientePorCodigo(string strCodigo)
        {
            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorCodigo(strCodigo));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
        }


        public ClienteModel GetClientePorId(int id)
        {
            try
            {
                return Mapper.Map<Cliente, ClienteModel>(oClienteRepositorio.GetClientePorId(id));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");

                return null;
            }
        }


        public List<ClienteModel> GetClientePorTipoCliente(int IdTipoCliente)
        {

            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorTipoCliente(IdTipoCliente));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }


        }

        public List<ClienteModel> GetClientePorIdNombre(int IdTipoCliente, string strCliente)
        {

            try
            {
                return Mapper.Map<List<Cliente>, List<ClienteModel>>(oClienteRepositorio.GetClientePorIdNombre(IdTipoCliente,  strCliente));
            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }


        }


        #region "Metodos de Actualizacion de Datos"







        public ClienteModel ActualizarCliente(ClienteModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = oClienteRepositorio.ActualizarCliente(Mapper.Map<ClienteModel, Cliente>(model));
                _mensaje?.Invoke("Se actualizo correctamente", "ok");

                return Mapper.Map<Cliente, ClienteModel>(newModel);
            }



            catch (System.Data.Entity.Validation.DbEntityValidationException ex)

            {
                string mensaje = "";

                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        //System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        mensaje += "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage;

                       
                    }
                }

               // _mensaje?.Invoke(mensaje);
                throw new Exception(mensaje);
            }



            //catch (Exception ex)
            //{
            //    _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "erro");
            //    throw new Exception();

            //}

        }



        public void Eliminar(int IdCliente)
        {
            try
            {


                var retorno = oClienteRepositorio.DeleteCliente(IdCliente);
                _mensaje?.Invoke("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }

        }


        public void BloquearCliente(int IdCliente)
        {
            try
            {


                var retorno = oClienteRepositorio.BloquearCliente(IdCliente);
                _mensaje?.Invoke("Se bloqueó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }
        }

        public void HabilitarCliente(int IdCliente)
        {
            try
            {


                var retorno = oClienteRepositorio.HabilitarCliente(IdCliente);
                _mensaje?.Invoke("Se Habilitó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }
        }




        public ClienteModel GuardarCliente(ClienteModel model)
        {

            try
            {

                model.Activo = true;
                model.UltimaModificacion = DateTime.Now;
                var newModel = oClienteRepositorio.Agregar(Mapper.Map<ClienteModel, Cliente>(model));
                _mensaje?.Invoke("Se registro correctamente", "ok");
                return Mapper.Map<Cliente, ClienteModel>(newModel);
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador" + ex.Message, "erro");
                throw new Exception();

            }


        }

     


        #endregion
    }
}
