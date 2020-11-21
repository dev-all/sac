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


        public List<Pais> ObtenerPais(int idPais)
        {
            List<Pais> pais = context.Pais.Where(p => p.Id == idPais).ToList();
            return pais;
        }


        public Pais ObtenerPaisPorId(int idPais)
        {
            var pais = context.Pais.Where(p => p.Id == idPais).FirstOrDefault();
            return pais;
        }

        public Pais ActualizarPais(Pais model)
        {
            Pais paisExistente = ObtenerPaisPorId(model.Id);
            paisExistente.Nombre = model.Nombre;
            context.SaveChanges();
            return paisExistente;
        }

        public Pais ObtenerPaisPorNombre(string nombre)
        {           
            return context.Pais.Where(p => p.Nombre == nombre).FirstOrDefault();
        }


        public Pais ObtenerPaisPorCodigoAfip(string codigoafip)
        {
            return context.Pais.Where(p => p.CodigoAfip == codigoafip).FirstOrDefault();
            //return context.Pais.FirstOrDefault(l => l.CodigoAfip == codigoafip);
        }


    }
}