using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAC.Models;
using Entidad.Modelos;
using Negocio.Modelos;


namespace SAC.Infrastructure
{
    public class AutoMapperWebProfile : AutoMapper.Profile
    {
        public AutoMapperWebProfile()
        {

            CreateMap<RetencionModel, RetencionModelView>();
            CreateMap<RetencionModelView, RetencionModel>();

            CreateMap<TipoRetencionModel, TipoRetencionModelView>();
            CreateMap<TipoRetencionModelView, TipoRetencionModel>();

            CreateMap<PresupuestoItemModel, PresupuestoItemModelView>();
            CreateMap<PresupuestoItemModelView, PresupuestoItemModel>();

            CreateMap<PresupuestoCostoModel, PresupuestoCostoModelView>();
            CreateMap<PresupuestoCostoModelView, PresupuestoCostoModel>();

            CreateMap<CajaModel, CajaModelView>();
            CreateMap<CajaModelView, CajaModel>();


            CreateMap<CajaGrupoModel, CajaGrupoModelView>();
            CreateMap<CajaGrupoModelView, CajaGrupoModel>();


            CreateMap<TipoMonedaModel, TipoMonedaModelView>();
            CreateMap<TipoMonedaModelView, TipoMonedaModel>();


            CreateMap<TipoComprobanteModel, TipoComprobanteModelView>();
            CreateMap<TipoComprobanteModelView, TipoComprobanteModel>();

            CreateMap<CompraFacturaModel, CompraFacturaViewModel>();
            CreateMap<CompraFacturaViewModel, CompraFacturaModel>();

            CreateMap<CompraIvaModel, CompraIvaModelView>();
            CreateMap<CompraIvaModelView, CompraIvaModel>();


            CreateMap< PresupuestoActualModel, PresupuestoActualModelView>();
            CreateMap<PresupuestoActualModelView, PresupuestoActualModel>();

            CreateMap<TarjetaModel, TarjetaModelView>();
            CreateMap<TarjetaModelView, TarjetaModel>();

            CreateMap<BancoCuentaModel, BancoCuentaModelView>();
            CreateMap<BancoCuentaModelView, BancoCuentaModel>();

            CreateMap<BancoCuentaBancariaModel, BancoCuentaBancariaModelView>();
            CreateMap<BancoCuentaBancariaModelView, BancoCuentaBancariaModel>();

            CreateMap<ChequeraModel, ChequeraModelView>();
            CreateMap<ChequeraModelView, ChequeraModel>();

            CreateMap<ChequeModel, ChequeModelView>();
            CreateMap<ChequeModelView, ChequeModel>();
     
            CreateMap<FacturaPagoModel, FacturaPagoViewModel>();
            CreateMap<FacturaPagoViewModel, FacturaPagoModel>();

            CreateMap <ImputacionModel, ImputacionModelView>();
            CreateMap <ImputacionModelView, ImputacionModel>();

            CreateMap<TipoComprobanteModel, TipoComprobanteModelView>();
            CreateMap<TipoComprobanteModelView, TipoComprobanteModel>();

            CreateMap<TipoMonedaModel, TipoMonedaModelView>();
            CreateMap<TipoMonedaModelView, TipoMonedaModel>();

            CreateMap<CompraFacturaModel, CompraFacturaViewModel>();
            CreateMap<CompraFacturaViewModel, CompraFacturaModel>();

            CreateMap<CuentaCteProveedorModel, CuentaCteProveedorModelView>();
            CreateMap<CuentaCteProveedorModelView, CuentaCteProveedorModel>();

            CreateMap<PersonaModel,PersonaModelView>();
            CreateMap<PersonaModelView,PersonaModel >();

            CreateMap<PrioridadModel, PrioridadModelView>();
            CreateMap<PrioridadModelView , PrioridadModel>();

            CreateMap<EventoModel, EventoModelView>();             
            CreateMap<EventoModelView, EventoModel>();

            CreateMap<PaisModel, PaisModelView>();
            CreateMap<PaisModelView, PaisModel>();

            CreateMap<ProvinciaModel, ProvinciaModelView>();
            CreateMap<ProvinciaModelView, ProvinciaModel>();

            CreateMap<LocalidadModel, LocalidadModelView>();
            CreateMap<LocalidadModelView, LocalidadModel>();

            CreateMap<ProveedorModel, ProveedorModelView>();
            CreateMap<ProveedorModelView, ProveedorModel>();

            CreateMap<AfipRegimenModel, AfipRegimenModelView>();
            CreateMap<AfipRegimenModelView, AfipRegimenModel>();

            CreateMap<TipoIvaModel, TipoIvaViewModel>();
            CreateMap<TipoIvaViewModel, TipoIvaModel>();


            CreateMap<SubRubroModel, SubRubroModelView>();
            CreateMap<SubRubroModelView, SubRubroModel>();

            CreateMap<AccionModel, AccionModelView>();
            CreateMap<AccionModelView, AccionModel>();
          

            CreateMap<RolModel, RolModelView>();
            CreateMap<RolModelView, RolModel>();
            CreateMap<AccionPorRolModel, AccionPorRolModelView>();
            CreateMap<AccionPorRolModelView, AccionPorRolModel>();
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