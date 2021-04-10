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
    public class ServicioFacturaVenta : ServicioBase
    {
        private FacturaVentasRepositorio oFacturaVentasRepositorio;

        public ServicioFacturaVenta()
        {
            oFacturaVentasRepositorio = kernel.Get<FacturaVentasRepositorio>();
        }


        public List<FacturaVentaModel> GetAllFacturaVenta()
        {
            List<FacturaVentaModel> listaFacturaVenta = Mapper.Map<List<FactVenta>, List<FacturaVentaModel>>(oFacturaVentasRepositorio.GetAllFacturaVenta());
            return listaFacturaVenta;
        }

        public FacturaVentaModel Agregar(FacturaVentaModel oFacturaVentaModel)
        {
            try
            {
                var oModel = Mapper.Map<FacturaVentaModel, FactVenta>(oFacturaVentaModel);
                return Mapper.Map<FactVenta, FacturaVentaModel>(oFacturaVentasRepositorio.Agregar(oModel));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, Ocurrio un error. Comuníquese con el administrador del sistema", "error");
                return null;
            }
        }


    }
}
