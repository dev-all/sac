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
        private ProveedorRepositorio pProveedorRepositorio;
        public Action<string, string> _mensaje;

        public ServicioProveedor()
        {
            pProveedorRepositorio = kernel.Get<ProveedorRepositorio>();
        }

        public List<ProveedorModel> GetAllProveedor()
        {
            return Mapper.Map<List<Proveedor>, List<ProveedorModel>>(pProveedorRepositorio.GetAllProveedor());             
        }

        public int ActualizarProveedor(ProveedorModel oProveedorModel)
        {
            //controlar que no exista 
            Proveedor oProveedor = pProveedorRepositorio.ObtenerProveedorPorNombre(oProveedorModel.Nombre, oProveedorModel.Cuit, oProveedorModel.Id);
            if (oProveedor != null) //significa que existe
            {
                return -2;
            }
            else //significa que no existe el dato a ingresar
            {
                Proveedor oPaisNuevo = new Proveedor();
                Proveedor oPaisRespuesta = new Proveedor();

                oPaisNuevo.Id = oProveedorModel.Id;
                oPaisNuevo.Nombre = oProveedorModel.Nombre;
                oPaisNuevo.Activo = true;

                oPaisRespuesta = pProveedorRepositorio.ActualizarProveedor(oPaisNuevo);

                if (oPaisRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public int GuardarProveedor(ProveedorModel oProveedorModel)
        {
            //controlar que no exista 
            Proveedor oProveedor = pProveedorRepositorio.ObtenerProveedorPorNombre(oProveedorModel.Nombre, oProveedorModel.Cuit);
            if (oProveedor != null)
            {
                return -2;
            }
            else
            {
                Proveedor oProveedorNuevo = new Proveedor();
                Proveedor oProveedorRespuesta = new Proveedor();

                oProveedorNuevo.Nombre = oProveedorModel.Nombre;
               
                //oProveedorNuevo.IdPais = oProveedorModel.IdPais;
              
                //oProveedorNuevo.Activo = oProveedorModel.Activo;
                //oProveedorNuevo.IdUsuario = oProveedorModel.IdUsuario;
                //oProveedorNuevo.UltimaModificacion = oProveedorModel.UltimaModificacion;

                oProveedorRespuesta = pProveedorRepositorio.InsertarProveedor(oProveedorNuevo);
                if (oProveedorRespuesta == null)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }

        public ProveedorModel GetProveedorPorID(int idProveedor)
        {
            return Mapper.Map<Proveedor, ProveedorModel>(pProveedorRepositorio.GetProveedorPorId(idProveedor));
        }

        //public int Eliminar(int idProveedor)
        //{
        //    var retorno = pProveedorRepositorio.EliminarProveedor(idProveedor);
        //    if (retorno == 1)
        //    {
        //        return 0; //ok
        //    }
        //    else
        //    {
        //        return -1;//paso algo
        //    }
        //}

    }

}
