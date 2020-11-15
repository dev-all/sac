using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Datos.Repositorios
{
    public class NotificacionRepositorio : RepositorioBase<Notificacion>
    {
       private INCORPORACIONES_Entities context;
        
        public NotificacionRepositorio(INCORPORACIONES_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }
    
        public Notificacion CrearNotificacion(Notificacion notificacion)
        {
            return Insertar(notificacion);
        }

        public List<Notificacion> ObtenerNotificacion()
        {
            return context.Notificacion.ToList();
        }

        public Notificacion ObtenerNotificacionPorID(int id)
        {           
            return context.Notificacion.Where(per => per.id == id).FirstOrDefault(); 
        }

        //public Persona ObtenerPersonaPorDocumento(string documento)
        //{
        //    return context.Persona.Where(per => per.documento == documento).FirstOrDefault();
        //}

        //public Persona ActualizarPersona(Persona personaParaActualizar)
        //{
        //    var perDB = context.Persona.Where(p => p.id == personaParaActualizar.id);
        //    Persona personaParaMostrar;
        //    if ((perDB == null)) { 
        //        personaParaMostrar = null;
        //    }
        //    else { 
        //        foreach (var perGuardada in perDB)
        //        {
        //            if ((perGuardada.email != string.Empty | perGuardada.email == null))
        //            { perGuardada.email = personaParaActualizar.email; }
        //            if ((perGuardada.telefono != string.Empty | perGuardada.telefono == null))
        //            { perGuardada.telefono = personaParaActualizar.telefono; }

        //            perGuardada.primerNombre = personaParaActualizar.primerNombre;
        //            //...
        //        }
                 
        //        context.SaveChanges();
        //    personaParaMostrar = ObtenerPersonaPorID(personaParaActualizar.id);
        //    }

        //    return personaParaMostrar;
        //}

        //public bool CodeDisponible(string code)
        //{
        //    Persona perDB = context.Persona.Where(p => p.codigoValidacion == code).FirstOrDefault(); 
        //    if ((perDB == null))
        //    {
        //        return  true;
        //    }
        //    else {
        //        return false;
        //    }
        //}
    }
}