using System;
using Datos.Repositorios;
using Datos.ModeloDeDatos;
using Ninject;
using System.Collections.Generic;
using AutoMapper;
using Negocio.Modelos;

namespace Negocio.Servicios
{   
    public class ServicioRol : ServicioBase
    {
        private Datos.Repositorios.RolRepositorio _RolRepositorio;
    
        public ServicioRol()
        {
            _RolRepositorio = kernel.Get<RolRepositorio>();
        }
        public List<Modelos.RolModel> getAllRol()
        { 
            return Mapper.Map < List<Rol>,  List<Modelos.RolModel> >(_RolRepositorio.getAllRol());            
        }
    
        public Modelos.RolModel CrearRol(Modelos.RolModel rol)
        {
            Rol p = Mapper.Map< Modelos.RolModel, Rol>(rol);
            return Mapper.Map<Rol, Modelos.RolModel>(_RolRepositorio.CrearRol(p));
        }
        public RolModel getRolPorId(int idRol)
        {
            return Mapper.Map <Rol, Modelos.RolModel> (_RolRepositorio.getAccionPorId(idRol));
        }
        public RolModel ActualizarRol(RolModel rol)
        {
            Rol rolParaActualizar = Mapper.Map<Modelos.RolModel, Rol>(rol);
            return Mapper.Map<Rol, Modelos.RolModel>(_RolRepositorio.ActualizarRol(rolParaActualizar));
        }

      
    }

}
