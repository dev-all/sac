using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Negocio.Modelos;
using Datos.ModeloDeDatos;

namespace Agenda.Infrastructure
{
    public class AutoMapperNegProfile: AutoMapper.Profile
    {
        public AutoMapperNegProfile()
        {
            CreateMap<TipoMonedaModel, TipoMoneda>();
            CreateMap<TipoMoneda, TipoMonedaModel>();


            CreateMap<TipoComprobanteModel, TipoComprobante>();
            CreateMap<TipoComprobante, TipoComprobanteModel>();
            
            CreateMap<CompraFacturaModel, CompraFactura>();
            CreateMap<CompraFactura, CompraFacturaModel>();

            CreateMap<CompraIvaModel, CompraIva>();
            CreateMap<CompraIva, CompraIvaModel>();
          

            CreateMap<ValorCotizacionModel, ValorCotizacion>();
            CreateMap<ValorCotizacion, ValorCotizacionModel>();

            CreateMap<ProveedorModel, Proveedor>();
            CreateMap<Proveedor, ProveedorModel>();


            CreateMap<PersonaModel, Persona>();
            CreateMap<Persona, PersonaModel>();


            CreateMap<PrioridadModel, Prioridad>();
            CreateMap<Prioridad, PrioridadModel>();

            CreateMap<Pais, PaisModel>();
            CreateMap<PaisModel, Pais>();

            CreateMap<Provincia, ProvinciaModel>();
            CreateMap<ProvinciaModel, Provincia>();

            CreateMap<Localidad, LocalidadModel > ();
            CreateMap< LocalidadModel, Localidad>();

            CreateMap<TipoIva, TipoIvaModel>();
            CreateMap<TipoIvaModel, TipoIva>();

            CreateMap<SubRubro, SubRubroModel>();
            CreateMap<SubRubroModel, SubRubro>();

            CreateMap<TipoProveedor, TipoProveedorModel>();
            CreateMap<TipoProveedorModel, TipoProveedor>();

            CreateMap<TipoMoneda, TipoMonedaModel>();
            CreateMap<TipoMonedaModel, TipoMoneda>();

            CreateMap<Evento, EventoModel>();
            CreateMap<EventoModel, Evento>();

            CreateMap<Accion, AccionModel>();
            CreateMap<AccionModel, Accion>();

            CreateMap<AccionPorRol, AccionPorRolModel>();
            CreateMap<AccionPorRolModel, AccionPorRol>();


            CreateMap<UsuarioModel, Usuario>();
            CreateMap<Usuario, UsuarioModel>();


            CreateMap<RolModel, Rol>();
            CreateMap<Rol, RolModel>();

            CreateMap<MenuSidebar, MenuSideBarModel>()
                .ForMember(
                    dest => dest.Group,
                    opt => opt.MapFrom(src => src.MenuSidebar1)
                );

            CreateMap<MenuSideBarModel, MenuSidebar>();



        }      
    }
}