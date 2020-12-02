using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class ImputacionRepositorio : RepositorioBase<Imputacion>
    {

        private SAC_Entities context;

        public ImputacionRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public Imputacion InsertarImputacion(Imputacion Imputacion)
        {
            return Insertar(Imputacion);
        }


        public Imputacion ObtenerImputacion(int idImputacion)
        {
            Imputacion Imputacion = context.Imputacion.Where(p => p.Id == idImputacion).First();
            return Imputacion;
        }


        public Imputacion ObtenerImputacionPorId(int idImputacion)
        {
            var Imputacion = context.Imputacion.Where(p => p.Id == idImputacion).FirstOrDefault();
            return Imputacion;
        }

        public Imputacion ActualizarImputacion(Imputacion model)
        {
            Imputacion ImputacionExistente = ObtenerImputacionPorId(model.Id);

            ImputacionExistente.Id = model.Id;
            ImputacionExistente.Descripcion = model.Descripcion;
            ImputacionExistente.IdSubRubro = model.IdSubRubro;
            ImputacionExistente.SaldoInicial = model.SaldoInicial;
            ImputacionExistente.SaldoFin = model.SaldoFin;
            ImputacionExistente.IdTipo  = model.IdTipo;
            ImputacionExistente.Alias = model.Alias;
            ImputacionExistente.Enero = model.Enero;
            ImputacionExistente.Febrero = model.Febrero;
            ImputacionExistente.Marzo = model.Marzo;
            ImputacionExistente.Abril = model.Abril;
            ImputacionExistente.Mayo = model.Mayo;
            ImputacionExistente.Junio = model.Junio;
            ImputacionExistente.Julio = model.Julio;
            ImputacionExistente.Agosto= model.Agosto;
            ImputacionExistente.Septiembre = model.Septiembre;
            ImputacionExistente.Octubre = model.Octubre;
            ImputacionExistente.Noviembre = model.Noviembre;
            ImputacionExistente.Diciembre = model.Diciembre;
            ImputacionExistente.Activo = model.Activo;
            ImputacionExistente.IdUsuario = model.IdUsuario;
            ImputacionExistente.UltimaModificacion = model.UltimaModificacion;

            context.SaveChanges();
            return ImputacionExistente;
        }

        public Imputacion ObtenerImputacionPorDescripcion(string oDescripcion)
        {
            return context.Imputacion.Where(p => p.Descripcion == oDescripcion).FirstOrDefault();
        }

        public Imputacion ObtenerImputacionPorDescripcion(string oDescripcion, int oCodigo)
        {
            return context.Imputacion.Where(p => p.Descripcion == oDescripcion && p.Id== oCodigo).FirstOrDefault();
        }


        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idImputacion"></param>
        /// <returns></returns>
        public Imputacion ObtenerImputacionPorDescripcion(string oDescripcion, int oIdSubrubro, int oIdImputacion)
        {
            return context.Imputacion.Where(p => p.Descripcion == oDescripcion && p.IdSubRubro == oIdSubrubro && p.Id != oIdImputacion).FirstOrDefault();
        }



        public List<Imputacion> GetAllImputacion()
        {
            List<Imputacion> listaImputacion = context.Imputacion.Where(P=> P.Activo == true).ToList();
            listaImputacion = listaImputacion.OrderBy(p => p.Descripcion).ToList();
            return listaImputacion;
        }


        public int EliminarImputacion(int idImputacion)
        {
            Imputacion ImputacionExistente = ObtenerImputacionPorId(idImputacion);
            ImputacionExistente.Activo = false;
            context.SaveChanges();
            return 1;

        }



    }
}
