using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using AutoMapper;

namespace Negocio.Servicios
{   
    public class ServicioNotificacion : ServicioBase
    {
        private Datos.Repositorios.NotificacionRepositorio _notificacionRepositorio;

        public ServicioNotificacion()
        {
            _notificacionRepositorio = kernel.Get<NotificacionRepositorio>();
        }
        public List<Notificacion> ObtenerPersonas()
        {
            return _notificacionRepositorio.ObtenerNotificacion();
        }

        public Notificacion ObtenerPersonaPorID(int id   )
        {

            return _notificacionRepositorio.ObtenerNotificacionPorID(id);
        }
     
        public Notificacion CrearNotificacion(Modelos.NotificacionModel notificacion)
        {
            Notificacion p = Mapper.Map< Modelos.NotificacionModel, Notificacion>(notificacion);
            return _notificacionRepositorio.CrearNotificacion(p);
        }
   //public Modelos.PersonaModel ObtenerPersonaPorDocumento(string documento)
   //     {
   //         Persona persona =_personaRepositorio.ObtenerPersonaPorDocumento(documento);
   //         return Mapper.Map<Persona,Modelos.PersonaModel>(persona);
   //     }
         
    }

}
