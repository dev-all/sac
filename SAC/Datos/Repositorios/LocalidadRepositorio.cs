using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class LocalidadRepositorio : RepositorioBase<Localidad>
    {
       private INCORPORACIONES_Entities context;

        public LocalidadRepositorio(INCORPORACIONES_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Localidad CrearLocalidad(Localidad localidad)
        {
            return Insertar(localidad);
        }
     
        public Localidad ObtenerLocalidadPorNombreIdProvincia(string nombreLocalidad, int idProvincia)
        {
            var provincia = context.Provincia.FirstOrDefault(p => p.id == idProvincia);
            return context.Localidad.FirstOrDefault(l => l.nombre == nombreLocalidad && l.idProvincia == provincia.id);
        }

        public Provincia ObtenerProvinciaPorNombre( string nombreProvincia)
        {            
            return context.Provincia.FirstOrDefault(p => p.nombre == nombreProvincia);
        }

    }
}