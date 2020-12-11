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
        private CajaGrupoRepositorio oCajaGrupoRepositorio;
      
        public ServicioCajaGrupo()
        {
            oCajaGrupoRepositorio = kernel.Get<CajaGrupoRepositorio>();
            
        }



        #region "Metodos de Lectura de Datos"


        public List<CajaGrupoModel> GetAllCajaGrupo()
        {
            try
            {
                var CajaGrupo = Mapper.Map<List<GrupoCaja>, List<CajaGrupoModel>>(oCajaGrupoRepositorio.GetAllGrupoCaja());
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
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(oCajaGrupoRepositorio.GetGrupoCajaPorId(id));
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
                return Mapper.Map<GrupoCaja, CajaGrupoModel>(oCajaGrupoRepositorio.GetGrupoCajaPorCodigo(codigo));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Intente mas tarde por favor", "error");
                return null;
            }
        }

        #endregion

        #region "Metodos de Actualizacion de Datos"

        public int Eliminar(int IdGrupoCaja)
        {
            var retorno = oCajaGrupoRepositorio.DeleteGrupoCaja(IdGrupoCaja);

            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }



     

        public int GuardarGrupoCaja(CajaGrupoModel oGrupoCajaModel)
        {
            //controlar que no exista 
            GrupoCaja oCodigo = oCajaGrupoRepositorio.GetGrupoCajaPorCodigo(oGrupoCajaModel.Codigo);

           

            if (oCodigo != null)
            {
                return -2;
            }
            else
            {
                GrupoCaja oGrupoCajaNuevo = new GrupoCaja();
                GrupoCaja oGrupoCajaRespuesta = new GrupoCaja();

                oGrupoCajaNuevo.Codigo = oGrupoCajaModel.Codigo;
                oGrupoCajaNuevo.Concepto = oGrupoCajaModel.Concepto;

                oGrupoCajaNuevo.Activo = true;
                oGrupoCajaNuevo.IdUsuario = oGrupoCajaModel.IdUsuario;
                oGrupoCajaNuevo.UltimaModificacion = oGrupoCajaModel.UltimaModificacion;

                oGrupoCajaRespuesta = oCajaGrupoRepositorio.Insertar(oGrupoCajaNuevo);

                if (oGrupoCajaRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }



        public int ActualizarGrupoCaja(CajaGrupoModel oGrupoCajaModel)
        {
            //controlar que no exista 




                GrupoCaja oCodigo = oCajaGrupoRepositorio.GetGrupoCajaPorId(oGrupoCajaModel.Id);



            if (oCodigo == null)  //significa que no existe
            {
                return -2;
            }
            else //significa que  existe el dato a ingresar
            {
                GrupoCaja oGrupoCajaNuevo = new GrupoCaja();
                GrupoCaja oGrupoCajaRespuesta = new GrupoCaja();
                oGrupoCajaNuevo.Id = oGrupoCajaModel.Id;
                oGrupoCajaNuevo.Codigo = oGrupoCajaModel.Codigo ;
                oGrupoCajaNuevo.Concepto = oGrupoCajaModel.Concepto;
                oGrupoCajaNuevo.IdImputacion = oGrupoCajaModel.IdImputacion;


                oGrupoCajaNuevo.Activo = true;
                oGrupoCajaNuevo.IdUsuario = oGrupoCajaModel.IdUsuario;
                oGrupoCajaNuevo.UltimaModificacion = oGrupoCajaModel.UltimaModificacion;

                oGrupoCajaRespuesta = oCajaGrupoRepositorio.ActualizarGrupoCaja(oGrupoCajaNuevo);

                if (oGrupoCajaRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }



        #endregion



    }
}
