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





        public void ActualizarAsientoImputacion(ImputacionModel model)
        {
            try
            {                                
               
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
                throw new Exception();
            }
               
        }
        public void AsintoContableGeneral(Diario model)
        {
            try
            {
                /// BUSCAR LA CTA            
                ImputacionModel cta = GetImputacion(model.IdImputacion);
                /// UPDATE
                /// SALDO FIN 
                /// SALDO MES 
                switch (model.Fecha.Month)
                {
                    case 1:
                        cta.Enero = cta.Enero ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 2:
                        cta.Febrero = cta.Febrero ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 3:
                        cta.Marzo = cta.Marzo ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 4:
                        cta.Abril = cta.Abril ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 5:
                        cta.Mayo = cta.Mayo ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 6:
                        cta.Junio = cta.Junio ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 7:
                        cta.Julio = cta.Julio ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 8:
                        cta.Agosto = cta.Agosto ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 9:
                        cta.Septiembre = cta.Septiembre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 10:
                        cta.Octubre = cta.Octubre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 11:
                        cta.Noviembre = cta.Noviembre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    case 12:
                        cta.Diciembre = cta.Diciembre ?? 0 + model.Importe;
                        cta.SaldoFin += model.Importe;
                        break;
                    default:
                        throw new InvalidOperationException("unknown item type");
                }

                cta.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString());
                cta.Activo = true;
                ImputacionRepositorio.ActualizarAsientoImputacion(Mapper.Map<ImputacionModel, Imputacion>(cta));
                _mensaje("Se registro eñ asineto contable correctamente", "ok");



            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                throw new Exception();
            }
        }



        public ImputacionModel GetImputacionPorAlias(string alias)
        {
            try
            {
                return Mapper.Map<Imputacion, ImputacionModel>(ImputacionRepositorio.GetImputacionPorAlias(alias));
            }
            catch (Exception)
            {
                 _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
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
