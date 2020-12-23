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
   public class ServicioCajaGrupo : ServicioBase
    {
        private CajaGrupoRepositorio cajaGrupoRepositorio;
      
        public ServicioCajaGrupo()
        {
            cajaGrupoRepositorio = kernel.Get<CajaGrupoRepositorio>();
            
        }



        #region "Metodos de Lectura de Datos"


        public List<CajaGrupoModel> GetAllCajaGrupo()
        {
            try
            {
                var CajaGrupo = Mapper.Map<List<GrupoCaja>, List<CajaGrupoModel>>(cajaGrupoRepositorio.GetAllGrupoCaja());
                return CajaGrupo;

            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }



        public CajaGrupoModel GetGrupoCajaPorId(int id)
        {
            try
            {
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(cajaGrupoRepositorio.GetGrupoCajaPorId(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }


        public CajaGrupoModel GetGrupoCajaPorCodigo(string codigo )
        {
            try
            {
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(cajaGrupoRepositorio.GetGrupoCajaPorCodigo(codigo));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }

        #endregion

        #region "Metodos de Actualizacion de Datos"

        public void Eliminar(int IdGrupoCaja)
        {
            try
            {


                var retorno = cajaGrupoRepositorio.DeleteGrupoCaja(IdGrupoCaja);
                _mensaje("Se eliminó correctamente", "ok");

            }
            catch (Exception)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }

        }



     

        public CajaGrupoModel GuardarGrupoCaja(CajaGrupoModel model)
        {

            try
            {
                
                model.Activo = true;               
                model.UltimaModificacion = DateTime.Now;
                var newModel = cajaGrupoRepositorio.Insertar(Mapper.Map< CajaGrupoModel,GrupoCaja>(model));
                _mensaje("Se registro correctamente", "ok");
                return Mapper.Map<GrupoCaja,CajaGrupoModel> (newModel);               
            }
            catch (Exception )
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }


        }



        public CajaGrupoModel ActualizarGrupoCaja(CajaGrupoModel model)
        {

            try
            {

                model.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                var newModel = cajaGrupoRepositorio.ActualizarGrupoCaja(Mapper.Map<CajaGrupoModel, GrupoCaja>(model));              
                _mensaje("Se actualizo correctamente", "ok");
                
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(newModel);
            }
            catch (Exception)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();

            }

        }

        public void CerrarGrupoCaja()
        {

            try
            {


                var retorno = cajaGrupoRepositorio.CerrarGrupoCaja();
            _mensaje("Se Cerro correctamente el Grupo Caja", "ok");


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


