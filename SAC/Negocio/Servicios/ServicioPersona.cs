using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using AutoMapper;
using Negocio.Modelos;

namespace Negocio.Servicios
{   
    public class ServicioPersona : ServicioBase
    {
        private Datos.Repositorios.PersonaRepositorio _personaRepositorio;
        public Action<string, string> _mensaje;
        public ServicioPersona()
        {        
            _personaRepositorio = kernel.Get<PersonaRepositorio>();
        }

        public Modelos.PersonaModel CrearPersona(Modelos.PersonaModel persona)
        {
            try
            {
                Persona p = Mapper.Map< Modelos.PersonaModel, Persona>(persona);
            return Mapper.Map< Persona, Modelos.PersonaModel>(_personaRepositorio.CrearPersona(p));
            }
            catch (Exception ex)
            {
                _mensaje?.Invoke("Ops!, A ocurriodo un error. Intente mas tarde por favor", "erro");
                return null;
            }
        }

    }

}
