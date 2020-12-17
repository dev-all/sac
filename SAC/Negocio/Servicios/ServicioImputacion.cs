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
   public class ServicioImputacion : ServicioBase
    {

        private ImputacionRepositorio ImputacionRepositorio;

        public ServicioImputacion()
        {
            ImputacionRepositorio = kernel.Get<ImputacionRepositorio>();
        }


        public List<ImputacionModel> GetAllImputacions()
        {
            try
            {
                var Imputacions = Mapper.Map<List<Imputacion>, List<ImputacionModel>>(ImputacionRepositorio.GetAllImputacion());
                return Imputacions;
            }
            catch (Exception ex)
            {
                _mensaje(ex.Message, "error");
                return null;
            }
        }


        public ImputacionModel GetImputacion(int _id)
        {
            Imputacion oImputacion = ImputacionRepositorio.ObtenerImputacionPorId(_id);
            ImputacionModel oImputacionModel = new ImputacionModel();

            oImputacionModel.Id = oImputacion.Id;
            oImputacionModel.Descripcion = oImputacion.Descripcion;
            oImputacionModel.IdSubRubro = oImputacion.IdSubRubro;
            oImputacionModel.SaldoInicial = oImputacion.SaldoInicial;
            oImputacionModel.SaldoFin = oImputacion.SaldoFin;
            oImputacionModel.IdTipo = oImputacion.IdTipo;
            oImputacionModel.Alias = oImputacion.Alias;
            oImputacionModel.Enero = oImputacion.Enero;
            oImputacionModel.Febrero = oImputacion.Febrero;
            oImputacionModel.Marzo = oImputacion.Marzo;
            oImputacionModel.Abril = oImputacion.Abril;
            oImputacionModel.Mayo = oImputacion.Mayo;
            oImputacionModel.Junio = oImputacion.Junio;
            oImputacionModel.Julio = oImputacion.Julio;
            oImputacionModel.Agosto = oImputacion.Agosto;
            oImputacionModel.Septiembre = oImputacion.Septiembre;
            oImputacionModel.Octubre = oImputacion.Octubre;
            oImputacionModel.Noviembre = oImputacion.Noviembre;
            oImputacionModel.Diciembre = oImputacion.Diciembre;
            oImputacionModel.Activo = oImputacion.Activo;
            oImputacionModel.IdUsuario = oImputacion.IdUsuario;
            oImputacionModel.UltimaModificacion = oImputacion.UltimaModificacion;

            return oImputacionModel;
        }

        public int ActualizarImputacion(ImputacionModel oImputacionModel)
        {
            //controlar que no exista 
            Imputacion oImputacion = ImputacionRepositorio.ObtenerImputacionPorDescripcion(oImputacionModel.Descripcion, oImputacionModel.IdSubRubro ?? 0, oImputacionModel.Id);
            if (oImputacion != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                Imputacion oImputacionNuevo = new Imputacion();
                Imputacion oPaisRespuesta = new Imputacion();

                oImputacionNuevo.Id = oImputacionModel.Id;
                oImputacionNuevo.Descripcion = oImputacionModel.Descripcion;
                oImputacionNuevo.IdSubRubro = oImputacionModel.IdSubRubro;
                oImputacionNuevo.SaldoInicial = oImputacionModel.SaldoInicial;
                oImputacionNuevo.SaldoFin = oImputacionModel.SaldoFin;
                oImputacionNuevo.IdTipo = oImputacionModel.IdTipo;
                oImputacionNuevo.Alias = oImputacionModel.Alias;
                oImputacionNuevo.Enero = oImputacionModel.Enero;
                oImputacionNuevo.Febrero = oImputacionModel.Febrero;
                oImputacionNuevo.Marzo = oImputacionModel.Marzo;
                oImputacionNuevo.Abril = oImputacionModel.Abril;
                oImputacionNuevo.Mayo = oImputacionModel.Mayo;
                oImputacionNuevo.Junio = oImputacionModel.Junio;
                oImputacionNuevo.Julio = oImputacionModel.Julio;
                oImputacionNuevo.Agosto = oImputacionModel.Agosto;
                oImputacionNuevo.Septiembre = oImputacionModel.Septiembre;
                oImputacionNuevo.Octubre = oImputacionModel.Octubre;
                oImputacionNuevo.Noviembre = oImputacionModel.Noviembre;
                oImputacionNuevo.Diciembre = oImputacionModel.Diciembre;
                oImputacionNuevo.Activo = oImputacionModel.Activo;
                oImputacionNuevo.IdUsuario = oImputacionModel.IdUsuario;
                oImputacionNuevo.UltimaModificacion = oImputacionModel.UltimaModificacion;

                oPaisRespuesta = ImputacionRepositorio.ActualizarImputacion(oImputacionNuevo);

                if (oPaisRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int GuardarImputacion(ImputacionModel oImputacionModel)
        {
            //controlar que no exista 
            Imputacion oImputacion = ImputacionRepositorio.ObtenerImputacionPorDescripcion(oImputacionModel.Descripcion, oImputacionModel.Id);
            if (oImputacion != null)
            {
                return -2;
            }
            else
            {
                Imputacion oImputacionNuevo = new Imputacion();
                Imputacion oImputacionRespuesta = new Imputacion();

                oImputacionNuevo.Id = oImputacionModel.Id;
                oImputacionNuevo.Descripcion = oImputacionModel.Descripcion;
                oImputacionNuevo.IdSubRubro = oImputacionModel.IdSubRubro;
                oImputacionNuevo.SaldoInicial = oImputacionModel.SaldoInicial;
                oImputacionNuevo.SaldoFin = oImputacionModel.SaldoFin;
                oImputacionNuevo.IdTipo = oImputacionModel.IdTipo;
                oImputacionNuevo.Alias = oImputacionModel.Alias;
                oImputacionNuevo.Enero = oImputacionModel.Enero;
                oImputacionNuevo.Febrero = oImputacionModel.Febrero;
                oImputacionNuevo.Marzo = oImputacionModel.Marzo;
                oImputacionNuevo.Abril = oImputacionModel.Abril;
                oImputacionNuevo.Mayo = oImputacionModel.Mayo;
                oImputacionNuevo.Junio = oImputacionModel.Junio;
                oImputacionNuevo.Julio = oImputacionModel.Julio;
                oImputacionNuevo.Agosto = oImputacionModel.Agosto;
                oImputacionNuevo.Septiembre = oImputacionModel.Septiembre;
                oImputacionNuevo.Octubre = oImputacionModel.Octubre;
                oImputacionNuevo.Noviembre = oImputacionModel.Noviembre;
                oImputacionNuevo.Diciembre = oImputacionModel.Diciembre;
                oImputacionNuevo.Activo = oImputacionModel.Activo;
                oImputacionNuevo.IdUsuario = oImputacionModel.IdUsuario;
                oImputacionNuevo.UltimaModificacion = oImputacionModel.UltimaModificacion;

                oImputacionRespuesta = ImputacionRepositorio.InsertarImputacion(oImputacionNuevo);
                if (oImputacionRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Eliminar(int idImputacion)
        {
            var retorno = ImputacionRepositorio.EliminarImputacion(idImputacion);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }




    }
}
