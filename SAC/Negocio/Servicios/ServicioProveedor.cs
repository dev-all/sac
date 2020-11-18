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

    public class ServicioProveedor : ServicioBase
    {
        private ProveedorRepositorio proveedorRepositorio;
        public Action<string, string> _mensaje;

        public ServicioProveedor()
        {
            proveedorRepositorio = kernel.Get<ProveedorRepositorio>();
        }

        public List<ProveedorModel> GetAllProveedor()
        {
            return Mapper.Map<List<Proveedor>, List<ProveedorModel>>(proveedorRepositorio.GetAllProveedor());             
        }


    }

}
