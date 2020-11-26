using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class LocalidadRepositorio : RepositorioBase<Localidad>
    {
       private SAC_Entities context;

        public LocalidadRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

        // INSERTAR

        public Localidad InsertarLocalidad(Localidad localidad)
        {
            return Insertar(localidad);
        }

        public Localidad ActualizarLocalidad(Localidad oLocalidad)
        {

            Localidad oLocalidadExistente = ObtenerLocalidadPorId(oLocalidad.Id);

            oLocalidadExistente.Id = oLocalidad.Id;
            oLocalidadExistente.Nombre = oLocalidad.Nombre;
            oLocalidadExistente.Codigo = oLocalidad.Codigo;
            oLocalidadExistente.CodigoProvincia = oLocalidad.CodigoProvincia;
            oLocalidadExistente.IdPais = oLocalidad.IdPais;
            oLocalidadExistente.IdProvincia = oLocalidad.IdProvincia;
            oLocalidadExistente.Activo = oLocalidad.Activo;

            context.SaveChanges();
            return oLocalidadExistente;

        }

        public int EliminarLocalidad(int idLocalidad)
        {
            Localidad LocalidadExistente = ObtenerLocalidadPorId(idLocalidad);
            LocalidadExistente.Activo = false;
            context.SaveChanges();
            return 1;
        }

        //public void EliminarLocalidad(Localidad localidad)
        //{
        //    localidad.IdUsuario = localidad.IdUsuario;
        //    localidad.Activo = false;
        //    localidad.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
        //    context.SaveChanges();
        //}

        public Localidad ModificarLocalidad(Localidad localidad)
        {
            return Insertar(localidad);
        }

        public Localidad ObtenerLocalidadPorId(int idLocalidad)
        {
            return context.Localidad.FirstOrDefault(l => l.Id == idLocalidad && l.Activo == true);
        }

        public Localidad ObtenerporCodigoPostal(int codigopostal)
        {
            return context.Localidad.FirstOrDefault(l => l.Codigo == codigopostal);
        }


        /// <summary>
        /// verifica que el nombre ingresado no exista para otro id que no sea el enviado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="codigo"></param>
        /// <param name="idLocalidad"></param>
        /// <returns></returns>
        public Localidad ObtenerLocalidadPorNombre(string nombre, int codigo, int idLocalidad)
        {
            return context.Localidad.Where(p => p.Nombre == nombre && p.Codigo == codigo && p.Id != idLocalidad).FirstOrDefault();
        }

        public List<Localidad> GetAllLocalidad()
        {
            List<Localidad> listaProvincia = context.Localidad.Where(p => p.Activo == true).ToList();
            return listaProvincia;
        }
        public List<Localidad> GetAllLocalidad(int idPais)
        {
            List<Localidad> listaProvincia = context.Localidad.Where(p => p.Activo == true && p.IdPais == idPais).ToList();
            return listaProvincia;
        }

        public List<Localidad> GetAllLocalidad(int idPais, int idProvincia)
        {
            List<Localidad> listaProvincia = context.Localidad.Where(p => p.Activo == true && p.IdPais == idPais && p.IdProvincia == idProvincia).ToList();

            List<Localidad> listaProvinciaOdernada = listaProvincia.OrderBy(P => P.Nombre).ToList();
 
            return listaProvinciaOdernada;
        }

    }
}