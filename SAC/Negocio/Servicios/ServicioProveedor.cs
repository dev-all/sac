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


        public ServicioProveedor()
        {
            pProveedorRepositorio = kernel.Get<ProveedorRepositorio>();
        }

        public List<ProveedorModel> GetAllProveedor()
        {
            return Mapper.Map<List<Proveedor>, List<ProveedorModel>>(pProveedorRepositorio.GetAllProveedor());             
        }

        public ProveedorModel GetProveedor(int _id)
        {           
            Proveedor oProveedor = pProveedorRepositorio.GetProveedorPorId(_id);
            return Mapper.Map<Proveedor, ProveedorModel>(oProveedor);
        }

        public ProveedorModel GetProveedorCompleto(int _id)
        {
            Proveedor oProveedor = pProveedorRepositorio.GetProveedorPorIdCompleto(_id);
            return Mapper.Map<Proveedor, ProveedorModel>(oProveedor);
        }
        public ProveedorModel ActualizarProveedor(ProveedorModel oProveedorModel)
        {
            try
            {
                //controlar que no exista 
                Proveedor oProveedor = pProveedorRepositorio.ObtenerProveedorPorNombre(oProveedorModel.Nombre, oProveedorModel.Cuit, oProveedorModel.Id);
                if (oProveedor != null) //significa que existe
                {
                    //return -2;
                    _mensaje("El proveedor que intenta resitrar ya se encuentra cargado", "error");
                    return null;
                }
                else //significa que no existe el dato a ingresar
                {
                    Proveedor oProveedorNuevo = new Proveedor();

                    oProveedorNuevo.Id = oProveedorModel.Id;
                    oProveedorNuevo.Nombre = oProveedorModel.Nombre;
                    oProveedorNuevo.Direccion = oProveedorModel.Direccion;
                    oProveedorNuevo.Telefono = oProveedorModel.Telefono;
                    oProveedorNuevo.IdPais = oProveedorModel.IdPais;
                    oProveedorNuevo.IdProvincia = oProveedorModel.IdProvincia;
                    oProveedorNuevo.IdLocalidad = oProveedorModel.IdLocalidad;
                    oProveedorNuevo.IdCodigoPostal = oProveedorModel.IdCodigoPostal;
                    oProveedorNuevo.IdImputacionProveedor = oProveedorModel.IdImputacionProveedor;
                    oProveedorNuevo.Email = oProveedorModel.Email;
                    oProveedorNuevo.IdTipoIva = oProveedorModel.IdTipoIva;
                    oProveedorNuevo.Cuit = oProveedorModel.Cuit;
                    oProveedorNuevo.IdImputacionFactura = oProveedorModel.IdImputacionFactura;
                    oProveedorNuevo.IdTipoProveedor = oProveedorModel.IdTipoProveedor;
                    oProveedorNuevo.IdTipoMoneda = oProveedorModel.IdTipoMoneda;
                    oProveedorNuevo.Observaciones = oProveedorModel.Observaciones;
                    oProveedorNuevo.UltimoPuntoVenta = oProveedorModel.UltimoPuntoVenta;
                    oProveedorNuevo.Activo = true;
                    oProveedorNuevo.IdUsuario = oProveedorModel.IdUsuario;//hay que poner el id del usuario logueado
                    oProveedorNuevo.UltimaModificacion = oProveedorModel.UltimaModificacion;

                    _mensaje("El proveedor se registró correctamente", "ok");

                    return Mapper.Map<Proveedor, ProveedorModel>( pProveedorRepositorio.ActualizarProveedor(oProveedorNuevo));
                   
                }

            }
            catch(Exception ex)
            {
                _mensaje("Ops!, A ocurrido un error. Contactese con el Administrador", "error");
                return null;
            }           
        }

     
        public int GuardarProveedor(ProveedorModel oProveedorModel)
        {
            try
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
                    oProveedorNuevo.Direccion = oProveedorModel.Direccion;
                    oProveedorNuevo.Telefono = oProveedorModel.Telefono;
                    oProveedorNuevo.IdPais = oProveedorModel.IdPais;
                    oProveedorNuevo.IdProvincia = oProveedorModel.IdProvincia;
                    oProveedorNuevo.IdLocalidad = oProveedorModel.IdLocalidad;
                    oProveedorNuevo.IdCodigoPostal = oProveedorModel.IdCodigoPostal;
                    oProveedorNuevo.IdImputacionProveedor = oProveedorModel.IdImputacionProveedor;
                    oProveedorNuevo.Email = oProveedorModel.Email;
                    oProveedorNuevo.IdTipoIva = oProveedorModel.IdTipoIva;
                    oProveedorNuevo.Cuit = oProveedorModel.Cuit;
                    oProveedorNuevo.IdImputacionFactura = oProveedorModel.IdImputacionFactura;
                    oProveedorNuevo.IdTipoProveedor = oProveedorModel.IdTipoProveedor;
                    oProveedorNuevo.IdTipoMoneda = oProveedorModel.IdTipoMoneda;
                    oProveedorNuevo.Observaciones = oProveedorModel.Observaciones;

                    oProveedorNuevo.Activo = true;
                    oProveedorNuevo.IdUsuario = oProveedorModel.IdUsuario;//hay que poner el id del usuario logueado
                    oProveedorNuevo.UltimaModificacion = oProveedorModel.UltimaModificacion;

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
            catch (Exception ex)
            {
                return 0;
            }
           
        }

        public int Eliminar(int idProveedor)
        {
            var retorno = pProveedorRepositorio.EliminarProveedor(idProveedor);
            if (retorno == 1)
            {
                return 0; //ok
            }
            else
            {
                return -1;//paso algo
            }
        }

    }

}
