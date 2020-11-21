using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class PaisRepositorio : RepositorioBase<Pais>
    {
       private SAC_Entities context;

        public PaisRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Pais InsertarPais(Pais pais)
        {
            return Insertar(pais);
        }


       


        public Pais ObtenerIdPais(int idPais)
        {
            var pais = context.Pais.FirstOrDefault(p => p.Id == idPais);
            return context.Pais.FirstOrDefault(l => l.Id == idPais);
        }


          


        public Pais ObtenerporNOMBRE(string nombre)
        {

            var pais = context.Pais.FirstOrDefault(p => p.Nombre == nombre);
            return context.Pais.FirstOrDefault(l => l.Nombre == nombre);


        }


        public Pais ObtenerporCodigoAfip(string codigoafip)
        {

            var pais = context.Pais.FirstOrDefault(p => p.CodigoAfip == codigoafip);
            return context.Pais.FirstOrDefault(l => l.CodigoAfip == codigoafip);


        }


    }
}