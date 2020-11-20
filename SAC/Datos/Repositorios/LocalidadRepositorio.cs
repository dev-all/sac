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



        #region "METODOS DE ACTUALIZACION"


        // INSERTAR

        public Localidad InsertarLocalidad(Localidad localidad)
        {
            return Insertar(localidad);
        }


        public Localidad ActualizarLocalidad(Localidad localidad)
        {



            localidad.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
            context.SaveChanges();
            return localidad;

        }


        public void EliminarLocalidad(Localidad localidad)
        {


            localidad.IdUsuario = localidad.IdUsuario;
            localidad.Activo = false;
            localidad.UltimaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
            context.SaveChanges();
            //return localidad;

        }








        #endregion


        #region "METODOS DE LECTOR "




      




        public Localidad ObtenerIdProvincia(int idProvincia)
        {
            var provincia = context.Localidad.FirstOrDefault(p => p.IdProvincia == idProvincia);
            return context.Localidad.FirstOrDefault(l => l.IdProvincia == idProvincia);
        }


        public Localidad ObtenerIdPais(int idPais)
        {
            var provincia = context.Localidad.FirstOrDefault(p => p.IdPais == idPais);
            return context.Localidad.FirstOrDefault(l => l.IdPais == idPais);
        }

        public Localidad ModificarLocalidad(Localidad localidad)
        {
            return Insertar(localidad);
        }


        public Localidad ObtenerporId(int idLocalidad)
        {

            var localidad = context.Localidad.FirstOrDefault(p => p.Id == idLocalidad);
            return context.Localidad.FirstOrDefault(l => l.Id == idLocalidad);


        }

        public Localidad ObtenerporCodigoPostal(int codigopostal)
        {

            var localidad = context.Localidad.FirstOrDefault(p => p.Codigo == codigopostal);
            return context.Localidad.FirstOrDefault(l => l.Codigo == codigopostal);


        }

        #endregion


    }
}