using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;

namespace Negocio.Servicios
{
    public class ServicioIvaVenta : ServicioBase
    {

        private IvaVentaRepositorio oIvaVentaRepositorio;
        public Action<string, string> _mensaje;

        public ServicioIvaVenta()
        {
            oIvaVentaRepositorio = kernel.Get<IvaVentaRepositorio>();
        }

        public List<IvaVentaModel> GetAllIvaVenta()
        {
            List<IvaVentaModel> listaIvaVenta = Mapper.Map < List <IvaVenta>, List<IvaVentaModel>>(oIvaVentaRepositorio.GetAllIvaVenta());
            return listaIvaVenta;
        }

        public IvaVentaModel Agregar(IvaVentaModel oIvaVentaModel)
        {
            try
            {
                var oModel = Mapper.Map<IvaVentaModel, IvaVenta>(oIvaVentaModel);
                return Mapper.Map<IvaVenta, IvaVentaModel>(oIvaVentaRepositorio.Agregar(oModel));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }
    }
}
