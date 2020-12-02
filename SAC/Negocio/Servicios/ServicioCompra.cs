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

    public class ServicioCompra : ServicioBase
    {
        private CompraRepositorio repositorio { get; set; }
        public Action<string, string> _mensaje;
        public ServicioCompra()
        {
            repositorio = kernel.Get<CompraRepositorio>();
        }
        public CompraFacturaModel CreateFactura(CompraFacturaModel model)
        {
            try
            {
               var compraFactura = Mapper.Map<CompraFacturaModel, CompraFactura>(model);
                compraFactura = repositorio.Insertar(compraFactura);
                model.IdUsuario = compraFactura.IdUsuario;
                return model;
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
           
        }
        public List<CompraFacturaModel> GetAllCompraFactura()
        {
            try
            {

               return  Mapper.Map <List<CompraFactura>, List<CompraFacturaModel> > (repositorio.GetAllCompraFactura());
             
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
         
        }

        public List<ProveedorModel> GetProveedorPorNombre(string strProveedor)
        {
            try
            {
                return Mapper.Map<List<Proveedor>, List<ProveedorModel>>(repositorio.GetProveedorPorNombre(strProveedor));
            }
            catch (Exception ex)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
        }

        public CompraFacturaModel ObtenerPorID(int id)
        {
            try
            {              
                return  Mapper.Map<CompraFactura, CompraFacturaModel>(repositorio.GetCompraFacturaPorId(id));
            }
            catch (Exception)
            {
                _mensaje("Ops!, A ocurriodo un error. Contacte al Administrador", "erro");
                return null;
            }
            
        }
    
       

    }

}
