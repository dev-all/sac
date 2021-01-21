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
  public  class ServicioCuentaCteProveedor : ServicioBase
    {
        private ProveedorRepositorio pProveedorRepositorio;
        public Action<string, string> _mensaje;

        public ServicioCuentaCteProveedor()
        {
            pProveedorRepositorio = kernel.Get<ProveedorRepositorio>();
        }

        public List<CuentaCteProveedorModel> GetAllCuentasCteProveedor()
        {
            return Mapper.Map<List<CuentaCorriente>, List<CuentaCteProveedorModel>>(pProveedorRepositorio.GetAllCuentaCorriente());
        }

        public List<CuentaCteProveedorModel> GetAllCuentasCteProveedor(DateTime inicio ,DateTime fin)
        {
            return Mapper.Map<List<CuentaCorriente>, List<CuentaCteProveedorModel>>(pProveedorRepositorio.GetAllCuentaCorriente(inicio,fin));
        }

        public List<CuentaCorrienteProveedorDetallesModel> CtaCteDetalle(int idProveedor, DateTime fechaDesde)
        {
            return Mapper.Map<List<CuentaCorrienteProveedorDetalles>, List<CuentaCorrienteProveedorDetallesModel>>(pProveedorRepositorio.CtaCteDetalle(idProveedor, fechaDesde));
        }
    }
}
