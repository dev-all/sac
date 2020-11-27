using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datos.Repositorios
{
   public class AfipRegimenRepositorio : RepositorioBase<AfipRegimen>
    {
        private SAC_Entities context;

        public AfipRegimenRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        public AfipRegimen InsertarAfipRegimen(AfipRegimen AfipRegimen)
        {
            return Insertar(AfipRegimen);
        }


        public AfipRegimen ObtenerAfipRegimen(int idAfipRegimen)
        {
            AfipRegimen AfipRegimen = context.AfipRegimen.Where(p => p.Id == idAfipRegimen).First();
            return AfipRegimen;
        }


        public AfipRegimen ObtenerAfipRegimenPorId(int idAfipRegimen)
        {
            var AfipRegimen = context.AfipRegimen.Where(p => p.Id == idAfipRegimen).FirstOrDefault();
            return AfipRegimen;
        }

        public AfipRegimen ActualizarAfipRegimen(AfipRegimen model)
        {
            AfipRegimen AfipRegimenExistente = ObtenerAfipRegimenPorId(model.Id);

            AfipRegimenExistente.Id = model.Id;
            AfipRegimenExistente.Descripcion = model.Descripcion;
            AfipRegimenExistente.Concepto = model.Concepto;
            AfipRegimenExistente.Aliri = model.Aliri;
            AfipRegimenExistente.Alirni = model.Alirni;
            AfipRegimenExistente.Minimo= model.Minimo;
            AfipRegimenExistente.Imputacion = model.Imputacion;
            AfipRegimenExistente.Activo = model.Activo;

            context.SaveChanges();
            return AfipRegimenExistente;
        }

        public AfipRegimen ObtenerAfipRegimenPorNombre(string nombre)
        {
            return context.AfipRegimen.Where(p => p.Descripcion == nombre).FirstOrDefault();
        }

        public AfipRegimen ObtenerAfipRegimenPorNombre(string nombre, string Concepto)
        {
            return context.AfipRegimen.Where(p => p.Descripcion == nombre && p.Concepto == Concepto).FirstOrDefault();
        }


        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idAfipRegimen"></param>
        /// <returns></returns>
        public AfipRegimen ObtenerAfipRegimenPorNombre(string nombre, string Concepto, int id)
        {
            return context.AfipRegimen.Where(p => p.Descripcion == nombre && p.Concepto == Concepto && p.Id != id).FirstOrDefault();
        }


        public AfipRegimen ObtenerAfipRegimenPorConcepto(string oConcepto)
        {
            return context.AfipRegimen.Where(p => p.Concepto == oConcepto).FirstOrDefault();
            //return context.AfipRegimen.FirstOrDefault(l => l.CodigoAfip == codigoafip);

        }


        public List<AfipRegimen> GetAllAfipRegimen()
        {
            List<AfipRegimen> listaAfipRegimen = context.AfipRegimen.Where(p => p.Activo == true).ToList();
            return listaAfipRegimen;
        }
       

        public int EliminarAfipRegimen(int idAfipRegimen)
        {
            AfipRegimen AfipRegimenExistente = ObtenerAfipRegimenPorId(idAfipRegimen);
            AfipRegimenExistente.Activo = false;
            context.SaveChanges();
            return 1;

           
        }



    }
}
