using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAC.Models;
using Entidad.Modelos;
using Negocio.Modelos;


namespace SAC.Infrastructure
{
    public class AutoMapperWebProfile: AutoMapper.Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<PersonaModel,PersonaModelView>();
            CreateMap<PersonaModelView,PersonaModel >();
            CreateMap<PrioridadModel, PrioridadModelView>();
            CreateMap<PrioridadModelView , PrioridadModel>();
            CreateMap<EventoModel, EventoModelView>();             
            CreateMap<EventoModelView, EventoModel>();

            CreateMap<AccionModel, AccionModelView>();
            CreateMap<AccionModelView, AccionModel>();
          

            CreateMap<RolModel, RolModelView>();
            CreateMap<RolModelView, RolModel>();
            CreateMap<AccionPorRolModel, AccionPorRolView>();
            CreateMap<AccionPorRolView, AccionPorRolModel>();
            CreateMap<ConfiguracionModel, ConfiguracionModelView>();
            CreateMap<ConfiguracionModelView, ConfiguracionModel>();
            CreateMap<UsuarioModel, UsuarioModelView>();
            CreateMap<UsuarioModelView, UsuarioModel>();

            CreateMap<UsuarioModelView, UsuarioModel>();
            CreateMap<UsuarioModel, UsuarioModelView>();

            CreateMap<MenuSideBarModel, MenuItemModel>()
            .ForMember(mi => mi.Icono, msb => msb.MapFrom(i => i.Icono))
             .ForMember(mi => mi.Url, msb => msb.MapFrom(i => i.Url))
             .ForMember(mi => mi.Titulo, msb => msb.MapFrom(i => i.Titulo))
             .ForMember(mi => mi.Metodo, msb => msb.MapFrom(i => i.Accion.Nombre))
            .ForMember(mi => mi.Controller, msb => msb.MapFrom(i => i.Accion.Controlador));






        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfiles(new[] {
                                                        "SAC",
                                                        "Negocio"
                                                    }));
            //AutoMapper.Mapper.Initialize(a =>
            //{
            //    a.AddProfile<AutoMapperWebProfile>();               
            //});
        }
    }
}