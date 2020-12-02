using System.Data.Entity;
using Datos.ModeloDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Datos.Repositorios
{
    public class ProveedorRepositorio : RepositorioBase<Proveedor>
    {
       private SAC_Entities context;
    
        public ProveedorRepositorio(SAC_Entities contexto) : base(contexto)
        {
            this.context = contexto;
        }

      
        public Proveedor InsertarProveedor(Proveedor proveedor)
        {
            Proveedor a = new Proveedor();
            try {
                return Insertar(proveedor);
            }
            catch (Exception ex)
            {
                return a;
            }
          
        }

        public List<Proveedor> GetAllProveedor()
        {
           // context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor.Where( p=> p.Activo == true).ToList();




            //anda
            //context.Configuration.LazyLoadingEnabled = false;
            //var items = context.Proveedor
            //              .Include(x => x.Pais)
            //              .Include(x => x.Provincia)
            //              .Include(x => x.TipoIva)                        
            //              .Include(x => x.TipoProveedor)
            //              .Include(x => x.TipoMoneda).Where(x => x.Activo == true).ToList();
            //return items;

        }

        public Proveedor GetProveedorPorId(int id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Proveedor.Where(acc => acc.Id == id && acc.Activo == true).FirstOrDefault(); 
        }

        public Proveedor ObtenerProveedorPorNombre(string nombre)
        {
            return context.Proveedor.Where(p => p.Nombre == nombre).FirstOrDefault();
        }
        public Proveedor ObtenerProveedorPorNombre(string oNombre, string oCuit)
        {
            return context.Proveedor.Where(p => p.Nombre == oNombre && p.Cuit == oCuit).FirstOrDefault();
        }

        public Proveedor ObtenerProveedorPorNombre(string oNombre, string oCuit, int oId)
        {
            return context.Proveedor.Where(p => p.Nombre == oNombre && p.Cuit == oCuit && p.Id != oId).FirstOrDefault();
        }

        public Proveedor ActualizarProveedor(Proveedor ProveedorParaActualizar)
        {
            Proveedor Proveedor = GetProveedorPorId(ProveedorParaActualizar.Id);

            Proveedor.Id = ProveedorParaActualizar.Id;   
            Proveedor.Nombre = ProveedorParaActualizar.Nombre;
            Proveedor.Direccion = ProveedorParaActualizar.Direccion;
            Proveedor.Telefono = ProveedorParaActualizar.Telefono;
            Proveedor.IdPais = ProveedorParaActualizar.IdPais;
            Proveedor.IdProvincia = ProveedorParaActualizar.IdProvincia;
            Proveedor.IdLocalidad = ProveedorParaActualizar.IdLocalidad;
            Proveedor.IdCodigoPostal = ProveedorParaActualizar.IdCodigoPostal;
            Proveedor.IdImputacionProveedor = ProveedorParaActualizar.IdImputacionProveedor;
            Proveedor.Email = ProveedorParaActualizar.Email;
            Proveedor.IdTipoIva = ProveedorParaActualizar.IdTipoIva;
            Proveedor.Cuit = ProveedorParaActualizar.Cuit;
            Proveedor.IdImputacionFactura = ProveedorParaActualizar.IdImputacionFactura;
            Proveedor.IdTipoProveedor = ProveedorParaActualizar.IdTipoProveedor;
            Proveedor.IdTipoMoneda = ProveedorParaActualizar.IdTipoMoneda;
            Proveedor.Observaciones = ProveedorParaActualizar.Observaciones;

            Proveedor.Activo = true;
            Proveedor.IdUsuario = ProveedorParaActualizar.IdUsuario;//hay que poner el id del usuario logueado
            Proveedor.UltimaModificacion = ProveedorParaActualizar.UltimaModificacion;
                   
            context.SaveChanges();
            return Proveedor;
        }



        public int EliminarProveedor(int IdProveedor)
        {
            Proveedor Proveedor = GetProveedorPorId(IdProveedor);
            Proveedor.Activo = false;
            // Proveedor.fechaModificacion = Convert.ToDateTime(DateTime.Now.ToString()); ;
            return context.SaveChanges();
        }

      
    }
}