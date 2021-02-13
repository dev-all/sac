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
using System.Linq;
using Negocio.Enumeradores;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
                 _mensaje?.Invoke(ex.Message, "error");
                return null;
            }
        }

       

        public ImputacionModel GetImputacion(int _id)
        {
            //modifico esto porque da error de bucle inf bre 06/02/2021
            //Imputacion oImputacion = 
            //ImputacionModel oImputacionModel = new ImputacionModel();
            return Mapper.Map<Imputacion, ImputacionModel>(ImputacionRepositorio.ObtenerImputacionPorId(_id));


            //oImputacionModel.Id = oImputacion.Id;
            //oImputacionModel.Descripcion = oImputacion.Descripcion;
            //oImputacionModel.IdSubRubro = oImputacion.IdSubRubro;
            //oImputacionModel.SaldoInicial = oImputacion.SaldoInicial;
            //oImputacionModel.SaldoFin = oImputacion.SaldoFin;
            //oImputacionModel.IdTipo = oImputacion.IdTipo;
            //oImputacionModel.Alias = oImputacion.Alias;
            //oImputacionModel.Enero = oImputacion.Enero;
            //oImputacionModel.Febrero = oImputacion.Febrero;
            //oImputacionModel.Marzo = oImputacion.Marzo;
            //oImputacionModel.Abril = oImputacion.Abril;
            //oImputacionModel.Mayo = oImputacion.Mayo;
            //oImputacionModel.Junio = oImputacion.Junio;
            //oImputacionModel.Julio = oImputacion.Julio;
            //oImputacionModel.Agosto = oImputacion.Agosto;
            //oImputacionModel.Septiembre = oImputacion.Septiembre;
            //oImputacionModel.Octubre = oImputacion.Octubre;
            //oImputacionModel.Noviembre = oImputacion.Noviembre;
            //oImputacionModel.Diciembre = oImputacion.Diciembre;
            //oImputacionModel.Activo = oImputacion.Activo;
            //oImputacionModel.IdUsuario = oImputacion.IdUsuario;
            //oImputacionModel.UltimaModificacion = oImputacion.UltimaModificacion;

            //return oImputacionModel;
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


                var imp =  Mapper.Map<ImputacionModel, Imputacion> (oImputacionModel);

                //Imputacion oImputacionNuevo = new Imputacion();
               // = new Imputacion();

                //oImputacionNuevo.Id = oImputacionModel.Id;
                //oImputacionNuevo.Descripcion = oImputacionModel.Descripcion;
                //oImputacionNuevo.IdSubRubro = oImputacionModel.IdSubRubro;
                //oImputacionNuevo.SaldoInicial = oImputacionModel.SaldoInicial;
                //oImputacionNuevo.SaldoFin = oImputacionModel.SaldoFin;
                //oImputacionNuevo.IdTipo = oImputacionModel.IdTipo;
                //oImputacionNuevo.Alias = oImputacionModel.Alias;
                //oImputacionNuevo.Enero = oImputacionModel.Enero;
                //oImputacionNuevo.Febrero = oImputacionModel.Febrero;
                //oImputacionNuevo.Marzo = oImputacionModel.Marzo;
                //oImputacionNuevo.Abril = oImputacionModel.Abril;
                //oImputacionNuevo.Mayo = oImputacionModel.Mayo;
                //oImputacionNuevo.Junio = oImputacionModel.Junio;
                //oImputacionNuevo.Julio = oImputacionModel.Julio;
                //oImputacionNuevo.Agosto = oImputacionModel.Agosto;
                //oImputacionNuevo.Septiembre = oImputacionModel.Septiembre;
                //oImputacionNuevo.Octubre = oImputacionModel.Octubre;
                //oImputacionNuevo.Noviembre = oImputacionModel.Noviembre;
                //oImputacionNuevo.Diciembre = oImputacionModel.Diciembre;
                //oImputacionNuevo.Activo = oImputacionModel.Activo;
                //oImputacionNuevo.IdUsuario = oImputacionModel.IdUsuario;
                //oImputacionNuevo.UltimaModificacion = oImputacionModel.UltimaModificacion;

                Imputacion oPaisRespuesta = ImputacionRepositorio.ActualizarImputacion(imp);

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
                 _mensaje?.Invoke("Ops!, Ha ocurriodo un error. contacte al administrador", "erro");
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
                 _mensaje?.Invoke("Se registro el asineto contable correctamente", "ok");



            }
            catch (Exception ex)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
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
                  _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
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


        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Here's how you could make a second paragraph in a description. <see cref="System.Console.WriteLine(System.String)"/> for information about output statements.</para>
        /// <seealso cref="TestClass.Main"/>
        /// </summary>

        public List<DiarioModel> GetAsientosContables(string periodo, string tipo)
        {
            try
            {
                IList<Diario> diario = ImputacionRepositorio.GetAsientosContables(periodo, tipo);
                //var asientos = (from c in diario
                //                 select c)
                //                   .Select(u => new DiarioModel()
                //                   {
                //                       IdImputacion = u.IdImputacion,
                //                       Descripcion =  u.Descripcion,
                //                       Debe = (u.Importe > 0) ? u.Importe : 0,
                //                       Haber = (u.Importe < 0) ? u.Importe : 0
                //                   }).ToList();

                var asientos = diario.GroupBy(x => new { x.IdImputacion, x.Descripcion })
                                .Select(c => new DiarioModel()
                                {
                                    IdImputacion = c.Key.IdImputacion,
                                    Descripcion = c.Key.Descripcion,
                                    Debe = c.Sum(x => (x.Importe > 0) ? x.Importe : 0),
                                    Haber = c.Sum(x => (x.Importe < 0) ? x.Importe : 0)
                                }).ToList();

                return asientos;
            }
            catch (Exception ex)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }
        public List<DiarioModel> GetCompraFactura(string periodo)
        {
            try
            {
                return Mapper.Map<List<Diario>,List<DiarioModel>>(ImputacionRepositorio.GetCompraFactura(periodo));
            }
            catch (Exception ex)
            {
                 _mensaje?.Invoke("Ops!, A ocurriodo un error. Contacte al Administrador" + ex.Message, "error");
                return null;
            }
        }

        public Dictionary<int, string> ObtenerTipoAsiento()
        {
            Dictionary<int, string> diccionario = new Dictionary<int, string>();
            diccionario.Add((int)TipoAsientoEnum.CF, EnumDescription(TipoAsientoEnum.CF));
       
            return diccionario;
        }
        public Dictionary<string, string> GetTipoAsiento()
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.CF), EnumDescription(TipoAsientoEnum.CF));
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.CP), EnumDescription(TipoAsientoEnum.CP));
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.VF), EnumDescription(TipoAsientoEnum.VF));
            diccionario.Add(EnumDescriptionName(TipoAsientoEnum.VP), EnumDescription(TipoAsientoEnum.VP));

            return diccionario;
        }
        public string EnumDescription(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetDescription();
        }
        public string EnumDescriptionName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

    }
}
