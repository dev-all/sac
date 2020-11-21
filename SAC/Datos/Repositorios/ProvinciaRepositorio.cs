using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class ProvinciaRepositorio : RepositorioBase<Provincia>
    {
       private SAC_Entities context;

        public ProvinciaRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Provincia InsertarProvincia(Provincia provincia)
        {
            return Insertar(provincia);
        }


       


        public Provincia ObtenerIdPais(int idPais)
        {
            var provincia = context.Provincia.FirstOrDefault(p => p.IdPais == idPais);
            return context.Provincia.FirstOrDefault(l => l.IdPais == idPais);
        }


          


        public Provincia ObtenerporId(int idprovincia)
        {

            var provincia = context.Provincia.FirstOrDefault(p => p.Id == idprovincia);
            return context.Provincia.FirstOrDefault(l => l.Id == idprovincia);


        }


        public Provincia ObtenerporNOMBRE(string nombre)
        {

            var provincia = context.Provincia.FirstOrDefault(p => p.Nombre == nombre);
            return context.Provincia.FirstOrDefault(l => l.Nombre == nombre);


        }


        public Provincia ObtenerporCodigoAfip(int codigoafip)
        {

            var provincia = context.Provincia.FirstOrDefault(p => p.CodigoAfip == codigoafip);
            return context.Provincia.FirstOrDefault(l => l.CodigoAfip == codigoafip);


        }




    }
}